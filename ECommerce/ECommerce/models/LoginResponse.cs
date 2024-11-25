using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.models
{
    public class LoginResponse
    {
        public string token { get; set; }
        public UserRead user { get; set; }
    }
}
