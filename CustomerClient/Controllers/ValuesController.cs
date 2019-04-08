using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerFramework;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Client;

namespace CustomerClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ICustomerService _service;
        public ValuesController()
        {
            _service = ServiceProxy.Create<ICustomerService>(new Uri("fabric:/Walmart/Customer"), new ServicePartitionKey(0));
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> Get()
        {
            IEnumerable<Users> users = null;
            try
            {
                users = await _service.GetAllCustomers();
            }
            catch(Exception ex)
            {
                
            }
            return Ok(users);

        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] Users value)
        {
            try
            {
                await _service.AddCustomer(value);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
