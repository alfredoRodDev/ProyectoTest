using devsu.project.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Infrastructure.Persistence.Configurations
{
    public class ClienteConf : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Password).IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.NombreYApellido).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Identificacion).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Telefono).IsRequired().HasMaxLength(35);
            builder.Property(x => x.Direccion).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Genero).HasConversion<int>();



        }
    }
}
