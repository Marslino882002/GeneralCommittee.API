using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.BunnyServices.PodCast.Token
{
    internal static class TimeExtensions
    {
        internal static string ToUnixTimestamp(this DateTimeOffset time)
                   => time.ToUnixTimeSeconds().ToString();
    }
}
