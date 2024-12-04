using FuscaFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.Repo.Contexts;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Diretor> Diretores { get; set; }

    public DbSet<DiretorFilme> DiretoresFilmes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diretor>(
        d =>
        {
            d.HasMany(d => d.Filmes)
        .WithMany(f => f.Diretores)
        .UsingEntity<DiretorFilme>(
            df => df.HasOne<Filme>(e => e.Filme)
            .WithMany(e => e.DiretoresFilmes)
            .HasForeignKey(e => e.FilmeId),
            df => df.HasOne<Diretor>(e => e.Diretor)
            .WithMany(e => e.DiretoresFilmes)
            .HasForeignKey(e => e.DiretorId),
            df =>
            {
                df.HasKey(e => new { e.DiretorId, e.FilmeId });
                df.ToTable("DiretoresFilmes");
            }
         );
            //rename diretor table key
            d.Property(diretor => diretor.Id).HasColumnName("id_diretor");

            //Defining foreign Key
            d.HasOne(d => d.DiretorDetalhe).WithOne(d => d.Diretor).HasForeignKey<DiretorDetalhe>(dd => dd.DiretorId);

            //Initial data
            d.HasData(
               new Diretor { Id = 1, Name = "Christopher Nolan" },
               new Diretor { Id = 2, Name = "Steven Spielberg" },
               new Diretor { Id = 3, Name = "Quentin Tarantino" },
               new Diretor { Id = 4, Name = "Martin Scorsese" },
               new Diretor { Id = 5, Name = "James Cameron" }
               );
        }
    );

        modelBuilder.Entity<DiretorDetalhe>(
            dd =>
            {
                //DefaultValue = Function execution
                //dd.Property(dd => dd.DataCriacao).HasDefaultValueSql("GETDATE()");

                dd.HasData(
            new DiretorDetalhe { Id = 1, DiretorId = 1, Biografia = "Biografia do Christopher Nolan", DataNascimento = new DateTime(1970, 7, 30) },
            new DiretorDetalhe { Id = 2, DiretorId = 2, Biografia = "Biografia do Steven Spielberg", DataNascimento = new DateTime(1946, 12, 18) }
                );
            }
         );

        modelBuilder.Entity<Filme>(
            f =>
            {
                //Type Decimal, Max 18 char, 2 Decimals
                f.Property(filme => filme.Orcamento).HasColumnType("decimal(18,2)");

                f.Property(filme => filme.Titulo).HasMaxLength(100).IsRequired(false); //not required

                f.HasData(
            new Filme { Id = 1, Titulo = "Inception", Ano = 2010 },
            new Filme { Id = 2, Titulo = "Jurassic Park", Ano = 1993 },
            new Filme { Id = 3, Titulo = "Schindler's List", Ano = 1993 },
            new Filme { Id = 4, Titulo = "Pulp Fiction", Ano = 1994 },
            new Filme { Id = 5, Titulo = "Kill Bill: Volume 1", Ano = 2003 },
            new Filme { Id = 6, Titulo = "Goodfellas", Ano = 1990 },
            new Filme { Id = 7, Titulo = "The Wolf of Wall Street", Ano = 2013 },
            new Filme { Id = 8, Titulo = "Titanic", Ano = 1997 },
            new Filme { Id = 9, Titulo = "Avatar", Ano = 2009 }
        );
            }
        );

        modelBuilder.Entity<DiretorFilme>().HasData(
            new { DiretorId = 1, FilmeId = 1 },
            new { DiretorId = 2, FilmeId = 2 },
            new { DiretorId = 2, FilmeId = 3 },
            new { DiretorId = 3, FilmeId = 4 },
            new { DiretorId = 3, FilmeId = 5 },
            new { DiretorId = 4, FilmeId = 6 },
            new { DiretorId = 4, FilmeId = 7 },
            new { DiretorId = 5, FilmeId = 8 },
            new { DiretorId = 5, FilmeId = 9 }

        );
    }
}
