using devsu.project.Application.WrappersModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Commands.CreateCuenta
{
    public class CreateCuentaValidator : AbstractValidator<CreateCuentaCommand>
    {
        public CreateCuentaValidator()
        {
            RuleFor(c => c.NumeroCuenta)
                .NotEmpty().WithMessage("{PropertyName} es requerido.")
                .MaximumLength(100).WithMessage("{PropertyName} no debe exceder los {MaxLength} caracteres.");

            RuleFor(c => c.TipoDeCuenta).IsInEnum();

            RuleFor(c => c.SaldoInicial).GreaterThanOrEqualTo(0);

            RuleFor(c => c.ClienteId).GreaterThanOrEqualTo(1);

        }
    }
}
