using System.Text.Json.Serialization;
using FuscaFilmes.API.EndpointsExtensions;
using FuscaFilmes.Repo;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Repo.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(
    options => options.UseSqlite(builder.Configuration["ConnectionStrings:FuscaFilmesStr"])
    .LogTo(Console.WriteLine, LogLevel.Information)
);

// using (var context = new Context ())
// {
//     context.Database.EnsureCreated();
// }

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IDiretorRepository, DiretorRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.AllowTrailingCommas = true;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.DiretoresEndpoints();

app.FilmesEndpoints();

app.Run();

