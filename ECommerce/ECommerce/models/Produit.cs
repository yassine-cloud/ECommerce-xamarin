using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.models
{
    public class Produit
    {
        public string id { get; set; }
        public string designation { get; set; }
        public double prix { get; set; }
        public string photo { get; set; }
        public string categorie { get; set; }

    }

    public class ProduitCreate
    {
        public string designation { get; set; }
        public double prix { get; set; }
        public string photo { get; set; }
    }

    public class ProduitUpdate 
    {
        public string id { get; set; }
        public string designation { get; set; }
        public double prix { get; set; }
        public string photo { get; set; }
    }

}
