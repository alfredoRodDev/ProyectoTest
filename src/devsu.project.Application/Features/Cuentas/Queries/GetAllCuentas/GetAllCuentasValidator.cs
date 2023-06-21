using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Queries.GetAllCuentas
{
    public class GetAllCuentasValidator : AbstractValidator<GetAllCuentasQuery>
    {
        public GetAllCuentasValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.TipoDeCuenta).GreaterThanOrEqualTo(0).LessThanOrEqualTo(2);
            RuleFor(x => x.Estado).GreaterThanOrEqualTo(0);

        }
    }
}
