using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using W24_TP2_Maxime_Caron.Models;

namespace W24_TP2_Maxime_Caron.ViewModels
{
    public class sujetView
    {
        [Key, Column(Order = 0)]
        public int SujetId { get; set; }
        public string SujetTitre { get; set; }
        public string SujetTexte { get; set; }
        public int? SujetVues { get; set; }
        public string Utilisateur { get; set; }
        public DateTime SujetDate { get; set; }
        public DateTime? dateRecente { get; set; }
        public string? UserConnect { get; set; }

        public string? User { get; set; }

        public virtual Category Cat { get; set; } = null!;
        public virtual AspNetUser? UserNavigation { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}