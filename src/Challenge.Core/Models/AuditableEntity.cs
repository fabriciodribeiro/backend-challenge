using System;

namespace Challenge.Core.Models
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }        
        public DateTime? Modified { get; set; }
    }
}
