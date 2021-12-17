using Microsoft.EntityFrameworkCore;
using senai_SPMedicalGroup_wepAPI.Contexts;
using senai_SPMedicalGroup_wepAPI.Domains;
using senai_SPMedicalGroup_wepAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_SPMedicalGroup_wepAPI.Repositories
{
   
    public class ConsultumRepository : IConsultumRepository
    {
        SPMedicalGroupContext ctx = new();

        public void AlterarDescricao(string descricao, int IdConsulta)
        {
            Consultum consultaBuscada = BuscarPorId(IdConsulta);

            if (descricao != null)
            {
                consultaBuscada.Descricao = descricao;

                ctx.Consulta.Update(consultaBuscada);

                ctx.SaveChanges();
            };

        }

        public Consultum BuscarPorId(int IdConsulta)
        {
            return ctx.Consulta.FirstOrDefault(u => u.IdConsulta == IdConsulta);
        }

        public void CadastrarConsulta(Consultum novaConsulta)
        {
            novaConsulta.Descricao = "";
            novaConsulta.IdSituacaoConsulta = 2;

            ctx.Consulta.Add(novaConsulta);

            ctx.SaveChanges();
        }

        public void CancelarConsulta(int IdConsulta)
        {
            Consultum consultaBuscada = BuscarPorId(IdConsulta);

            consultaBuscada.IdSituacaoConsulta = 3;
            consultaBuscada.Descricao= "Consulta Cancelada!";

            ctx.Consulta.Update(consultaBuscada);
            ctx.SaveChanges();
        }

        public List<Consultum> ListarMinhasConsultas(int IdConsulta, int IdUsuario, int idTipo)
        {
            if (idTipo == 2)
            {
                Medico medico = ctx.Medicos.FirstOrDefault(u => u.IdUsuario == IdUsuario);

                int idMedico = medico.IdMedico;

                return ctx.Consulta
                                .Where(c => c.IdMedico == idMedico)
                                .AsNoTracking()
                                .Select(p => new Consultum()
                                {
                                    DataConsulta = p.DataConsulta,
                                    IdConsulta = p.IdConsulta,
                                    IdMedicoNavigation = new Medico()
                                    {
                                        Crm = p.IdMedicoNavigation.Crm,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Nome = p.IdMedicoNavigation.IdUsuarioNavigation.Nome,
                                            Email = p.IdMedicoNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                    IdPacienteNavigation = new Paciente()
                                    {
                                        Cpf = p.IdPacienteNavigation.Cpf,
                                        Telefone = p.IdPacienteNavigation.Telefone,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Nome = p.IdPacienteNavigation.IdUsuarioNavigation.Nome,
                                            Email = p.IdPacienteNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                    IdSituacaoConsultaNavigation = new SituacaoConsultum()
                                    {
                                        Descricao = p.IdSituacaoConsultaNavigation.Descricao
                                    }


                                })
                                .ToList();
            }
            else if (idTipo == 3)
            {
                Paciente paciente = ctx.Pacientes.FirstOrDefault(c => c.IdUsuario == IdUsuario);

                int idPaciente= paciente.IdPaciente;
                return ctx.Consulta
                                .Where(c => c.IdPaciente == idPaciente)
                                .AsNoTracking()
                                .Select(p => new Consultum()
                                {
                                    DataConsulta = p.DataConsulta,
                                    IdConsulta = p.IdConsulta,
                                    IdMedicoNavigation = new Medico()
                                    {
                                        Crm = p.IdMedicoNavigation.Crm,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Nome = p.IdMedicoNavigation.IdUsuarioNavigation.Nome,
                                            Email = p.IdMedicoNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                    IdPacienteNavigation = new Paciente()
                                    {
                                        Cpf = p.IdPacienteNavigation.Cpf,
                                        Telefone = p.IdPacienteNavigation.Telefone,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Nome = p.IdPacienteNavigation.IdUsuarioNavigation.Nome,
                                            Email = p.IdPacienteNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                 
                                        Descricao = p.Descricao


                                })
                                .ToList();
            }

            return null;
        }

        public List<Consultum> ListarTodas()
        {
            return ctx.Consulta
                .Select(p => new Consultum()
                {
                    DataConsulta = p.DataConsulta,
                    IdConsulta = p.IdConsulta,
                    IdMedicoNavigation = new Medico()
                    {
                        Crm = p.IdMedicoNavigation.Crm,
                        IdUsuarioNavigation = new Usuario()
                        {
                            Nome = p.IdMedicoNavigation.IdUsuarioNavigation.Nome,
                            Email = p.IdMedicoNavigation.IdUsuarioNavigation.Email
                        }
                    },
                    IdPacienteNavigation = new Paciente()
                    {
                        Cpf = p.IdPacienteNavigation.Cpf,
                        Telefone = p.IdPacienteNavigation.Telefone,
                        IdUsuarioNavigation = new Usuario()
                        {
                            Nome = p.IdPacienteNavigation.IdUsuarioNavigation.Nome,
                            Email = p.IdPacienteNavigation.IdUsuarioNavigation.Email
                        }
                    },
                    IdSituacaoConsultaNavigation = new SituacaoConsultum()
                    {
                        Descricao = p.IdSituacaoConsultaNavigation.Descricao
                    }


                })
                .ToList();
        }

        public void RemoverConsultaSistema(int IdConsulta)
        {
            ctx.Consulta.Remove(BuscarPorId(IdConsulta));
            ctx.SaveChanges();
        }
    }
}
