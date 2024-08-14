using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace celsia_assetsment_johan_arboleda.App.Interfaces
{
    public interface IExcel
    {
        Task ImportDataAsync(Stream fileStream);
    }
}