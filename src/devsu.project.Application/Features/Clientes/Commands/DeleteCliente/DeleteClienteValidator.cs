using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Commands.DeleteCliente
{
    public class DeleteClienteValidator : AbstractValidator<DeleteClienteCommand>
    {
        public DeleteClienteValidator()
        {
            RuleFor(x => x.id).GreaterThan(0).WithMessage("El id debe ser mayor a 0");
        }
    }
}
