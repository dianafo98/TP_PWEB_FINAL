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

        public int UtilizadorID { get; set; }
        public Utilizador Utilizador { get; set; }

        public int EstacaoID { get; set; }
        public Estacao EstacaoReservada { get; set; }
        public int PostoID { get; set; }
        public Posto PostoReservado { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data Reserva")]
        public DateTime DataReserva { get; set; }

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