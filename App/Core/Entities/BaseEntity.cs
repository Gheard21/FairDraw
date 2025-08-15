using System.ComponentModel.DataAnnotations;

namespace FairDraw.App.Core.Entities
{
    public class BaseEntity
    {
        public required Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
