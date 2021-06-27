using PaymentGatewayAPI.Models.Domain;
using PaymentGatewayAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Services.MapperProfile
{
    public class PaymentStateProfile : AutoMapper.Profile
    {
        public PaymentStateProfile()
        {
            CreateMap<PaymentStateDTO, PaymentStateManagement>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.PaymentState.ToString()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.PaymentStateDate))
                .ForMember(dest => dest.Payment, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentId, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentStateId, opt => opt.Ignore())
                .ReverseMap();
                
        }
    }
}
