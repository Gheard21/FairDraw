using System.ComponentModel.DataAnnotations;

namespace FairDraw.App.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public required Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
