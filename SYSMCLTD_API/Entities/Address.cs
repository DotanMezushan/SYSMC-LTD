using System.ComponentModel.DataAnnotations;

namespace SYSMCLTD_API.Entities
{
    public class Address
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public int CustomerId { get; set; }///FK
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
        [Required]
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
