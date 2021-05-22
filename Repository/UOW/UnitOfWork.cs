using DAL;
using Repository.Implementations;
using Repository.Interfaces;

namespace Repository.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        DatabaseContext _dbContext;
        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IProductRepository _productRepo;
        public IProductRepository ProductRepo
        {
            get
            {
                if (_productRepo == null)
                    _productRepo = new ProductRepository(_dbContext);
                return _productRepo; 
            }
        }

        private IRepository<Category> _categoryRepo;
        public IRepository<Category> CategoryRepo
        {
            get 
            {
                if (_categoryRepo == null)
                    _categoryRepo = new Repository<Category>(_dbContext);
                return _categoryRepo;
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
