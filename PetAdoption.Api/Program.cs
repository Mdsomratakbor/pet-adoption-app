using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Api.Data;
using PetAdoption.Api.Hubs;
using PetAdoption.Api.Services;
using PetAdoption.Api.Services.Interfaces;
using PetAdoption.Api.Utilities;
using PetAdoption.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = TokenService.GetTokenValidationParameters(builder.Configuration);
});

var connectionString = builder.Configuration.GetConnectionString("PetConnection");
builder.Services.AddDbContext<PetContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);
builder.Services.AddTransient<IAuthService, AuthService>()
                .AddTransient<TokenService>()
                .AddTransient<IPetService,PetService>()
                .AddTransient<IUserPetService ,UserPetService>();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    ApplyMigration.ApplyDbMigration(app.Services);
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PetHub>(AppConstants.HubPattern);
app.Run();

