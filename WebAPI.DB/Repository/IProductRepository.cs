using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.DB.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsSync();
    }
}
