using Microsoft.AspNetCore.Mvc;

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
        public ActionResult GetAll() 
        { 
            var networks = _context.Networks.ToList();
            return Ok(networks);
        }
    }
}
