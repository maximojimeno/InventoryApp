﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared.OpenInventary
{
    public class OpenInventaryCollectionPagingResponse 
    {
        public OpenInventaryViewModel[] Records { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? NextPage { get; set; }
        public int Count { get; set; }
    }
}
