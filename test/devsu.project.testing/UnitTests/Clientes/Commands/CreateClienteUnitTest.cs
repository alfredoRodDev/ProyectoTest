using devsu.project.Application.Common.Exceptions;
using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using devsu.project.testing.Common;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.testing.UnitTests.Clientes.Commands
{
    public class CreateClienteUnitTest : Testing
    {

        [Test]
        [TestCase("", "", 0, "", "", "",6)]
        [TestCase("Juan Perez", "", 0, "", "", "",5)]
        [TestCase("", "1234", 0, "", "", "",5)]
        [TestCase("", "", 12, "", "", "",6)]
        [TestCase("", "", 18, "", "", "",5)]
        [TestCase("", "", 18, "calle jacinto", "", "",4)]
        [TestCase("", "", 18, "", "555555", "",4)]
        [TestCase("", "", 18, "", "", "1234",4)]
        [TestCase("Juan Perez", "1234", 18, "calle jacinto", "1234", "1234",0)]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "1234", 18, "calle jacinto", "1234", "1234",1)]
        public void Create_ShouldReturnValidationErrors(string nombreYApellido, string identificacion,
            int edad, string direccion, string telefono, string password, int expectedErrors)
        {
            var command = new CreateClienteCommand
            {
                NombreYApellido = nombreYApellido,
                Identificacion = identificacion,
                Edad = edad,
                Direccion = direccion,
                Telefono = telefono,
                Password = password,
                Estado = true,
                Genero = Domain.Enums.Genero.Femenino,
            };

            var validator = new CreateClienteValidator();
            var validationResult = validator.Validate(command);

            validationResult.Errors.Should().HaveCount(expectedErrors);

        }
    }
}
