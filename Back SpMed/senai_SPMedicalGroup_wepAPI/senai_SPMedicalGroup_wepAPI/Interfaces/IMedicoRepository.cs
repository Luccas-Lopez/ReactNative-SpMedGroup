using senai_SPMedicalGroup_wepAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_SPMedicalGroup_wepAPI.Interfaces
{
    interface IMedicoRepository
    {
        List<Medico> ListarTodos();
        void Cadastrar(Medico novaMedico);
        void Atualizar(int IdMedico, Medico atualizarMedico);
        void Deletar(int IdMedico);
        Medico BuscarPorId(int IdMedico);
    }
}
