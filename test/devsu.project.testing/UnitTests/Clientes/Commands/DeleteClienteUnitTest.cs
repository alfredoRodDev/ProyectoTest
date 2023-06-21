using devsu.project.Application.Features.Clientes.Commands.DeleteCliente;
using devsu.project.Application.Features.Clientes.Commands.UpdateCliente;
using devsu.project.testing.Common;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.testing.UnitTests.Clientes.Commands
{
    public class DeleteClienteUnitTest : Testing
    {


        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        public void Delete_ShouldReturnValidationErrors(int id, int expectedErrors)
        {
            var command = new DeleteClienteCommand(id);

            var validator = new DeleteClienteValidator();
            var validationResult = validator.Validate(command);

            validationResult.Errors.Should().HaveCount(expectedErrors);

        }
    }
}
