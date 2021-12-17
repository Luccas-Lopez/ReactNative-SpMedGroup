using senai_SPMedicalGroup_wepAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_SPMedicalGroup_wepAPI.Interfaces
{
    interface IPacienteRepository
    {
        void Cadastrar(Paciente novaPaciente);
        void Atualizar(int IdPaciente, Paciente atualizarPaciente);
        void Deletar(int IdPaciente);
        List<Paciente> ListarTodos();
        Paciente BuscarPorId(int IdPaciente);
    }
}
