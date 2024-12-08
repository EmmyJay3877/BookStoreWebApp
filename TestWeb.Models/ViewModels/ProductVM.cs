using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb.Models.ViewModels
{
	public class ProductVM
	{
		public Product Product { get; set; }
		
		public IEnumerable<Category>? CategoryList { get; set; }
    }
}
