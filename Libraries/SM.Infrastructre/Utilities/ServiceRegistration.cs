using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SM.Core.Domain;
using SM.Infrastructre.Persistence.Contexts;
using System.Text;

namespace SM.Infrastructre.Utilities
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructreServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database context

            services.AddDbContext<SMDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SMDbConnection")));

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
            });

            #endregion
        }
    }
}
