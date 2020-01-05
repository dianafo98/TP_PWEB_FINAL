using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace TP_PWEB.Models
{
	public class VerificaValidade : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var utilizador = (RegisterViewModel)validationContext.ObjectInstance;

            //se não for numero de mês válido ou ano menor que o atual
            if (utilizador.CartaoValMonth > 12 || utilizador.CartaoValMonth < 0
                || utilizador.CartaoValYear < DateTime.Today.Year) 
				return new ValidationResult("Data de validade do cartão inválida.");

			var validadeY = utilizador.CartaoValYear - DateTime.Today.Year;

            //se o ano for igual ou maior ao atual
            if (validadeY >= 0)
			{
				var validadeM = utilizador.CartaoValMonth - DateTime.Today.Month;
                //se o mês for maior ao atual
                if (validadeM > 0) 
				    return ValidationResult.Success;
                else
                    return new ValidationResult("Cartão com mês expirado.");
            }	
            else
                return new ValidationResult("Cartão com data de validade expirado.");
		}
	}
}