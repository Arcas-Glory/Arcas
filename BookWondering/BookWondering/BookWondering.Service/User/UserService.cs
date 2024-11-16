using AutoMapper;
using BookWondering.Common.db;
using BookWondering.Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhaoxiPotal.Service.User.Dto;

namespace BookWondering.Service.User
{
    public class UserService:IUserService
    {
        private readonly IMapper _mapper;
        public UserService(IMapper _mapper) {
            this._mapper = _mapper;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<Users> CheckLogin(LoginDto login)
            {
            return await DbContext.db.Queryable<Users>().FirstAsync(m => m.QQ.Equals(login.QQ) && m.PassWord.Equals(login.PassWord));
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public UserDto AddUser(InputUserDto input)
        {
            Users user = TransInputDto(input);
            if (!DbContext.db.Queryable<Users>().Any(m => m.QQ.Equals(input.QQ) || m.Mobile.Equals(input.Mobile)))
            {
                DbContext.db.Insertable(user).ExecuteCommand();
                return _mapper.Map<UserDto>(user);
            }
            else throw new Exception("QQ 或者 手机号已存在");
        }
        private Users TransInputDto(InputUserDto input)
        {
            var user = _mapper.Map<Users>(input);
            var date = DateTime.Now;
            user.RegDate = date;
            user.CreateTime = date;
            user.LastModifyTime = date;
            user.LoginNum = 0;
            user.UserType = 1;
            user.Status = 1;
            user.CreatorId = 1;
            user.LastModifierId = 1;
            return user;
        }
    }
}
