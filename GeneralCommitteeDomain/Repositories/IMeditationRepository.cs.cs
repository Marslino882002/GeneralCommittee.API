using GeneralCommittee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Repositories
{
    public interface IMeditationRepository
    {
        public Task<int> AddMeditationAsync(Meditation New_Meditation);//Create
        public Task<string> DeleteMeditationAsync(Meditation Removed_Meditation);//Delete
        public Task<string> UpdateMeditationAsync(Meditation meditation);//Update
        public Task<(int, IEnumerable<Meditation>)> GetAllMeditationsAsync(string? searchName, int pageNumber, int pageSize, string? sortBy);//GetAll
        public Task<Meditation> GetMeditationsById(int MeditationId);//GetByID
        public Task<bool> IsExistByTitle(string title);
        public Task SaveChangesAsync();


    }
}
