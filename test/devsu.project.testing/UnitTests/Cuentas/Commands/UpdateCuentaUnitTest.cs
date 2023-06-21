using devsu.project.Application.Common.Exceptions;
using devsu.project.Application.Features.Cuentas.Commands.UpdateCuenta;
using devsu.project.testing.Common;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.testing.UnitTests.Cuentas.Commands
{
    public class UpdateCuentaUnitTest : Testing
    {


        [Test]
        [TestCase(0, "", 0, 2)]
        [TestCase(1, "", 0, 1)]
        [TestCase(0, "1234", 0, 1)]
        [TestCase(0, "", 100, 2)]
        [TestCase(1, "1234", 100, 0)]
        public void Update_ShouldReturnValidationErrors(int id,string numeroCuenta, decimal saldoInicial, int expectedErrors)
        {
            var command = new UpdateCuentaCommand
            {
                Id = id,
                NumeroCuenta = numeroCuenta,
               SaldoInicial = saldoInicial,
               TipoDeCuenta = Domain.Enums.TipoDeCuenta.Ahorro,
               Estado = true
            };

            var validator = new UpdateCuentaValidator();
            var validationResult = validator.Validate(command);

            validationResult.Errors.Should().HaveCount(expectedErrors);

        }
    }
}
