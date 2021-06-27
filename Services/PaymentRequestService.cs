using AutoMapper;
using PaymentGatewayAPI.Models.Domain;
using PaymentGatewayAPI.Models.DTO;
using PaymentGatewayAPI.PaymentProvider;
using PaymentGatewayAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Services
{
    public class PaymentRequestService : IPaymentRequestService
    {
        private readonly IMapper _mapper;
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentStateMgmtRepository _paymentStateMgmtRepository;

        public PaymentRequestService(IMapper mapper, ICheapPaymentGateway cheapPaymentGateway, IExpensivePaymentGateway expensivePaymentGateway, IPaymentRepository paymentRepository, IPaymentStateMgmtRepository paymentStateMgmtRepository)
        {
            _mapper = mapper;
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _paymentRepository = paymentRepository;
            _paymentStateMgmtRepository = paymentStateMgmtRepository;
        }

        private async Task<PaymentStateDTO> ProcessPaymentStatesDTO(IGenericPaymentProvider paymentGateway, PaymentRequestDTO paymentRequestDTO, Payment entity)
        {
            var paymentStateDTO = paymentGateway.PaymentGateway(paymentRequestDTO);
            var paymentStateEntityIsProcessed = new PaymentStateManagement()
            {
                Payment = entity,
                State = paymentStateDTO.PaymentState.ToString(),
                CreationDate = paymentStateDTO.PaymentStateDate,
                PaymentId = entity.PaymentId
            };
            paymentStateEntityIsProcessed = await _paymentStateMgmtRepository.Create(paymentStateEntityIsProcessed);
            return paymentStateDTO;
        }

        public async Task<PaymentStateDTO> Paymnt(PaymentRequestDTO paymentRequestDTO)
        {
            var Entity = _mapper.Map<PaymentRequestDTO, Payment>(paymentRequestDTO);
            Entity = await _paymentRepository.Create(Entity);

            var entityState = new PaymentStateManagement()
            {
                Payment = Entity,
                PaymentId = Entity.PaymentId,
                CreationDate = DateTime.Now,
                State = PaymentStateEnumerator.Pending.ToString()
            };

            if (paymentRequestDTO.Amount <= 20)
            {
                var paymentStateDTO = await ProcessPaymentStatesDTO(_cheapPaymentGateway, paymentRequestDTO, Entity);
                return paymentStateDTO;
            }
            else if(paymentRequestDTO.Amount > 20 && paymentRequestDTO.Amount <= 500)
            {
                PaymentStateDTO paymentStateDTO = new PaymentStateDTO()
                {
                    PaymentState = PaymentStateEnumerator.Failed,
                    PaymentStateDate = DateTime.Now
                };

                int tries = 0;

                try
                {
                    paymentStateDTO = await ProcessPaymentStatesDTO(_expensivePaymentGateway, paymentRequestDTO, Entity);
                    if(paymentStateDTO != null && paymentStateDTO.PaymentState == PaymentStateEnumerator.Processed)
                    {
                        return paymentStateDTO;
                    }
                    else
                    {
                        tries++;
                        paymentStateDTO = await ProcessPaymentStatesDTO(_cheapPaymentGateway, paymentRequestDTO, Entity);
                        return paymentStateDTO;
                    }
                }
                catch (Exception e)
                {
                    if (tries == 0)
                    {
                        paymentStateDTO = await ProcessPaymentStatesDTO(_cheapPaymentGateway, paymentRequestDTO, Entity);
                        return paymentStateDTO;
                    }
                }
                return paymentStateDTO;
            }
            else
            {
                int tries = 0;
                PaymentStateDTO paymentStateDTO = new PaymentStateDTO()
                {
                    PaymentState = PaymentStateEnumerator.Failed,
                    PaymentStateDate = DateTime.Now
                };
                while (tries < 3)
                {
                    try
                    {
                        paymentStateDTO = await ProcessPaymentStatesDTO(_expensivePaymentGateway, paymentRequestDTO, Entity);
                        if (paymentStateDTO != null && paymentStateDTO.PaymentState == PaymentStateEnumerator.Processed)
                        {
                            return paymentStateDTO;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        tries++;
                    }
                    return paymentStateDTO;
                }
                throw new Exception("Payment can't processed.");
            }

        }
    }
}
