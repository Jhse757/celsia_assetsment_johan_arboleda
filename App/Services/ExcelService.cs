using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using celsia_assetsment_johan_arboleda.App.Interfaces;
using celsia_assetsment_johan_arboleda.Infraestructure.Context;
using celsia_assetsment_johan_arboleda.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace celsia_assetsment_johan_arboleda.App.Services
{
    public class ExcelService : IExcel
    {
        // Inyección de la base del ManagementContext

        private readonly ManagementContext _context;
        private readonly ILogger<ExcelService> _logger;
        public ExcelService(ManagementContext context, ILogger<ExcelService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task ImportDataAsync(Stream fileStream)
        {
            using (ExcelPackage package = new ExcelPackage(fileStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // Listas para almacenar las entidades importadas
                List<DocumentType> documentTypes = new List<DocumentType>();
                List<Prefix> prefixes = new List<Prefix>();
                List<Customer> customers = new List<Customer>();
                List<Invoice> invoices = new List<Invoice>();
                List<Platform> platforms = new List<Platform>();
                List<TransactionType> transactionTypes = new List<TransactionType>();
                List<Transaction> transactions = new List<Transaction>();
                List<InvoiceTransaction> invoiceTransactions = new List<InvoiceTransaction>();

                int rowCount = worksheet.Dimension.Rows;

                // Cargar datos existentes en diccionarios
                var existingDocumentTypes = await _context.DocumentTypes.ToDictionaryAsync(dt => dt.Name);
                var existingPrefixes = await _context.Prefixes.ToDictionaryAsync(p => p.Name);
                var existingCustomers = await _context.Customers.ToDictionaryAsync(c => c.Email);
                var existingInvoices = await _context.Invoices.ToDictionaryAsync(i => i.InvoiceNumber);
                var existingPlatforms = await _context.Platforms.ToDictionaryAsync(p => p.Name);
                var existingTransactionTypes = await _context.TransactionTypes.ToDictionaryAsync(tt => tt.Name);
                var existingTransactions = await _context.Transactions.ToDictionaryAsync(t => t.PaymentReference);
                var existingInvoiceTransactions = await _context.InvoiceTransactions.ToDictionaryAsync(it => it.IdInvoiceTransaction);

                // Procesar cada fila del archivo Excel
                for (int row = 2; row <= rowCount; row++)
                {
                    #region DocumentType
                    string documentTypeName = worksheet.Cells[row, 1].Text;
                    if (!existingDocumentTypes.TryGetValue(documentTypeName, out var documentType))
                    {
                        documentType = new DocumentType
                        {
                            Name = documentTypeName,
                            Status = "Active"
                        };
                        documentTypes.Add(documentType);
                        existingDocumentTypes[documentTypeName] = documentType;
                    }
                    #endregion

                    #region Prefix
                    string prefixName = worksheet.Cells[row, 2].Text;
                    if (!existingPrefixes.TryGetValue(prefixName, out var prefix))
                    {
                        prefix = new Prefix
                        {
                            Name = prefixName,
                            Status = "Active"
                        };
                        prefixes.Add(prefix);
                        existingPrefixes[prefixName] = prefix;
                    }
                    #endregion

                    #region Customer
                    string customerEmail = worksheet.Cells[row, 3].Text;
                    if (!existingCustomers.TryGetValue(customerEmail, out var customer))
                    {
                        var documentTypeId = int.Parse(worksheet.Cells[row, 4].Text);
                        var telephonePrefixId = int.TryParse(worksheet.Cells[row, 5].Text, out var prefixId) ? (int?)prefixId : null;

                        customer = new Customer
                        {
                            Name = worksheet.Cells[row, 6].Text,
                            LastName = worksheet.Cells[row, 7].Text,
                            TypeDocumentId = documentTypeId,
                            NumberDocument = worksheet.Cells[row, 8].Text,
                            Email = customerEmail,
                            TelephonePrefixId = telephonePrefixId,
                            Phone = worksheet.Cells[row, 9].Text,
                            Status = "Active"
                        };
                        customers.Add(customer);
                        existingCustomers[customerEmail] = customer;
                    }
                    #endregion

                    #region Invoice
                    string invoiceNumber = worksheet.Cells[row, 10].Text;
                    if (!existingInvoices.TryGetValue(invoiceNumber, out var invoice))
                    {
                        invoice = new Invoice
                        {
                            InvoiceNumber = invoiceNumber,
                            BillingYear = int.Parse(worksheet.Cells[row, 11].Text),
                            BillingMonth = int.Parse(worksheet.Cells[row, 12].Text),
                            BilledAmount = decimal.Parse(worksheet.Cells[row, 13].Text),
                            CustomerId = int.Parse(worksheet.Cells[row, 14].Text),
                            Status = "Active"
                        };
                        invoices.Add(invoice);
                        existingInvoices[invoiceNumber] = invoice;
                    }
                    #endregion

                    #region Platform
                    string platformName = worksheet.Cells[row, 15].Text;
                    if (!existingPlatforms.TryGetValue(platformName, out var platform))
                    {
                        platform = new Platform
                        {
                            Name = platformName,
                            Status = "Active"
                        };
                        platforms.Add(platform);
                        existingPlatforms[platformName] = platform;
                    }
                    #endregion

                    #region TransactionType
                    string transactionTypeName = worksheet.Cells[row, 16].Text;
                    if (!existingTransactionTypes.TryGetValue(transactionTypeName, out var transactionType))
                    {
                        transactionType = new TransactionType
                        {
                            Name = transactionTypeName,
                            Status = "Active"
                        };
                        transactionTypes.Add(transactionType);
                        existingTransactionTypes[transactionTypeName] = transactionType;
                    }
                    #endregion

                    #region Transaction
                    string paymentReference = worksheet.Cells[row, 17].Text;
                    if (!existingTransactions.TryGetValue(paymentReference, out var transaction))
                    {
                        transaction = new Transaction
                        {
                            PaymentReference = paymentReference,
                            PayerTypeDocumentId = int.Parse(worksheet.Cells[row, 18].Text),
                            PayerIdentification = worksheet.Cells[row, 19].Text,
                            PayerName = worksheet.Cells[row, 20].Text,
                            TransactionAmount = decimal.Parse(worksheet.Cells[row, 21].Text),
                            TransactionTypeId = int.Parse(worksheet.Cells[row, 22].Text),
                            PlatformId = int.Parse(worksheet.Cells[row, 23].Text),
                            InvoiceId = int.Parse(worksheet.Cells[row, 24].Text),
                            Status = "Active"
                        };
                        transactions.Add(transaction);
                        existingTransactions[paymentReference] = transaction;
                    }
                    #endregion

                    #region InvoiceTransaction
                    int invoiceId = int.Parse(worksheet.Cells[row, 25].Text);
                    int transactionId = int.Parse(worksheet.Cells[row, 26].Text);

                    var existingInvoiceTransaction = existingInvoiceTransactions.Values
                        .FirstOrDefault(it => it.InvoiceId == invoiceId && it.TransactionId == transactionId);

                    if (existingInvoiceTransaction == null)
                    {
                        var invoiceTransaction = new InvoiceTransaction
                        {
                            InvoiceId = invoiceId,
                            TransactionId = transactionId
                        };
                        invoiceTransactions.Add(invoiceTransaction);
                        existingInvoiceTransactions[invoiceTransaction.IdInvoiceTransaction] = invoiceTransaction;
                    }
                    #endregion

                // Guardar todas las entidades en la base de datos
                if (documentTypes.Any())
                {
                    _context.DocumentTypes.AddRange(documentTypes);
                }
                if (prefixes.Any())
                {
                    _context.Prefixes.AddRange(prefixes);
                }
                if (customers.Any())
                {
                    _context.Customers.AddRange(customers);
                }
                if (invoices.Any())
                {
                    _context.Invoices.AddRange(invoices);
                }
                if (platforms.Any())
                {
                    _context.Platforms.AddRange(platforms);
                }
                if (transactionTypes.Any())
                {
                    _context.TransactionTypes.AddRange(transactionTypes);
                }
                if (transactions.Any())
                {
                    _context.Transactions.AddRange(transactions);
                }
                if (invoiceTransactions.Any())
                {
                    _context.InvoiceTransactions.AddRange(invoiceTransactions);
                }

                await _context.SaveChangesAsync();
            }
        }
        }
    }
}