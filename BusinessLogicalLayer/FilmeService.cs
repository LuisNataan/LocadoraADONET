using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.ResultSets;
using Entities.Enums;
using DAO;
using BusinessLogicalLayer.Interfaces;
using System.Text.RegularExpressions;
using System.IO;

namespace BusinessLogicalLayer
{
    public class FilmeService : IFilmeService
    {

        public Response Insert(Filme filme)
        {
            Response response = Validate(filme);
            if (response.Erros.Count > 0)
            {
                response.Sucesso = true;
                return response;
            }

            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
                try
                {
                    db.Filmes.Add(filme);
                    db.SaveChanges();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Sucesso = false;

                    response.Erros.Add("Erro no banco de dados, contate o Administrador.");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
        }

        public Response Update(Filme filme)
        {
            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext()) 
            {
                Response response = new Response();
                try
                {
                    Filme f = db.Filmes.Find(filme.ID);
                    f = filme;
                    db.SaveChanges();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Sucesso = false;
                    response.Erros.Add("Erro no banco de dados, contate o Administrado.");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public Response Delete(Filme filmeID)
        {
            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext()) 
            {
                Filme filme = db.Filmes.Find(filmeID);

                Response response = new Response();

                if (filme.ID <= 0)
                {
                    response.Sucesso = false;
                    response.Erros.Add("Filme não encontrado!");
                }
                try
                {
                    db.Filmes.Remove(filme);
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

        public Response GetData(Filme filme)
        {
            DataResponse<Filme> response = new DataResponse<Filme>();

            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                List<Filme> filmes = db.Filmes.Select(f => new Filme()
                {
                    ID = f.ID,
                    Nome = f.Nome,
                    Classificacao = f.Classificacao,
                    DataLancamento = f.DataLancamento,
                    Duracao = f.Duracao,
                    Genero = f.Genero,
                }).ToList();

                response.Data = filmes;
            }

            return response;
        }

        public Response GetByName(Filme filmesName)
        {
            DataResponse<Filme> response = new DataResponse<Filme>();

            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                try
                {
                    List<Filme> filmes = new List<Filme>();
                    filmes.Add(db.Filmes.Find(filmesName.Nome));
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

        public Response GetByID(Filme filmeID)
        {
            DataResponse<Filme> response = new DataResponse<Filme>();
            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                try
                {
                    List<Filme> filmes = new List<Filme>();
                    filmes.Add(db.Filmes.Find(filmeID.ID));
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
