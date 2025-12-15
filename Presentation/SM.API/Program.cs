using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using SM.Core.Common.Extensions;
using SM.Core.Common.Model;
using SM.Core.Utilities;
using SM.Infrastructre.Persistence.Seeds;
using SM.Infrastructre.Utilities;

var builder = WebApplication.CreateBuilder(args);

#region Base services

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SM API",
        Version = "v1"
    });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme
        }
    };

    options.AddSecurityDefinition(
        JwtBearerDefaults.AuthenticationScheme,
        securityScheme
    );

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securityScheme,
            Array.Empty<string>()
        }
    });
});

#endregion

#region Layer services

builder.Services.AddCoreServices();
builder.Services.AddInfrastructreServices(builder.Configuration);

#endregion

#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("Member", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins(
                builder.Configuration["CORS:AllowedOrigins"]!
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
            );
    });
});

#endregion

#region Email Account Options

builder.Services.Configure<EmailAccountOptions>(
    builder.Configuration.GetSection("EmailAccount"));

#endregion

var app = builder.Build();

#region Swagger

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#endregion

#region Middlewares

DbInitializer.Initialize(app);

app.ConfigureExceptionHandler<Program>();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("Member");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Welcome to Small. Smaller than medium.");
});

#endregion

app.Run();
