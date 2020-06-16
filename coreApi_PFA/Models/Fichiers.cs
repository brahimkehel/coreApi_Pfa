using System;
using System.Collections.Generic;

namespace coreApi_PFA.Models
{
    public partial class Fichiers
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Status { get; set; }
        public string NomFichier { get; set; }
    }
}
