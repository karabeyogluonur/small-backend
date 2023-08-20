using SM.Core.Common.Model;
using SM.Core.Utilities;
using SM.Infrastructre.Utilities;
using SM.Infrastructre.Persistence.Seeds;

var builder = WebApplication.CreateBuilder(args);

#region Base services

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

#region Development

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#endregion


#region Middlewares

DbInitializer.Initialize(app);
app.UseStaticFiles();
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Welcome to Small.");
});
app.UseCors("Member");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

#endregion


app.Run();
