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
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Assunto { get; set; }
        public EstadoTarefa Estado { get; set; }


    }

}
