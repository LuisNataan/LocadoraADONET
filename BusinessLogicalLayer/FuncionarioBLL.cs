﻿using BusinessLogicalLayer.Security;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    //public class FuncionarioBLL : IEntityCRUD<Funcionario>, IFuncionarioService
    //{
    //    private FuncionarioDAL funcionarioDAL = new FuncionarioDAL();

    //    public DataResponse<Funcionario> Autenticar(string email, string senha)
    //    {
    //        //TODO: Validar email e Senha! As implementações não serão feitas 
    //        //pq a gente já viu isso 
    //        //999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999
    //        //999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999
    //        //999999999999999999999999999999999999999999999999999999999999999999 vezes

    //        //Após validar, caso esteja tudo fofinho e pronto pra funcionar, chama o banco!

    //        senha = HashUtils.HashPassword(senha);

    //        DataResponse<Funcionario> response = funcionarioDAL.Autenticar(email, senha);
    //        if (response.Sucesso)
    //        {
    //            User.FuncionarioLogado = response.Data[0];
    //        }
    //        return response;
    //    }

    //    public Response Delete(int id)
    //    {
    //        Response response = new Response();
    //        if (id <= 0)
    //        {
    //            response.Erros.Add("ID do filme não foi informado.");
    //        }
    //        if (response.Erros.Count != 0)
    //        {
    //            response.Sucesso = false;
    //            return response;
    //        }
    //        return funcionarioDAL.Delete(id);
    //    }

    //    public DataResponse<Funcionario> GetByID(int id)
    //    {
    //        return funcionarioDAL.GetByID(id);
    //    }

    //    public DataResponse<Funcionario> GetData()
    //    {
    //        return funcionarioDAL.GetData();
    //    }

    //    public Response Insert(Funcionario item)
    //    {
    //        Response response = Validate(item);

    //        if (response.HasErrors())
    //        {
    //            response.Sucesso = false;
    //            return response;
    //        }

    //        item.EhAtivo = true;
    //        item.Senha = HashUtils.HashPassword(item.Senha);
    //        return funcionarioDAL.Insert(item);
    //    }

    //    public Response Update(Funcionario item)
    //    {
    //        Response response = new Response();

    //        if (string.IsNullOrWhiteSpace(item.CPF))
    //        {
    //            response.Erros.Add("O cpf deve ser informado");
    //        }
    //        else
    //        {
    //            item.CPF = item.CPF.Trim();
    //            if (!item.CPF.IsCpf())
    //            {
    //                response.Erros.Add("O cpf informado é inválido.");
    //            }
    //        }
    //        if (response.HasErrors())
    //        {
    //            response.Sucesso = false;
    //            return response;
    //        }
    //        return funcionarioDAL.Update(item);
    //    }
    //    private Response Validate(Funcionario item)
    //    {
    //        Response response = new Response();

    //        if (string.IsNullOrWhiteSpace(item.CPF))
    //        {
    //            response.Erros.Add("O cpf deve ser informado");
    //        }
    //        else
    //        {
    //            item.CPF = item.CPF.Trim();
    //            if (!item.CPF.IsCpf())
    //            {
    //                response.Erros.Add("O cpf informado é inválido.");
    //            }
    //        }

    //        string validacaoSenha = SenhaValidator.ValidateSenha(item.Senha, item.DataNascimento);
    //        if (validacaoSenha != "")
    //        {
    //            response.Erros.Add(validacaoSenha);
    //        }
    //        return response;
    //    }
    //}
}
