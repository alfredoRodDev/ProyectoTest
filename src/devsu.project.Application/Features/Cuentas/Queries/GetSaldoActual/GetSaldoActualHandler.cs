using devsu.project.Application.Common.Exceptions;
using devsu.project.Application.Common.Interfaces;
using devsu.project.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Queries.GetSaldoActual
{
    public class GetSaldoActualhandler : IRequestHandler<GetSaldoActualQuery, decimal>
    {
        private readonly IAppDbContext _dbContext;

        public GetSaldoActualhandler(IAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<decimal> Handle(GetSaldoActualQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext
            .Cuentas
            .Include(x => x.Movimientos)
            .FirstOrDefaultAsync(x => x.Id == request.cuentaId);

            if (entity == null)
                throw new NotFoundException(nameof(Cuenta), request.cuentaId);

            return entity.SaldoActual;
        }
    }
}
