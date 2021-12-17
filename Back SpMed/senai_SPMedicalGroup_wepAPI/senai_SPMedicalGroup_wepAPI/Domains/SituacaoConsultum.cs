using System;
using System.Collections.Generic;

#nullable disable

namespace senai_SPMedicalGroup_wepAPI.Domains
{
    public partial class SituacaoConsultum
    {
        public SituacaoConsultum()
        {
            Consulta = new HashSet<Consultum>();
        }

        public byte IdSituacaoConsulta { get; set; }
        public string SituacaoConsulta { get; set; }

        public virtual ICollection<Consultum> Consulta { get; set; }
        public string Descricao { get; internal set; }
    }
}
