using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SYSMCLTD_API.Data;
using SYSMCLTD_API.DTOs;
using SYSMCLTD_API.Entities;

namespace SYSMCLTD_API.Controllers
{
    public class CustomerController : BaseApiController
    {
        private readonly Repository repository;
        public CustomerController(Repository repo)
        {
            this.repository = repo; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await repository.GetCustomersAsync();
            if (customers == null)
                return NotFound();
            else
            return Ok(customers);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer = await repository.GetCustomerForDeleteByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.IsDeleted = true;
            await repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            var customer = await repository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomerAsync(int id, CustomerDto customerDto)
        {
            if (id != customerDto.Id)
                return BadRequest("Customer ID mismatch");

            var success = await repository.UpdateCustomerAsync(customerDto);
            return success ? NoContent() : NotFound();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCustomerAsync([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest("Invalid customer data.");
            }

            var existingCustomer = await repository.GetCustomerByNameAsync(customerDto.Name);
            if (existingCustomer != null)
            {
                return BadRequest("A customer with this name already exists.");
            }

            var newCustomer = new Customer
            {
                Name = customerDto.Name,
                CustomerNumber = customerDto.CustomerNumber,
                Created = DateTime.UtcNow,
                Addresses = customerDto.Addresses.Select(a => new Address
                {
                    City = a.City,
                    Street = a.Street,
                }).ToList(),
                Contacts = customerDto.Contacts.Select(c => new Contact
                {
                    FullName = c.FullName,
                    OfficeNumber = c.OfficeNumber,
                    Email = c.Email,
                }).ToList()
            };

            repository.AddCustomer(newCustomer);
            await repository.SaveChangesAsync();

            return Ok(new { message = "Customer added successfully", customerId = newCustomer.Id });
        }
    }
}
