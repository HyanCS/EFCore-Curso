using System;
using FuscaFilmes.API.EndPointsHandlers;

namespace FuscaFilmes.API.EndpointsExtensions;

public static class EndpointFilmes
{
    public static void FilmesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/filmes/{id}", FilmesHandlers.GetFilmesByIdAsync).WithOpenApi();

        app.MapGet("/filmes", FilmesHandlers.GetFilmesAsync).WithOpenApi();

        app.MapGet("/filmesContains/byName/{titulo}", FilmesHandlers.GetFilmeContainsByTituloAsync).WithOpenApi();

        app.MapGet("/filmesEFFunctions/byName/{titulo}", FilmesHandlers.GetFilmeEFFunctionsByTituloAsync).WithOpenApi();

        app.MapDelete("/filmes/{filmeId}", FilmesHandlers.ExecuteDeleteFilmeAsync).WithOpenApi();
        // Patch Altera parcial, Put total
        app.MapPatch("/filmesUpdate", FilmesHandlers.UpdateFilmeAsync).WithOpenApi();

        app.MapPatch("/filmesExecuteUpdate", FilmesHandlers.ExecuteUpdateFilmeAsync).WithOpenApi();
    }
}
