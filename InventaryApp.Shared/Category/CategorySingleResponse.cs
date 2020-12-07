using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared.Category
{
    public class CategorySingleResponse : BaseApiResponse
    {
        public CategoryViewModel Record { get; set; }
    }
}
