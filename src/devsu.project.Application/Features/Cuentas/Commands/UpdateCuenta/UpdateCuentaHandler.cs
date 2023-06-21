using AutoMapper;
using devsu.project.Application.Common.Exceptions;
using devsu.project.Application.Common.Interfaces;
using devsu.project.Application.Features.Cuentas.Commands.CreateCuenta;
using devsu.project.Application.WrappersModels;
using devsu.project.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Commands.UpdateCuenta
{
    public class UpdateCuentaHandler : IRequestHandler<UpdateCuentaCommand, Response<CreateCuentaDTO>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateCuentaHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<Response<CreateCuentaDTO>> Handle(UpdateCuentaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext
             .Cuentas
             .Include(x => x.Cliente)
             .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Cuenta), request.Id);

            //validar cambio de numero de cuenta
            var exist = await _dbContext
                .Cuentas
                .Where(x => x.NumeroCuenta.ToUpper() == request.NumeroCuenta.ToUpper() && x.Id != request.Id)
                .AnyAsync();

            if (exist)
                return Response<CreateCuentaDTO>.Failure("¡El numero de cuenta que desea cambiar es usado por otro usario, por favor revise e intente nuevamente!", null);

            entity.NumeroCuenta = request.NumeroCuenta.ToUpper();
            entity.TipoDeCuenta = request.TipoDeCuenta;
            entity.SaldoInicial = request.SaldoInicial;
            entity.SaldoActual = request.SaldoInicial;
            entity.Estado = request.Estado;

            var result = await _dbContext.SaveChangesAsync(cancellationToken) >= 1;

            return result ? Response<CreateCuentaDTO>.Success(_mapper.Map<CreateCuentaDTO>(entity))
                : Response<CreateCuentaDTO>.Failure("¡No se pudo actualizar la cuenta!", null);

        }
    }
}
