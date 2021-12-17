using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_SPMedicalGroup_wepAPI.Domains;
using senai_SPMedicalGroup_wepAPI.Interfaces;
using senai_SPMedicalGroup_wepAPI.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace senai_SPMedicalGroup_wepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }
        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(Usuario novoUsuario)
        {
            if (novoUsuario.IdTipo <= 0)
            {
                return BadRequest(new
                {
                    Mensagem = "Informe todos os dados necessários"
                });
            }

            _usuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201, new
            {
                Mensagem = "Usuario criado"
            });
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Listar()
        {
            List<Usuario> listaUsuarios = _usuarioRepository.ListarTodos();

            if (listaUsuarios == null)
            {
                return BadRequest(new
                {
                    Mensagem = "Nenhum úsuario encontrado"
                });
            }

            return Ok(listaUsuarios);
        }

        [Authorize(Roles = "1")]
        [HttpGet("Imagem/Bd/{id}")]
        public IActionResult ConsultarImagem(int id)
        {
            string base64 = _usuarioRepository.ConsultarPerfil(id);

            if (base64 == null)
            {
                return NotFound(new
                {
                    Mensagem = "Imagem de perfil não cadastrada"
                });
            }

            return Ok(base64);
        }

        [Authorize(Roles = "1")]
        [HttpPost("Imagem/Bd")]
        public IActionResult CadastrarImagem(IFormFile arquivo)
        {
            if (arquivo.Length > 1000000)
            {
                return BadRequest(new
                {
                    Mensagem = "Tamanho de imagem não suportado"
                });
            }

            string extensao = arquivo.FileName.Split('.').Last();

            if (extensao != "png")
            {
                return BadRequest(new
                {
                    Mensagem = "Formato de imagem não suportado"
                });
            }

            int idUser = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

            _usuarioRepository.SalvarPerfil(arquivo, idUser);

            return Ok();
        }
    }
}
