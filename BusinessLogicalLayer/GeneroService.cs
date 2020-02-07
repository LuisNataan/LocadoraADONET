using BLL.Interfaces;
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
    class GeneroService : IGeneroService
    {
        public Response Insert(Genero genero)
        {
            Response response = Validate(genero);
            if (response.Erros.Count > 0)
            {
                response.Sucesso = false;
                return response;
            }

            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                try
                {
                    db.Generos.Add(genero);
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

        public Response Update(Genero genero)
        {
            Response response = Validate(genero);
            if (response.Erros.Count > 0)
            {
                response.Sucesso = false;
                return response;
            }

            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                try
                {
                    db.Entry<Genero>(genero).State = System.Data.Entity.EntityState.Modified;
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

        public Response Delete(int GeneroId)
        {
            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                Genero genero = db.Generos.Find(GeneroId);

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

        public Response GetData(Genero genero)
        {
            DataResponse<Genero> response = new DataResponse<Genero>();

            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                List<Genero> generos = db.Generos.Select(g => new Genero()
                {
                    ID             = g.ID,
                    Nome           = g.Nome,
                }).ToList();

                response.Data = generos;
            }

            return response;
        }

        private Response Validate(Genero item)
        {
            Response response = new Response();
            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                response.Erros.Add("O nome do gênero deve ser informado.");
            }
            else
            {
                item.Nome = item.Nome.Trim();

                item.Nome = Regex.Replace(item.Nome, @"\s+", " ");
                if (item.Nome.Length < 2 || item.Nome.Length > 50)
                {
                    response.Erros.Add("O nome do gênero deve conter entre 2 e 50 caracteres");
                }
            }
            return response;
        }
    }
}
