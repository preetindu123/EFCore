using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity:class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);     
        TEntity FindById(object Id);
        IEnumerable<TEntity> GetAll();
        void DeleteById(object Id);
    }
}
