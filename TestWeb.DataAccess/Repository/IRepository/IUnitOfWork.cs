using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICategoryRepository CategoryContext { get; }

		IProductRepository ProductContext { get; }

		void Save();
	}
}
