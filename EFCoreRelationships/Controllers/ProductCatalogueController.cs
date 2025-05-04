using EFCoreRelationships.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationships.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductCatalogueController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public IConfiguration _configuration { get; }

        public ProductCatalogueController(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        [HttpPost("CreateProductCatalogue")]
        public async Task<ActionResult<List<ProductCatalogues>>> Create(ProductCatalogueDto request)
        {
            var catelogue = await this._unitOfWork.catalogueRepo.GetAsync(request.CatalogueId);
            var product = await this._unitOfWork.productRepo.GetAsync(request.ProductId);

            if (catelogue == null && product == null)
                return NotFound();

            var newProductCatalogue = new ProductCatalogues
            {
                CatelogueId = request.CatalogueId,
                ProductId = request.ProductId
            };


            var _data = await this._unitOfWork.productCatalogueRepo.AddEntity(newProductCatalogue);
            await this._unitOfWork.CompleteAsync();

            return await this._unitOfWork.productCatalogueRepo.GetAllAsync();
        }

        [HttpPut("UpdateProductCatelogues")]
        public async Task<ActionResult<List<ProductCatalogues>>> Edit(ProductCatalogueDto request)
        {
            var productCatelogue = await this._unitOfWork.productCatalogueRepo.GetAsync(request.Id);

            if (productCatelogue == null)
                return NotFound();

            productCatelogue.ProductId = request.ProductId;
            productCatelogue.CatelogueId = request.CatalogueId;

            var _data = await this._unitOfWork.productCatalogueRepo.UpdateEntity(productCatelogue);
            await this._unitOfWork.CompleteAsync();

            return await this._unitOfWork.productCatalogueRepo.GetAllAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductCatalogues>>> Get()
        {
            return await this._unitOfWork.productCatalogueRepo.GetAllAsync();
        }
    }
}
