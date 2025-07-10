using System.ComponentModel.DataAnnotations;

namespace SantoSabor.Core.Entities
{
    public class Client
    {
        [Key]
        public Guid ClientId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Cpf { get; set; }
        public string? Company { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}