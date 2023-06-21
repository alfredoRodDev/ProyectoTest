using AutoMapper;
using devsu.project.Application.Common.Exceptions;
using devsu.project.Application.Common.Interfaces;
using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using devsu.project.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Queries.GetClienteById
{
    public class GetClienteByIdHandler : IRequestHandler<GetClienteByIdQuery, CreateClienteDTO>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetClienteByIdHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<CreateClienteDTO> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext
              .Clientes
              .FirstOrDefaultAsync(x => x.Id == request.id);

            if (entity == null)
                throw new NotFoundException(nameof(Cliente), request.id);

            return _mapper.Map<CreateClienteDTO>(entity);

        }
    }
}
