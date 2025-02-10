using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SYSMCLTD_API.Entities
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CustomerNumber { get; set; }// זה החפ
        [Required]
        public bool IsDeleted { get; set; } = false;
        [Required]
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
