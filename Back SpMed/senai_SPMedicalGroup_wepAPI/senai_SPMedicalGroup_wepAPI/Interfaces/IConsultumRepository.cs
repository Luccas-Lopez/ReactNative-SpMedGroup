using senai_SPMedicalGroup_wepAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_SPMedicalGroup_wepAPI.Interfaces
{
    interface IConsultumRepository
    {
        List<Consultum> ListarTodas();

        List<Consultum> ListarMinhasConsultas(int IdConsulta, int idUsuario, int idTipo);

        void CadastrarConsulta(Consultum novaConsulta);

        void CancelarConsulta(int IdConsulta);

        void RemoverConsultaSistema(int IdConsulta);

        void AlterarDescricao(string descricao, int IdConsulta);

        Consultum BuscarPorId(int IdConsulta);
    }
}
