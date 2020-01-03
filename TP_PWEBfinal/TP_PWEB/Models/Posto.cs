using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PWEB.Models
{
    public class Posto
    {
        [Display(Name = "Posto")]
        public int PostoID { get; set; }

        public string UtilizadorID { get; set; }
        public Utilizador Utilizador { get; set; } //o posto pode ter um utilizador apenas
        [Required]
        public bool Disponibilidade { get; set; }

    }
}