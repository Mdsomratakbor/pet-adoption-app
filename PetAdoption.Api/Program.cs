using Microsoft.EntityFrameworkCore;
using PetAdoption.Api.Data;
using PetAdoption.Api.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PetConnection");
builder.Services.AddDbContext<PetContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    ApplyMigration.ApplyDbMigration(app.Services);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

