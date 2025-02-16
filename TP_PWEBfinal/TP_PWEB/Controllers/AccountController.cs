﻿using System;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TP_PWEB.Models;

namespace TP_PWEB.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string username = "";
            var userex = await UserManager.FindByEmailAsync(model.Email);

            if (userex != null)
            {
                username = userex.UserName;
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(username, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var roles = db.Roles.Where(m => m.Name == TipoPerfil.User || m.Name == TipoPerfil.Company);
            ViewBag.RoleName = new SelectList(roles, "Name", "Name");
            return View();
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            if (ModelState.IsValid)
            {
                var newuser = new ApplicationUser { UserName = model.NomeUsr, Email = model.Email };
                var result = await UserManager.CreateAsync(newuser, model.Password);

                if (result.Succeeded)
                {
                    //try
                    //{
                        if (model.TipoPerfil != TipoPerfil.Admin)
                        {
                            UserManager.AddToRole(newuser.Id, model.TipoPerfil);
                        }

                        if (model.TipoPerfil == TipoPerfil.User)
                        {
                            Utilizador user = new Utilizador { ID = newuser.Id, Email = model.Email, NomeUsr = model.NomeUsr, PerfilID = model.TipoPerfil ,DataNasc=model.DataNasc, CartaoCVC=model.CartaoCVC, CartaoNum=model.CartaoNum, CartaoValMonth=model.CartaoValMonth, CartaoValYear=model.CartaoValYear, Dinheiro=model.Dinheiro,  MetodoPagamento=true,   Telefone=model.Telefone};
                            db.Utilizadores.Add(user);
                            db.SaveChanges();
                        }
                        if (model.TipoPerfil == TipoPerfil.Company)
                        {
                            Empresa emp = new Empresa { ID = newuser.Id, Email = model.Email, NomeEmpresa=model.NomeUsr, PerfilID=model.TipoPerfil };
                            db.Empresas.Add(emp);
                            db.SaveChanges();
                        }
                    //}
                    //catch (DbEntityValidationException ex)
                    //{
                    //    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    //    {
                    //        foreach (var validationError in entityValidationErrors.ValidationErrors)
                    //        {
                    //            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    //        }
                    //    }
                    //}
                    await SignInManager.SignInAsync(newuser, isPersistent:false, rememberBrowser:false);
                    
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                   

                    return RedirectToAction("Index", "Manage");
                }
                AddErrors(result);
            }
            var perfil = db.Roles.Where(x => x.Name == TipoPerfil.User || x.Name == TipoPerfil.Company);
            ViewBag.TipoPerfil = new SelectList(perfil, "Name", "Name");
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}