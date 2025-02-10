using Microsoft.EntityFrameworkCore;
using SYSMCLTD_API.DTOs;
using SYSMCLTD_API.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SYSMCLTD_API.Data
{
    public class Repository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Customer>> GetCustomersAsync() 
        {
            return await context.Customers
               .Where(cmr => cmr.IsDeleted == false)
               .ToListAsync(); 
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            return await context.Customers
                .Include(c => c.Addresses)
                .Include(c => c.Contacts)
                .AsNoTracking()
                .Where(c => c.Id == id && c.IsDeleted == false)
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CustomerNumber = c.CustomerNumber,
                    Created = c.Created,
                    Addresses = c.Addresses
                        .Where(a => !a.IsDeleted) // Filter out deleted addresses
                        .Select(a => new AddressDto
                        {
                            Id = a.Id,
                            City = a.City,
                            Street = a.Street,
                        }).ToList(),
                    Contacts = c.Contacts
                        .Where(contact => !contact.IsDeleted) // Filter out deleted contacts
                        .Select(contact => new ContactDto
                        {
                            Id = contact.Id,
                            FullName = contact.FullName,
                            OfficeNumber = contact.OfficeNumber,
                            Email = contact.Email,
                        }).ToList()
                })
                .FirstOrDefaultAsync();
        }



        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync(); 
        }

        public async Task<Customer> GetCustomerForDeleteByIdAsync(int id)
        {
            return await context.Customers
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();
        }


        public async Task<bool> UpdateCustomerAsync(CustomerDto customerDto)
        {
            var existingCustomer = await context.Customers
                .Include(c => c.Addresses)
                .Include(c => c.Contacts)
                .FirstOrDefaultAsync(c => c.Id == customerDto.Id);

            if (existingCustomer == null)
                return false;

            existingCustomer.Name = customerDto.Name;
            existingCustomer.CustomerNumber = customerDto.CustomerNumber;

            var addressIds = customerDto.Addresses.Select(a => a.Id).ToList();
            foreach (var address in existingCustomer.Addresses.ToList())
            {
                if (!addressIds.Contains(address.Id))
                {
                    address.IsDeleted = true;
                }
            }

            foreach (var addressDto in customerDto.Addresses)
            {
                var existingAddress = existingCustomer.Addresses.FirstOrDefault(a => a.Id == addressDto.Id);
                if (existingAddress != null)
                {
                    existingAddress.City = addressDto.City;
                    existingAddress.Street = addressDto.Street;
                    existingAddress.IsDeleted = false;
                }
                else
                {
                    existingCustomer.Addresses.Add(new Address
                    {
                        City = addressDto.City,
                        Street = addressDto.Street,
                        CustomerId = existingCustomer.Id 
                    });
                }
            }

            var contactIds = customerDto.Contacts.Select(c => c.Id).ToList();
            foreach (var contact in existingCustomer.Contacts.ToList())
            {
                if (!contactIds.Contains(contact.Id))
                {
                    contact.IsDeleted = true;
                }
            }

            foreach (var contactDto in customerDto.Contacts)
            {
                var existingContact = existingCustomer.Contacts.FirstOrDefault(c =>
                    c.FullName == contactDto.FullName &&
                    c.OfficeNumber == contactDto.OfficeNumber &&
                    c.Email == contactDto.Email);

                if (existingContact != null)
                {
                    existingContact.FullName = contactDto.FullName;
                    existingContact.OfficeNumber = contactDto.OfficeNumber;
                    existingContact.Email = contactDto.Email;
                    existingContact.IsDeleted = false;
                }
                else
                {
                    existingCustomer.Contacts.Add(new Contact
                    {
                        FullName = contactDto.FullName,
                        OfficeNumber = contactDto.OfficeNumber,
                        Email = contactDto.Email,
                        CustomerId = existingCustomer.Id 
                    });
                }
            }

            await context.SaveChangesAsync();
            return true;
        }




        public async Task<Customer> GetCustomerByNameAsync(string name)
        {
            return await context.Customers
                .FirstOrDefaultAsync(c => c.Name == name && !c.IsDeleted);
        }

        public void AddCustomer(Customer customer)
        {
            context.Customers.Add(customer);
        }


    }
}
