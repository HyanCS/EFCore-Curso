using System.Text.Json.Serialization;
using FuscaFilmes.API.DbContexts;
using FuscaFilmes.API.Entities;
using FuscaFilmes.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => {
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


app.MapGet("/diretores", (Context context) =>
{
    return context.Diretores.Include(diretor => diretor.Filmes).ToList();

})
.WithOpenApi();

app.MapGet("/diretores/agregacao{name}", (string name,
    Context context) =>
{
    // return context.Diretores
    // .Include(diretor => diretor.Filmes)
    // .FirstOrDefault(diretor => diretor.Name.Contains(name))
    // ?? new Diretor { Id = 5555, Name = "Marina"}; // Mesmo tipo que FirstOrDefault

    return context.Diretores
    .Include(diretor => diretor.Filmes)
    .Select(diretor => diretor.Name)
    .FirstOrDefault()
    ?? "Marina"; // Mesmo tipo que FirstOrDefault




    // .OrderBy(diretor => diretor.Name)
    // .FirstOrDefault(diretor => diretor.Id == id);
    //  precisa de OrderBy
    // .LastOrDefault(diretor => diretor.Id == id);

})
.WithOpenApi();

app.MapGet("/diretores/where{id}", (int id,
    Context context) =>
{
    return context.Diretores
    .Where(diretor => diretor.Id == id)
    .Include(diretor => diretor.Filmes)
    .ToList();

})
.WithOpenApi();

app.MapGet("/filmes/{id}", (int id,
    Context context) =>
{
    return context.Filmes
    .Where(filme => filme.Id == id)
    .Include(filme => filme.Diretor).ToList();

})
.WithOpenApi();

app.MapGet("/filmes", (Context context) =>
{
    return context.Filmes
    .Include(filme => filme.Diretor)
    //.OrderBy
    .OrderByDescending(filme =>filme.Ano)
    //.ThenBy
    .ThenByDescending(filme => filme.Titulo)
    .ToList();

})
.WithOpenApi();

app.MapGet("/filmesLinQ/byName/{titulo}", (string titulo,
    Context context) =>
{
    return context.Filmes
    .Where(filme => filme.Titulo.Contains(titulo))
    .Include(filme => filme.Diretor).ToList();

// Raramente mais útil, função do EF similar para busca no banco de dados
    //  return context.Filmes
    // .Where(filme =>
    // EF.Functions.Like(filme.Titulo, $"%{titulo}%")
    // )
    // .Include(filme => filme.Diretor).ToList();

})
.WithOpenApi();

app.MapGet("/filmesEFFunctions/byName/{titulo}", (string titulo,
    Context context) =>
{
    // return context.Filmes
    // .Where(filme => filme.Titulo.Contains(titulo))
    // .Include(filme => filme.Diretor).ToList();

// Raramente mais útil, função do EF similar para busca no banco de dados
     return context.Filmes
    .Where(filme =>
    EF.Functions.Like(filme.Titulo, $"%{titulo}%")
    )
    .Include(filme => filme.Diretor).ToList();

})
.WithOpenApi();

app.MapPost("/diretores", (Context context, Diretor diretor) =>
{
   context.Diretores.Add(diretor);
   context.SaveChanges(); 
})
.WithOpenApi();

app.MapPut("/diretores/{diretorId}", (Context context, int diretorId, Diretor diretorNovo) =>
{
  var diretor = context.Diretores.Find(diretorId);

  if (diretor != null){
    diretor.Name = diretorNovo.Name;
    if (diretorNovo.Filmes.Count > 0) {
        diretor.Filmes.Clear();
        foreach(var filme in diretorNovo.Filmes) {
            diretor.Filmes.Add(filme);
        }
    }
  }
  context.SaveChanges();
})
.WithOpenApi();

app.MapDelete("/filmes/{filmeId}", (Context context, int filmeId) =>
{ 
//   var diretor = context.Filmes.Find(filmeId);

//   if (filme != null)
   context.Filmes
   .Where (filme => filme.Id == filmeId)
   .ExecuteDelete<Filme>(); // return number of rows affected by deletion
   
//    context.SaveChanges(); 
})
.WithOpenApi();

// Patch Altera parcial, Put total
app.MapPatch("/filmesUpdate", (Context context, FilmeUpdate filmeUpdate) =>
{ 
    var filme = context.Filmes.Find(filmeUpdate.Id);

    if (filme == null)
    {
        return Results.NotFound("Filme não encontrado");
    }

    filme.Titulo = filmeUpdate.Titulo;
    filme.Ano = filmeUpdate.Ano;

    context.Filmes.Update(filme);
    context.SaveChanges();

    return Results.Ok($"Filme com ID {filmeUpdate.Id} foi atualizado com sucesso!");
})
.WithOpenApi();

app.MapPatch("/filmesExecuteUpdate", (Context context, FilmeUpdate filmeUpdate) =>
{ 
    var affectedRows = context.Filmes
   .Where (filme => filme.Id == filmeUpdate.Id)
   .ExecuteUpdate(setter => setter // return number of rows affected by update
   .SetProperty(f => f.Titulo, filmeUpdate.Titulo)
   .SetProperty(f => f.Ano, filmeUpdate.Ano)
   ); 

   if (affectedRows > 0) {
    return Results.Ok($"Você teve um total de {affectedRows} Linha(s) afetada(s)");
   } else {
   return Results.NoContent();
   }
})
.WithOpenApi();

app.MapDelete("/diretores/{diretorId}", (Context context, int diretorId) =>
{ 
  var diretor = context.Diretores.Find(diretorId);

  if (diretor != null)
   context.Diretores.Remove(diretor);
   
   context.SaveChanges(); //return 0 = false ou 1 < true
})
.WithOpenApi();

app.Run();

