using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    public interface ILocacaoService
    {
        Response EfetuarLocacao(Locacao locacao);
    }
}
