using GeneralCommittee.Domain.Dtos;
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
    public class ArticleRepository (GeneralCommitteeDbContext dbContext ,
         ILogger<ArticleRepository> logger) : IArticleRepositor
    {
        public async Task<int> CreateAsync(Article Article)
        {
            try
            {
                await dbContext.Articles.AddAsync(Article);
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
                            throw new ResourceNotFound(nameof(Author), Article.AuthorId.ToString());
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

            return Article.ArticleId;
        }

        public async Task<string> DeleteArticlAsync(Article article)
        {
            var Process = dbContext.Database.BeginTransaction();

            try
            {
                dbContext.Articles.Remove(article);
                dbContext.Database.CommitTransaction(); ;
                return "Success Process!";
            }
            catch
            {

                dbContext.Database.RollbackTransaction();
                return "failed Process!";
            }
        }

        public async Task<(int, IEnumerable<Article>)> GetAllAsync(string? search, int requestPageNumber, int requestPageSize)
        {

            search ??= string.Empty;
            search = search.ToLower();
            var baseQuery = dbContext.Articles
                .Where(r => r.Title.ToLower().Contains(search));
            var totalCount = await baseQuery.CountAsync();
            var articles = await baseQuery
                .Skip(requestPageSize * (requestPageNumber - 1))
                .Take(requestPageSize)
                .ToListAsync();
            return (totalCount, articles);















        }

        public async Task<Article> GetArticleByIdAsync(int Id)
        {


            var article = await dbContext.Articles
                .Include(c => c.Title)
                .FirstOrDefaultAsync(c => c.ArticleId == Id);
            if (article == null)
            {
                throw new ResourceNotFound(nameof(Article), Id.ToString());
            }

            return article;
        }

        public async Task<ArticleDto> GetByIdAsync(int Id)
        {


            var article = await dbContext.Articles
                  .AsNoTracking() //TODO: avoid tracking the entity for performance
                  .Where(a => a.ArticleId == Id)
                  .Select(a => new ArticleDto
                  {
                      ArticleId = a.ArticleId,
                      AuthorId = a.AuthorId,
                      UploadedById = a.UploadedById, //TODO Set the foreign key property
                      Content = a.Content,
                      PhotoUrl = a.PhotoUrl,
                      Title = a.Title,
                      CreatedDate = a.CreatedDate,
                      UploadedBy = a.UploadedBy.FName, // Ensure this property is populated correctly
                      AuthorinDto = a.Author.Name // Assuming Author is a navigation property
                  })
                  .FirstOrDefaultAsync();

            if (article == null)
            {
                throw new ResourceNotFound(nameof(Article), Id.ToString());
            }

            return article;









        }

        public async Task<bool> IsExistByTitle(string title)
        {


            return await dbContext.Articles
             .Where(a => a.Title.Equals(title))
             .FirstOrDefaultAsync() != null;





        }


        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<string> UpdateArticlAsync(Article articles)
        {



            dbContext.Update(articles);
            return "The Article has been Updated Successfully!";










        }

    }
}
