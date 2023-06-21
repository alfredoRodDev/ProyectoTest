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

namespace devsu.project.Application.Features.Cuentas.Commands.DeleteCuenta
{
    public class DeleteCuentaHandler : IRequestHandler<DeleteCuentaCommand, Response>
    {

        private readonly IAppDbContext _dbContext;

        public DeleteCuentaHandler(IAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Response> Handle(DeleteCuentaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext
             .Cuentas
             .Include(x => x.Movimientos)
             .FirstOrDefaultAsync(x => x.Id == request.id);

            if (entity == null)
                throw new NotFoundException(nameof(Cuenta), request.id);

            //validar si se puede eliminar --> si tiene cuentas asociadas no se puede eliminar
            if (entity.Movimientos.Any())
                return Response.Failure("¡esta cuenta no se puede eliminar porque posee movimientos bancarios!");

            //tambien se podria usar el parametro ItWasDeleted para eliminar el cliente, pero preservar el registro.
            _dbContext.Cuentas.Remove(entity);

            var result = await _dbContext.SaveChangesAsync(cancellationToken) >= 1;

            return result ? Response.Success() : Response.Failure(CrudErrorResponses.ErrorDeleting);
        }
    }
}
