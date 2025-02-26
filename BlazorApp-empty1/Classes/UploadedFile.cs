using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp_empty1.Classes
{
    public class UploadedFile
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "O nome do ficheiro não pode ter mais de 255 caracteres.")]
        public string FileName { get; set; }

        [Required]
        [Column(TypeName = "varbinary(max)")]
        public byte[] FileData { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O tipo do ficheiro não pode ter mais de 100 caracteres.")]
        public string FileType { get; set; }

        // Chave estrangeira para Ticket
        [Required]
        public string TicketId { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }


        public UploadedFile()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
