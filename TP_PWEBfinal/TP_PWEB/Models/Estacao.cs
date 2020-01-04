using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
    public class Estacao
    {
        [Key]
        public int EstacaoID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome Estação")]
        public string NomeEstacao { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:n} €")]
        [Display(Name = "Custo p/ Minuto")]
        public float CustoPMinuto { get; set; }

        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }

        [Required]
        [Display(Name = "Nº de Postos")]
        public int NPostos { get; set; }

        [Display(Name = "Nome Empresa Proprietária")] 
        public string EmpresaNome { get; set; }
        public Empresa Empresa { get;set; }

        [Required]
        public List<Posto> Postos { get; set; }



        


    }
}