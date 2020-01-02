using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DB.Repository;
using WebAPI.Model;

namespace WebAPIUsingEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IProductRepository iProductRepository;

        public InventoryController(IProductRepository productRepo)
        {
            iProductRepository = productRepo;
        }

        [HttpGet]
        [Route("GetTest")]
        public async Task<IEnumerable<Product>> GetTestResult()
        {
             return await iProductRepository.GetProductsSync();
        }
    }
}