﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Common
{
    public class PageResult<T>
    {

        public IEnumerable<T> Items { get; set; }
        public int TotalItemsCount { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }

        public PageResult(IEnumerable<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemsCount = totalCount;
            TotalPages = ((totalCount + pageSize - 1) / pageSize);
            ItemsFrom = Math.Min(totalCount, pageSize * (pageNumber - 1) + 1);
            ItemsTo = Math.Min(totalCount, ItemsFrom + pageSize - 1);
        }


    }
}
