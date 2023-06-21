using devsu.project.Application.Features.Cuentas.Commands.DeleteCuenta;
using devsu.project.testing.Common;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.testing.UnitTests.Clientes.Commands
{
    public class DeleteCuentaUnitTest : Testing
    {


        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        public void Delete_ShouldReturnValidationErrors(int id, int expectedErrors)
        {
            var command = new DeleteCuentaCommand(id);

            var validator = new DeleteCuentaValidator();
            var validationResult = validator.Validate(command);

            validationResult.Errors.Should().HaveCount(expectedErrors);

        }
    }
}
