using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.models
{
    public class CategorieRead : CategorieCreate
    {
        public string id { get; set; }
        public Produit[] produits { get; set; }
    }
    public class CategorieCreate
    {
        public string nom { get; set; }
        public string photo { get; set; }
    }

    public class CategorieUpdate : CategorieCreate
    {
        public string id { get; set; }
    }
}
