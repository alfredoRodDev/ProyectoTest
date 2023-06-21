using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Movimientos.Commands.UpdateMovimiento
{
    public class UpdateMovimientoValidator : AbstractValidator<UpdateMovimientoCommand>
    {
        public UpdateMovimientoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Valor).GreaterThan(0);
        }
    }
}
