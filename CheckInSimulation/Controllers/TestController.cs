using CheckInSimulation.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckInSimulation.Controllers
{
    
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        public TestController(AirlineDbContext context)
        {
            _context = context;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            try
            {
                var airlplane = _context.Airplanes.FirstOrDefault();
                return Ok(new { message = "Conection ok", airplane = airlplane });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error connecting to the database", error = ex.Message });
            }

        }
    }
}
