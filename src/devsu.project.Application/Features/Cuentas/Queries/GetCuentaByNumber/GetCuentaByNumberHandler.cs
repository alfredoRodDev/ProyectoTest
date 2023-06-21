using AutoMapper;
using devsu.project.Application.Common.Interfaces;
using devsu.project.Application.Features.Cuentas.Commands.CreateCuenta;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Queries.GetCuentaByNumber
{
    public class GetCuentaByNumberHandler : IRequestHandler<GetCuentaByNumberQuery, IEnumerable<CreateCuentaDTO>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCuentaByNumberHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<CreateCuentaDTO>> Handle(GetCuentaByNumberQuery request, CancellationToken cancellationToken)
        {
            var objList = await _dbContext.Cuentas.Where(x => x.NumeroCuenta.ToLower().Contains(request.numeroCuenta.ToLower()) && !x.ItWasDeleted).ToListAsync();

            return _mapper.Map<IEnumerable<CreateCuentaDTO>>(objList);
        }
    }
}
