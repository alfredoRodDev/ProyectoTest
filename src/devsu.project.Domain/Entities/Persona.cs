using devsu.project.Domain.Common;
using devsu.project.Domain.Enums;
using devsu.project.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Domain.Entities
{
    public abstract class Persona : BaseEntity<int>
    {
        public string NombreYApellido { get; set; } = string.Empty;
        public Genero Genero { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = string.Empty;

    }
}
