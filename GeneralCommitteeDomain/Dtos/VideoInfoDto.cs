﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Dtos
{
    public class VideoInfoDto
    {
        public string VideoId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Views { get; set; } = default!;
        public string StorageSize { get; set; } = default!;
        public string Duration { get; set; } = default!;
        public string Resolutions { get; set; } = default!;
        public string TotalWatchTime { get; set; } = default!;
        public string AverageWatchTime { get; set; } = default!;





    }
}
