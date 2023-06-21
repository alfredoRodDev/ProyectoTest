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
    public class CuentaConf : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.ToTable("Cuentas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.NumeroCuenta).IsRequired().HasMaxLength(100);
            builder.Property(x => x.TipoDeCuenta).HasConversion<int>();
            builder.Property(x => x.SaldoInicial).HasColumnType("decimal(16,2)");
            builder.Property(x => x.SaldoActual).HasColumnType("decimal(16,2)");

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Cuentas)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
