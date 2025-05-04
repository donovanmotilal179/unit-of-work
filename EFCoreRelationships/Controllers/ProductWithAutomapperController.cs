using AutoMapper;
using EFCoreRelationships.Dto;
using EFCoreRelationships.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace EFCoreRelationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWithAutomapperController : Controller
    {
        private readonly DataContext _context;
        public IConfiguration Configuration { get; }
        private readonly IMapper _mapper;

        public ProductWithAutomapperController(DataContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            Configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Raw>>> Get(int userId)
        {           
            //QUERY DB
            var catalogues = (from a in _context.Catalogues
                              where a.Id == userId
                              join b in _context.ProductCatalogues on a.Id equals b.CatelogueId
                              join c in _context.Products on b.ProductId equals c.Id
                              select new Raw
                              {
                                  CatId = a.Id,
                                  CatName = a.Name,
                                  CatType = a.Type,
                                  ProdCatId = b.Id,
                                  ProductId = b.ProductId,
                                  CatelogueId = b.CatelogueId,
                                  Product_Id = c.Id,
                                  ProductName = c.Name,
                                  ProdDescription = c.Description
                              }).ToListAsync();


            return await catalogues;
        }

        //INSERT INTO DB
        [HttpPost]
        public async Task<List<Products>> Create(ProductDto request)
       {
            var newProduct = _mapper.Map<Products>(request);
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            var res = await _context.Products.ToListAsync();
            return res;
        }

        [HttpPost("CreateCatalogue")]
        public async Task<ActionResult<List<Catalogues>>> Create(CatalogueDto request)
        {
            var newCatalogue = _mapper.Map<Catalogues>(request);
            _context.Catalogues.Add(newCatalogue);
            await _context.SaveChangesAsync();
            var res = await _context.Catalogues.ToListAsync();
            return res;
        }

        [HttpPost("CreateProductCatalogue")]
        public async Task<ActionResult<List<ProductCatalogues>>> Create(ProductCatalogueDto request)
        {
            var newProductCat = _mapper.Map<ProductCatalogues>(request);
            _context.ProductCatalogues.Add(newProductCat);
            await _context.SaveChangesAsync();
            var res = await _context.ProductCatalogues.ToListAsync();
            return res;
        }

        [HttpPost("CreateArmour")]
        public async Task<ActionResult<List<Armour>>> Create(ArmourDto request)
        {
            var newArmour = _mapper.Map<Armour>(request);
            _context.Armours.Add(newArmour);
            await _context.SaveChangesAsync();
            var res = await _context.Armours.ToListAsync();
            return res;
        }

        //UPDATE DB
        [HttpPut]
        public async Task<ActionResult<List<Products>>> Edit(ProductDto request)
        {
            var newProduct = _mapper.Map<Products>(request);
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            var res = await _context.Products.ToListAsync();
            return res;
        }

        [HttpPut("UpdateCatelogues")]
        public async Task<ActionResult<List<Catalogues>>> Edit(CatalogueDto request)
        {
            var catalogue = await _context.Catalogues.FindAsync(request.Id);
            _mapper.Map(request, catalogue);
            await _context.SaveChangesAsync();
            var res = await _context.Catalogues.ToListAsync();
            return res;
        }

        [HttpPut("UpdateProductCatelogues")]
        public async Task<ActionResult<List<ProductCatalogues>>> Edit(ProductCatalogueDto request)
        {
            var prodcat = await _context.ProductCatalogues.FindAsync(request.Id);
            _mapper.Map(request, prodcat);           
            await _context.SaveChangesAsync();
            var res = await _context.ProductCatalogues.ToListAsync();
            return res;
        }

        [HttpPut("UpdateArmour")]
        public async Task<ActionResult<List<Armour>>> Edit(ArmourDto request)
        {
            var armour = await _context.Armours.FindAsync(request.Id);
            _mapper.Map(request, armour);
            await _context.SaveChangesAsync();
            var res = await _context.Armours.ToListAsync();
            return res;
        }

        [HttpDelete("DeleteArmour")]
        public async Task<ActionResult<List<Armour>>> Delete(ArmourDto request)
        {
            var armour = await _context.Armours.FindAsync(request.Id);

            if (armour == null)
                return NotFound();

            _context.Armours.Remove(armour);
            await _context.SaveChangesAsync();
            var res = await _context.Armours.ToListAsync();
            return res;
        }

       
        [HttpGet("SP")]
        public async Task<ActionResult<List<spGetProductCatalogues>>?> GetAllProdCatSP(int userId)
        {
            try
            {
               List<spGetProductCatalogues> ProdCatalogues = new List<spGetProductCatalogues>();

               using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    String sql = "spGetProductCatalogues " + userId;                    

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                spGetProductCatalogues getProdCat = new spGetProductCatalogues();
                                getProdCat.ProductId = reader.GetInt32(0);
                                getProdCat.ProductName = reader.GetString(1);
                                getProdCat.ProductDescription = reader.GetString(2);
                                getProdCat.CatalogueId = reader.GetInt32(3);
                                getProdCat.CatalogueName = reader.GetString(4);
                                getProdCat.CatalogueType = reader.GetString(5);
                                getProdCat.UserId = reader.GetInt32(6);
                                getProdCat.UserName = reader.GetString(7);

                                ProdCatalogues.Add(getProdCat);
                            }

                        }
                    }
                }
                
                return await Task.FromResult(ProdCatalogues.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.ToString());
            }
            return null;
        }


        [HttpPost("InsertIntoProductCatelogues")]
        public async Task<ActionResult<List<spGetProductCatalogues>>?> Create(SPProductCatalogueDto request, int userId)
        {
            //save the client to the database
            try
            {
                List<spGetProductCatalogues> ProdCatalogues = new List<spGetProductCatalogues>();

                String connectionstring = "Data Source=DONPC\\MSSQLSERVER01;Initial Catalog=mystore;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    String sql = " spCreateProductCatalogues @Param1, @Param2 ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Param1", request.ProductId);
                        command.Parameters.AddWithValue("@Param2", request.CatalogueId);
                        command.ExecuteNonQuery();
                    }

                    sql = "spGetProductCatalogues @Param1";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Param1", userId);
                        command.ExecuteNonQuery();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                spGetProductCatalogues getProdCat = new spGetProductCatalogues();
                                getProdCat.ProductId = reader.GetInt32(0);
                                getProdCat.ProductName = reader.GetString(1);
                                getProdCat.ProductDescription = reader.GetString(2);
                                getProdCat.CatalogueId = reader.GetInt32(3);
                                getProdCat.CatalogueName = reader.GetString(4);
                                getProdCat.CatalogueType = reader.GetString(5);
                                getProdCat.UserId = reader.GetInt32(6);
                                getProdCat.UserName = reader.GetString(7);

                                ProdCatalogues.Add(getProdCat);
                            }

                        }                        

                    }
                }
                return await Task.FromResult(ProdCatalogues.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.ToString());
            }
            return null;
        }
       
    }
}