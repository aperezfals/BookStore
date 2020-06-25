using System;

namespace BookStore.Domain.Common
{
    public abstract class AuditableEntity : EntityBase 
    {
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
