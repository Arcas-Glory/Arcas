using BookWondering.common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZhaoxiPotal.Common;

namespace BookWondering.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class LogController : ControllerBase
    {
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
    }
}
