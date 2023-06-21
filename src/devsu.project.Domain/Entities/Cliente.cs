using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Domain.Entities
{
    public class Cliente : Persona
    {

        public Cliente()
        {
                Cuentas = new HashSet<Cuenta>();
        }

        public string Password { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
        public IEnumerable<Cuenta> Cuentas { get; set; }
    }
}
