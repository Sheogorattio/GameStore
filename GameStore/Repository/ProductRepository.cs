using GameStore.Data;
using GameStore.Interfaces;
using GameStore.Models;
using GameStore.Models.Pages;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repository
{
	public class ProductRepository : IProduct
	{
		private ApplicationContext _context;

		public ProductRepository(ApplicationContext context)
		{
			_context = context;
		}

		public void AddProduct(Product product)
		{
			_context.Products.Add(product);
			_context.SaveChanges();
		}

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
			_context.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts()
		{
			return _context.Products.Include(e => e.Category);
		}

        public Product GetProduct(int id)
        {
			return _context.Products.Include(e => e.Category).FirstOrDefault(e => e.Id == id);
        }

        public PagedList<Product> GetProducts(QueryOptions options, int category = 0)
        {
            IQueryable<Product> products = _context.Products.Include(e => e.Category);
            if (category != 0)
            {
                products = products.Where(e => e.CategoryId == category);
            }

            return new PagedList<Product>(products, options);
            //return new PagedList<Product>(_context.Products.Include(e => e.Category), options);
        }

        public void UpdateAll(Product[] products)
        {
			Dictionary<int, Product> data = products.ToDictionary(e => e.Id);
			IEnumerable<Product> baseline = _context.Products.Where(e => data.Keys.Contains(e.Id));
            foreach (Product product in baseline)
			{
                Product requestProduct = data[product.Id];
                product.Name = requestProduct.Name;
                //product.Category = requestProduct.Category;
                product.CategoryId = requestProduct.CategoryId;
                product.RetailPrice = requestProduct.RetailPrice;
                product.PurchasePrice = requestProduct.PurchasePrice;
            }
			
			//_context.Products.UpdateRange(products);
			_context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
			Product product2 = GetProduct(product.Id);
			product2.Name = product.Name;
			//product2.Category = product.Category;
            product2.CategoryId = product.CategoryId;
			product2.RetailPrice = product.RetailPrice;
			product2.PurchasePrice = product.PurchasePrice;

            //_context.Products.Update(product);
			_context.SaveChanges();
        }
    }
}
