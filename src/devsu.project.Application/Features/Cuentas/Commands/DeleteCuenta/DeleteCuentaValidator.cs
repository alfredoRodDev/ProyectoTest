using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Commands.DeleteCuenta
{
    public class DeleteCuentaValidator : AbstractValidator<DeleteCuentaCommand>
    {
        public DeleteCuentaValidator()
        {
            RuleFor(x => x.id).GreaterThan(0);
        }
    }
}
