using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Queries.GetAllClientes
{
    public class GetAllClientesValidator : AbstractValidator<GetAllClientesQuery>
    {
        public GetAllClientesValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.MinEdad).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MaxEdad).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Genero).GreaterThanOrEqualTo(0).LessThanOrEqualTo(3);
            RuleFor(x => x.Estado).GreaterThanOrEqualTo(0).LessThanOrEqualTo(2);
        }
    }
}
