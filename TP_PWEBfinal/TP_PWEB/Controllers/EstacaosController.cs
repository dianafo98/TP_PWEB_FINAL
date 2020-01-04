using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TP_PWEB.Models;

namespace TP_PWEB.Controllers
{
    public class EstacaosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Estacaos
        public ActionResult Index()
        {
            return View(db.Estacoes.ToList());
        }

        // GET: Estacaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estacao estacao = db.Estacoes.Find(id);
            if (estacao == null)
            {
                return HttpNotFound();
            }
            return View(estacao);
        }

        // GET: Estacaos/Create
        [Authorize(Roles = TipoPerfil.Company)]
        [Authorize(Roles = TipoPerfil.Admin)]
        public ActionResult Create()
        {
            return View();
        }

        [NonAction]
        public static List<Posto> InicializaPostos(int tam)
        {
            //int i = 0;
            //return Enumerable.Repeat(new Posto(i++,null,null,true))
            List<Posto> p = new List<Posto>(tam);
            for (int i = 0; i < tam; i++)
                p.Add(new Posto() { PostoID = i++, Disponibilidade = true });
            return p;
        }

        // POST: Estacaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstacaoID,NomeEstacao,CustoPMinuto,Localizacao,NPostos,EmpresaNome")] Estacao estacao)
        {
            if (ModelState.IsValid)
            {
                List<Posto> p = InicializaPostos(estacao.NPostos);
                estacao.Postos = p;
                estacao.Empresa = db.Empresas.Where(e => e.NomeEmpresa == estacao.EmpresaNome).SingleOrDefault();
                db.Estacoes.Add(estacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estacao);
        }

        // GET: Estacaos/Edit/5
        [Authorize(Roles = TipoPerfil.User)]
        [Authorize(Roles = TipoPerfil.Admin)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estacao estacao = db.Estacoes.Find(id);
            if (estacao == null)
            {
                return HttpNotFound();
            }
            return View(estacao);
        }

        // POST: Estacaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstacaoID,NomeEstacao,CustoPMinuto,Localizacao,NPostos")] Estacao estacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estacao);
        }

        // GET: Estacaos/Delete/5
        [Authorize(Roles = TipoPerfil.User)]
        [Authorize(Roles = TipoPerfil.Admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estacao estacao = db.Estacoes.Find(id);
            if (estacao == null)
            {
                return HttpNotFound();
            }
            return View(estacao);
        }

        // POST: Estacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estacao estacao = db.Estacoes.Find(id);
            db.Estacoes.Remove(estacao);
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
    }
}
