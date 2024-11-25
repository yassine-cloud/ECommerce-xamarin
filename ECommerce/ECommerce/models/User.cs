using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.models
{
    public class UserRead : UserCreate
    {
        public string id { get; set; }
        public PanierRead[] paniers { get; set; }
      }
    public class UserCreate
    {
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Role role { get; set; }
        public string contact { get; set; }
    }

    public class UserUpdate : UserCreate
    {
        public string id { get; set; }
    }

    public enum Role
    {
        ADMIN,
        USER
    }

}
