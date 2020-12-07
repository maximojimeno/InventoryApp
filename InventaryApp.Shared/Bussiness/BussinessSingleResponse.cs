using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared.Bussiness
{
    public class BussinessSingleResponse : BaseApiResponse
    {
        public BussinessViewModel Record { get; set; }
    }
}
