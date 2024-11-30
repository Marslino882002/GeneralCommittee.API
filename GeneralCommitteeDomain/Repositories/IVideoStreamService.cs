using GeneralCommittee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Repositories
{
    public interface IVideoStreamService
    {
        public Task<string> CreateAsync(PendingVideoUpload course);
        public Task<bool> ConfirmUpload(string videoId, bool Confirmed);

    }
}
