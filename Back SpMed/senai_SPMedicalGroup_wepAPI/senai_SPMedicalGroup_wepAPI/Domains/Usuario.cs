using System;
using System.Collections.Generic;

#nullable disable

namespace senai_SPMedicalGroup_wepAPI.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            ImagemUsuarios = new HashSet<ImagemUsuario>();
            Medicos = new HashSet<Medico>();
            Pacientes = new HashSet<Paciente>();
        }

        public int IdUsuario { get; set; }
        public byte? IdTipo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public virtual TipoUsuario IdTipoNavigation { get; set; }
        public virtual ICollection<ImagemUsuario> ImagemUsuarios { get; set; }
        public virtual ICollection<Medico> Medicos { get; set; }
        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
