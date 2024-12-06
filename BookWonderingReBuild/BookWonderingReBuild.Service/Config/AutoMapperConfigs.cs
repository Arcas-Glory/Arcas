using AutoMapper;
using BookWonderingReBuild.Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhaoxiPotal.Service.User.Dto;

namespace BookWonderingReBuild.Service.Config
{
    public class AutoMapperConfigs : Profile
    {
        //管理映射关系
        public AutoMapperConfigs()
        {
            CreateMap<Users, UserDto>();
            CreateMap<InputUserDto, Users>();
        }
    }
}
