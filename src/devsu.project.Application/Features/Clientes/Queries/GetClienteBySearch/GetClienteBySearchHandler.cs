using AutoMapper;
using devsu.project.Application.Common.Interfaces;
using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Queries.GetClienteBySearch
{
    public class GetClienteBySearchHandler : IRequestHandler<GetClienteBySearchQuery, IEnumerable<CreateClienteDTO>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetClienteBySearchHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<CreateClienteDTO>> Handle(GetClienteBySearchQuery request, CancellationToken cancellationToken)
        {
            var objList = await _dbContext.Clientes.Where(x => x.NombreYApellido.ToLower().Contains(request.searchParam.ToLower()) && !x.ItWasDeleted).ToListAsync();

            return _mapper.Map<IEnumerable<CreateClienteDTO>>(objList);

        }
    }
}
