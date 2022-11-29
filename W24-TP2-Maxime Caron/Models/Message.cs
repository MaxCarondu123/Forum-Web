using System;
using System.Collections.Generic;

namespace W24_TP2_Maxime_Caron.Models
{
    public partial class Message
    {
        public int MsgId { get; set; }
        public int SujetId { get; set; }
        public string? Utilisateur { get; set; }
        public string MsgText { get; set; } = null!;
        public DateTime MsgDate { get; set; }
        public bool MsgActif { get; set; }
        public string? User { get; set; }

        public virtual Sujet Sujet { get; set; } = null!;
        public virtual AspNetUser? UserNavigation { get; set; }
    }
}
