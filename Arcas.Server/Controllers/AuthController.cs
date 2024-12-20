using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sqlTest.Server.Data;
using sqlTest.Server.Models;
using sqlTest.Server.Helpers;
using Microsoft.EntityFrameworkCore;

namespace sqlTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ArcasDbContext _context;
        public AuthController(ArcasDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if(string.IsNullOrWhiteSpace(user.username) || string.IsNullOrWhiteSpace(user.password))
            {
                return BadRequest("用户名和密码不能为空");
            }

            bool userExists = await _context.Users.AnyAsync(u => u.username == user.username);
            if(userExists)
            {
                return BadRequest("该用户名已经被占用");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("注册成功");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.username) || string.IsNullOrWhiteSpace(user.password))
            {
                return BadRequest("用户名和密码不能为空");
            }

            var u = await _context.Users.FirstOrDefaultAsync(u => u.username == user.username);
//            bool userExists = await _context.Users.AnyAsync(u => u.username == user.username && u.password == user.password);
            if (u == null || u.password != user.password)
            {
                return BadRequest("用户名或密码错误");
            }

            var token = TokenGenerate.Get(32);

            u.token = token;
            await _context.SaveChangesAsync();

            return Ok(new {token});
        }
    }
}
