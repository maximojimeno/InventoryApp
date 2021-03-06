﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared.Category
{
    public class CategoryCollectionPagingResponse : BaseApiResponse
    {
        public CategoryViewModel[] Records { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? NextPage { get; set; }
        public int Count { get; set; }
    }
}
