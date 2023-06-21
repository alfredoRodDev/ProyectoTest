using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Movimientos.Commands.DeleteMovimiento
{
    public class DeleteMovimientoValidator : AbstractValidator<DeleteMovimientoCommand>
    {
        public DeleteMovimientoValidator()
        {
            RuleFor(p => p.id).GreaterThanOrEqualTo(1);
        }
    }
}
