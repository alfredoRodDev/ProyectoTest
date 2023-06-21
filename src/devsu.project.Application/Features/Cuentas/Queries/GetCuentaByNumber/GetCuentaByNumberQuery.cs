using devsu.project.Application.Features.Cuentas.Commands.CreateCuenta;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Queries.GetCuentaByNumber
{
    public record GetCuentaByNumberQuery(string numeroCuenta) : IRequest<IEnumerable<CreateCuentaDTO>>
    {
    }
}
