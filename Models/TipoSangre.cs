using System;
using System.Collections.Generic;

namespace MiPrimeraAppNetCore.Models
{
    public partial class TipoSangre
    {
        public TipoSangre()
        {
            Paciente = new HashSet<Paciente>();
        }

        public int Iidtiposangre { get; set; }
        public string Nombre { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual ICollection<Paciente> Paciente { get; set; }
    }
}
