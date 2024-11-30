using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Repositories
{
    public interface ISearchServiceRepository<T> where T : class 
    {public  Task<T> SearchAsync(string searchTerm, string materialType, int pageNumber, int pageSize);  }
    
    }

