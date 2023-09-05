using AutoMapper;
using SM.Core.Domain;
using SM.Core.DTOs.Auth;
using SM.Core.DTOs.Blog;
using SM.Core.DTOs.Membership;
using SM.Core.Features.Articles.GetAllArticle;
using SM.Core.Features.Articles.GetComment;
using SM.Core.Features.Articles.UpdateArticle;
using SM.Core.Features.Auth.Login;
using SM.Core.Features.Auth.RefreshToken;
using SM.Core.Features.Auth.Register;
using SM.Core.Features.Comments.GetReply;
using SM.Core.Features.Comments.InsertReply;
using SM.Core.Features.Topics.GetAllTopic;
using SM.Core.Features.Topics.SearchTopic;
using SM.Core.Interfaces.Collections;

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

            #region Membership

            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();

            #endregion

            #region Article
            

            CreateMap<Article, ArticleDTO>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => src.Topics))
                .ReverseMap();

            CreateMap<IPagedList<Article>, GetAllArticleResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();

            CreateMap<Article, UpdateArticleRequest>().ForMember(article => article.ArticleId, opt => opt.Ignore()).ReverseMap();
            CreateMap<Article, UpdateArticleResponse>().ReverseMap();

            #endregion

            #region Comment

            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ReverseMap();
            CreateMap<InsertReplyRequest, CommentReply>();

            CreateMap<CommentReply, CommentReplyDTO>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ReverseMap();

            CreateMap<IPagedList<Comment>, GetCommentResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();

            CreateMap<IPagedList<CommentReply>, GetReplyResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();


            #endregion


        }
    }
}
