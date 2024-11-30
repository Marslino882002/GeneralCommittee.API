﻿using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Repositories
{
    public interface IArticleRepository
    {
        public Task<int> CreateAsync(Article Article);
        public Task<Article> GetByTitleAsync(int? PodcastId ,string Article_title);
        public Task<Article> GetArticleByIdAsync(int Id);
        public Task<bool> IsExistByTitle(string title);
        public Task<(int, IEnumerable<Article>)> GetAllArticlesAsync(string? search, int requestPageNumber, int requestPageSize, string? sortBy);

        public Task SaveChangesAsync();
        public Task<string> DeleteArticleAsync(Article article);
        public Task<string> UpdateArticleAsync(Article articles);
    }
}