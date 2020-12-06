using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared.Brand
{
    public class BrandCollectionResponse : BaseApiResponse
    {
        public BrandViewModel[] Records { get; set; }
        public int Count { get; set; }
    }
}
