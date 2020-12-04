using System;
using System.Collections.Generic;
using System.Text;

namespace InventaryApp.Shared
{
  public  class BaseApiResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
