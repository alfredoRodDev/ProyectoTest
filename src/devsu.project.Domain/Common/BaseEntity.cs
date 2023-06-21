using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Domain.Common
{
    public abstract class BaseEntity<T> : BaseAuditableEntity
    {
        public T Id { get; set; }
    }
}
