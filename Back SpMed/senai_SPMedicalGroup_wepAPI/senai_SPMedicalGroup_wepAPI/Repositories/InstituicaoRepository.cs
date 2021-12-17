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
    public class InstituicaoRepository : IInstituicaoRepository
    {
        SPMedicalGroupContext ctx = new();
        public void Atualizar(int IdInstituicao, Instituicao instituicaoAtualizada)
        { 
             Instituicao instituicaoBuscada = BuscarPorId(IdInstituicao);
            if (instituicaoAtualizada.Endereco != null || instituicaoAtualizada.Cnpj != null || instituicaoAtualizada.NomeFantasia != null || instituicaoAtualizada.RazaoSocial != null)
            {
                instituicaoBuscada.Endereco = instituicaoAtualizada.Endereco;
                instituicaoBuscada.Cnpj = instituicaoAtualizada.Cnpj;
                instituicaoBuscada.NomeFantasia = instituicaoAtualizada.NomeFantasia;
                instituicaoBuscada.RazaoSocial = instituicaoAtualizada.RazaoSocial;

                ctx.Instituicaos.Update(instituicaoBuscada);

                ctx.SaveChanges();
            }
        }

        public Instituicao BuscarPorId(int id)
        {
            return ctx.Instituicaos.FirstOrDefault(c => c.IdInstituicao == id);
        }

        public void Cadastrar(Instituicao novaInstituicao)
        {
            ctx.Instituicaos.Add(novaInstituicao);

            ctx.SaveChanges();
        }

        public void Deletar(int IdInstituicao)
        {
            ctx.Instituicaos.Remove(BuscarPorId(IdInstituicao));

            ctx.SaveChanges();
        }

            public List<Instituicao> ListarTodos()
        {
            return ctx.Instituicaos
                   .AsNoTracking()
                   .Select(c => new Instituicao
                   {
                       Cnpj = c.Cnpj,
                       Endereco = c.Endereco,
                       NomeFantasia = c.NomeFantasia,
                       Medicos = ctx.Medicos.Where(m => m.IdInstituicao == c.IdInstituicao).ToList()
                   })
                   .ToList();
        }
    }
}
