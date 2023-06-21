using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Queries.GetClienteBySearch
{
    public class GetClienteBySearchValidator : AbstractValidator<GetClienteBySearchQuery>
    {
        public GetClienteBySearchValidator()
        {
            RuleForEach(x => x.searchParam).NotEmpty();
        }
    }
}
