using GameStore.Data;
using GameStore.Interfaces;
using GameStore.Models;
using GameStore.Models.Pages;

namespace GameStore.Repository
{
    public class CategoryRepository : ICategory
    {
		private ApplicationContext _context;
        
        public CategoryRepository(ApplicationContext context) 
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public PagedList<Category> GetCategories(QueryOptions options)
        {
            return new PagedList<Category>(_context.Categories, options);
        }

        public void UpdateAll(Category[] categories)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
    }
}
