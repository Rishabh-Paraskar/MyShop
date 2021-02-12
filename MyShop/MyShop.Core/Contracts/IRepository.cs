using MyShop.Core.Models;
using System.Linq;

namespace MyShop.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> collection();
        void commit();
        void delete(string Id);
        T find(string Id);
        void insert(T t);
        void update(T t);
    }
}