using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Commands.CreateCliente
{
    public class CreateClienteValidator : AbstractValidator<CreateClienteCommand>
    {
        public CreateClienteValidator()
        {
            RuleFor(x => x.NombreYApellido).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Identificacion).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Telefono).NotEmpty().MaximumLength(35);
            RuleFor(x => x.Direccion).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Genero).IsInEnum();
            RuleFor(x => x.Edad).GreaterThanOrEqualTo(18).WithMessage("¡Edad mínima requerida para aperturar cuenta es 18 años!");
            RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
        }
    }
}
