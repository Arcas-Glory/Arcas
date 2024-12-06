using BookWonderingReBuild.Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhaoxiPotal.Service.User.Dto;

namespace BookWonderingReBuild.Service.User
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<Users> CheckLogin(LoginDto login);
        //注册
        UserDto AddUser(InputUserDto input);
    }
}
