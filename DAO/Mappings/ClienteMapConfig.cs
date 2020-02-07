using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class ClienteMapConfig : EntityTypeConfiguration<Cliente>
    {
        public ClienteMapConfig()
        {
            //Definimos o nome da tabela que está vinculada a entidade escrita lá em cima (Cliente)
            this.ToTable("CLIENTES");
            this.Property(c => c.Nome).HasMaxLength(50);
            this.Property(c => c.CPF).IsFixedLength().HasMaxLength(14);
            this.Property(c => c.DataNascimento).HasColumnType("Date").IsRequired();

        }
    }
}
