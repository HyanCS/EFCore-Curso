using System;
using FuscaFilmes.API.EndPointsHandlers;

namespace FuscaFilmes.API.EndpointsExtensions;

public static class EndpointFilmes
{
    public static void FilmesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/filmes/{id}", FilmesHandlers.GetFilmesById).WithOpenApi();

        app.MapGet("/filmes", FilmesHandlers.GetFilmes).WithOpenApi();

        app.MapGet("/filmesContains/byName/{titulo}", FilmesHandlers.GetFilmeContainsByTitulo).WithOpenApi();

        app.MapGet("/filmesEFFunctions/byName/{titulo}", FilmesHandlers.GetFilmeEFFunctionsByTitulo).WithOpenApi();

        app.MapDelete("/filmes/{filmeId}", FilmesHandlers.ExecuteDeleteFilme).WithOpenApi();
        // Patch Altera parcial, Put total
        app.MapPatch("/filmesUpdate", FilmesHandlers.UpdateFilme).WithOpenApi();

        app.MapPatch("/filmesExecuteUpdate", FilmesHandlers.ExecuteUpdateFilme).WithOpenApi();
    }
}
