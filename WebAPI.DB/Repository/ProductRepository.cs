using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Core;
using WebAPI.Model;

namespace WebAPI.DB.Repository
{
    public class ProductRepository : IProductRepository
    {
        readonly InventoryContext inventoryContext;
        public ProductRepository(InventoryContext invContext)
        {
            inventoryContext = invContext;
        }
        public async Task<IEnumerable<Product>> GetProductsSync()
        {
            try
            {
                return inventoryContext.Products.ToList();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
