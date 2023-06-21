using devsu.project.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DatabaseFacade Database { get; }
        DbSet<Cliente> Clientes { get; }
        DbSet<Cuenta> Cuentas { get; }
        DbSet<Movimiento> Movimientos { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
