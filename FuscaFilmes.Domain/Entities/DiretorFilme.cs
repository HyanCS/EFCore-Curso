using System;

namespace FuscaFilmes.Domain.Entities;

public class DiretorFilme
{
    public Diretor Diretor { get; set; } = null!;
    
    public int DiretorId { get; set; }

    public int FilmeId { get; set; }

    public Filme Filme { get; set; } = null!;
}
