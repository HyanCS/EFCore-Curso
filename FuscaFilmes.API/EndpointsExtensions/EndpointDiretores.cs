using System;
using FuscaFilmes.API.EndPointsHandlers;
using Microsoft.AspNetCore.Routing;

namespace FuscaFilmes.API.EndpointsExtensions;

public static class EndpointDiretores
{

    public static void DiretoresEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/diretores", DiretoresHandlers.GetDiretores).WithOpenApi();

        app.MapGet("/diretores/agregacao{name}", DiretoresHandlers.GetDiretorByName).WithOpenApi();

        app.MapGet("/diretores/where{id}", DiretoresHandlers.GetDiretoresById).WithOpenApi();

        app.MapPost("/diretores", DiretoresHandlers.AddDiretor).WithOpenApi();

        app.MapPut("/diretores", DiretoresHandlers.UpdateDiretor).WithOpenApi();

        app.MapDelete("/diretores", DiretoresHandlers.DeleteDiretor).WithOpenApi();
    }
}
