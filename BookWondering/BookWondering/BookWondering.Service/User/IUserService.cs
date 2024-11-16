using BookWondering.Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhaoxiPotal.Service.User.Dto;

namespace BookWondering.Service.User
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns> 
        Task<Users> CheckLogin(LoginDto login);
    }
}
