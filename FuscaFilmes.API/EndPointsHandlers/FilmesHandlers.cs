using System;
using System.Security.Cryptography.X509Certificates;
using FuscaFilmes.API.Models;
using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.EndPointsHandlers;

public static class FilmesHandlers
{
    public static async Task<IEnumerable<Filme>> GetFilmesByIdAsync(int id,
        Context context)
    {
        return await context.Filmes
        .Include(filme => filme.Diretores)
        .Where(filme => filme.Id == id)
        .ToListAsync();
    }

    public static async Task<IEnumerable<Filme>> GetFilmesAsync(Context context)
    {
        return await context.Filmes
        .Include(filme => filme.Diretores)
        //.OrderBy
        .OrderByDescending(filme => filme.Ano)
        //.ThenBy
        .ThenByDescending(filme => filme.Titulo)
        .ToListAsync();

    }

    public static async Task<IEnumerable<Filme>> GetFilmeContainsByTituloAsync(string titulo,
        Context context)
    {
        return await context.Filmes
        .Include(filme => filme.Diretores)
        .Where(filme => filme.Titulo.Contains(titulo))
        .ToListAsync();

    }

    public static async Task<IEnumerable<Filme>> GetFilmeEFFunctionsByTituloAsync(string titulo,
        Context context)
    {
        return await context.Filmes
       .Include(filme => filme.Diretores)
       .Where(filme =>
       EF.Functions.Like(filme.Titulo, $"%{titulo}%")
       )
       .ToListAsync();
    }

    public static async void ExecuteDeleteFilmeAsync(Context context, int filmeId)
    {
        await context.Filmes
        .Where(filme => filme.Id == filmeId)
        .ExecuteDeleteAsync<Filme>(); // return number of rows affected by deletion
    }

    public static async Task<IResult> UpdateFilmeAsync(Context context, FilmeUpdate filmeUpdate)
    {
        var filme = await context.Filmes.FindAsync(filmeUpdate.Id);

        if (filme == null)
        {
            return Results.NotFound("Filme não encontrado");
        }

        filme.Titulo = filmeUpdate.Titulo;
        filme.Ano = filmeUpdate.Ano;

        context.Filmes.Update(filme);
        await context.SaveChangesAsync();

        return Results.Ok($"Filme com ID {filmeUpdate.Id} foi atualizado com sucesso!");
    }

    public static async Task<IResult> ExecuteUpdateFilmeAsync (Context context, FilmeUpdate filmeUpdate)
{ 
    var affectedRows = await context.Filmes
   .Where (filme => filme.Id == filmeUpdate.Id)
   .ExecuteUpdateAsync(setter => setter // return number of rows affected by update
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
