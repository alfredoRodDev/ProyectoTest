using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Commands.UpdateCliente
{
    public class UpdateClienteValidator : AbstractValidator<UpdateClienteCommand>
    {
        public UpdateClienteValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.NombreYApellido).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Identificacion).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Telefono).NotEmpty().MaximumLength(35);
            RuleFor(x => x.Direccion).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Genero).IsInEnum();
            RuleFor(x => x.Edad).GreaterThan(0);
            RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
        }
    }
}
