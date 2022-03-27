
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackEndApiEF.repo
{
    public interface IGenericDataStore<T> where T : class
    {
       
       
      
        void AddRange(IEnumerable<T> entities);
       
      



    }
}
