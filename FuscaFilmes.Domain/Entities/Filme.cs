using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace FuscaFilmes.Domain.Entities;

public class Filme
{
public int Id { get; set; }

[Required]
[MaxLength(100)]
public required string Titulo { get; set; } = string.Empty;
[Range(1900, 2050)] //Year range
public int Ano { get; set; }
[Column(TypeName = "decimal(18,2)")] //Column Type Decimal, 18 char, 2 decimals
public decimal Orcamento { get; set; }
public ICollection<DiretorFilme> DiretoresFilmes { get; set; } = null!;

public ICollection<Diretor> Diretores { get; set; } = null!;
}
