using ETicaretAPI.Application.Repositories.Customer;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.Customer
{
    public class CustomerReadRepository : ReadRepository<ETicaretAPI.Domain.Entities.Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(ETicaretAPIDbCoxtext context) : base(context)
        {
        }
    }
}
