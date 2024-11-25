using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.models
{
    public class PanierRead
    {
        public string id { get; set; }
        public Produit produit { get; set; }
        public string user { get; set; }
        public int quantite { get; set; }
    }
    public class PanierCreate
    {
        public string produitId { get; set; }
        public string userId { get; set; }
        public int quantite { get; set; }
    }
    public class PanierUpdate : PanierCreate
    {
        public string id { get; set; }
    }
}
