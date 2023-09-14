using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Products.GetProduct
{
    public class GetProductQueryRequest: IRequest<GetProductQueryResponse>
    {
        public long ID { get; set; }
    }
}
