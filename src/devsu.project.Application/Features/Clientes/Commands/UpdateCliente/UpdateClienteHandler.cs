using AutoMapper;
using devsu.project.Application.Common.Exceptions;
using devsu.project.Application.Common.Interfaces;
using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using devsu.project.Application.WrappersModels;
using devsu.project.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Commands.UpdateCliente
{
    public class UpdateClienteHandler : IRequestHandler<UpdateClienteCommand, Response<CreateClienteDTO>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateClienteHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }
        public async Task<Response<CreateClienteDTO>> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        { 
            var entity = await _dbContext
               .Clientes
               .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Cliente), request.Id);

            //validar cambio de Id
            if(entity.Identificacion.ToUpper() != request.Identificacion.ToUpper())
            {
                var exist = await _dbContext
               .Clientes
               .Where(x => x.Identificacion.ToUpper() == request.Identificacion.ToUpper() && x.Id != request.Id)
               .AnyAsync();

                if (exist)
                    return Response<CreateClienteDTO>.Failure("¡El Id que desea cambiar es usado por otro usario, por favor revise e intente nuevamente!", null);

            }

            entity.NombreYApellido = request.NombreYApellido.ToUpper();
            entity.Genero = request.Genero;
            entity.Identificacion = request.Identificacion.ToUpper();
            entity.Edad = request.Edad;
            entity.Direccion = request.Direccion.ToUpper();
            entity.Telefono = request.Telefono.ToUpper();
            entity.Password = request.Password;
            entity.Estado = request.Estado;

            var result = await _dbContext.SaveChangesAsync(cancellationToken) >= 1;

            return result ? Response<CreateClienteDTO>.Success(_mapper.Map<CreateClienteDTO>(entity))
                : Response<CreateClienteDTO>.Failure(CrudErrorResponses.ErrorAdding, null);

        }
    }
}
