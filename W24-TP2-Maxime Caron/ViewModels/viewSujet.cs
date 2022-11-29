using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W24_TP2_Maxime_Caron.ViewModels
{
    public class viewSujet
    {
        [Key, Column(Order = 0)]
        public int SujetId { get; set; }
        public string SujetTitre { get; set; }
        public string SujetTexte { get; set; }
        public int MessageCount { get; set; }
        public int? SujetVues { get; set; }
        public string Utilisateur { get; set; }
        public DateTime SujetDate { get; set; }
        public DateTime? dateRecente { get; set; }
        public string User { get; set; }
    }
}