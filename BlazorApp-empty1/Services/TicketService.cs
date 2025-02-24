using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlazorApp_empty1.Classes;

namespace BlazorApp_empty1.Services
{

    public class TicketService
    {
        private static readonly Random random = new();

        private readonly List<Ticket> tickets = new()
{
    new Ticket
    {
        Id = GenerateFixedRandomId("John Doe"),
        Nome = "John Doe",
        Empresa = "Tech Corp",
        Email = "johndoe@example.com",
        Telefone = "123456789",
        Assunto = "Technical Issue",
        Date = DateTime.Now.AddDays(-2),
        Estado = EstadoTarefa.PorIniciar,
        Departamento = "Financeiro",
        Ficheiros = new List<UploadedFile>
        {
            new UploadedFile
            {
                FileName = "issue_report.pdf",
                FileType = "application/pdf",
                FileData = new byte[] { }
            }
        }
    },
    new Ticket
    {
        Id = GenerateFixedRandomId("Alice Smith"),
        Nome = "Alice Smith",
        Empresa = "Health Inc.",
        Email = "alice@example.com",
        Telefone = "987654321",
        Assunto = "Billing Inquiry",
        Date = DateTime.Now,
        Estado = EstadoTarefa.Concluido,
        Departamento = "Financeiro",
        Ficheiros = new List<UploadedFile>()
    },
   
};

        private static string GenerateFixedRandomId(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes.Take(8).ToArray()).Replace("-", "").ToLower();
            }
        }

        private string GenerateUniqueId() => Guid.NewGuid().ToString("N");

        public Task<Ticket[]> GetTicketsAsync(DateOnly startDate)
        {

            return Task.FromResult(tickets.ToArray());
        }

        public Task AddNewTicketAsync(Ticket newTicket)
        {
            if (newTicket == null)
                throw new ArgumentNullException(nameof(newTicket), "O ticket não pode ser nulo.");
            if (string.IsNullOrEmpty(newTicket.Id))
            {
                newTicket.Id = GenerateFixedRandomId(newTicket.Nome);
            }

            newTicket.Date = DateTime.Now;
            tickets.Add(newTicket);
            return Task.CompletedTask;
        }


        public Task UpdateTicketStateAsync(string ticketId, EstadoTarefa newState)
        {
            var ticket = tickets.FirstOrDefault(t => t.Id.Equals(ticketId, StringComparison.OrdinalIgnoreCase));

            if (ticket == null)
            {
                throw new InvalidOperationException($"Ticket com ID {ticketId} não encontrado.");
            }

            ticket.Estado = newState;


            return Task.CompletedTask;
        }


        public Task<Ticket[]> SearchTicketsByClientContactAsync(string telefone)
        {
            var result = string.IsNullOrWhiteSpace(telefone)
                ? Array.Empty<Ticket>()
                : tickets.Where(t => t.Telefone.Contains(telefone, StringComparison.OrdinalIgnoreCase)).ToArray();

            return Task.FromResult(result);
        }

        public Task<Ticket[]> SearchTicketsByCodeAsync(string codigo)
        {
            var result = string.IsNullOrWhiteSpace(codigo)
                ? Array.Empty<Ticket>()
                : tickets.Where(t => t.Id.Equals(codigo, StringComparison.OrdinalIgnoreCase)).ToArray();

            return Task.FromResult(result);
        }
    }
}
