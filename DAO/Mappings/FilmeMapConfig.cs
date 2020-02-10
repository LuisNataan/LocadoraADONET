using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class FilmeMapConfig : EntityTypeConfiguration<Filme>
    {
        public FilmeMapConfig()
        {
            //Definimos o nome da tabela que está vinculada a entidade escrita lá em cima (Cliente)
            this.ToTable("FILMES");
            this.Property(f => f.Nome).HasMaxLength(50);
            this.Property(f => f.DataLancamento).HasColumnType("Date").IsRequired();
            this.HasMany(f => f.Locacoes).WithMany(l => l.Filmes).Map(m =>
             m.MapLeftKey("LocacaoID").MapRightKey("FilmeID").ToTable("LOCACAO_FILME"));

        }
    }
}
