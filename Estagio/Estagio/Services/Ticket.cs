using System;
using System.ComponentModel.DataAnnotations;
using DevExpress.Printing.Core.Native;
using Microsoft.AspNetCore.Http;


namespace Estagio.Services
{
    public enum EstadoTarefa
    {
        [Display(Name = "Todos")]
        Todos = -1,

        [Display(Name = "Por Iniciar")]
        PorIniciar = 0,

        [Display(Name = "Em Curso")]
        EmCurso,

        [Display(Name = "Concluído")]
        Concluido,

        [Display(Name = "Publicado")]
        Publicado,

        [Display(Name = "Arquivadas")]
        Arquivado,

        [Display(Name = "Em espera")]
        EmEspera
    }

    public class Ticket
    {
        [Key]
        public string Id { get; set; } 

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required, MaxLength(100)]
        public string Empresa { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Telefone { get; set; }

        [Required, MaxLength(200)]
        public string Assunto { get; set; }

        [Required]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public IFormFile? Ficheiro { get; set; }

        [Required]
        public EstadoTarefa Estado { get; set; } = EstadoTarefa.PorIniciar;
    }
}
