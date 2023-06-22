using AutoMapper;
using devsu.project.Application.Common.Exceptions;
using devsu.project.Application.Common.Interfaces;
using devsu.project.Application.Features.Movimientos.Commands.CreateMovDeposito;
using devsu.project.Application.WrappersModels;
using devsu.project.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Movimientos.Commands.UpdateMovimiento
{
    public class UpdateMovimientoHandler : IRequestHandler<UpdateMovimientoCommand, Response<CreateMovimientoDTO>>
    {
        private readonly IAppDbContext _dbContext;

        public UpdateMovimientoHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
        }

        public async Task<Response<CreateMovimientoDTO>> Handle(UpdateMovimientoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext
          .Movimientos
          .Include(x => x.Cuenta).ThenInclude(x => x.Cliente)
          .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Movimiento), request.Id);

      
            if(entity.TipoDeMovimiento == Domain.Enums.TipoDeMovimiento.Deposito)
            {
                entity.SaldoFinal = entity.SaldoInicial + entity.Valor;
                
                if(request.Valor > entity.Valor)
                {
                    entity.Cuenta.SaldoActual += (request.Valor - entity.Valor);
                }
                else
                {
                    entity.Cuenta.SaldoActual -= (entity.Valor - request.Valor);
                }
            }
            else
            {
                entity.SaldoFinal = entity.SaldoInicial - entity.Valor;

                if (request.Valor > entity.Valor)
                {
                    entity.Cuenta.SaldoActual -= (request.Valor - entity.Valor);
                }
                else
                {
                    entity.Cuenta.SaldoActual += (entity.Valor - request.Valor);
                }
            }

            //validar que el saldo no sea menor a 0
            if(entity.Cuenta.SaldoActual < 0)
            {
                return Response<CreateMovimientoDTO>.Failure("¡El saldo no puede ser menor a 0!", null);
            }

            //validar que si retiro no sea mayor a 1000
            if(entity.TipoDeMovimiento == Domain.Enums.TipoDeMovimiento.Retiro && entity.Valor > 1000)
            {
                return Response<CreateMovimientoDTO>.Failure("¡Cupo Excedido!", null);
            }

            if(entity.TipoDeMovimiento == Domain.Enums.TipoDeMovimiento.Retiro)
            {
                //validar retiros diarios
                var retirosDelDia = await _dbContext
                    .Movimientos
                    .Where(x => x.TipoDeMovimiento == Domain.Enums.TipoDeMovimiento.Retiro 
                    && x.CreateAt.Date == entity.CreateAt.Date && x.CuentaId == entity.CuentaId && x.Id != request.Id)
                    .SumAsync(x => x.Valor);

                var retiros = retirosDelDia + request.Valor;

                if (retiros > 1000)
                {
                    return Response<CreateMovimientoDTO>.Failure("¡Cupo Excedido!", null);
                }
            }


            entity.Valor = request.Valor;

            var result = await _dbContext.SaveChangesAsync(cancellationToken) >= 1;

            if (result)
            {
                return Response<CreateMovimientoDTO>.Success(new CreateMovimientoDTO
                {
                    Id = entity.Id,
                    FechaMovimiento = entity.CreateAt.ToString("dd-MM-yyyy"),
                    DescripcionMovimiento = entity.TipoDeMovimiento == Domain.Enums.TipoDeMovimiento.Deposito ? $"Deposito de {entity.Valor}" : $"Retiro de {entity.Valor}",
                    Valor = entity.Valor,
                    SaldoInicial = entity.SaldoInicial,
                    Estado = entity.Estado,
                    Cliente = entity.Cuenta.Cliente.NombreYApellido,
                    NumeroCuenta = entity.Cuenta.NumeroCuenta
                });
            }


            return Response<CreateMovimientoDTO>.Failure("¡Error al actualizar el movimiento!", null);
        }
    }
}
