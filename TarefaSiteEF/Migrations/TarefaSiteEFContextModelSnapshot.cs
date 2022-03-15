﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TarefaSiteEF.Data;

namespace TarefaSiteEF.Migrations
{
    [DbContext(typeof(TarefaSiteEFContext))]
    partial class TarefaSiteEFContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tarefas.Dominio.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TarefaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TarefaId");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("Tarefas.Dominio.Models.Tarefa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int");

                    b.Property<bool>("Notificacao")
                        .HasColumnType("bit");

                    b.Property<string>("UsuarioEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioEmail");

                    b.ToTable("Tarefa");
                });

            modelBuilder.Entity("Tarefas.Dominio.Models.Usuario", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Tarefas.Dominio.Models.Categoria", b =>
                {
                    b.HasOne("Tarefas.Dominio.Models.Tarefa", "Tarefa")
                        .WithMany("Categorias")
                        .HasForeignKey("TarefaId");

                    b.Navigation("Tarefa");
                });

            modelBuilder.Entity("Tarefas.Dominio.Models.Tarefa", b =>
                {
                    b.HasOne("Tarefas.Dominio.Models.Usuario", "Usuario")
                        .WithMany("Tarefas")
                        .HasForeignKey("UsuarioEmail");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Tarefas.Dominio.Models.Tarefa", b =>
                {
                    b.Navigation("Categorias");
                });

            modelBuilder.Entity("Tarefas.Dominio.Models.Usuario", b =>
                {
                    b.Navigation("Tarefas");
                });
#pragma warning restore 612, 618
        }
    }
}
