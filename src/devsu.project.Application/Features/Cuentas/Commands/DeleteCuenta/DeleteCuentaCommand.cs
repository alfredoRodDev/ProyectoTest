using devsu.project.Application.WrappersModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Commands.DeleteCuenta
{
    public record DeleteCuentaCommand(int id) : IRequest<Response>
    {
    }

}
