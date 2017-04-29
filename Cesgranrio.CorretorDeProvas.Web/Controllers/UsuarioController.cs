using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cesgranrio.CorretorDeProvas.Web;
using Cesgranrio.CorretorDeProvas.Web.Controllers.Shared;
using Cesgranrio.CorretorDeProvas.Web.Models;
using Cesgranrio.CorretorDeProvas.Util;

namespace Cesgranrio.CorretorDeProvas.Web.Controllers
{
    
    public class UsuarioController : MainController
    {
        
        /// <summary>  
        /// GET: /Account/Login    
        /// </summary>  
        /// <param name="returnUrl">Return URL parameter</param>  
        /// <returns>Return login view</returns>  
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            
            try
            {
                // Verification.    
                if (this.Request.IsAuthenticated)
                {
                    // Info.    
                    return this.RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                Console.Write(ex);
            }
            // Info.    
            return this.View();
        }

        /// <summary>  
        /// POST: /Account/Login    
        /// </summary>  
        /// <param name="model">Model parameter</param>  
        /// <param name="returnUrl">Return URL parameter</param>  
        /// <returns>Return login view</returns>  
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM lvm, string returnUrl)
        {
            try
            {
                
                if (ModelState.IsValid)
                {

                    if (lvm.CPF.ÉCPFVálido())
                    {
                        var login = this.db.Autenticar(lvm.CPF.RetirarFormato(), lvm.Senha.ConverterParaMD5()).ToList();

                        bool autenticou = login != null && login.FirstOrDefault() == "1";
                        if (autenticou)
                        {
                            //memoriza cpf que se autenticou para mostrar em outras áreas do site
                            Session.Gravar<string>("CPF", lvm.CPF);
                            return this.RedirectToLocal(returnUrl);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Usuário ou senha inválida.");
                        }
                    }
                    else {
                        ModelState.AddModelError(string.Empty, "CPF é inválido.");
                        
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.Write(ex);
            }
            //algo falhou e devolvemos o viewmodel
            return this.View(lvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            try
            {
                bool autenticou = Session.Ler<string>("CPF")!=null; 
                if (autenticou)
                {
                    Session.Abandon();
                    return this.RedirectToAction("Login");
                }   
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            
            return this.View();
        }

        // GET: Usuario
        [VerificarAcessoFilter]
        public async Task<ActionResult> Index()
        {
            var usuario = db.Usuario.Include(u => u.Grupo);
            return View(await usuario.ToListAsync());
        }

        // GET: Usuario/Details/5
        [VerificarAcessoFilter]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        [VerificarAcessoFilter]
        public ActionResult Create()
        {
            ViewBag.GrupoID = new SelectList(db.Grupo, "GrupoID", "GrupoNome");
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UsuarioID,UsuarioCPF,UsuarioSenha,GrupoID")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GrupoID = new SelectList(db.Grupo, "GrupoID", "GrupoNome", usuario.GrupoID);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        [VerificarAcessoFilter]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrupoID = new SelectList(db.Grupo, "GrupoID", "GrupoNome", usuario.GrupoID);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UsuarioID,UsuarioCPF,UsuarioSenha,GrupoID")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GrupoID = new SelectList(db.Grupo, "GrupoID", "GrupoNome", usuario.GrupoID);
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Usuario usuario = await db.Usuario.FindAsync(id);
            db.Usuario.Remove(usuario);
            await db.SaveChangesAsync();
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
