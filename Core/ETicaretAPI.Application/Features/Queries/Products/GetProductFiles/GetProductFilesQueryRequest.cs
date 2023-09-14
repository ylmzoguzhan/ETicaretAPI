using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Products.GetProductFiles
{
    public class GetProductFilesQueryRequest: IRequest<List<GetProductFilesQueryResponse>>
    {
        public long id { get; set; }
    }
}
