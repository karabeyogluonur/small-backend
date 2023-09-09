using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Interfaces.Repositores;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Blog;
using SM.Core.Interfaces.Services.Media;
using SM.Core.Interfaces.Services.Membership;
using SM.Core.Interfaces.Services.Notification;
using SM.Infrastructre.Persistence.Contexts;
using SM.Infrastructre.Persistence.Repositories;
using SM.Infrastructre.Services;
using SM.Infrastructre.Services.Auth;
using SM.Infrastructre.Services.Blog;
using SM.Infrastructre.Services.Media;
using SM.Infrastructre.Services.Membership;
using SM.Infrastructre.Services.Notification;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace SM.Infrastructre.Utilities
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructreServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database context

            services.AddDbContext<SMDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SMDbConnection")));

            #endregion

            #region Repositories

            services.AddScoped<IUnitOfWork,UnitOfWork>();
            //services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            #endregion

            #region Identity

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => {

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = true;
                options.Lockout.AllowedForNewUsers = false;

            })
            .AddEntityFrameworkStores<SMDbContext>().AddDefaultTokenProviders();

            #endregion

            #region Auth Cookie

            services.ConfigureApplicationCookie(configure =>
            {
                configure.Cookie.Name = "Member";
                configure.Cookie.HttpOnly = true;
                configure.ExpireTimeSpan = TimeSpan.FromMinutes(90);
                configure.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                configure.SlidingExpiration = true;
            });


            #endregion

            #region Authentication and JWT

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = configuration["JWT:Audience"],
                    ValidIssuer = configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecurityKey"])),

                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync(JsonSerializer.Serialize(ApiResponse<object>.Error(null,"Authorization failed.")));
                    }
                };
            });

            #endregion

            #region Services

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAuthService,AuthService>();
            services.AddTransient<IUserService,UserService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ITopicService,TopicService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<ICommentService, CommentService>();






            services.AddScoped<IWorkContext, WorkContext>();
            #endregion
        }
    }
}
