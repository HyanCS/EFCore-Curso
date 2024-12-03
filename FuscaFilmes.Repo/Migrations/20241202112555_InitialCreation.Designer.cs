﻿// <auto-generated />
using FuscaFilmes.Repo.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FuscaFilmes.Repo.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20241202112555_InitialCreation")]
    partial class InitialCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("FuscaFilmes.Domain.Entities.Diretor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Diretores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Christopher Nolan"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Steven Spielberg"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Quentin Tarantino"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Martin Scorsese"
                        },
                        new
                        {
                            Id = 5,
                            Name = "James Cameron"
                        });
                });

            modelBuilder.Entity("FuscaFilmes.Domain.Entities.Filme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ano")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DiretorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DiretorId");

                    b.ToTable("Filmes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ano = 2010,
                            DiretorId = 1,
                            Titulo = "Inception"
                        },
                        new
                        {
                            Id = 2,
                            Ano = 1993,
                            DiretorId = 2,
                            Titulo = "Jurassic Park"
                        },
                        new
                        {
                            Id = 3,
                            Ano = 1993,
                            DiretorId = 2,
                            Titulo = "Schindler's List"
                        },
                        new
                        {
                            Id = 4,
                            Ano = 1994,
                            DiretorId = 3,
                            Titulo = "Pulp Fiction"
                        },
                        new
                        {
                            Id = 5,
                            Ano = 2003,
                            DiretorId = 3,
                            Titulo = "Kill Bill: Volume 1"
                        },
                        new
                        {
                            Id = 6,
                            Ano = 1990,
                            DiretorId = 4,
                            Titulo = "Goodfellas"
                        },
                        new
                        {
                            Id = 7,
                            Ano = 2013,
                            DiretorId = 4,
                            Titulo = "The Wolf of Wall Street"
                        },
                        new
                        {
                            Id = 8,
                            Ano = 1997,
                            DiretorId = 5,
                            Titulo = "Titanic"
                        },
                        new
                        {
                            Id = 9,
                            Ano = 2009,
                            DiretorId = 5,
                            Titulo = "Avatar"
                        });
                });

            modelBuilder.Entity("FuscaFilmes.Domain.Entities.Filme", b =>
                {
                    b.HasOne("FuscaFilmes.Domain.Entities.Diretor", "Diretor")
                        .WithMany("Filmes")
                        .HasForeignKey("DiretorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diretor");
                });

            modelBuilder.Entity("FuscaFilmes.Domain.Entities.Diretor", b =>
                {
                    b.Navigation("Filmes");
                });
#pragma warning restore 612, 618
        }
    }
}
