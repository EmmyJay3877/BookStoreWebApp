using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeb.DataAccess.Data;
using TestWeb.DataAccess.Repository.IRepository;
using TestWeb.Models;

namespace TestWeb.DataAccess.Repository
{
    public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository
    {
        private readonly AppDbContext _context = context;

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
