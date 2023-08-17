using SM.Core.Utilities;
using SM.Infrastructre.Utilities;

var builder = WebApplication.CreateBuilder(args);

#region Base services

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region Layer services

builder.Services.AddCoreServices();
builder.Services.AddInfrastructreServices();

#endregion

#region Cors
builder.Services.AddCors(cors => cors.AddPolicy("Member", policy =>
    policy.AllowCredentials()
          .AllowAnyHeader()
          .AllowAnyMethod()
          .WithOrigins(builder.Configuration["CORS:AllowedOrigins"].Split(";"))));
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

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Welcome to Small.");
});

app.UseCors("Member");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

#endregion


app.Run();
