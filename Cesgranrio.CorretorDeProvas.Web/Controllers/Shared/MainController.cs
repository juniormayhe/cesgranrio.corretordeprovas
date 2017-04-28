using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cesgranrio.CorretorDeProvas.Web.Controllers.Shared
{
    /// <summary>
    /// Permite que controllers usem o método RedirectToLocal
    /// </summary>
    public abstract class MainController : Controller
    {
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
    
}