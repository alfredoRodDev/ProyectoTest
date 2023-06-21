using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Movimientos.Commands.CreateMovRetiro
{
    public class CreateMovRetiroValidator : AbstractValidator<CreateMovRetiroCommand>
    {
        public CreateMovRetiroValidator()
        {
            RuleFor(x => x.Valor).GreaterThan(0);
            RuleFor(x => x.CuentaId).GreaterThan(0);
        }
    }
}
