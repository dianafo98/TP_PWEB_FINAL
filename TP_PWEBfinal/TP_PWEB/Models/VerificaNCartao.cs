
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace TP_PWEB.Models
{
	public class VerificaNCartao : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var utilizador = (RegisterViewModel)validationContext.ObjectInstance;

			if (utilizador.CartaoNum == 0) //se não inserir
				return new ValidationResult("Número do cartão necessário.");

			var tamanho = utilizador.CartaoNum.ToString().Length;

            if (tamanho == 16)
				return ValidationResult.Success;
            else
				return new ValidationResult("Numero de cartão é composto por 16 digitos");
		}
	}
}