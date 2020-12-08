using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared.OpenInventary
{
    public class OpenInventaryCollectionResponse : BaseApiResponse
    {
        public OpenInventaryViewModel[] Records { get; set; }
        public int Count { get; set; }
    }
}
