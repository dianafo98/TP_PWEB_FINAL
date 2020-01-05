using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TP_PWEB.Models;
using TP_PWEB.ViewModels;

namespace TP_PWEB.Controllers
{
    public class ReservasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservas
        public ActionResult Index()

        {
            if (User.IsInRole(TipoPerfil.User))
            {
                var id = User.Identity.GetUserId();
                var reservaCliente = db.Reservas.Include(x => x.EstacaoReservada).Include(x => x.PostoReservado).Where(x => x.Utilizador.ID == id);
                return View("IndexCliente", reservaCliente.ToList());
            }
            if (User.IsInRole(TipoPerfil.Company))
            {
                var id = User.Identity.GetUserId();
                var reservaEmpresa = db.Reservas.Include(x => x.EstacaoReservada).Include(x => x.PostoReservado).Where(x => x.EstacaoReservada.Empresa.ID == id);
                return View("IndexEmpresa", reservaEmpresa.ToList());
            }
            var reservas = db.Reservas.Include(x => x.EstacaoReservada).Include(x => x.PostoReservado).Include(x => x.Utilizador);
            return View(reservas.ToList());
        }
       

        // GET: Reservas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole(TipoPerfil.User))
            {
                return View("DetalhesUtilizador", reserva);
            }
            if (User.IsInRole(TipoPerfil.Company))
            {
                return View("DetalhesEmpresa", reserva);
            }
                return View(reserva);
        }
        private List<Estacao> GetEstacoesDisponiveis()
        {
            List<Estacao> estacoes = new List<Estacao>();
            var lista = db.Estacoes
                    .GroupJoin(db.Reservas,
                               e => e.EstacaoID,
                               p => p.PostoID,
                               (e, p) => new { estacao= e, posto = p });
            foreach (var e in lista)
            {
                if (e.posto.Count() > 0)
                {
                    var rpLista = e.posto.Where(p => p.PostoReservado.Disponibilidade == true);
                    foreach (var e1 in rpLista)
                        estacoes.Add(e1.EstacaoReservada);
                }
                else
                {
                    estacoes.Add(e.estacao);
                }
            }
            return estacoes;
        }

        // GET: Reservas/Create
        [Authorize(Roles = "admin,Cliente")]
        public ActionResult Create()
        {
            ViewBag.EstacaoID = new SelectList(db.Estacoes, "ID", "Nome");
            if (User.IsInRole(TipoPerfil.User))
            {

                var ID = User.Identity.GetUserId();
                var reserva = new Reserva { UtilizadorID =  ID};

                return View("CreateCliente", reserva);
            }
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DataReserva,HoraReserva,Duracao,EstacaoNome,PostoID, UtilizadorID")] Reserva reserva)
        {
            reserva.Custo= reserva.CustoPrevisto();
            reserva.Utilizador = db.Utilizadores.Where(i => i.ID == reserva.UtilizadorID).SingleOrDefault();
            reserva.EstacaoReservada = db.Estacoes.Where(i => i.NomeEstacao == reserva.EstacaoNome).SingleOrDefault();
            reserva.PostoReservado = db.Postos.Where(i => i.PostoID == reserva.PostoID).SingleOrDefault();

            if (reserva.Utilizador.Dinheiro - reserva.Custo < 0)
                ModelState.AddModelError("Utilizador.Dinheiro", "Fundos Insuficientes!");
            if( DateTime.Compare(reserva.DataReserva,DateTime.Today)<0)
                ModelState.AddModelError("DataReserva", "Data invalida!");
            if (ModelState.IsValid)
            {
                reserva.Utilizador.Dinheiro -= reserva.Custo;
                reserva.ReservaConfirmada = true;
                db.Reservas.Add(reserva);
                db.SaveChanges();
                return RedirectToAction("Details/" + reserva.CodigoServico);
            }

            return View(reserva);
        }

        // GET: Reservas/Edit/5
        [Authorize(Roles = "admin,Cliente")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estacao = new SelectList(db.Estacoes, "ID","Nome Estação",reserva.EstacaoID);
            ViewBag.Posto = new SelectList(db.Postos,"ID","Disponibilidade",reserva.PostoReservado);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoServico,DataReserva,Duracao,CustoPrevisto,ReservaConfirmada")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        [Authorize(Roles = "admin,Cliente")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Reserva reserva = db.Reservas.Find(id);
            db.Reservas.Remove(reserva);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //public ActionResult CancelaReserva(int id, string nome)
        //{
        //    var estacao = _context.Estacoes.SingleOrDefault(e=> e.NomeEstacao==nome && e.Postos.Exists(x=> x.PostoID==id));

        //    reservas.estacao.Postos.Find(x => x.PostoID == id).Disponibilidade = true;
        //    estacao.AluguerAceite = false;

        //    _context.SaveChanges();

        //    return RedirectToAction("ListaVeiculosAluguer");
        //}
    }
}
