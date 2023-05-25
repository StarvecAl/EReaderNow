using System;
using System.Collections.Generic;
using System.Text;

namespace ERederNow_android.Models
{
    public class Account
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
