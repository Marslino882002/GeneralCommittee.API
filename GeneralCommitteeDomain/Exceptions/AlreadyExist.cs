using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Exceptions
{
    public class AlreadyExist(string msg) : Exception(msg)
    {

    }
}
