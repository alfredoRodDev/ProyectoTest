using AutoMapper;
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

namespace devsu.project.Application.Features.Clientes.Commands.CreateCliente
{
    public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, Response<CreateClienteDTO>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateClienteHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<Response<CreateClienteDTO>> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var exist = await _dbContext
                .Clientes
                .Where(x => x.Identificacion.ToUpper() == request.Identificacion.ToUpper())
                .AnyAsync();

            if (exist)
                return Response<CreateClienteDTO>.Failure(CrudErrorResponses.ErrorRepeatedResource, null);

            var entity = _mapper.Map<Cliente>(request);

            _dbContext.Clientes.Add(entity);

            var result = await _dbContext.SaveChangesAsync(cancellationToken) >= 1;

            return result ? Response<CreateClienteDTO>.Success(_mapper.Map<CreateClienteDTO>(entity))
                : Response<CreateClienteDTO>.Failure(CrudErrorResponses.ErrorAdding, null);

        }
    }
}
