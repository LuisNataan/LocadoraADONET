using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class FuncionarioMapConfig : EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioMapConfig()
        {
            //Definimos o nome da tabela que está vinculada a entidade escrita lá em cima (Cliente)
            this.ToTable("CLIENTES");
            this.Property(fun => fun.Nome).HasMaxLength(50);
            this.Property(fun => fun.CPF).IsFixedLength().HasMaxLength(14);
            this.Property(fun => fun.DataNascimento).HasColumnType("Date").IsRequired();
            this.Property(fun => fun.Email).HasMaxLength(100);
            this.Property(fun => fun.Telefone).IsFixedLength().HasMaxLength(15);

        }
    }
}
