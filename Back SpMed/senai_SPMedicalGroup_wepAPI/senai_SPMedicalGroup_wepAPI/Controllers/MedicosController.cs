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
    public class MedicosController : ControllerBase
    {
        private IMedicoRepository _medicoRepository { get; set; }

        public MedicosController()
        {
            _medicoRepository = new MedicoRepository();
        }

        [Authorize(Roles = "2")]
        [HttpPost]
        public IActionResult Cadastrar(Medico novoMedico)
        {
            if (novoMedico.IdUsuario <= 0 || novoMedico.IdEspecializacao <= 0 || novoMedico.IdInstituicao <= 0)
            {
                return BadRequest(new
                {
                    Mensagem = "Dados invalidos ou incompletos"
                });
            }

            _medicoRepository.Cadastrar(novoMedico);

            return Ok(new
            {
                Mensagem = "Novo médico cadastrado",
                novoMedico
            });
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Listar()
        {
            if (_medicoRepository.ListarTodos().Count <= 0)
            {
                return BadRequest(new
                {
                    Mensagem = "Nenhum médico encontrado"
                });
            }

            return Ok(_medicoRepository.ListarTodos());
        }
    }
}
