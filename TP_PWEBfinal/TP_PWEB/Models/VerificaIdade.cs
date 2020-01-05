using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace TP_PWEB.Models
{
    public class VerificaIdade : ValidationAttribute
    {
		protected override ValidationResult IsValid(object date, ValidationContext validationContext)
		{
			var utilizador = validationContext.ObjectInstance;
			var data = (DateTime)date;
			if (utilizador == null || data==null) //se não inserir
				return new ValidationResult("Data de nascimento necessária.");

			var idade = DateTime.Today.Year - data.Year;

			if (idade >= 18) //se for >18
				return ValidationResult.Success;
			else //se <18
				return new ValidationResult("Utilizador deve ter pelo menos 18 anos de idade");
		}
	}
}