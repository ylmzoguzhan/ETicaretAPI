using ETicaretAPI.Application.Repositories.Order;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.Order
{
    public class OrderReadRepository : ReadRepository<ETicaretAPI.Domain.Entities.Order>, IOrderReadRepository
    {
        public OrderReadRepository(ETicaretAPIDbCoxtext context) : base(context)
        {
        }
    }
}
