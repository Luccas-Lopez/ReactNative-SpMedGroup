using Microsoft.AspNetCore.Http;
using senai_SPMedicalGroup_wepAPI.Contexts;
using senai_SPMedicalGroup_wepAPI.Domains;
using senai_SPMedicalGroup_wepAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace senai_SPMedicalGroup_wepAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        SPMedicalGroupContext ctx = new();
        public void Atualizar(int IdUsuario, Usuario atualizarUsuario)
        {
            Usuario usuarioBuscado = ctx.Usuarios.FirstOrDefault(p => p.IdUsuario == IdUsuario);

            usuarioBuscado.IdTipo = usuarioBuscado.IdTipo;
            usuarioBuscado.Nome = atualizarUsuario.Nome;
            usuarioBuscado.Email = atualizarUsuario.Email;
            usuarioBuscado.Senha = atualizarUsuario.Senha;

            ctx.Usuarios.Update(usuarioBuscado);

            ctx.SaveChanges();
        }

        public Usuario BuscarPorId(int IdUsuario)
        {
            return ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == IdUsuario);
        }

        public void Cadastrar(Usuario novoUsuario)
        {
            ctx.Usuarios.Add(novoUsuario);

            ctx.SaveChanges();
        }

        public string ConsultarPerfil(int IdUsuario)
        {
            ImagemUsuario imagemBuscada = new();

            imagemBuscada = ctx.ImagemUsuarios.FirstOrDefault(i => i.IdUsuario == IdUsuario);

            if (imagemBuscada != null)
            {
                return Convert.ToBase64String(imagemBuscada.Binario);
            }

            return null;
        }

        public void Deletar(int IdUsuario)
        {
            ctx.Usuarios.Remove(BuscarPorId(IdUsuario));

            ctx.SaveChanges();
        }

        public List<Usuario> ListarTodos()
        {
            return ctx.Usuarios
                         .Select(u => new Usuario()
                         {
                             IdUsuario = u.IdUsuario,
                             Nome = u.Nome,
                             Email = u.Email,
                             IdTipo = u.IdTipo,
                             IdTipoNavigation = new TipoUsuario()
                             {
                                 IdTipo = u.IdTipoNavigation.IdTipo,
                                 NomeTipo = u.IdTipoNavigation.NomeTipo
                             }
                         })
                         .ToList();
        }

        public Usuario Login(string email, string senha)
        {
            return ctx.Usuarios.FirstOrDefault(e => e.Email == email && e.Senha == senha);
        }

        public void SalvarPerfil(IFormFile foto, int id)
        {
            ImagemUsuario novaImagem = new();

            using (var ms = new MemoryStream())
            {
                foto.CopyTo(ms);

                novaImagem.Binario = ms.ToArray();

                novaImagem.NomeArquivo = foto.FileName;
                novaImagem.MimeType = foto.FileName.Split('.').Last();
                novaImagem.IdUsuario = id;
            }

            ImagemUsuario imagemExistente = new();
            imagemExistente = ctx.ImagemUsuarios.FirstOrDefault(i => i.IdUsuario == id);

            if (imagemExistente != null)
            {
                imagemExistente.Binario = novaImagem.Binario;
                imagemExistente.NomeArquivo = novaImagem.NomeArquivo;
                imagemExistente.MimeType = novaImagem.MimeType;
                imagemExistente.IdUsuario = id;

                ctx.ImagemUsuarios.Update(imagemExistente);
            }
            else
            {
                ctx.ImagemUsuarios.Add(novaImagem);
            }

            ctx.SaveChanges();
        }
    }
}
