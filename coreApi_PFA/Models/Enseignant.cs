using System;
using System.Collections.Generic;

namespace coreApi_PFA.Models
{
    public partial class Enseignant
    {
        public Enseignant()
        {
            Affectation = new HashSet<Affectation>();
        }

        public int Id { get; set; }
        public string Cin { get; set; }
        public DateTime? DateNais { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Genre { get; set; }
        public string Email { get; set; }
        public string MotdePasse { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public DateTime? DateEmb { get; set; }
        public string Cnss { get; set; }
        public double? Salaire { get; set; }

        public virtual ICollection<Affectation> Affectation { get; set; }
    }
}
