using SM.Core.Common.Model;
using SM.Core.Utilities;
using SM.Infrastructre.Utilities;
using SM.Infrastructre.Persistence.Seeds;
using SM.Core.Common.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#region Base services

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    OpenApiSecurityScheme jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
        Reference = new OpenApiReference
        {
           Id = JwtBearerDefaults.AuthenticationScheme,
           Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

#endregion

#region Layer services

builder.Services.AddCoreServices();
builder.Services.AddInfrastructreServices(builder.Configuration);

#endregion

#region Cors
builder.Services.AddCors(cors => cors.AddPolicy("Member", policy =>
    policy.AllowCredentials()
          .AllowAnyHeader()
          .AllowAnyMethod()
          .WithOrigins(builder.Configuration["CORS:AllowedOrigins"].Split(";"))));
#endregion

#region Email Account Options

builder.Services.Configure<EmailAccountOptions>(builder.Configuration.GetSection("EmailAccount"));

#endregion

var app = builder.Build();

#region Swagger

app.UseSwagger();
app.UseSwaggerUI();

#endregion

#region Development

if (app.Environment.IsDevelopment())
{
    
}

#endregion


#region Middlewares

DbInitializer.Initialize(app);
app.ConfigureExceptionHandler<Program>();
app.UseStaticFiles();
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Welcome to Small. Smaller then medium.");
});
app.UseCors("Member");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

#endregion


app.Run();
