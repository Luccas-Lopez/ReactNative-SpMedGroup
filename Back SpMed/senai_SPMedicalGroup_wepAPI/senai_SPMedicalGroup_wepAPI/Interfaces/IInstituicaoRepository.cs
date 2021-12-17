using senai_SPMedicalGroup_wepAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_SPMedicalGroup_wepAPI.Interfaces
{
    interface IInstituicaoRepository
    {
        void Cadastrar(Instituicao novaInstituicao);
        void Atualizar(int IdInstituicao,Instituicao instituicaoAtualizada);
        void Deletar(int IdInstituicao);
        List<Instituicao> ListarTodos();
        Instituicao BuscarPorId(int Instituicao);
    }
}
