using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Queries.GetClienteById
{
    public class GetClienteByIdValidator : AbstractValidator<GetClienteByIdQuery>
    {
        public GetClienteByIdValidator()
        {
            RuleFor(x => x.id);
        }
    }
}
