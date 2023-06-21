using devsu.project.Application.Common.Interfaces;
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

namespace devsu.project.Application.Features.Reportes.Queries.GetAllReportes
{
    public class GetAllReportesHandler : IRequestHandler<GetAllReportesQuery, PaginationResponse<GetAllReportesDTO>>
    {
        private readonly IAppDbContext _dbContext;

        public GetAllReportesHandler(IAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<PaginationResponse<GetAllReportesDTO>> Handle(GetAllReportesQuery request, CancellationToken cancellationToken)
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

        private IEnumerable<GetAllReportesDTO> ToDto(IEnumerable<Movimiento> entities)
        {
            foreach (var item in entities)
            {
                yield return new GetAllReportesDTO
                {
                    Id = item.Id,
                    Cliente = item.Cuenta.Cliente.NombreYApellido,
                    NumeroCuenta = item.Cuenta.NumeroCuenta,
                    TipoDeMovimiento = item.TipoDeMovimiento.ToString(),
                    FechaMovimiento = item.CreateAt.ToString("dd-MM-yyyy"),
                    DescripcionMovimiento = item.TipoDeMovimiento == TipoDeMovimiento.Deposito ? $"Deposito de {item.Valor}" : $"Retiro de {item.Valor}",
                    Valor = item.Valor,
                    SaldoInicial = item.SaldoInicial,
                    SaldoFinal = item.SaldoFinal,
                    Estado = item.Estado,
                };
            }
        }

        private Expression<Func<Movimiento, bool>> GetPredicate(GetAllReportesQuery request)
        {
            var predicate = PredicateBuilder.True<Movimiento>();

            predicate = predicate.And(x => !x.ItWasDeleted);

            if (request.CustomerId >= 1)
            {
                predicate = predicate.And(x => x.Cuenta.ClienteId == request.CustomerId);
            }


            if (request.CuentaId >= 1)
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

            if (request.FechaDesde != null)
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
