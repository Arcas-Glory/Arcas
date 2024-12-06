using BookWonderingReBuild.Common;
using BookWonderingReBuild.Service.User;
using BookWonderingReBuild.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZhaoxiPotal.Service.User.Dto;

namespace BookWonderingReBuild.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IUserService _userService;
        private ICustomJWTService _JWTService;
        public LoginController(IUserService _userService, ICustomJWTService _JWTService) {
            this._userService = _userService;
            this._JWTService = _JWTService;
        }
        //验证码
        [HttpGet]
        public IActionResult GetValidateCodeImages(string t)
        {
            System.Console.WriteLine(t);
            var validateCodeString = Tools.CreateValidateString();
            //将验证码记入缓存中
            MemoryHelper.SetMemory(t, validateCodeString, 1);

            //接收图片返回的二进制流
            byte[] buffer = Tools.CreateValidateCodeBuffer(validateCodeString);
            return File(buffer, @"image/jpeg");
        }
        //登入
        [HttpGet]
        public string CheckLogin(string qq, string pwd)
        {
                LoginDto loginDto = new LoginDto();
                loginDto.QQ = qq;
                loginDto.PassWord = pwd;
                var user = _userService.CheckLogin(loginDto).Result;
                if (user != null)
                {
                    return "Checked";  
                }
                else
                {
                    return "UnChecked01";
                }
        }
    }
}
