﻿using PaymentGatewayAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Services
{
    public interface IPaymentRequestService
    {
        Task<PaymentStateDTO> Paymnt(PaymentRequestDTO paymentRequestDTO);
    }
}
