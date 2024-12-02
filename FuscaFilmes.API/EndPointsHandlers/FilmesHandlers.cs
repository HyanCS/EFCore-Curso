using System;
using System.Security.Cryptography.X509Certificates;
using FuscaFilmes.API.DbContexts;
using FuscaFilmes.API.Models;
using FuscaFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.EndPointsHandlers;

public static class FilmesHandlers
{
    public static List<Filme> GetFilmesById(int id,
        Context context)
    {
        return context.Filmes
        .Where(filme => filme.Id == id)
        .Include(filme => filme.Diretor).ToList();
    }

    public static List<Filme> GetFilmes(Context context)
    {
        return context.Filmes
        .Include(filme => filme.Diretor)
        //.OrderBy
        .OrderByDescending(filme => filme.Ano)
        //.ThenBy
        .ThenByDescending(filme => filme.Titulo)
        .ToList();

    }

    public static List<Filme> GetFilmeContainsByTitulo(string titulo,
        Context context)
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

    }

    public static List<Filme> GetFilmeEFFunctionsByTitulo(string titulo,
        Context context)
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
    }

    public static void ExecuteDeleteFilme(Context context, int filmeId)
    {
        //   var diretor = context.Filmes.Find(filmeId);
        //   if (filme != null)
        context.Filmes
        .Where(filme => filme.Id == filmeId)
        .ExecuteDelete<Filme>(); // return number of rows affected by deletion
        //    context.SaveChanges(); 
    }

    public static IResult UpdateFilme(Context context, FilmeUpdate filmeUpdate)
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
    }

    public static IResult ExecuteUpdateFilme (Context context, FilmeUpdate filmeUpdate)
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
}


}
