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
    public class FuncionarioService : IFuncionarioService
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

        public Response Update(Funcionario funcionario)
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
                    db.Entry<Funcionario>(funcionario).State = System.Data.Entity.EntityState.Modified;
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

        public Response Delete(int FuncionarioID)
        {
            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                Genero genero = db.Generos.Find(FuncionarioID);

                Response response = new Response();

                if (genero.ID <= 0)
                {
                    response.Sucesso = false;
                    response.Erros.Add("Genero não encontrado!");
                }

                try
                {
                    db.Generos.Remove(genero);
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

        public DataResponse<Funcionario> GetData()
        {
            DataResponse<Funcionario> response = new DataResponse<Funcionario>();

            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                List<Funcionario> funcionarios = db.Funcionarios.Select(f => new Funcionario()
                {
                    ID = f.ID,
                    Nome = f.Nome,
                    Email = f.Email,
                    CPF = f.CPF,
                    DataNascimento = f.DataNascimento,
                    Telefone = f.Telefone,
                    Senha = f.Senha,
                    EhAtivo = f.EhAtivo,
                }).ToList();

                response.Data = funcionarios;
            }

            return response;
        }

        public Response GetByID(int FuncionarioID)
        {
            DataResponse<Funcionario> response = new DataResponse<Funcionario>();

            if (FuncionarioID <= 0)
            {
                response.Sucesso = false;
                response.Erros.Add("Funcionario não encontrado!");
            }

            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                try
                {
                    List<Funcionario> funcionario = new List<Funcionario>();
                    funcionario.Add(db.Funcionarios.Find(FuncionarioID));

                    response.Sucesso = true;
                    response.Data = funcionario;

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
