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

namespace devsu.project.Application.Features.Cuentas.Commands.CreateCuenta
{
    public class CreateCuentaHandler : IRequestHandler<CreateCuentaCommand, Response<CreateCuentaDTO>>
    {

        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCuentaHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<Response<CreateCuentaDTO>> Handle(CreateCuentaCommand request, CancellationToken cancellationToken)
        {
            //validar numero cuenta repetido
            var exist = await _dbContext
                .Cuentas
                .Where(x => x.NumeroCuenta.ToUpper() == request.NumeroCuenta.ToUpper())
                .AnyAsync();

            if (exist)
                return Response<CreateCuentaDTO>.Failure(CrudErrorResponses.ErrorRepeatedResource, null);

            //validar cliente existente
            var cliente = await _dbContext
                .Clientes
                .Where(x => x.Id == request.ClienteId)
                .FirstOrDefaultAsync();

            if (cliente == null)
                return Response<CreateCuentaDTO>.Failure("¡Cliente no existe en la base de dato!", null);

            var entity = _mapper.Map<Cuenta>(request);

            entity.SaldoActual = entity.SaldoInicial;
            _dbContext.Cuentas.Add(entity);

            var result = await _dbContext.SaveChangesAsync(cancellationToken) >= 1;

            if (result)
            {
                var objDto = _mapper.Map<CreateCuentaDTO>(entity);
                objDto.Cliente = cliente.NombreYApellido;

                return Response<CreateCuentaDTO>.Success(objDto);

            }


            return Response<CreateCuentaDTO>.Failure(CrudErrorResponses.ErrorAdding, null);


        }
    }
}
