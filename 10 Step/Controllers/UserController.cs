// Jorge Hernandez Cruz

using _10_Step.Data;
using _10_Step.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace _10_Step.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
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
