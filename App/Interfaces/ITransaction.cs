using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using celsia_assetsment_johan_arboleda.Dtos;

namespace celsia_assetsment_johan_arboleda.App.Interfaces
{
    public interface ITransaction
    {
        Task<List<TransactionDto>> GetAllTransactionsAsync();
    }
}