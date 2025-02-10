using Microsoft.EntityFrameworkCore;
using SYSMCLTD_API.Entities;
using System.Text.Json;

namespace SYSMCLTD_API.Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (await context.Customers.AnyAsync()) return;

            var customerData = await File.ReadAllTextAsync("Data/CustomerSeedData.json");
            var customers = JsonSerializer.Deserialize<List<Customer>>(customerData);

            foreach (var customer in customers)
            {
                context.Customers.Add(customer);

                foreach (var address in customer.Addresses)
                {
                    address.CustomerId = customer.Id;
                    context.Addresses.Add(address);
                }

                foreach (var contact in customer.Contacts)
                {
                    contact.CustomerId = customer.Id;
                    context.Contacts.Add(contact);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
