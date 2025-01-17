﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Arcas.Server.Data;
using Arcas.Server.Models;
using Arcas.Server.DTOs;
using Arcas.Server.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace Arcas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowLocalhost5173")]
    public class AuthController : ControllerBase
    {
        private readonly ArcasDbContext _context;

        public AuthController(ArcasDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 用户登录接口
        /// POST /api/auth/login
        /// 前端输入: username, password, nickname
        /// 后端输出: vertify, token, nickname, userID
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<AuthLoginRequest>> Login([FromBody] AuthLoginRequest request)
        {
            // 根据username和password去数据库查询用户
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.username == request.username && u.password == request.password);

            if (user == null)
            {
                // 用户名或密码不正确
                return BadRequest(new AuthLoginResponse
                {
                    vertify = "fail",
                    token = null,
                    nickname = null,
                    userID = 0
                });
            }

            // 用户存在且密码匹配，生成token（简单示例，用Guid代替，实际请使用JWT或其他安全方式）
            string token = TokenHelper.Get(30);
            TokenHelper.Write(_context, QuickQueryHelper.GetUserID(_context, request.username), token);

            // 返回结果
            var response = new AuthLoginResponse
            {
                vertify = "success",
                token = token,
                nickname = user.nickname,
                userID = user.userID
            };

            return Ok(response);
        }


        /// <summary>
        /// 用户注册接口
        /// POST /api/auth/register
        /// 前端输入： username, password, nickname
        /// 后端输出： vertify (success 或 fail)
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<AuthRegisterResponse>> Register([FromBody] AuthRegisterRequest request)
        {
            // 检查用户名是否已存在
            var existUser = await _context.User.FirstOrDefaultAsync(u => u.username == request.username);
            if (existUser != null)
            {
                // 用户名已存在，返回失败
                return BadRequest(new AuthRegisterResponse
                {
                    vertify = "fail"
                });
            }

            // 如果用户名不存在，则创建新用户
            var newUser = new User
            {
                username = request.username,
                password = request.password,
                nickname = request.nickname,
                telephone = "",
                token = ""
            };

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            // 注册成功
            return Ok(new AuthRegisterResponse
            {
                vertify = "success"
            });
        }
    }
}
