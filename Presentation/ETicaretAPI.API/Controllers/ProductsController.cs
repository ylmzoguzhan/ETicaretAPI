using ETicaretAPI.Application.Repositories.File;
using ETicaretAPI.Application.Repositories.Product;
using ETicaretAPI.Application.Repositories.ProductImageFİle;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.Services;
using ETicaretAPI.Application.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        readonly private IProductReadRepository _productReadRepository;
        readonly private IFileReadRepository _fileReadRepository;
        readonly private IFileWriteRepository _fileWriteRepository;
        readonly private IProductImageFileReadRepository _productImageFileReadRepository;
        readonly private IProductImageFileWriteRepository _productImageFileWriteReadRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService, IFileReadRepository fileReadRepository, IFileWriteRepository fileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteReadRepository = productImageFileWriteRepository;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Select(p => new
            {
                p.ID,
                p.Name,
                p.Stock,
                p.Price
            }).Skip(pagination.Page * pagination.PageSize).Take(pagination.PageSize).ToList();
            return Ok(new
            {
                totalCount,
                products
            });
        }
        [HttpGet("{ID}")]
        public async Task<IActionResult> Get(long ID)
        {
            return Ok(await _productReadRepository.GetByIdAsync(ID, false));
        }
        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            if (ModelState.IsValid)
            {

            }
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            ETicaretAPI.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(model.ID);
            product.Stock = model.Stock;
            product.Name = model.Name;
            product.Price = model.Price;
            await _productWriteRepository.SaveAsync();

            return Ok();
        }
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Delete(long ID)
        {
            await _productWriteRepository.RemoveAsync(ID);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            var datas = await _fileService.UploadAsync("resource/products-images", Request.Form.Files);
            await _productImageFileWriteReadRepository.AddRangeAsync(datas.Select(d => new ETicaretAPI.Domain.Entities.ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.path
            }).ToList());
            await _productImageFileWriteReadRepository.SaveAsync();
            var d1 = _fileReadRepository.GetAll();
            var d2 = _productImageFileReadRepository.GetAll();
            return Ok();
        }
    }
}
