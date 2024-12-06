using BookWonderingReBuild.Common.Db;
using BookWonderingReBuild.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZhaoxiPotal.Service.User.Dto;

namespace BookWonderingReBuild.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> _logger, IUserService _userService) {
            this._logger = _logger;
            this._userService = _userService;
        }

        //用户注册
        [HttpPost]
        public ActionResult<UserDto> RegistUser([FromBody] InputUserDto input)
        {
            try
            {
                var res = _userService.AddUser(input);
                return res;
            }
            catch (System.Exception ex)
            {
                // 记录异常信息到日志
                _logger.LogError(ex, "An error occurred while registering user.");

                // 创建一个错误响应对象
                var errorResponse = new
                {
                    success = false,
                    message = ex.Message, // 只传递异常消息
                    statusCode = 500
                };

                // 返回错误响应对象的JSON
                return new JsonResult(errorResponse)
                {
                    StatusCode = 500,
                };
            }
        }
    }
}
