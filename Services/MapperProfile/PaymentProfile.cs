using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PaymentGatewayAPI.Models.DTO;
using PaymentGatewayAPI.Models.Domain;

namespace PaymentGatewayAPI.Services.MapperProfile
{
    public class PaymentProfile : AutoMapper.Profile
    {
        public PaymentProfile() 
        {
            CreateMap<PaymentRequestDTO, Payment>()
                .ForMember(dest => dest.CreditCardNumber, opt => opt.MapFrom(src => src.CreditCardNumber))
                .ForMember(dest => dest.CardHolder, opt => opt.MapFrom(src => src.CardHolder))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.CVV, opt => opt.MapFrom(src => src.CVV))
                .ForMember(dest => dest.CreditCardNumber, opt => opt.MapFrom(src => src.CreditCardNumber))
                .ForMember(dest => dest.PaymentId, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentStates, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
