using BusinessLogicalLayer.Interfaces;
using DAO;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ClienteService : IClienteService
    {
        public void Insert(Cliente cliente)
        {
            //Validar
            using (XXXLocadoraDbContext db = new XXXLocadoraDbContext())
            {
             

                Cliente c = new Cliente()
                {
                    Nome = "Danizinho Bernart",
                    EhAtivo = true,
                    CPF = "901.917.069-49",
                    Email = "matfys2@gmail.com",
                    DataNascimento = DateTime.Now.AddYears(-25)
                };

                db.Clientes.Add(c);
                db.SaveChanges();
            }
        }
    }
}
