using Domain.entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackEndApiEF.data;
using System.Linq;

namespace BackEndApiEF.repo
{
    public class GenericRepo<T> : IGenericDataStore<T> where T : class
    {
        protected readonly ProjectDbContext  _context;
        public GenericRepo(ProjectDbContext context)
        {
            _context = context;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
     
    }
}

