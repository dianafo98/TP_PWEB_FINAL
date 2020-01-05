using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TP_PWEB.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {   
        [Required]
        [Display(Name = "Tipo Perfil")]
        public string TipoPerfil { get; set; }
       
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nome")]
        public string NomeUsr { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} tem de ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Palavra-Passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repita a Palavra-Passe")]
        [Compare("Password", ErrorMessage = "As Palavra-Passe não coincidem.")]
        public string ConfirmPassword { get; set; }

     
        //[VerificaNCartao]
        [Display(Name = "Número do Cartão de Crédito")]
        public int CartaoNum { get; set; }

       
        [Display(Name = "Validade do Cartão")]
        //[VerificaValidade]
        [Range(/*DateTime.Today.Month*/1, 12, ErrorMessage = "Mês Invalido")]
        public int CartaoValMonth { get; set; }
        [Range(/*DateTime.Today.Year*/2020, 2025, ErrorMessage = "Ano Invalido")]
        public int CartaoValYear { get; set; }

        

       
        [Display(Name = "CVC")]
        [VerificaCVC]
        public int CartaoCVC { get; set; }

        
        [Display(Name = "Data de Nascimento")]
        //[VerificaIdade]
        [DataType(DataType.Date)]
        public DateTime? DataNasc { get; set; }

        
        [Display(Name = "Número de telemóvel")]
        public int Telefone { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "O valor tem de ser maior do que 0")]
        public double Dinheiro { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
