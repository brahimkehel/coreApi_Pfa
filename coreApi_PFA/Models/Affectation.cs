using System;
using System.Collections.Generic;

namespace coreApi_PFA.Models
{
    public partial class Affectation
    {
        public Affectation()
        {
            Seance = new HashSet<Seance>();
        }

        public int IdFiliere { get; set; }
        public int IdMatiere { get; set; }
        public int? IdEns { get; set; }

        public virtual Enseignant IdEnsNavigation { get; set; }
        public virtual Filiere IdFiliereNavigation { get; set; }
        public virtual Matiere IdMatiereNavigation { get; set; }
        public virtual ICollection<Seance> Seance { get; set; }
    }
}
