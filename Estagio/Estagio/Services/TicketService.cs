using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Estagio.Services
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
        Ficheiros = new List<UploadedFile>() 
    },
    new Ticket
    {
        Id = GenerateFixedRandomId("Peter Parker"),
        Nome = "Peter Parker",
        Empresa = "Daily Bugle",
        Email = "peterparker@example.com",
        Telefone = "111222333",
        Assunto = "Web Design Issue",
        Date = DateTime.Now.AddDays(-1),
        Estado = EstadoTarefa.EmCurso,
        Ficheiros = new List<UploadedFile>() 
    },
    new Ticket
    {
        Id = GenerateFixedRandomId("Bruce Wayne"),
        Nome = "Bruce Wayne",
        Empresa = "Wayne Enterprises",
        Email = "brucewayne@example.com",
        Telefone = "444555666",
        Assunto = "System Outage",
        Date = DateTime.Now.AddDays(-3),
        Estado = EstadoTarefa.Concluido,
        Ficheiros = new List<UploadedFile>()
    },
    new Ticket
    {
        Id = GenerateFixedRandomId("Clark Kent"),
        Nome = "Clark Kent",
        Empresa = "Daily Planet",
        Email = "clarkkent@example.com",
        Telefone = "777888999",
        Assunto = "Printing Error",
        Date = DateTime.Now.AddDays(-4),
        Estado = EstadoTarefa.PorIniciar,
        Ficheiros = new List<UploadedFile>() 
    },
    new Ticket
    {
        Id = GenerateFixedRandomId("Diana Prince"),
        Nome = "Diana Prince",
        Empresa = "Themyscira",
        Email = "dianaprince@example.com",
        Telefone = "222333444",
        Assunto = "Database Issue",
        Date = DateTime.Now.AddDays(-1),
        Estado = EstadoTarefa.EmCurso,
        Ficheiros = new List<UploadedFile>()
    },
    new Ticket
    {
        Id = GenerateFixedRandomId("Tony Starks"),
        Nome = "Tony Stark",
        Empresa = "Stark Industries",
        Email = "tonystark@example.com",
        Telefone = "555666777",
        Assunto = "Hardware Failure",
        Date = DateTime.Now.AddDays(-5),
        Estado = EstadoTarefa.Concluido,
        Ficheiros = new List<UploadedFile>()
    },
    new Ticket
    {
        Id = GenerateFixedRandomId("Steve Rogers"),
        Nome = "Steve Rogers",
        Empresa = "Avengers Inc.",
        Email = "steverogers@example.com",
        Telefone = "888999000",
        Assunto = "Login Problem",
        Date = DateTime.Now.AddDays(-2),
        Estado = EstadoTarefa.PorIniciar,
        Ficheiros = new List<UploadedFile>()
    },
    new Ticket
    {
        Id = GenerateFixedRandomId("Natasha Romanoff"),
        Nome = "Natasha Romanoff",
        Empresa = "S.H.I.E.L.D",
        Email = "natasha@example.com",
        Telefone = "333444555",
        Assunto = "Email Issue",
        Date = DateTime.Now.AddDays(-1),
        Estado = EstadoTarefa.EmCurso,
        Ficheiros = new List<UploadedFile>() 
    },
    new Ticket
    {
        Id = GenerateFixedRandomId("Barry Allen"),
        Nome = "Barry Allen",
        Empresa = "Central City Labs",
        Email = "barryallen@example.com",
        Telefone = "666777888",
        Assunto = "Network Lag",
        Date = DateTime.Now.AddDays(-2),
        Estado = EstadoTarefa.Concluido,
        Ficheiros = new List<UploadedFile>() 
    }
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

            // Se o ID não foi atribuído, gera um ID fixo e aleatório baseado no nome
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
