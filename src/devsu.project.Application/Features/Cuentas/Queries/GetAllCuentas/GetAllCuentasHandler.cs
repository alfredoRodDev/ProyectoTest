using AutoMapper;
using devsu.project.Application.Common.Interfaces;
using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using devsu.project.Application.Features.Clientes.Queries.GetAllClientes;
using devsu.project.Application.Features.Cuentas.Commands.CreateCuenta;
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

namespace devsu.project.Application.Features.Cuentas.Queries.GetAllCuentas
{
    public class GetAllCuentasHandler : IRequestHandler<GetAllCuentasQuery, PaginationResponse<CreateCuentaDTO>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllCuentasHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<PaginationResponse<CreateCuentaDTO>> Handle(GetAllCuentasQuery request, CancellationToken cancellationToken)
        {
            var filterExpresion = GetPredicate(request);

            int pageSize = 30;
            var skip = (request.PageNumber - 1) * pageSize;

            var objList = await _dbContext
                .Cuentas
                .Include(x => x.Cliente)
                .Where(filterExpresion)
                .Skip(skip)
                .Take(pageSize)
                .OrderBy(x => x.ClienteId)
                .AsNoTracking()
                .ToListAsync();

            var totalRecords = await _dbContext.Cuentas.Where(filterExpresion).CountAsync();

            return PaginationHelper
                .CreatePaginatedResponse(request.BaseUrl, totalRecords, pageSize, request.PageNumber, _mapper.Map<IEnumerable<CreateCuentaDTO>>(objList));
        }

        private Expression<Func<Cuenta, bool>> GetPredicate(GetAllCuentasQuery request)
        {
            var predicate = PredicateBuilder.True<Cuenta>();

            predicate = predicate.And(x => !x.ItWasDeleted);

            if (request.CustomerId >= 1)
            {
                predicate = predicate.And(x => x.ClienteId == request.CustomerId);
            }

            if (request.Estado >= 1)
            {
                var myEstado = request.Estado == 1 ? true : false;
                predicate = predicate.And(x => x.Estado == myEstado);
            }

            if(request.TipoDeCuenta >= 1)
            {
                var myTipoDeCuenta = (TipoDeCuenta)request.TipoDeCuenta;
                predicate = predicate.And(x => x.TipoDeCuenta == myTipoDeCuenta);
            }



            return predicate;

        }

    }
}
