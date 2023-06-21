using AutoMapper;
using devsu.project.Application.Common.Interfaces;
using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using devsu.project.Application.WrappersModels;
using devsu.project.Domain.Entities;
using devsu.project.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Queries.GetAllClientes
{
    public class GetAllClientesHandler : IRequestHandler<GetAllClientesQuery, PaginationResponse<CreateClienteDTO>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllClientesHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<PaginationResponse<CreateClienteDTO>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
        {
            var filterExpresion = GetPredicate(request);

            int pageSize = 30;
            var skip = (request.PageNumber - 1) * pageSize;

            var objList = await _dbContext
                .Clientes
                .Where(filterExpresion)
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .OrderBy(x => x.NombreYApellido)
                .ToListAsync();

            var totalRecords = await _dbContext.Clientes.Where(filterExpresion).CountAsync();

            return PaginationHelper
                .CreatePaginatedResponse(request.BaseUrl, totalRecords, pageSize, request.PageNumber, _mapper.Map<IEnumerable<CreateClienteDTO>>(objList));

        }

        private Expression<Func<Cliente, bool>> GetPredicate(GetAllClientesQuery request)
        {
            var predicate = PredicateBuilder.True<Cliente>();

            predicate = predicate.And(x => !x.ItWasDeleted);

            if (request.CustomerId >= 1)
            {
                predicate = predicate.And(x => x.Id == request.CustomerId);
            }

            if (request.Estado >= 1)
            {
                var myEstado = request.Estado == 1 ? true : false;
                predicate = predicate.And(x => x.Estado == myEstado);
            }

            if (request.Genero >= 1)
            {
                var myGenero = (Genero)request.Genero;
                predicate = predicate.And(x => x.Genero == myGenero);
            }

            if (request.MinEdad >= 18)
            {
                predicate = predicate.And(x => x.Edad >= request.MinEdad);
            }

            if (request.MaxEdad >= 18 && request.MaxEdad >= request.MinEdad)
            {
                predicate = predicate.And(x => x.Edad <= request.MaxEdad);
            }



            return predicate;

        }

    }
}
