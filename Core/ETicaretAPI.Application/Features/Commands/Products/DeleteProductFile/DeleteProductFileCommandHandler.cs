using ETicaretAPI.Application.Repositories.Product;
using ETicaretAPI.Application.Repositories.ProductImageFİle;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Products.DeleteProductFile
{
    public class DeleteProductFileCommandHandler : IRequestHandler<DeleteProductFileCommandRequest, DeleteProductFileCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public DeleteProductFileCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository, IProductReadRepository productReadRepository = null)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<DeleteProductFileCommandResponse> Handle(DeleteProductFileCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.Table.Include(p => p.images).FirstOrDefaultAsync(p => p.ID == request.id);
            var image = product.images.FirstOrDefault(p => p.ID == request.imageid);
            product.images.Remove(image);
            await _productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
