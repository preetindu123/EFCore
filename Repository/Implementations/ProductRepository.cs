using DAL;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        //DbContext _dbContext;
        DatabaseContext _dbContext;
        public ProductRepository(DbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext as DatabaseContext;
        }
        public IEnumerable<ProductModel> GetAllProducts()
        {
          return (from prd in _dbContext.Products
                        join cat in _dbContext.Categories
                        on prd.CategoryId equals cat.CategoryId
                        select new ProductModel
                        {
                            ProductId = prd.Id,
                            ProductName = prd.Name,
                            UnitPrice = prd.UnitPrice,
                            Description = prd.Description,
                            CategoryId = cat.CategoryId,
                            CategoryName = cat.Name,
                        }).ToList();
        }
    }
}
