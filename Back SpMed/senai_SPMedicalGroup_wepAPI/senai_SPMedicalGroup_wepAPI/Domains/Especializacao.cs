using System;
using System.Collections.Generic;

#nullable disable

namespace senai_SPMedicalGroup_wepAPI.Domains
{
    public partial class Especializacao
    {
        public Especializacao()
        {
            Medicos = new HashSet<Medico>();
        }

        public byte IdEspecializacao { get; set; }
        public string NomeEspecializacao { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
