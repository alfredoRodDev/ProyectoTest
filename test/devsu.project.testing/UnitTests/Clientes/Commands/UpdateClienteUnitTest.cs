using devsu.project.Application.Common.Exceptions;
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
    public class UpdateClienteUnitTest : Testing
    {

        [Test]
        [TestCase(0,"", "", 0, "", "", "",7)]
        [TestCase(1,"Juan Perez", "", 0, "", "", "",5)]
        [TestCase(1,"", "1234", 0, "", "", "",5)]
        [TestCase(1,"", "", 12, "", "", "",5)]
        [TestCase(1,"", "", 18, "", "", "",5)]
        [TestCase(1,"", "", 18, "calle jacinto", "", "",4)]
        [TestCase(1,"", "", 18, "", "555555", "",4)]
        [TestCase(1,"", "", 18, "", "", "1234",4)]
        [TestCase(1,"Juan Perez", "1234", 18, "calle jacinto", "1234", "1234",0)]
        [TestCase(1,"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "1234", 18, "calle jacinto", "1234", "1234",1)]
        public void Update_ShouldReturnValidationErrors(int id, string nombreYApellido, string identificacion,
            int edad, string direccion, string telefono, string password, int expectedErrors)
        {
            var command = new UpdateClienteCommand
            {
                Id = id,
                NombreYApellido = nombreYApellido,
                Identificacion = identificacion,
                Edad = edad,
                Direccion = direccion,
                Telefono = telefono,
                Password = password,
                Estado = true,
                Genero = Domain.Enums.Genero.Femenino,
            };

            var validator = new UpdateClienteValidator();
            var validationResult = validator.Validate(command);

            validationResult.Errors.Should().HaveCount(expectedErrors);

        }
    }
}
