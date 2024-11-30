using GeneralCommittee.Application.Articles.Commands.AddCArticle;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Articles.Commands.AddArticle
{
    public class AddArticleCommand : IRequest<AddArticleCommandResponse>
    {
        // Only include attributes necessary for creating an Article
        public int AuthorId { get; set; }
        public int UploadedById { get; set; } // Foreign Key property

        [Required]
        public string Content { get; set; } = default!;
         [Required]
        public string Title { get; set; } = default!;

        public IFormFile? photo { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
