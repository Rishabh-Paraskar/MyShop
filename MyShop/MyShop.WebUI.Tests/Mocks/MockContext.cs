using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.WebUI.Tests.Mocks
{
   public class MockContext<T> : IRepository<T> where T : BaseEntity
    {
        List<T> items;
        string className;
        public MockContext()
        {
            items = new List<T>();
        }

        public void commit()
        {
            return;
        }
        public void insert(T t)
        {
            items.Add(t);
        }

        public void update(T t)
        {

            T tToUpdate = items.Find(i => i.Id == t.Id);
            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + " Not Found");
            }
        }
        public T find(string Id)
        {

            T toFind = items.Find(i => i.Id == Id);
            if (toFind != null)
            {
                return toFind;
            }
            else
            {
                throw new Exception(className + " Not Found");
            }
        }
        public void delete(string Id)
        {
            T tToUpdate = items.Find(i => i.Id == Id);
            if (tToUpdate != null)
            {
                items.Remove(tToUpdate);
            }
            else
            {
                throw new Exception(className + " Not Found");
            }
        }

        public IQueryable<T> collection()
        {
            return items.AsQueryable();
        }
    }
}
