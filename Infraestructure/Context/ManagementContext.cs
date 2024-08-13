using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using celsia_assetsment_johan_arboleda.Models;
using Microsoft.EntityFrameworkCore;

namespace celsia_assetsment_johan_arboleda.Infraestructure.Context
{
    public class ManagementContext : DbContext
    {
        public ManagementContext(DbContextOptions<ManagementContext> options) : base(options)
        {
        }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Prefix> Prefixes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<InvoiceTransaction> InvoiceTransactions { get; set; }
    }
}