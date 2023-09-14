using ETicaretAPI.Application.Repositories.Product;
using ETicaretAPI.Application.Repositories.ProductImageFİle;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Products.GetProductFiles
{
    public class GetProductFilesQueryHandler : IRequestHandler<GetProductFilesQueryRequest, List<GetProductFilesQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration _configuration;
        public GetProductFilesQueryHandler(IProductReadRepository productReadRepository = null, IConfiguration configuration = null)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        public async Task<List<GetProductFilesQueryResponse>> Handle(GetProductFilesQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.Table.Include(p => p.images).FirstOrDefaultAsync(p => p.ID == request.id);
            var images = product.images.Select(op => new GetProductFilesQueryResponse
            {
                ID = op.ID,
                FileName = op.FileName,
                Path = op.Path = $"{_configuration["BaseStorageUrl"]}/{op.Path}"
            }).ToList();
            return images;

        }
    }
}
