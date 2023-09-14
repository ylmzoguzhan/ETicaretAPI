using ETicaretAPI.Application.Repositories.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Products.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, GetProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        public GetProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }
        public async Task<GetProductQueryResponse> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetByIdAsync(request.ID);
            return new() {ID = product.ID, Name = product.Name, Price = product.Price, Stock = product.Stock};
        }
    }
}
