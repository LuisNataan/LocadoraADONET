using BusinessLogicalLayer.Interfaces;
using DAO;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class ClienteService : IClienteService
    {
        public Response Insert(Cliente cliente)
        {
            Response response = new Response();
            if (response.Erros.Count > 0)
            {
                response.Sucesso = true;
                return response;
            }

            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {   
                try
                {
                    db.Clientes.Add(cliente);
                    db.SaveChanges();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Sucesso = false;
                    response.Erros.Add("Erro no banco de dados contate o administrador.");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }
        public Response Update(Cliente cliente)
        {
            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext()) 
            {
                Response response = new Response();
                try
                {
                    Cliente c = db.Clientes.Find(cliente.ID);
                    c = cliente;
                    db.SaveChanges();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Sucesso = false;
                    response.Erros.Add("Erros no banco de dados, contate um administrador.");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public Response Delete(Cliente cliente)
        {
            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext()) 
            {
                Cliente c = db.Clientes.Find(cliente.ID);

                Response response = new Response();
                if (cliente.ID <= 0)
                {
                    response.Sucesso = false;
                    response.Erros.Add("Cliente não encontrado!");
                }

                try
                {
                    db.Clientes.Remove(cliente);
                    db.SaveChanges();

                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Sucesso = false;
                    response.Erros.Add("Erro no banco de dados, contate o administrador.");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public DataResponse<Cliente> GetData()
        {
            DataResponse<Cliente> response = new DataResponse<Cliente>();
            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext()) 
            {
                List<Cliente> clientes = db.Clientes.Select(c => new Cliente()
                {
                    ID = c.ID,
                    Nome = c.Nome,
                    CPF = c.CPF,
                    Email = c.Email,
                    DataNascimento = c.DataNascimento,
                    EhAtivo = c.EhAtivo,
                }).ToList();
                response.Data = clientes;
            }
            return response;
        }

        public DataResponse<Cliente> GetByID(int ClienteID)
        {
            DataResponse<Cliente> response = new DataResponse<Cliente>();

            if (ClienteID <= 0)
            {
                response.Sucesso = false;
                response.Erros.Add("Funcionario não encontrado!");
            }

            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                try
                {
                    List<Cliente> cliente = new List<Cliente>();
                    cliente.Add(db.Clientes.Find(ClienteID));

                    response.Sucesso = true;
                    response.Data = cliente;

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
        private Response Validate(Filme item)
        {
            Response response = new Response();
            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                response.Erros.Add("O nome do filme deve ser informado.");
            }
            else
            {
                item.Nome = item.Nome.Trim();

                item.Nome = Regex.Replace(item.Nome, @"\s+", " ");
                if (item.Nome.Length < 2 || item.Nome.Length > 50)
                {
                    response.Erros.Add("O nome do filme deve conter entre 2 e 50 caracteres");
                }
            }
            return response;
        }


    }
}
