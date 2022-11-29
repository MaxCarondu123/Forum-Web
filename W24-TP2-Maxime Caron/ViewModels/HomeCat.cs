using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using W24_TP2_Maxime_Caron.Models;

namespace W24_TP2_Maxime_Caron.ViewModels
{
    public class HomeCat
    {
        [Key, Column(Order = 0)]
        public int CatId { get; set; }
        public string? CatNom { get; set; }
        public string? CatDescription { get; set; }
        public bool CatActif { get; set; }
        public IEnumerable<Sujet> TopSujet { get; set; }
    }
}