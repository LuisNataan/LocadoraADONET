using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface IFuncionarioService
    {
        Response Insert(Funcionario funcionario);
        Response Update(Funcionario funcionario);
        Response Delete(int FuncionarioID);
        DataResponse<Funcionario> GetData();
    }
}
