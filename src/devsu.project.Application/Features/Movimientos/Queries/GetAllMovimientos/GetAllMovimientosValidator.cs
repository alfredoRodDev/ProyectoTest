using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Movimientos.Queries.GetAllMovimientos
{
    public class GetAllMovimientosValidator : AbstractValidator<GetAllMovimientosQuery>
    {
        public GetAllMovimientosValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.CustomerId).GreaterThanOrEqualTo(0);
            RuleFor(x => x.TipoDeMovimiento).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Estado).GreaterThanOrEqualTo(0);
        }
    }
}
