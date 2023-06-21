using devsu.project.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Infrastructure.Persistence.Configurations
{
    public class MovimientoConf : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> builder)
        {
            builder.ToTable("Movimientos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TipoDeMovimiento).HasConversion<int>();
            builder.Property(x => x.Valor).HasColumnType("decimal(16,2)");
            builder.Property(x => x.SaldoInicial).HasColumnType("decimal(16,2)");
            builder.Property(x => x.SaldoFinal).HasColumnType("decimal(16,2)");

            builder.HasOne(x => x.Cuenta)
                .WithMany(x => x.Movimientos)
                .HasForeignKey(x => x.CuentaId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
