using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W24_TP2_Maxime_Caron.ViewModels
{
    public class usager
    {
        [Key, Column(Order = 0)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public int SujetCount { get; set; }
        public int MessageCount { get; set; }
    }
}