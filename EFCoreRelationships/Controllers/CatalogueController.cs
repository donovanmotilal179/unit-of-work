using EFCoreRelationships.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationships.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogueController : Controller
    {
         private readonly IUnitOfWork _unitOfWork;
        public IConfiguration _configuration { get; }

        public CatalogueController(IConfiguration configuration, IUnitOfWork iUnitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = iUnitOfWork;
        }

        [HttpPost("CreateCatalogue")]
        public async Task<ActionResult<List<Catalogues>>> Create(CatalogueDto request)
        {
            var user = await this._unitOfWork.catalogueRepo.GetAllAsync();
            if (user == null)
                return NotFound();


            var newCatalogue = new Catalogues
            {
                Name = request.Name,
                Type = request.Type,
                UserId = request.Id
            };

            var _data = await this._unitOfWork.catalogueRepo.AddEntity(newCatalogue);
            await this._unitOfWork.CompleteAsync();
            return await this._unitOfWork.catalogueRepo.GetAllAsync();

        }

        [HttpPut("UpdateCatelogues")]
        public async Task<ActionResult<List<Catalogues>>> Edit(CatalogueDto request)
        {
            var user = await this._unitOfWork.catalogueRepo.GetAllAsync();
            if (user == null)
                return NotFound();


            var newCatalogue = new Catalogues
            {
                Name = request.Name,
                Type = request.Type,
                UserId = request.Id
            };

            var _data = await this._unitOfWork.catalogueRepo.UpdateEntity(newCatalogue);
            await this._unitOfWork.CompleteAsync();
            return await this._unitOfWork.catalogueRepo.GetAllAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<Catalogues>>> Get()
        {
            return await this._unitOfWork.catalogueRepo.GetAllAsync();
        }
    }
}
