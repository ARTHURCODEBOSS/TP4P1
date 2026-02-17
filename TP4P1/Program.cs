using Microsoft.EntityFrameworkCore;
using TP4P1.Models.DataManager;
using TP4P1.Models.EntityFramework;
using TP4P1.Models.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDataRepository<Utilisateur>, UtilisateurManager>();
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<FilmRatingsDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FilmRatingsContext")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
