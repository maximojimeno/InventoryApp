using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared
{
    public class ProductCollectionPagingResponse : BaseApiResponse
    {
        public ProductViewModel[] Records { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? NextPage { get; set; }
        public int Count { get; set; }
    }
}
