using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared.Bussiness
{
    public class BussinessCollectionResponse : BaseApiResponse
    {
        public BussinessViewModel[] Records { get; set; }
        public int Count { get; set; }
    }
}
