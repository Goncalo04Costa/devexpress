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
        private readonly List<Ticket> tickets = new();
        private readonly Random rng = new();

        // Método para gerar um ID único e aleatório
        private string GenerateUniqueId(int length = 16)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            using var crypto = RandomNumberGenerator.Create();
            var data = new byte[length];

            crypto.GetBytes(data);

            var result = new StringBuilder(length);
            foreach (var b in data)
            {
                result.Append(chars[b % chars.Length]);
            }

            return result.ToString();
        }

        // Método que preenche a lista com tickets de exemplo
        public async Task<Ticket[]> GetTicketsAsync(DateOnly startDate)
        {
            if (tickets.Count == 0)
            {
                var estados = Enum.GetValues<EstadoTarefa>();

                tickets.AddRange(Enumerable.Range(1, 20).Select(index => new Ticket
                {
                    Id = GenerateUniqueId(),  // Gerar um ID único para cada ticket
                    Date = startDate.AddDays(index),
                    Nome = $"Nome {index}",
                    Email = $"client{index}@email.com",
                    Telefone = $"Phone {index}",
                    Assunto = $"Assunto do Ticket {index}",
                    Estado = estados.ElementAt(rng.Next(estados.Length))
                }));
            }
            return await Task.Run(() => tickets.ToArray());
        }


        public async Task AddNewTicketAsync(Ticket newTicket)
        {
            if (newTicket == null)
            {
                throw new ArgumentNullException(nameof(newTicket), "O ticket não pode ser nulo.");
            }

            newTicket.Id = GenerateUniqueId();  
            newTicket.Date = DateOnly.FromDateTime(DateTime.UtcNow);

            await Task.Run(() => tickets.Add(newTicket));
        }


        public async Task<Ticket[]> SearchTicketsByClientContactAsync(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
            {
                return Array.Empty<Ticket>();
            }

            return await Task.Run(() =>
                tickets.Where(ticket => ticket.Telefone.Equals(telefone, StringComparison.OrdinalIgnoreCase)).ToArray()
            );
        }



      
        public async Task<Ticket[]> SearchTicketsByCOdetAsync(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return Array.Empty<Ticket>();
            }

            return await Task.Run(() =>
                tickets.Where(ticket => ticket.Id.Equals(codigo, StringComparison.OrdinalIgnoreCase)).ToArray()
            );
        }
    }
}
