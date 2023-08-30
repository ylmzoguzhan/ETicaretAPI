using ETicaretAPI.Application.Repositories.Customer;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.Customer
{
    public class CustomerWriteRepository : WriteRepository<ETicaretAPI.Domain.Entities.Customer>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(ETicaretAPIDbCoxtext context) : base(context)
        {

        }
    }
}
