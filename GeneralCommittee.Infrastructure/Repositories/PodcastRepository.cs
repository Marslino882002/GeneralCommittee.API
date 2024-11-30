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
    public class PodcastRepository(
         GeneralCommitteeDbContext dbContext,
    ILogger<PodcastRepository> logger


        ) : IPodcastRepository
    {
        public async Task<int> AddPodcastAsync(Podcast podcast)
        {

            if (podcast.Title == null)
            {
                throw new ArgumentNullException(nameof(podcast));
            }


            try
            {
                await dbContext.Podcasts.AddAsync(podcast);
                await dbContext.SaveChangesAsync();
            }

            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException)
                {
                    // Handle specific SQL errors if needed
                    logger.LogError(ex, "An error occurred while saving the pending video upload.");
                }
                logger.LogError(ex, "An error occurred while saving the pending video upload.");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while saving the pending video upload.");
                throw;
            }



            return podcast.PodcastId;



        }

        public async Task DeletePodcastAsync(int podcast_Id )
        {

            var Selected_podcast = await dbContext.Podcasts
                .FirstOrDefaultAsync(c => c.PodcastId == podcast_Id);

            if (Selected_podcast == null)
            {
                throw new ResourceNotFound("Video", podcast_Id.ToString());
            }
            dbContext.Podcasts.Remove(Selected_podcast);    
            dbContext.SaveChanges();

            logger.LogInformation($"Podcast with ID {podcast_Id} has been deleted successfully.");







        }

       

        public async Task<(int, IEnumerable<Podcast>)> GetAllAsync(string? searchName, int requestPageNumber, int requestPageSize)
        {
            searchName ??= string.Empty;
            searchName = searchName.ToLower();
            var baseQuery = dbContext.Podcasts
                .Where(r => r.Title.ToLower().Contains(searchName));
            var totalCount = await baseQuery.CountAsync();
            var courses = await baseQuery
                .Skip(requestPageSize * (requestPageNumber - 1))
                .Take(requestPageSize)
                .ToListAsync();
            return (totalCount, courses);
        }

        public async Task<Podcast> GetByIdOrTitle(int? PodcastId , string Title_Podcast)
        {

            var Podcast = dbContext.Podcasts
                .Include(r => r.Title.ToLower().Contains(Title_Podcast))
                .FirstOrDefault(P => P.PodcastId == PodcastId);
                
            
            if (Podcast == null)
            {
                throw new ResourceNotFound(nameof(Podcast), PodcastId.ToString());
            }

            return Podcast;   }
    }
}
