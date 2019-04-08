using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomerFramework;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;

namespace Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private IReliableStateManager _stateManager;
        public CustomerRepository(IReliableStateManager stateManager)
        {
            _stateManager = stateManager;
        }

        public async Task AddCustomer(Users customer)
        {
            var customerDictionary = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Users>>("myCustomers");

            using (var tx = _stateManager.CreateTransaction())
            {
                await customerDictionary.AddOrUpdateAsync(tx, customer.CustomerId, customer, (id, key) => customer);
                await tx.CommitAsync();
            }
        }

        public async Task<IEnumerable<Users>> GetAllCustomers()
        {
            var customerDictionary = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Users>>("myCustomers");

            List<Users> allUsers = new List<Users>();

            using (var tx = _stateManager.CreateTransaction())
            {
                var customerEnumerable = await customerDictionary.CreateEnumerableAsync(tx, EnumerationMode.Ordered);

                using (var customerEnumerator = customerEnumerable.GetAsyncEnumerator())
                {
                    while(await customerEnumerator.MoveNextAsync(CancellationToken.None))
                    {
                        KeyValuePair<Guid, Users> cust = customerEnumerator.Current;
                        allUsers.Add(cust.Value);
                    }

                }
            }

            return allUsers;
        }
    }
}
