using devsu.project.Application.Common.Exceptions;
using devsu.project.Application.Features.Movimientos.Commands.CreateMovDeposito;
using devsu.project.testing.Common;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.testing.UnitTests.Movimientos.Commands
{
    public class CreateMovimientoUnitTest : Testing
    {

        [Test]
        [TestCase(0, 0, 2)]
        [TestCase(0, 1, 1)]
        [TestCase(100, 0, 1)]
        [TestCase(100, 1, 0)]
        public void Create_ShouldReturnValidationErrors(decimal valor, int cuentaId, int expectedErrors)
        {
            var command = new CreateMovDepositoCommand
            {
                Valor = valor,
                CuentaId = cuentaId
            };

            var validator = new CreateMovDepositoValidator();
            var validationResult = validator.Validate(command);

            validationResult.Errors.Should().HaveCount(expectedErrors);

        }
    }
}
