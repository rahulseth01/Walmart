using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerFramework
{
    public interface ICustomerService: IService
    {
        Task AddCustomer(Users customer);

        Task<IEnumerable<Users>> GetAllCustomers();
    }
}
