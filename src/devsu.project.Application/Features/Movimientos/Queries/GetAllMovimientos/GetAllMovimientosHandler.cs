using devsu.project.Application.Common.Interfaces;
using devsu.project.Application.Features.Cuentas.Commands.CreateCuenta;
using devsu.project.Application.Features.Cuentas.Queries.GetAllCuentas;
using devsu.project.Application.Features.Movimientos.Commands.CreateMovDeposito;
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

namespace devsu.project.Application.Features.Movimientos.Queries.GetAllMovimientos
{
    public class GetAllMovimientosHandler : IRequestHandler<GetAllMovimientosQuery, PaginationResponse<CreateMovimientoDTO>>
    {
        private readonly IAppDbContext _dbContext;

        public GetAllMovimientosHandler(IAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<PaginationResponse<CreateMovimientoDTO>> Handle(GetAllMovimientosQuery request, CancellationToken cancellationToken)
        {
            var filterExpresion = GetPredicate(request);

            int pageSize = 30;
            var skip = (request.PageNumber - 1) * pageSize;

            var objList = await _dbContext
                .Movimientos
                .Include(x => x.Cuenta).ThenInclude(x => x.Cliente)
                .Where(filterExpresion)
                .Skip(skip)
                .Take(pageSize)
                .OrderByDescending(x => x.CreateAt)
                .AsNoTracking()
                .ToListAsync();

            var totalRecords = await _dbContext.Movimientos.Where(filterExpresion).CountAsync();

            return PaginationHelper
               .CreatePaginatedResponse(request.BaseUrl, totalRecords, pageSize, request.PageNumber, ToDto(objList));
        }

        private IEnumerable<CreateMovimientoDTO> ToDto(IEnumerable<Movimiento> entities)
        {
            foreach (var item in entities)
            {
                yield return new CreateMovimientoDTO
                {
                    Id = item.Id,
                    FechaMovimiento = item.CreateAt.ToString("dd-MM-yyyy"),
                    DescripcionMovimiento = item.TipoDeMovimiento == TipoDeMovimiento.Deposito ? $"Deposito de {item.Valor}" : $"Retiro de {item.Valor}",
                    Valor = item.Valor,
                    SaldoInicial = item.SaldoInicial,
                    Estado = item.Estado,
                    Cliente = item.Cuenta.Cliente.NombreYApellido,
                    NumeroCuenta = item.Cuenta.NumeroCuenta
                };
            }
        }

        private Expression<Func<Movimiento, bool>> GetPredicate(GetAllMovimientosQuery request)
        {
            var predicate = PredicateBuilder.True<Movimiento>();

            predicate = predicate.And(x => !x.ItWasDeleted);

            if (request.CustomerId >= 1)
            {
                predicate = predicate.And(x => x.Cuenta.ClienteId == request.CustomerId);
            }

            if(request.CuentaId >= 1)
            {
                predicate = predicate.And(x => x.CuentaId == request.CuentaId);
            }

            if (request.Estado >= 1)
            {
                var myEstado = request.Estado == 1 ? true : false;
                predicate = predicate.And(x => x.Estado == myEstado);
            }

            if (request.TipoDeMovimiento >= 1)
            {
                var myTipoDeMovimiento = (TipoDeMovimiento)request.TipoDeMovimiento;
                predicate = predicate.And(x => x.TipoDeMovimiento == myTipoDeMovimiento);
            }

            if (request.FechaDesde != null )
            {
                predicate = predicate.And(x => x.CreateAt.Date >= request.FechaDesde.Value.Date);
            }

            if (request.FechaHasta != null)
            {
                predicate = predicate.And(x => x.CreateAt.Date <= request.FechaHasta.Value.Date);
            }



            return predicate;

        }
    }
}
