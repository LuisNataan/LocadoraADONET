using BLL.Interfaces;
using BusinessLogicalLayer.Security;
using DAO;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    class FuncionarioService : IFuncionarioService
    {
        public Response Insert(Funcionario funcionario)
        {
            Response response = Validate(funcionario);
            if (response.Erros.Count > 0)
            {
                response.Sucesso = false;
                return response;
            }

            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                try
                {
                    db.Funcionarios.Add(funcionario);
                    db.SaveChanges();

                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Sucesso = false;

                    response.Erros.Add("Erro no banco de dados, contate o administrador!");
                    File.WriteAllText("log.txt", ex.Message);

                    return response;
                }
            }
        }

        private Response Validate(Funcionario item)
        {
            Response response = new Response();

            if (string.IsNullOrWhiteSpace(item.CPF))
            {
                response.Erros.Add("O cpf deve ser informado");
            }
            else
            {
                item.CPF = item.CPF.Trim();
                if (!item.CPF.IsCpf())
                {
                    response.Erros.Add("O cpf informado é inválido.");
                }
            }

            string validacaoSenha = SenhaValidator.ValidateSenha(item.Senha, item.DataNascimento);
            if (validacaoSenha != "")
            {
                response.Erros.Add(validacaoSenha);
            }
            return response;
        }
    }
}
