using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared.Brand
{
    public class BrandSingleResponse : BaseApiResponse
    {
        public BrandViewModel Record { get; set; }
    }
}
