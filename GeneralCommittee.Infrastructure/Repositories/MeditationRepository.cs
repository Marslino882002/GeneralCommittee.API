using GeneralCommittee.Domain.Entities;
using GeneralCommittee.Domain.Exceptions;
using GeneralCommittee.Domain.Repositories;
using GeneralCommittee.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Infrastructure.Repositories
{
    public class MeditationRepository(GeneralCommitteeDbContext dbContext,
         ILogger<ArticleRepository> logger) : IMeditation
    {
        public async Task<int> AddMeditationsync(Meditation meditation)
        {


            try
            {
                await dbContext.Meditations.AddAsync(meditation);
                await dbContext.SaveChangesAsync();
            }

            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException)
                {
                    // Check for specific foreign key constraint violations
                    foreach (SqlError error in sqlException.Errors)
                    {
                        if (error.Number == 547) // SQL Server foreign key violation error number
                        {
                            logger.LogError(ex, "Foreign key violation: {Message}", error.Message);
                            throw new ResourceNotFound(nameof(Author), meditation.MeditationId.ToString());
                        }
                    }
                }
                logger.LogError(ex, "An error occurred while saving the article.");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while saving the article.");
                throw;
            }

            return meditation.MeditationId;











        }

        public async Task<string> DeleteMeditationAsync(Meditation meditation)
        {

            var Process = dbContext.Database.BeginTransaction();

            try
            {
                dbContext.Meditations.Remove(meditation);
                dbContext.Database.CommitTransaction(); 
                return "Success Process!";
            }
            catch
            {

                dbContext.Database.RollbackTransaction();
                return "failed Process!";
            }




        }

        public async Task<(int, IEnumerable<Meditation>)> GetAllAsync(string? searchName, int pageNumber, int pageSize)
        {

            searchName ??= string.Empty;
            searchName = searchName.ToLower();
            var baseQuery = dbContext.Meditations
                .Where(r => r.Title.ToLower().Contains(searchName));
            var totalCount = await baseQuery.CountAsync();
            var Meditation = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return (totalCount, Meditation);







        }


        public async Task<Meditation> GetById(int MeditationId)
        {

            var Meditation = await dbContext.Meditations
                  .Include(c => c.Title)
                  .FirstOrDefaultAsync(c => c.MeditationId == MeditationId);
            if (Meditation == null)
            {
                throw new ResourceNotFound(nameof(Meditation), MeditationId.ToString());
            }

            return  Meditation;









        }

        public async Task<bool> IsExist(int MeditationId)
        {

            return await dbContext.Meditations
          .Where(a => a.MeditationId.Equals(MeditationId))
          .FirstOrDefaultAsync() != null;










        }

        public async Task<bool> IsExistByContent(string content)
        {

            return await dbContext.Meditations
           .Where(a => a.Content.Equals(content))
           .FirstOrDefaultAsync() != null;

        }

        public async Task<bool> IsExistByTitle(string title)
        { return await dbContext.Meditations
           .Where(a => a.Title.Equals(title))
           .FirstOrDefaultAsync() != null; }

        public async Task<bool> IsExistDuringUpdate(string Content, int Id)
        {

            var medition = await dbContext.Meditations
           .Where(a => a.Content.Equals(Content))
           .Where(a => a.MeditationId.Equals(Id))
           .FirstOrDefaultAsync() != null;


            if (medition != null)
            {
                throw new Exception("Another Medition has Same Data");

                return false;

            }
           
            
            return true;    
            
            

        }

        public async Task<string> UpdateMeditationAsync(Meditation meditation)
        { dbContext.Update(meditation);
            return "The Article has been Updated Successfully!"; }
    }



}



   