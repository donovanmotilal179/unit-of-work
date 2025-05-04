using EFCoreRelationships.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreRelationships.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArmourController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public IConfiguration _configuration { get; }

        public ArmourController(IConfiguration configuration, IUnitOfWork iUnitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = iUnitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Armour>>> Get()
        {
            return Ok(await this._unitOfWork.armourRepo.GetAllAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Armour>> Get(int id)
        {
            return Ok(await this._unitOfWork.armourRepo.GetAsync(id));
        }

        [HttpPut("UpdateArmour")]
        public async Task<ActionResult<List<Armour>>> Edit(ArmourDto request)
        {
            var armour = await this._unitOfWork.armourRepo.GetAsync(request.Id);

            if (armour == null)
                return NotFound();

            var _data = await this._unitOfWork.armourRepo.UpdateEntity(armour);
            await this._unitOfWork.CompleteAsync();
            return Ok(_data);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Armour>>> Delete(int id)
        {
            var armour = await this._unitOfWork.armourRepo.DeleteEntity(id);

            if (armour == null)
                return NotFound();

            await this._unitOfWork.CompleteAsync();
            return Ok(armour);
        }

        [HttpPost("CreateArmour")]
        public async Task<ActionResult<List<Armour>>> Create(ArmourDto request)
        {
            var catalogue = await this._unitOfWork.armourRepo.GetAllAsync();
            var res = catalogue.Find(cat => cat.CatalogueId == request.CatalogueId);

            if (res == null)
                return NotFound();


            var newArmour = new Armour
            {
                Name = request.Name,
                Damage = request.Damage,
                CatalogueId = res.Id
            };

            var data = await this._unitOfWork.armourRepo.AddEntity(newArmour);
            await this._unitOfWork.CompleteAsync();
            //return Ok(data);

            return Ok(await this._unitOfWork.armourRepo.GetAllAsync());
        }
    }
}
