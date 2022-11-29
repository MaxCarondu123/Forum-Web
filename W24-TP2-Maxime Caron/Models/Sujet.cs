using System;
using System.Collections.Generic;

namespace W24_TP2_Maxime_Caron.Models
{
    public partial class Sujet
    {
        public Sujet()
        {
            Messages = new HashSet<Message>();
        }

        public int SujetId { get; set; }
        public int CatId { get; set; }
        public string? Utilisateur { get; set; }
        public string SujetTitre { get; set; } = null!;
        public string SujetTexte { get; set; } = null!;
        public DateTime SujetDate { get; set; }
        public int? SujetVues { get; set; }
        public bool SujetActif { get; set; }
        public string? User { get; set; }

        public virtual Category Cat { get; set; } = null!;
        public virtual AspNetUser? UserNavigation { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
