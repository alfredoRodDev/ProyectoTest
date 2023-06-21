using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Domain.ValueObjects
{
    public record DireccionPersona(string calle, string ciudad, string provincia, string nroCasa);
}
