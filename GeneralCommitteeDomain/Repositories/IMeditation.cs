using GeneralCommittee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Repositories
{
    public interface IMeditation
    {
        public Task<int> AddMeditationsync(Meditation meditation);//Create
        public Task<string> DeleteMeditationAsync(Meditation Meditation);//Delete
        public Task<Meditation> GetById(int MeditationId);//GetByID
        public Task<(int, IEnumerable<Meditation>)> GetAllAsync(string? searchName, int pageNumber, int pageSize);//GetAll
        public Task<string> UpdateMeditationAsync(Meditation meditation);//Update
        public Task<bool> IsExist(int MeditationId);//Check Existance of Entity
        public Task<bool> IsExistDuringUpdate(string Content, int Id);//Check Existance of Entity Before Insert New Data in updating Process

        public Task<bool> IsExistByTitle(string title);

        public Task<bool> IsExistByContent(string content);



    }
}
