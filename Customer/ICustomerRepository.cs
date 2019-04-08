using CustomerFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customer
{
    public interface ICustomerRepository
    {
        Task AddCustomer(Users customer);
        Task<IEnumerable<Users>> GetAllCustomers();
    }
}
