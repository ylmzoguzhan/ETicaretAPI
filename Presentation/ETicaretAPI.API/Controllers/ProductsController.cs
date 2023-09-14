using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Features.Commands.CreateProduct;
using ETicaretAPI.Application.Features.Commands.Products.CreateProductFile;
using ETicaretAPI.Application.Features.Commands.Products.DeleteProduct;
using ETicaretAPI.Application.Features.Commands.Products.DeleteProductFile;
using ETicaretAPI.Application.Features.Commands.Products.UpdateProduct;
using ETicaretAPI.Application.Features.Queries.GetAllProduct;
using ETicaretAPI.Application.Features.Queries.Products.GetProduct;
using ETicaretAPI.Application.Features.Queries.Products.GetProductFiles;
using ETicaretAPI.Application.Repositories.File;
using ETicaretAPI.Application.Repositories.Product;
using ETicaretAPI.Application.Repositories.ProductImageFİle;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly private IProductReadRepository _productReadRepository;
        readonly private IFileReadRepository _fileReadRepository;
        readonly private IFileWriteRepository _fileWriteRepository;
        readonly private IProductImageFileReadRepository _productImageFileReadRepository;
        readonly private IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IStorageService _storageService;
        readonly IConfiguration _configuration;

        readonly IMediator _mediator;
        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileReadRepository fileReadRepository, IFileWriteRepository fileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;
            _configuration = configuration;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest query)
        {
            var products = await _mediator.Send(query);
            return Ok(products);
        }
        [HttpGet("{ID}")]
        public async Task<IActionResult> Get([FromRoute] GetProductQueryRequest query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] CreateProductFileCommandRequest request)
        {
            request.Files = Request.Form.Files;
            await _mediator.Send(request);
            return Ok();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetImages([FromRoute] GetProductFilesQueryRequest query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteImage([FromRoute]DeleteProductFileCommandRequest request, long imageid)
        {
            request.imageid = imageid;
            await _mediator.Send(request);
            return Ok();
        }
    }
}
