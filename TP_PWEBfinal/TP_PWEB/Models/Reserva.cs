using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace TP_PWEB.Models
{
    public class Reserva
    {
        [Key]
        [Required]
        [Display(Name = "Código de Serviço")]
        public string CodigoServico { get; set; }

        public string UtilizadorID { get; set; }
        public string UtilizadorNome{ get; set; }
        public Utilizador Utilizador { get; set; }

        public int EstacaoID { get; set; }
        public string EstacaoNome { get; set; }
        public Estacao EstacaoReservada { get; set; }
        public int PostoID { get; set; }
        public Posto PostoReservado { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Reserva")]
        public DateTime DataReserva { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0: H:mm}")]
        [Display(Name = "Hora Reserva")]
        public DateTime HoraReserva { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0: H:mm}")]
        [Display(Name = "Duração")]
        public TimeSpan Duracao { get; set; }
        public float Custo { get; set; }

        [Required]
        [Display(Name = "Reserva Confimada")]
        public bool ReservaConfirmada { get; set; }

        public float CustoPrevisto() => EstacaoReservada.CustoPMinuto * (float)Duracao.TotalMinutes;



    }

}