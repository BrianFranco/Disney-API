﻿// <auto-generated />
using System;
using Disney_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Disney_API.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20220127172205_SecondMigration")]
    partial class SecondMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Disney_API.Models.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genero");
                });

            modelBuilder.Entity("Disney_API.Models.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Calificación")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GeneroId")
                        .HasColumnType("int");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GeneroId");

                    b.ToTable("Pelicula");
                });

            modelBuilder.Entity("Disney_API.Models.Personaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<string>("Historia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Peso")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Personaje");
                });

            modelBuilder.Entity("Disney_API.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Token")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("PeliculaPersonaje", b =>
                {
                    b.Property<int>("PeliculasId")
                        .HasColumnType("int");

                    b.Property<int>("PersonajesId")
                        .HasColumnType("int");

                    b.HasKey("PeliculasId", "PersonajesId");

                    b.HasIndex("PersonajesId");

                    b.ToTable("PeliculaPersonaje");
                });

            modelBuilder.Entity("Disney_API.Models.Pelicula", b =>
                {
                    b.HasOne("Disney_API.Models.Genero", null)
                        .WithMany("Peliculas")
                        .HasForeignKey("GeneroId");
                });

            modelBuilder.Entity("PeliculaPersonaje", b =>
                {
                    b.HasOne("Disney_API.Models.Pelicula", null)
                        .WithMany()
                        .HasForeignKey("PeliculasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Disney_API.Models.Personaje", null)
                        .WithMany()
                        .HasForeignKey("PersonajesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Disney_API.Models.Genero", b =>
                {
                    b.Navigation("Peliculas");
                });
#pragma warning restore 612, 618
        }
    }
}
