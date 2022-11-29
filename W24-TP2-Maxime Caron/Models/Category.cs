using System;
using System.Collections.Generic;

namespace W24_TP2_Maxime_Caron.Models
{
    public partial class Category
    {
        public Category()
        {
            Sujets = new HashSet<Sujet>();
        }

        public int CatId { get; set; }
        public string CatNom { get; set; } = null!;
        public string CatDescription { get; set; } = null!;
        public string? CatImg { get; set; }
        public bool CatActif { get; set; }

        public virtual ICollection<Sujet> Sujets { get; set; }
    }
}
