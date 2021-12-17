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
using System.Security.Claims;
using System.Threading.Tasks;

namespace senai_SPMedicalGroup_wepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    { 
   private IConsultumRepository _consultaRepository { get; set; }
    private IMedicoRepository _medicoRepository { get; set; }

    public ConsultaController()
    {
        _consultaRepository = new ConsultumRepository();
        _medicoRepository = new MedicoRepository();
    }

    [Authorize(Roles = "2")]
    [HttpPost]
    public IActionResult CadastrarConsulta(Consultum novaConsulta)
    {
        if (novaConsulta == null)
        {
            return BadRequest(new
            {
                Mensagem = "Dados da consulta invalidos ou incompletos"
            });
        }

        _consultaRepository.CadastrarConsulta(novaConsulta);

        return StatusCode(201, new
        {
            Mensagem = "Cadastro de consulta feito"
        });
    }

    [Authorize(Roles = "2")]
    [HttpPatch("Cancelar/{id}")]
    public IActionResult CancelarConsulta(int IdConsulta)
    {
        if (IdConsulta < 0 || _consultaRepository.BuscarPorId(IdConsulta) == null)
        {
            return BadRequest(new
            {
                Mensagem = "Id invalido ou inexistente"
            });
        }

        _consultaRepository.CancelarConsulta(IdConsulta);

        return StatusCode(204, new
        {
            Mensagem = "Consulta cancelada"
        });
    }

    [Authorize(Roles = "2,3")]
    [HttpGet("Listar/Minhas")]
    public IActionResult ListarMinhas()
    {
        int IdConsulta = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

        int idTipo = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value);

        int IdUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

            List<Consultum> listaConsultas = _consultaRepository.ListarMinhasConsultas(IdConsulta, IdUsuario,idTipo);

        if (listaConsultas.Count == 0)
        {
            return NotFound(new
            {
                Mensagem = "Nenhuma consulta encontrada"
            });
        }

        if (idTipo == 3)
        {
            return Ok(new
            {
                Mensagem = $"O paciente buscado tem {_consultaRepository.ListarMinhasConsultas(IdConsulta,IdUsuario, idTipo).Count} consultas",
                listaConsultas
            });
        }

        if (idTipo == 2)
        {
            return Ok(new
            {
                Mensagem = $"O médico buscado tem {_consultaRepository.ListarMinhasConsultas (IdConsulta, IdUsuario, idTipo).Count} consultas",
                listaConsultas
            });
        }

        return null;
    }

    [Authorize(Roles = "3")]
    [HttpPatch("Descricao/{id}")]
    public IActionResult AlterarDescricao(Consultum consultaAtualizada, int IdConsulta)
    {
        Consultum consultaBuscada = _consultaRepository.BuscarPorId(IdConsulta);
        int idUser = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
        int idMed = _medicoRepository.BuscarPorId(idUser).IdMedico;

        if (consultaAtualizada.Descricao == null)
        {
            return BadRequest(new
            {
                Mensagem = "É necessário informar a descrição"
            });
        }

        if (IdConsulta <= 0 || _consultaRepository.BuscarPorId(IdConsulta) == null)
        {
            return NotFound(new
            {
                Mensagem = "Id informado invalido ou inexistente"
            });
        }

        if (consultaBuscada.IdMedico != idMed)
        {
            return Unauthorized(new
            {
                Mensagem = "Somente o médico titular pode alterar a descrição dessa consulta"
            });
        }

        _consultaRepository.AlterarDescricao(consultaAtualizada.Descricao, IdConsulta);

        return Ok(new
        {
            Mensagem = "Descrição da consulta alterada com sucesso",
            consultaAtualizada
        });
    }

    [Authorize(Roles = "1")]
    [HttpGet]
    public IActionResult ListarTodas()
    {
        if (_consultaRepository.ListarTodas().Count == 0)
        {
            return BadRequest(new
            {
                Mensagem = "Nenhuma consulta encontrada"
            });
        }

        return Ok(_consultaRepository.ListarTodas());
    }
}
}
