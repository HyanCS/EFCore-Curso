using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuscaFilmes.Domain.Entities;

public class Diretor
{
    [Key] //Identifies the table Key
    [Column("id_diretor")] //change Key table name
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<DiretorFilme> DiretoresFilmes { get; set; } = null!;

    public ICollection<Filme> Filmes { get; set; } = null!;

    public DiretorDetalhe? DiretorDetalhe { get; set; }
}
