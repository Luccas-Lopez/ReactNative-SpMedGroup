using senai_SPMedicalGroup_wepAPI.Contexts;
using senai_SPMedicalGroup_wepAPI.Domains;
using senai_SPMedicalGroup_wepAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_SPMedicalGroup_wepAPI.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        SPMedicalGroupContext ctx = new();
        public void Atualizar(int IdPaciente, Paciente atualizarPaciente)
        {
            Paciente pacienteBuscado = ctx.Pacientes.FirstOrDefault(p => p.IdPaciente == IdPaciente);

            pacienteBuscado.IdUsuario = pacienteBuscado.IdUsuario;
            pacienteBuscado.DataNascimento = atualizarPaciente.DataNascimento;
            pacienteBuscado.Telefone = atualizarPaciente.Telefone;
            pacienteBuscado.Rg = atualizarPaciente.Rg;
            pacienteBuscado.Cpf = atualizarPaciente.Cpf;
            pacienteBuscado.Endereco = atualizarPaciente.Endereco;

            ctx.Pacientes.Update(pacienteBuscado);

            ctx.SaveChanges();
        }

        public Paciente BuscarPorId(int IdPaciente)
        {
            return ctx.Pacientes.FirstOrDefault(p => p.IdPaciente == IdPaciente);
        }

        public void Cadastrar(Paciente novaPaciente)
        {
            ctx.Pacientes.Add(novaPaciente);

            ctx.SaveChanges();
        }

        public void Deletar(int IdPaciente)
        {
            ctx.Pacientes.Remove(BuscarPorId(IdPaciente));

            ctx.SaveChanges();
        }

        public List<Paciente> ListarTodos()
        {
            return ctx.Pacientes.ToList();
        }
    }
}
