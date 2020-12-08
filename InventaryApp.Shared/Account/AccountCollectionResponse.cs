using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared.Account
{
    public class AccountCollectionResponse : BaseApiResponse
    {
        public AccountViewModel[] Records { get; set; }
        public int Count { get; set; }
    }
}
