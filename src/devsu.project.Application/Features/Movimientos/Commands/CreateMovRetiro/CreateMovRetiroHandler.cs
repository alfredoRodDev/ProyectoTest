using devsu.project.Application.Common.Interfaces;
using devsu.project.Application.Features.Movimientos.Commands.CreateMovDeposito;
using devsu.project.Application.WrappersModels;
using devsu.project.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Movimientos.Commands.CreateMovRetiro
{
    public class CreateMovRetiroHandler : IRequestHandler<CreateMovRetiroCommand, Response<CreateMovimientoDTO>>
    {

        private readonly IAppDbContext _dbContext;
        private readonly ILogger<CreateMovRetiroHandler> _logger;

        public CreateMovRetiroHandler(IAppDbContext dbContext, ILogger<CreateMovRetiroHandler> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        public async Task<Response<CreateMovimientoDTO>> Handle(CreateMovRetiroCommand request, CancellationToken cancellationToken)
        {
            var cuenta = await _dbContext
                .Cuentas
                .Include(x => x.Cliente)
                .Where(x => x.Id == request.CuentaId)
                .FirstOrDefaultAsync();

            if (cuenta == null)
                return Response<CreateMovimientoDTO>.Failure("¡Cuenta no existe!", null);

            if (cuenta.SaldoActual == 0)
                return Response<CreateMovimientoDTO>.Failure("¡Saldo no disponible!", null);

            if (cuenta.SaldoActual < request.Valor)
                return Response<CreateMovimientoDTO>.Failure("¡Saldo no disponible!", null);

            var totalRetirosHoy = await _dbContext
                .Movimientos
                .Where(x => x.CuentaId == request.CuentaId && x.TipoDeMovimiento == Domain.Enums.TipoDeMovimiento.Retiro && x.CreateAt.Date == DateTime.Now.Date)
                .SumAsync(x => x.Valor);

            if (totalRetirosHoy + request.Valor > 1000)
                return Response<CreateMovimientoDTO>.Failure("¡Cupo diario excedido!", null);

            var saldoFinal = cuenta.SaldoActual - request.Valor;

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var entity = new Movimiento { CuentaId = request.CuentaId, Valor = request.Valor, TipoDeMovimiento = Domain.Enums.TipoDeMovimiento.Retiro, SaldoInicial = cuenta.SaldoActual, SaldoFinal = saldoFinal, Estado = true };

                    _dbContext.Movimientos.Add(entity);

                    cuenta.SaldoActual = saldoFinal;

                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync();

                    return Response<CreateMovimientoDTO>.Success(new CreateMovimientoDTO
                    {
                        Id = entity.Id,
                        FechaMovimiento = entity.CreateAt.ToString("dd-MM-yyyy"),
                        DescripcionMovimiento = $"Retiro de {request.Valor}",
                        Valor = entity.Valor,
                        SaldoInicial = entity.SaldoInicial,
                        Estado = entity.Estado,
                        Cliente = cuenta.Cliente.NombreYApellido,
                        NumeroCuenta = cuenta.NumeroCuenta
                    });

                }
                catch (Exception ex)
                {
                    _logger.LogError("Error en Retiro-Movimiento", ex);
                    await transaction.RollbackAsync();
                }
            }

            return Response<CreateMovimientoDTO>.Failure("¡Error al crear el movimiento!", null);
        }
    }
}
