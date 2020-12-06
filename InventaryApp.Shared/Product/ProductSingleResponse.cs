using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared
{
   public class ProductSingleResponse : BaseApiResponse
    { 
        public ProductViewModel Record { get; set; }
    }
}
