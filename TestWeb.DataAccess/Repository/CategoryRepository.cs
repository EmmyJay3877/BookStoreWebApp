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
	public class CategoryRepository(AppDbContext context) : Repository<Category>(context), ICategoryRepository
	{
		private readonly AppDbContext _context = context;

		public void Update(Category category)
		{
			_context.Categories.Update(category);
		}
	}
}
