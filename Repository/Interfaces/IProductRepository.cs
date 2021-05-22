using DAL;
using DomainModels;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<ProductModel> GetAllProducts();
    }
}
