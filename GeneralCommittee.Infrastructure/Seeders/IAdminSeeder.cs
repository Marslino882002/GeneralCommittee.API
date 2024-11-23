using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Infrastructure.Seeders
{
    public interface IAdminSeeder
    {
        public Task seed();
    }
}
