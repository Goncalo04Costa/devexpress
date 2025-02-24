using System.ComponentModel.DataAnnotations;

namespace BlazorApp_empty1.Classes
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
        public string Departamento { get; set; }



        public string FicheirosNames => string.Join(", ", Ficheiros.Select(f => f.FileName));
        public List<UploadedFile> Ficheiros { get; set; } = new();

    }

    public class UploadedFile
    {
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public string FileType { get; set; }

        public string TicketId { get; set; }
        public Ticket Ticket { get; set; }

    }
}

