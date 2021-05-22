using DAL;
using Repository.Interfaces;

namespace Repository.UOW
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepo { get; }
        IRepository<Category> CategoryRepo { get; }
        void SaveChanges();
    }
}
