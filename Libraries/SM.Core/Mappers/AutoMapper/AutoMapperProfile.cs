using AutoMapper;
using SM.Core.DTOs.Auth;
using SM.Core.Features.Auth.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Mappers.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TokenDTO, LoginResponse>().ReverseMap();
        }
    }
}
