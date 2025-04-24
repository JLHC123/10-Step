// Jorge Hernandez Cruz

using _10_Step.Data;
using _10_Step.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace _10_Step.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("Hello World")]
        public IActionResult HelloWorld()
        {
            return Ok("Hello World");
        }

        public string connectionString = "Data Source=CS-19;Initial Catalog=Testable;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [HttpGet("test-connection")]
        public IActionResult Connection()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                return Ok("Connection successful!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("test-EF-connection")]
        public IActionResult TestEFConnection()
        {
            MyContext _context = new MyContext();
            if (_context.Database.CanConnect())
            {
                return Ok("Database connected!");
            }
            else
            {
                return StatusCode(500, "Database not connected");
            }    

        }

        [HttpGet("Get Users")]
        public IActionResult GetUsers()
        {
            MyContext _context = new MyContext();
            var users = _context.user.ToList();
            return Ok(users);
        }

        [HttpPost("Add User")]
        public IActionResult AddUser(User user)
        {
            MyContext _context = new MyContext();
            _context.user.Add(user);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("Delete User")]
        public IActionResult DeleteUser(User user)
        {
            MyContext _context = new MyContext();
            _context.user.Remove(user);
            _context.SaveChanges();
            return Ok();
        }
    }
}
