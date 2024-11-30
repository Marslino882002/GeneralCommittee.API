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
    public class VideoStreamService(
    GeneralCommitteeDbContext dbContext,
    ILogger<VideoStreamService> logger
) : IVideoStreamService
    {
        public async Task<bool> ConfirmUpload(string videoId, bool Confirmed)
        {

 if (string.IsNullOrEmpty(videoId))
            {
                throw new ArgumentException("Video ID cannot be null or empty.", nameof(videoId));
            }

            try
            {
                var videoUpload = await dbContext.VideoUploads
                    .FirstOrDefaultAsync(v => v.PendingVideoUploadId == videoId);

                if (videoUpload == null)
                {
                    throw new ResourceNotFound("Pending Video Upload", videoId);
                }

                await dbContext.SaveChangesAsync();
                return true;
               
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while confirming the video upload.");
                throw;
            }












        }

        public async Task<string> CreateAsync(PendingVideoUpload videoUpload)
        {

            if (videoUpload == null)
            {
                throw new ArgumentNullException(nameof(videoUpload));
            }


            try
            {
                await dbContext.VideoUploads.AddAsync(videoUpload);
                 await dbContext.SaveChangesAsync();
                return videoUpload.PendingVideoUploadId; // Assuming this property exists
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












        }
    }

}
