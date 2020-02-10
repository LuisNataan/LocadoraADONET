using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class LocacaoMapConfig : EntityTypeConfiguration<Locacao>
    {
        public LocacaoMapConfig()
        {
            //Definimos o nome da tabela que está vinculada a entidade escrita lá em cima (Cliente)
            this.ToTable("LOCACOES");
            this.Property(l => l.Preco).HasColumnType("FLOAT");
            this.Property(l => l.Multa).HasColumnName("FLOAT");
        }
    }
}
