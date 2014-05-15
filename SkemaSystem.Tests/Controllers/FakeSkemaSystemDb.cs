using SkemaSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkemaSystem.Tests.Controllers
{
    class FakeSkemaSystemDb : ISkemaSystemDb
    {
        public IQueryable<T> Query<T>() where T : class
        {
            return Sets[typeof(T)] as IQueryable<T>;
        }

        public void Dispose() { }

        public void AddSet<T>(IQueryable<T> objects)
        {
            Sets.Add(typeof(T), objects);
        }

        public Dictionary<Type, object> Sets = new Dictionary<Type, object>();
    }
}
