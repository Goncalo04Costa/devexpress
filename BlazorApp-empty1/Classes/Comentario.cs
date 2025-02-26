using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp_empty1.Classes
{
    public class Comentario
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(1000, ErrorMessage = "O comentário não pode ter mais de 1000 caracteres.")]
        public string Conteudo { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DataCriacao { get; set; }

        // Chave estrangeira para User (Quem fez o comentário)
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        // Chave estrangeira para Ticket (Comentário pertence a um Ticket)
        [Required]
        public string TicketId { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }

        // Construtor para garantir que a data seja sempre definida corretamente
        public Comentario()
        {
            DataCriacao = DateTime.UtcNow;
        }
    }
}
