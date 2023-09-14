using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repositories.Product;
using ETicaretAPI.Application.Repositories.ProductImageFİle;
using ETicaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Products.CreateProductFile
{
    public class CreateProductFileCommandHandler : IRequestHandler<CreateProductFileCommandRequest, CreateProductFileCommandResponse>
    {
        readonly IStorageService _storageService;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IProductReadRepository _productReadRepository;

        public CreateProductFileCommandHandler(IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository = null, IStorageService storageService = null)
        {
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;
        }

        public async Task<CreateProductFileCommandResponse> Handle(CreateProductFileCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("product-images", request.Files);
            var product = await _productReadRepository.GetByIdAsync(request.id);
            await _productImageFileWriteRepository.AddRangeAsync(result.Select(op => new ProductImageFile
            {
                FileName = op.fileName,
                Path = op.pathOrContainerName,
                Storage = "Azure",
                Products = new List<Product>() { product }
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
