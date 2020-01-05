using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DB.Repository;
using WebAPI.Logging.NLogLogging;
using WebAPI.Model;

namespace WebAPIUsingEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IProductRepository iProductRepository;
        private ILog nlogLogger;
        public InventoryController(IProductRepository productRepo, ILog logger)
        {
            iProductRepository = productRepo;
            nlogLogger = logger;
        }

        [HttpGet]
        [Route("GetTest")]
        public async Task<IEnumerable<Product>> GetTestResult()
        {
            nlogLogger.Information("Hiiting API");
            return await iProductRepository.GetProductsSync();
        }
    }
}