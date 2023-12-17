﻿using CryptoCalculator.Entities;
using CryptoCalculator.Interfaces;
using CryptoExchange.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CryptoCalculator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConvertController : ControllerBase

    {

        private readonly IExchangeService _exchangeService;
        private readonly IBalanceService _balanceService;
        public ConvertController(IExchangeService exchangeService,IBalanceService balanceService)
        {
            _exchangeService = exchangeService;
            _balanceService = balanceService;
        }

        [HttpGet("rate")]
        public async Task<ActionResult<ExchangeRateResponse>> GetRate([FromQuery] ExchangeRequest request)
        {
            try
            {
                decimal rate = await _exchangeService.GetRate(request.FromId, request.FromAmount, request.ToId);

                decimal commission = 1 - request.Commission;
                var response = new ExchangeRateResponse
                {
                    FromId = request.FromId,
                    ToId = request.ToId,
                    Rate = rate,
                    FromAmount = request.FromAmount,
                    ToAmount = request.FromAmount * rate * commission,
                    Commission = request.Commission
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);

            }
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> Convert([FromBody] ExchangeRequest request, [FromRoute] Guid userId)
        {
            try
            {
                decimal rate = await _exchangeService.GetRate(request.FromId, request.FromAmount, request.ToId);

                decimal commission = 1 - request.Commission;

                decimal toAmount = request.FromAmount * rate * commission;
                _balanceService.Convert(userId, request.FromId, request.ToId, request.FromAmount, toAmount);
                return Ok();
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
        }

    }
   
    
}