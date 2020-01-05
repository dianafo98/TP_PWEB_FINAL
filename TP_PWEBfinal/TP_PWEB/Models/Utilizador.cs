
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace TP_PWEB.Models
{
    public class Utilizador
    {
        [Key]
        public int UtilizadorID { get; set; }

        public string PerfilID { get; set; }
        public string ID { get; set; }

        [Required]
        [StringLength(255)]
        public string NomeUsr { get; set; }

        [Required]
       // [VerificaIdade]
        public DateTime? DataNasc { get; set; }

        [Required]
        public int Telefone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool MetodoPagamento { get; set; }
        [Required]
       // [VerificaNCartao]
        public int CartaoNum { get; set; }

        [Required]
       /// [VerificaValidade]
		public int CartaoValMonth { get; set; }
        public int CartaoValYear { get; set; }
    
        
		[Required]
		//[VerificaCVC]
		public int CartaoCVC { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O valor tem de ser maior do que 0")]
        public double Dinheiro { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "A {0} tem de ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Palavra-Passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repita a Palavra-Passe")]
        [Compare("Password", ErrorMessage = "As Palavra-Passe não coincidem.")]
        public string ConfirmPassword { get; set; }


    }
}
