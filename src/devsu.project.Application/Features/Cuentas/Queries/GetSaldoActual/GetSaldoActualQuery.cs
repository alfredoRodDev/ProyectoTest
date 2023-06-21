using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Queries.GetSaldoActual
{
    public record GetSaldoActualQuery(int cuentaId) : IRequest<decimal>
    {
    }
}
