using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Queries.GetSaldoActual
{
    public class GetSaldoActualValidator : AbstractValidator<GetSaldoActualQuery>
    {
        public GetSaldoActualValidator()
        {
            RuleFor(x => x.cuentaId).GreaterThan(0);
        }
    }
}
