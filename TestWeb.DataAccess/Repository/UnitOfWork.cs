using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeb.DataAccess.Data;
using TestWeb.DataAccess.Repository.IRepository;

namespace TestWeb.DataAccess.Repository
{
	public class UnitOfWork(AppDbContext context) : IUnitOfWork
	{
		private readonly AppDbContext _context = context;
		public ICategoryRepository CategoryContext { get; private set; } = new CategoryRepository(context);

		public IProductRepository ProductContext { get; private set; } = new ProductRepository(context);

        public void Save()
		{
			_context.SaveChanges();
		}
	}
}
