using GeneralCommittee.Domain.Repositories;
using GeneralCommittee.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Infrastructure.Repositories
{
    public class SearchServiceRepository(GeneralCommitteeDbContext dbContext

        ) : ISearchServiceRepository<object>
    {
        public async Task<object> SearchAsync(string searchTerm, string materialType, int pageNumber, int pageSize)
        {
            IQueryable<object> query;
            switch (materialType.ToLower())
            { 
                case "videos": query = dbContext
                        .VideoUploads.Where(v => EF.Functions
                        .Like(v.Title, $"%{searchTerm}%"))
                        .AsQueryable<object>(); 
                    break;

                case "podcasts": query = dbContext.Podcasts.
                        Where(p => EF.Functions
                        .Like(p.Title, $"%{searchTerm}%"))
                        .AsQueryable<object>(); 
                    break;

                case "articles": query = dbContext.Articles
                        .Where(a => EF.Functions
                        .Like(a.Title, $"%{searchTerm}%"))
                        .AsQueryable<object>();
                    break;

                case "meditations": query = 
                        dbContext.Meditations
                        .Where(m => EF.Functions
                        .Like(m.Title, $"%{searchTerm}%"))
                        .AsQueryable<object>(); 
                    break; 
                case "courses": query = 
                        dbContext.Courses
                        .Where(c => EF.Functions
                        .Like(c.Name, $"%{searchTerm}%"))
                        .AsQueryable<object>(); 
                    break; 
                default:

                    return (Task<object>)Enumerable.Empty<object>();
            }
            var totalCount = await query.CountAsync();
            var results = await query.Skip((pageNumber - 1) * pageSize)
              .  Take(pageSize).ToListAsync(); return results;





        }
    }
}
