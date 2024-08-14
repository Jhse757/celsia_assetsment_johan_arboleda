using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using celsia_assetsment_johan_arboleda.App.Interfaces;
using celsia_assetsment_johan_arboleda.Dtos;
using celsia_assetsment_johan_arboleda.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace celsia_assetsment_johan_arboleda.App.Services
{
    public class TransactionService : ITransaction
    {

        // Inyección de la base del ManagementContext

        private readonly ManagementContext _context;
        private readonly ILogger<TransactionService> _logger;
        public TransactionService(ManagementContext context, ILogger<TransactionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<TransactionDto>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                .Include(t => t.Invoice)
                .Include(t => t.Platform)
                .Include(t => t.DocumentType)
                .Select(t => new TransactionDto
                {
                    TransactionId = t.IdTransaction,
                    InvoiceId = t.InvoiceId,
                    CustomerName = t.DocumentType.Name, 
                    TransactionTime = t.CreateAt,
                    PlatformName = t.Platform.Name,
                    TransactionStatus = t.Status
                })
                .ToListAsync();
        }
    }
}