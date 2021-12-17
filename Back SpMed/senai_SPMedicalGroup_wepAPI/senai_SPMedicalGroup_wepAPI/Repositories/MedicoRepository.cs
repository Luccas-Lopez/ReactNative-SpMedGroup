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
    public class MedicoRepository : IMedicoRepository
    {
        SPMedicalGroupContext ctx = new();
        public void Atualizar(int IdMedico, Medico atualizarMedico)
        {
            Medico medicoBuscado = ctx.Medicos.FirstOrDefault(p => p.IdMedico == IdMedico);

            medicoBuscado.IdUsuario = medicoBuscado.IdUsuario;
            medicoBuscado.IdEspecializacao = atualizarMedico.IdEspecializacao;
            medicoBuscado.IdInstituicao = atualizarMedico.IdInstituicao;
            medicoBuscado.Crm = atualizarMedico.Crm;

            ctx.Medicos.Update(medicoBuscado);

            ctx.SaveChanges();
        }

        public Medico BuscarPorId(int IdMedico)
        {
            return ctx.Medicos.FirstOrDefault(e => e.IdMedico == IdMedico);
        }

        public void Cadastrar(Medico novoMedico)
        {
            ctx.Medicos.Add(novoMedico);

            ctx.SaveChanges();
        }

        public void Deletar(int IdMedico)
        {
            ctx.Medicos.Remove(BuscarPorId(IdMedico));

            ctx.SaveChanges();
        }

        public List<Medico> ListarTodos()
        {
            return ctx.Medicos.Include(m => m.IdInstituicaoNavigation).Include(m => m.IdEspecializacaoNavigation).Include(m => m.IdUsuarioNavigation).ToList();
        }
    }
}
