using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Web.Models
{
    public class LocalUserInfo
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }

    }
}
