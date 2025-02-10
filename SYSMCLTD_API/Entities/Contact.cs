using System.ComponentModel.DataAnnotations;

namespace SYSMCLTD_API.Entities
{
    public class Contact
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string? OfficeNumber { get; set; }
        public string? Email { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
        [Required]
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }

}
