using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared.Account
{
    public class AccountSingleResponse : BaseApiResponse
    {
        public AccountViewModel Record { get; set; }
    }
}
