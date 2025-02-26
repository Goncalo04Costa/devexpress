using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp_empty1.Classes
{
    public enum UserRole
    {
        Admin,
        User
    }

    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public UserRole Role { get; set; } // Enum para garantir valores válidos

        // Relacionamento 1:N - Um usuário pode fazer vários comentários
        public List<Comentario> Comentarios { get; set; }

        // Construtor para inicializar ID e lista de comentários
        public User()
        {
            Id = Guid.NewGuid().ToString();
            Comentarios = new List<Comentario>();
        }
    }
}
