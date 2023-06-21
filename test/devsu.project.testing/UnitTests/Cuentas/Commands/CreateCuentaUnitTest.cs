using devsu.project.Application.Common.Exceptions;
using devsu.project.Application.Features.Cuentas.Commands.CreateCuenta;
using devsu.project.testing.Common;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.testing.UnitTests.Cuentas.Commands
{
    public class CreateCuentaUnitTest : Testing
    {


        [Test]
        [TestCase("", 0, 0, 2)]
        [TestCase("abcx", 0, 0, 1)]
        [TestCase("", 1, 0, 1)]
        [TestCase("", 1, 100, 1)]
        [TestCase("", 1, -100, 2)]
        [TestCase("acxv", 1, 100, 0)]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 1, 100, 1)]
        public void Create_ShouldReturnValidationErrors(string numeroCuenta, int clienteId, decimal saldoInicial, int expectedErrors)
        {
            var command = new CreateCuentaCommand
            {
               NumeroCuenta = numeroCuenta,
               ClienteId = clienteId,
               SaldoInicial = saldoInicial,
               TipoDeCuenta = Domain.Enums.TipoDeCuenta.Ahorro,
            };

            var validator = new CreateCuentaValidator();
            var validationResult = validator.Validate(command);

            validationResult.Errors.Should().HaveCount(expectedErrors);

        }
    }
}
