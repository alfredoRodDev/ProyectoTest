﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using devsu.project.Infrastructure.Persistence;

#nullable disable

namespace devsu.project.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230621152210_primeraMigracion")]
    partial class primeraMigracion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("devsu.project.Domain.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<int>("Genero")
                        .HasColumnType("int");

                    b.Property<string>("Identificacion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("ItWasDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("NombreYApellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("SyncAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("devsu.project.Domain.Entities.Cuenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<bool>("ItWasDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("NumeroCuenta")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("SaldoActual")
                        .HasColumnType("decimal(16,2)");

                    b.Property<decimal>("SaldoInicial")
                        .HasColumnType("decimal(16,2)");

                    b.Property<DateTime>("SyncAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("TipoDeCuenta")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Cuentas", (string)null);
                });

            modelBuilder.Entity("devsu.project.Domain.Entities.Movimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<int>("CuentaId")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<bool>("ItWasDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("SaldoFinal")
                        .HasColumnType("decimal(16,2)");

                    b.Property<decimal>("SaldoInicial")
                        .HasColumnType("decimal(16,2)");

                    b.Property<DateTime>("SyncAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("TipoDeMovimiento")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(16,2)");

                    b.HasKey("Id");

                    b.HasIndex("CuentaId");

                    b.ToTable("Movimientos", (string)null);
                });

            modelBuilder.Entity("devsu.project.Domain.Entities.Cuenta", b =>
                {
                    b.HasOne("devsu.project.Domain.Entities.Cliente", "Cliente")
                        .WithMany("Cuentas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("devsu.project.Domain.Entities.Movimiento", b =>
                {
                    b.HasOne("devsu.project.Domain.Entities.Cuenta", "Cuenta")
                        .WithMany("Movimientos")
                        .HasForeignKey("CuentaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cuenta");
                });

            modelBuilder.Entity("devsu.project.Domain.Entities.Cliente", b =>
                {
                    b.Navigation("Cuentas");
                });

            modelBuilder.Entity("devsu.project.Domain.Entities.Cuenta", b =>
                {
                    b.Navigation("Movimientos");
                });
#pragma warning restore 612, 618
        }
    }
}
