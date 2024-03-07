using CryptoExchange.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NetworkController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public NetworkController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet("all")]
        public async Task< ActionResult<List<Network>>> GetAll() 
        { 
            var networks = await _context.Networks.OrderBy(x => x.Id).ToListAsync();
            return Ok(networks);
        }
    }
}
