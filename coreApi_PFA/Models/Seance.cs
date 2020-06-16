using System;
using System.Collections.Generic;

namespace coreApi_PFA.Models
{
    public partial class Seance
    {
        public Seance()
        {
            Absence = new HashSet<Absence>();
        }

        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Sujet { get; set; }
        public int? IdFiliere { get; set; }
        public int? IdMatiere { get; set; }
        public int? Duree { get; set; }

        public virtual Affectation IdNavigation { get; set; }
        public virtual ICollection<Absence> Absence { get; set; }
    }
}
