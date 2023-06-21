using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Queries.GetCuentaByNumber
{
    public class GetCuentaByNumberValidator : AbstractValidator<GetCuentaByNumberQuery>
    {
        public GetCuentaByNumberValidator()
        {
            RuleFor(x => x.numeroCuenta).NotEmpty();
        }
    }
}
