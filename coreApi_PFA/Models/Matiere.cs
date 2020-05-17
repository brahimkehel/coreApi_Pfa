using System;
using System.Collections.Generic;

namespace coreApi_PFA.Models
{
    public partial class Matiere
    {
        public Matiere()
        {
            Affectation = new HashSet<Affectation>();
        }

        public int Id { get; set; }
        public string Libelle { get; set; }

        public virtual ICollection<Affectation> Affectation { get; set; }
    }
}
