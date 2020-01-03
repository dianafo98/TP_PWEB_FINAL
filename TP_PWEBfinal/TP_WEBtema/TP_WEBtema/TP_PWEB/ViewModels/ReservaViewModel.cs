using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP_PWEB.Models;

namespace TP_PWEB.ViewModels
{
    public class ReservaViewModel
    {
        public List<Utilizador> Utilizadores { get; set; }
        public List<Estacao> Estacoes { get; set; }
        public int UtilizadorID { get; set; }

    }
}