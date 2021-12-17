using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_SPMedicalGroup_wepAPI.Domains;
using senai_SPMedicalGroup_wepAPI.Interfaces;
using senai_SPMedicalGroup_wepAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_SPMedicalGroup_wepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicaosController : ControllerBase
    {
        private IInstituicaoRepository _instituicaoRepository { get; set; }
        public InstituicaosController()
        {
            _instituicaoRepository = new InstituicaoRepository();
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id:int}")]
        public IActionResult Atualizar(int IdInstituicao, Instituicao instituicaoAtualizada)
        {
            try
            {
                if (IdInstituicao <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Insira um id válido"
                    });
                }

                if (_instituicaoRepository.BuscarPorId(IdInstituicao) == null)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma clínica com o id informado!"
                    });
                }
                if (instituicaoAtualizada.Cnpj == null || instituicaoAtualizada.Endereco == null || instituicaoAtualizada.NomeFantasia == null || instituicaoAtualizada.RazaoSocial == null || instituicaoAtualizada.Cnpj.Length != 14)
                {
                    return BadRequest(new
                    {
                        Mensagem = "As informações inseridas são inválidas!"
                    });
                }

                _instituicaoRepository.Atualizar(IdInstituicao, instituicaoAtualizada);
                return Ok(new
                {
                    Mensagem = "A clínica foi atualizada com sucesso!",
                    instituicaoAtualizada
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "2")]
        [HttpPost]
        public IActionResult Cadastrar(Instituicao novaInstituicao)
        {
            _instituicaoRepository.Cadastrar(novaInstituicao);

            return StatusCode(201, new
            {
                Mensagem = "Nova clinica criada",
                novaInstituicao
            });
        }
        [Authorize(Roles = "1")]
        [HttpDelete("{id:int}")]
        public IActionResult Deletar(int IdInstituicao)
        {
            try
            {
                if (IdInstituicao<= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Insira um ID válido"
                    });
                }

                if (_instituicaoRepository.BuscarPorId(IdInstituicao) == null)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma clínica com o ID informado!"
                    });
                }

                _instituicaoRepository.Deletar(IdInstituicao);
                return Ok(new
                {
                    Mensagem = "A instituição foi excluída com sucesso!",
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Listar()
        {
            List<Instituicao> listaInstituicaos = _instituicaoRepository.ListarTodos();

            if (listaInstituicaos == null)
            {
                return BadRequest(new
                {
                    Mensagem = "Nenhum paciente encontrado"
                });
            }

            return Ok(listaInstituicaos);
        }
    }
}
