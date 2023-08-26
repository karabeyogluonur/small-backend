using AutoMapper;
using SM.Core.Domain;
using SM.Core.DTOs.Auth;
using SM.Core.DTOs.Blog;
using SM.Core.Features.Auth.Login;
using SM.Core.Features.Auth.RefreshToken;
using SM.Core.Features.Auth.Register;
using SM.Core.Features.Topics.GetAllTopic;
using SM.Core.Features.Topics.SearchTopic;
using SM.Core.Interfaces.Collections;
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

            #region Auth
            CreateMap<TokenDTO, LoginResponse>().ReverseMap();
            CreateMap<TokenDTO, RegisterResponse>().ReverseMap();
            CreateMap<RegisterRequest, ApplicationUser>().ReverseMap();
            CreateMap<TokenDTO, RefreshTokenResponse>().ReverseMap();
            #endregion

            #region Topic

            CreateMap<TopicDTO, Topic>().ReverseMap();
            CreateMap<IPagedList<Topic>, GetAllTopicResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();
            CreateMap<IPagedList<Topic>, SearchTopicResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();
            #endregion


        }
    }
}
