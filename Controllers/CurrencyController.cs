using CryptoCalculator.Entities;
using CryptoCalculator.Interfaces;
using CryptoExchange.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCalculator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CurrencyController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public CurrencyController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpPost("")]
        public  ActionResult AddCurrency([FromBody] AddCurrencyRequest request)
        {
            try
            {
                var currency = _context.Currencies.FirstOrDefault(x => x.Name == request.Name || x.Name == request.Code);
                if (currency != null)
                {
                    return Forbid($"Currency already exist with id {currency.Id}");
                }
                var newCurrency = new Currency
                {
                    Code = request.Code,
                    Name = request.Name,
                    Type = request.Type
                };
                var newCurrencyEntity = _context.Currencies.Add(newCurrency).Entity;
                _context.SaveChanges();
                return Ok(newCurrency.Id);
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
        }
       

    }
}
