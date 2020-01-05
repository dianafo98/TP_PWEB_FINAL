using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
    public class Empresa
    {
        [Key]
        public int EmpresaID { get; set; }

        public string ID { get; set; }
        [Required]
        [Display(Name = "Nome Empresa")]
        public string NomeEmpresa { get; set;}

        [Required]
        [Display(Name = "Endereço Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string PerfilID { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "A {0} tem de ter pelo menos {2} caracteres.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Palavra-Passe")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Repita a Palavra-Passe")]
        //[Compare("Password", ErrorMessage = "As Palavra-Passe não coincidem.")]
        //public string ConfirmPassword { get; set; }

    }
}