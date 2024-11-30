using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Repositories
{
    public interface IDbContextRepository<T> where T : class
    {

        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsyncGetAllAsync(string? searchName, int requestPageNumber,
            int requestPageSize);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(object id);







    }
}
