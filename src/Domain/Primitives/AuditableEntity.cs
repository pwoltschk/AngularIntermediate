using System.ComponentModel.DataAnnotations;

namespace Domain.Primitives
{
    public abstract class AuditableEntity
    {
        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string CreatedBy { get; set; } = string.Empty;

        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; } = string.Empty;
    }
}
