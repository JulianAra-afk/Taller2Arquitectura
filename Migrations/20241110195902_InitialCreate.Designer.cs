﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace PasarelaPagos.Migrations
{
    [DbContext(typeof(PasarelaContext))]
    [Migration("20241110195902_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Recibo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Monto")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MontoPagado")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Pagado")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Recibos");
                });
#pragma warning restore 612, 618
        }
    }
}
