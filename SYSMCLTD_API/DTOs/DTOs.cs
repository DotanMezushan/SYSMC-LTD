namespace SYSMCLTD_API.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime Created { get; set; } 
        public List<AddressDto> Addresses { get; set; } = new();
        public List<ContactDto> Contacts { get; set; } = new();
    }

    public class AddressDto
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }

    public class ContactDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string? OfficeNumber { get; set; }
        public string? Email { get; set; }
    }
}
