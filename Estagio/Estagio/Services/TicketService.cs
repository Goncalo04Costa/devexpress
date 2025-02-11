namespace Estagio.Services
{
    public class TicketService
    {
        private static readonly string[] States = new[]
        {
            "Open", "Closed", "Stopped"
        };

        private readonly List<Ticket> tickets = new();

        // Este método preenche a lista com tickets de exemplo
        public Task<Ticket[]> GetTicketsAsync(DateOnly startDate)
        {
            var rng = new Random();
            if (tickets.Count == 0)
            {
                tickets.AddRange(Enumerable.Range(1, 20).Select(index => new Ticket
                {
                    Date = startDate.AddDays(index),
                    Title = $"Ticket {index}",
                    ClientContact = $"client{index}@email.com",
                    State = States[rng.Next(States.Length)]
                }));
            }
            return Task.FromResult(tickets.ToArray());
        }

        // Método para adicionar um novo ticket
        public Task AddNewTicket(Ticket newTicket)
        {
            if (newTicket == null)
            {
                throw new ArgumentNullException(nameof(newTicket), "The ticket cannot be null.");
            }

            newTicket.Date = DateOnly.FromDateTime(DateTime.Now); // Define a data atual
            tickets.Add(newTicket); // Adiciona o ticket à lista
            return Task.CompletedTask;
        }
    }
}
