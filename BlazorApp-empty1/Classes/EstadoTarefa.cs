using System.ComponentModel.DataAnnotations;

namespace BlazorApp_empty1.Classes
{
    /// <summary>
    /// Enumeração que representa os estados possíveis de uma tarefa ou ticket.
    /// </summary>
    public enum EstadoTarefa
    {
        [Display(Name = "Todos")]
        Todos = -1,

        [Display(Name = "Por Iniciar")]
        PorIniciar = 0,

        [Display(Name = "Em Curso")]
        EmCurso = 1,

        [Display(Name = "Concluído")]
        Concluido = 2,

        [Display(Name = "Publicado")]
        Publicado = 3,

        [Display(Name = "Arquivado")]
        Arquivado = 4,

        [Display(Name = "Em Espera")]
        EmEspera = 5
    }
}
