using System;
using System.Collections.Generic;

namespace coreApi_PFA.Models
{
    public partial class Filiere
    {
        public Filiere()
        {
            Affectation = new HashSet<Affectation>();
            Etudiant = new HashSet<Etudiant>();
        }

        public int Id { get; set; }
        public string Libelle { get; set; }

        public virtual ICollection<Affectation> Affectation { get; set; }
        public virtual ICollection<Etudiant> Etudiant { get; set; }
    }
}
