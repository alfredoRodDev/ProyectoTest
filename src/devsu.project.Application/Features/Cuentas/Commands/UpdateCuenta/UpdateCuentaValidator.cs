using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Commands.UpdateCuenta
{
    public class UpdateCuentaValidator : AbstractValidator<UpdateCuentaCommand>
    {
        public UpdateCuentaValidator()
        {
            RuleFor(c => c.Id).GreaterThanOrEqualTo(1);
            
            RuleFor(c => c.NumeroCuenta)
               .NotEmpty().WithMessage("{PropertyName} es requerido.")
               .MaximumLength(100).WithMessage("{PropertyName} no debe exceder los {MaxLength} caracteres.");

            RuleFor(c => c.TipoDeCuenta).IsInEnum();

            RuleFor(c => c.SaldoInicial).GreaterThanOrEqualTo(0);
        }
    }
}
