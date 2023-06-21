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

namespace devsu.project.Application.Features.Clientes.Commands.DeleteCliente
{
    public class DeleteClienteHandler : IRequestHandler<DeleteClienteCommand, Response>
    {
        private readonly IAppDbContext _dbContext;

        public DeleteClienteHandler(IAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Response> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext
                .Clientes
                .Include(x => x.Cuentas)
                .FirstOrDefaultAsync(x => x.Id == request.id);

            if (entity == null)
                throw new NotFoundException(nameof(Cliente), request.id);

            //validar si se puede eliminar --> si tiene cuentas asociadas no se puede eliminar
            if(entity.Cuentas.Any())
                return Response.Failure("¡El Cliente posee cuentas asociadas, no se puede eliminar, elimine las cuentas primero!");

            //tambien se podria usar el parametro ItWasDeleted para eliminar el cliente, pero preservar el registro.
            _dbContext.Clientes.Remove(entity);

            var result = await _dbContext.SaveChangesAsync(cancellationToken) >= 1;

            return result ? Response.Success() : Response.Failure(CrudErrorResponses.ErrorDeleting);

        }
    }
}
