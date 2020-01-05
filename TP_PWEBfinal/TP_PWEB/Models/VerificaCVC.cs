using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace TP_PWEB.Models
{
    public class VerificaCVC : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var utilizador = (RegisterViewModel)validationContext.ObjectInstance;

			var tamanho = utilizador.CartaoCVC.ToString().Length;

			if (tamanho == 3)
				return ValidationResult.Success;
			else
				return new ValidationResult("O CVC é composto por 3 digitos.");
		}
	}
}
