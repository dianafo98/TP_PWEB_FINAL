using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
                var nome = User.Identity.Name;
                var reservaCliente = db.Reservas.Include(x => x.EstacaoReservada).Include(x => x.PostoReservado).Include(x => x.Utilizador.NomeUsr == nome);
                return View("IndexCliente", reservaCliente.ToList());
            }
            if (User.IsInRole(TipoPerfil.User))
            {
                var nome = User.Identity.Name;
                var reservaCliente = db.Reservas.Include(x => x.EstacaoReservada).Include(x => x.PostoReservado).Include(x => x.Utilizador.NomeUsr == nome);
                return View("IndexCliente", reservaCliente.ToList());
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
            return View(reserva);
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoServico,DataReserva,Duracao,EstacaoID,PostoID, UtilizadorID")] Reserva reserva)
        {
            reserva.Custo= reserva.CustoPrevisto();
            reserva.Utilizador = db.Utilizadores.Where(i => i.UtilizadorID == reserva.UtilizadorID).SingleOrDefault();
            reserva.EstacaoReservada = db.Estacoes.Where(i => i.EstacaoID == reserva.EstacaoID).SingleOrDefault();
            reserva.PostoReservado = db.Postos.Where(i => i.PostoID == reserva.PostoID).SingleOrDefault();


            if (ModelState.IsValid)
            {
                reserva.Utilizador.Dinheiro -= reserva.Custo;
                db.Reservas.Add(reserva);
                db.SaveChanges();
                return RedirectToAction("Details/" + reserva.CodigoServico);
            }

            return View(reserva);
        }

        // GET: Reservas/Edit/5
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
