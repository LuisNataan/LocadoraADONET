using BLL.Interfaces;
using DAO;
using Entities;
using System;
using System.Collections.Generic;
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
                db.Generos.Add(genero);
                db.SaveChanges();
            }

            response.Sucesso = true;
            return response;
        }

        public Response Update(Genero genero)
        {
            throw new NotImplementedException();
        }

        public Response Delete(int GeenroId)
        {
            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
                Genero genero = db.Generos.Find(GeenroId);

                Response response = new Response();

                if (genero.ID > 0)
                {
                    response.Sucesso = true;
                    db.Generos.Remove(genero);
                    db.SaveChanges();
                }
                else
                {
                    response.Sucesso = false;
                    response.Erros.Add("Genero não encontrado!");
                }
                return response;
            }
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
