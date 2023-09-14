using ETicaretAPI.Application.Repositories.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Products.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository = null)
        {
            _productWriteRepository = productWriteRepository;
        }
        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.RemoveAsync(request.ID);
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
