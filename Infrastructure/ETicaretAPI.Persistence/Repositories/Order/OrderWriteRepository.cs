using ETicaretAPI.Application.Repositories.Order;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.Order
{
    public class OrderWriteRepository : WriteRepository<ETicaretAPI.Domain.Entities.Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(ETicaretAPIDbCoxtext context) : base(context)
        {
        }
    }
}
