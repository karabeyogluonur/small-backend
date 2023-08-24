using AutoMapper;
using SM.Core.Domain;
using SM.Core.DTOs.Auth;
using SM.Core.DTOs.Blog;
using SM.Core.Features.Auth.Login;
using SM.Core.Features.Auth.RefreshToken;
using SM.Core.Features.Auth.Register;
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
            CreateMap<TokenDTO, RegisterResponse>().ReverseMap();
            CreateMap<RegisterRequest, ApplicationUser>().ReverseMap();
            CreateMap<TokenDTO, RefreshTokenResponse>().ReverseMap();
            CreateMap<TopicDTO, Topic>().ReverseMap();
        }
    }
}
