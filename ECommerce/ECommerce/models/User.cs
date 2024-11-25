using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerce.models
{
    public class UserRead : UserCreate
    {
        public string id { get; set; }
        public PanierRead[] paniers { get; set; }
        public string FullName => $"{nom} {prenom}";
        public double TotalPanier => paniers?.Sum(p => p.produit.prix * p.quantite) ?? 0;
        public int NumberOfAchats => paniers?.Length ?? 0;
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
