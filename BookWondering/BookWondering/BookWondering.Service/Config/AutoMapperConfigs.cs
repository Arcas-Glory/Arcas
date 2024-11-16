using AutoMapper;
using BookWondering.Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhaoxiPotal.Service.User.Dto;

namespace BookWondering.Service.Config
{
    public class AutoMapperConfigs:Profile
    {
        public AutoMapperConfigs() {
            CreateMap<Users, UserDto>();
            CreateMap<InputUserDto, Users>();
        }
    }
}
