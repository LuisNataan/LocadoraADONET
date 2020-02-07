using BusinessLogicalLayer.Interfaces;
using DAO;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogicalLayer
{
    public class LocacaoService : ILocacaoService
    {
        public Response EfetuarLocacao(Locacao locacao)
        {
            Response response = Validate(locacao);

            if (response.Erros.Count > 0)
            {
                response.Sucesso = false;
                return response;
            }

            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                try
                {
                    db.Locacoes.Add(locacao);
                    db.SaveChanges();

                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Sucesso = false;
                    if (ex.Message.Contains("FK_LOCACOES_CLIENTES"))
                    {
                        response.Erros.Add("Cliente inexistente.");
                    }
                    else if (ex.Message.Contains("FK_LOCACOES_FUNCIONARIOS"))
                    {
                        response.Erros.Add("Funcionario inexistente.");
                    }
                    else
                    {
                        response.Erros.Add("Erro no banco de dados, contate o adm.");
                        File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                    }
                    return response;
                }
            }
             
            return response;
        }

        private Response Validate(Locacao locacao)
        {
            Response response = new Response();

            if (locacao.Filmes.Count == 0)
            {
                response.Erros.Add("Não é possível realizar a locação sem filmes.");
                response.Sucesso = false;
                return response;
            }

            TimeSpan ts = DateTime.Now.Subtract(locacao.Cliente.DataNascimento);
            int idade = (int)(ts.TotalDays / 365);

            foreach (Filme filme in locacao.Filmes)
            {
                if ((int)filme.Classificacao > idade)
                {
                    response.Erros.Add("A idade do cliente não corresponde com a classificação indicativa do filme " + filme.Nome);
                    response.Sucesso = false;
                }
            }

            locacao.DataLocacao = DateTime.Now;
            locacao.DataPrevistaDevolucao = DateTime.Now;

            foreach (Filme filme in locacao.Filmes)
            {
                locacao.DataPrevistaDevolucao = locacao.DataPrevistaDevolucao.AddHours(filme.CalcularDevolucao());
                locacao.Preco += filme.CalcularPreco();
            }

            return response;
        }
    }
}
