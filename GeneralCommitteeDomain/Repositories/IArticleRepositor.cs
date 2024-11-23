using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Repositories
{
    public interface IArticleRepositor
    {
        public Task<int> CreateAsync(Article Article);
        public Task<ArticleDto> GetByIdAsync(int Id);
        public Task<Article> GetArticleByIdAsync(int Id);
        public Task<(int, IEnumerable<Article>)> GetAllAsync(string? search, int requestPageNumber, int requestPageSize);

        public Task SaveChangesAsync();
        public Task<string> DeleteArticlAsync(Article article);
        public Task<string> UpdateArticlAsync(Article articles);
    }
}
