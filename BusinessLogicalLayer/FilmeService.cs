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

namespace BusinessLogicalLayer
{
    public class FilmeService : IFilmeService
    {
        public Response Delete(int id)
        {
            Response response = new Response();
            if (id <= 0)
            {
                response.Erros.Add("ID do filme não foi informado.");
            }
            if (response.Erros.Count != 0)
            {
                response.Sucesso = false;
                return response;
            }
            return filmeDAL.Delete(id);
        }

        public DataResponse<Filme> GetByID(int id)
        {
            DataResponse<Filme> response = new DataResponse<Filme>();

            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                List<Filme> filmes = db.Filmes.Find(id);
                response.Data = filmes;
            }

            return response;
        }

        public DataResponse<Filme> GetData()
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

        public DataResponse<FilmeResultSet> GetFilmes()
        {
            public DataResponse<FilmeResultSet> GetFilmesByClassificacao(Classificacao classificacao)
            {
                return filmeDAL.GetFilmesByClassificacao(classificacao);
            }

            public DataResponse<FilmeResultSet> GetFilmesByGenero(int genero)
            {
                if (genero <= 0)
                {
                    DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
                    response.Sucesso = false;
                    response.Erros.Add("Gênero deve ser informado.");
                    return response;
                }
                return filmeDAL.GetFilmesByGenero(genero);
            }

            public DataResponse<FilmeResultSet> GetFilmesByName(string nome)
            {
                if (string.IsNullOrWhiteSpace(nome))
                {
                    DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
                    response.Sucesso = false;
                    response.Erros.Add("Nome deve ser informado.");
                    return response;
                }
                nome = nome.Trim();
                return filmeDAL.GetFilmesByName(nome);
            }

            public Response Insert(Filme item)
            {
                Response response = Validate(item);
                //TODO: Verificar a existência desse gênero na base de dados
                //generoBLL.LerID(item.GeneroID);

                //Verifica se tem erros!
                if (response.Erros.Count != 0)
                {
                    response.Sucesso = false;
                    return response;
                }
                return filmeDAL.Insert(item);
            }
            public Response Update(Filme item)
            {
                Response response = Validate(item);
                //TODO: Verificar a existência desse gênero na base de dados
                //generoBLL.LerID(item.GeneroID);
                //Verifica se tem erros!
                if (response.Erros.Count != 0)
                {
                    response.Sucesso = false;
                    return response;
                }
                return filmeDAL.Update(item);
            }

            private Response Validate(Filme item)
            {
                Response response = new Response();

                if (item.Duracao <= 10)
                {
                    response.Erros.Add("Duração não pode ser menor que 10 minutos.");
                }

                if (item.DataLancamento == DateTime.MinValue
                                        ||
                    item.DataLancamento > DateTime.Now)
                {
                    response.Erros.Add("Data inválida.");
                }

                return response;
            }
        }
    }