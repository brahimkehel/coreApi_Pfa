using System;
using System.Collections.Generic;

namespace coreApi_PFA.Models
{
    public partial class Utilisateur
    {
        public int IdUti { get; set; }
        public int? Id { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string MotdePasse { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }
}
