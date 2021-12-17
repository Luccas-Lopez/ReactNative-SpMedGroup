using Microsoft.AspNetCore.Http;
using senai_SPMedicalGroup_wepAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_SPMedicalGroup_wepAPI.Interfaces
{
    interface IUsuarioRepository
    {
        void Cadastrar(Usuario novoUsuario);
        void Atualizar(int IdUsuario, Usuario atualizarUsuario);
        void Deletar(int IdUsuario);
        List<Usuario> ListarTodos();
        Usuario BuscarPorId(int IdUsuario);
        Usuario Login(string email, string senha);
        void SalvarPerfil(IFormFile foto, int IdUsuario);
        string ConsultarPerfil(int IdUsuario);
    }
}
