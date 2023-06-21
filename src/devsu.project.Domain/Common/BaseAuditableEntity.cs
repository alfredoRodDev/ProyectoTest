using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Domain.Common
{
    public abstract class BaseAuditableEntity
    {
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime SyncAt { get; set; }
        public int? CreateBy { get; set; }
        public int? UpdateBy { get; set; }
        public bool ItWasDeleted { get; set; }
    }
}
