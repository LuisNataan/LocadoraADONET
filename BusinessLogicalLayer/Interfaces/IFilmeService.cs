using Entities;
using Entities.ResultSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    interface IFilmeService
    {
        Response Insert(Filme filme);
        Response Update(Filme filme);
        Response Delete(int filme);
        DataResponse<FilmeResultSet> GetData();
    }
}
