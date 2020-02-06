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
                db.Clientes.Add(cliente);
                db.SaveChanges();
            }
        }
    }
}
