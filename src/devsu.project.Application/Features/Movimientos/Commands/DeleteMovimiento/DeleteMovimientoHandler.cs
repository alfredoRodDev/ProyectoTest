using devsu.project.Application.Common.Exceptions;
using devsu.project.Application.Common.Interfaces;
using devsu.project.Application.WrappersModels;
using devsu.project.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Movimientos.Commands.DeleteMovimiento
{
    public class DeleteMovimientoHandler : IRequestHandler<DeleteMovimientoCommand, Response>
    {
        private readonly IAppDbContext _dbContext;

        public DeleteMovimientoHandler(IAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Response> Handle(DeleteMovimientoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext
            .Movimientos
            .FirstOrDefaultAsync(x => x.Id == request.id);

            if (entity == null)
                throw new NotFoundException(nameof(Movimiento), request.id);

            using(var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var cuenta = await _dbContext
                        .Cuentas
                        .FirstOrDefaultAsync(x => x.Id == entity.CuentaId);

                    if(entity.TipoDeMovimiento == Domain.Enums.TipoDeMovimiento.Deposito)
                    {
                        cuenta.SaldoActual -= entity.Valor;
                    }
                    else
                    {
                        cuenta.SaldoActual += entity.Valor;
                    }

                    _dbContext.Movimientos.Remove(entity);

                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync();

                    return Response.Success();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                   
                }
            }

            return Response.Failure(CrudErrorResponses.ErrorDeleting);

        }
    }
}
