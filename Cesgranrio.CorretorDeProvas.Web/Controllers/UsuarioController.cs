using Cesgranrio.CorretorDeProvas.DAL;
using Cesgranrio.CorretorDeProvas.DAL.Model;
using Cesgranrio.CorretorDeProvas.Util;
using Cesgranrio.CorretorDeProvas.Web.Controllers.Shared;
using Cesgranrio.CorretorDeProvas.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using X.PagedList;


namespace Cesgranrio.CorretorDeProvas.Web.Controllers
{
    
    public class UsuarioController : MainController
    {
        private ILoginUsuarioRepository<Usuario> _repository;

        const int pageSize = 5;

        public UsuarioController(ILoginUsuarioRepository<Usuario> repository)
        {
            _repository = repository;
        }

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
        public async Task<ActionResult> Login(LoginVM lvm, string returnUrl)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    
                    if (lvm.CPF.RetirarFormato().ÉCPFVálido())
                    {
                        
                        var usuario = await _repository.Autenticar(lvm.CPF.RetirarFormato(), lvm.Senha.ConverterParaMD5());
                        List<string> logins = usuario.ToList();

                        bool autenticou = logins != null && logins.FirstOrDefault() == "1";
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
                
                System.Diagnostics.Trace.WriteLine(ex);
                TempData["Erro"] = new ErroVM { Erro = ex.GetType().FullName, Descrição=ex.Message};
                
                return this.RedirectToAction("FalhaNaAplicacao", "Erro");

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
        public ActionResult Index()
        {
            throw new NotImplementedException();
            //var usuario = db.Usuario.Include(u => u.Grupo);
            //return View(await usuario.ToListAsync());
        }

        // GET: Usuario/Details/5
        [VerificarAcessoFilter]
        public ActionResult Details(int? id)
        {
            throw new NotImplementedException();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Usuario usuario = await db.Usuario.FindAsync(id);
            //if (usuario == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(usuario);
        }

        // GET: Usuario/Adicionar
        [VerificarAcessoFilter]
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar([Bind(Include = "UsuarioID,UsuarioCPF,UsuarioSenha,GrupoID")] Usuario usuario)
        {
            throw new NotImplementedException();
            //if (ModelState.IsValid)
            //{
            //    db.Usuario.Add(usuario);
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.GrupoID = new SelectList(db.Grupo, "GrupoID", "GrupoNome", usuario.GrupoID);
            //return View(usuario);
        }

        // GET: Usuario/Edit/5
        [VerificarAcessoFilter]
        public ActionResult Alterar(int? id)
        {
            throw new NotImplementedException();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Usuario usuario = await db.Usuario.FindAsync(id);
            //if (usuario == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.GrupoID = new SelectList(db.Grupo, "GrupoID", "GrupoNome", usuario.GrupoID);
            //return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar([Bind(Include = "UsuarioID,UsuarioCPF,UsuarioSenha,GrupoID")] Usuario usuario)
        {
            throw new NotImplementedException();
            //if (ModelState.IsValid)
            //{
            //    db.Entry(usuario).State = EntityState.Modified;
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.GrupoID = new SelectList(db.Grupo, "GrupoID", "GrupoNome", usuario.GrupoID);
            //return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            throw new NotImplementedException();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Usuario usuario = await db.Usuario.FindAsync(id);
            //if (usuario == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            throw new NotImplementedException();
            //Usuario usuario = await db.Usuario.FindAsync(id);
            //db.Usuario.Remove(usuario);
            //await db.SaveChangesAsync();
            //return RedirectToAction("Index");
        }

        
    }
}
