using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared.OpenInventary
{
    public class OpenInventarySingleResponse : BaseApiResponse
    {
        public OpenInventaryViewModel Record { get; set; }
    }
}
