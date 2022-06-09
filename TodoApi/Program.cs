using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoApi;
using TodoApi.Data;
using TodoApi.Repositories;
using TodoApi.Repositories.Interfaces;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureServices(builder);
ConfigureAuthentication(builder);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
LoadConfigurations(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

void LoadConfigurations(WebApplication app) 
{
    Configuration.JwtKey = app.Configuration.GetValue<string>("JwtKey");
    Configuration.ApiKeyName = app.Configuration.GetValue<string>("ApiKeyName");
    Configuration.ApiKey = app.Configuration.GetValue<string>("ApiKey");
}
void ConfigureAuthentication(WebApplicationBuilder builder) 
{
    var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(x => 
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}
void ConfigureServices(WebApplicationBuilder builder) 
{
    builder.Services.AddControllers();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<TodoDataContext>(opt => opt.UseSqlServer(connectionString));

    builder.Services.AddTransient<TokenService>();
    builder.Services.AddTransient<IUserRepository, UserRepository>();
    builder.Services.AddTransient<ITodoRepository, TodoRepository>();

}


