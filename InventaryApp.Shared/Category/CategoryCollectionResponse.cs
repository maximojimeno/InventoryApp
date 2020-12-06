using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared.Category
{
    class CategoryCollectionResponse : BaseApiResponse
    {
        public CategoryViewModel[] Records { get; set; }
        public int Count { get; set; }
    }
}
