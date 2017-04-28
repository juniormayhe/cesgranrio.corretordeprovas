using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cesgranrio.CorretorDeProvas.Web.Controllers;
namespace Cesgranrio.CorretorDeProvas.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Tratamos os erros decorrentes de tentativas de ataque XSS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Error(object sender, EventArgs e)
        {
            var excecaoOriginal = Server.GetLastError();
            
            Response.Clear();
            Server.ClearError();

            var routeData = new RouteData();
            routeData.Values["controller"] = "Erro";
            routeData.Values["action"] = "PaginaNaoEncontrada";
            
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            
            if (excecaoOriginal is HttpException)
            {
                /*aqui podemos tratar erros http*/
                HttpException excecao = excecaoOriginal as HttpException;
                var codigoHttp = excecao.GetHttpCode();

                switch (codigoHttp)
                {
                    case (int)HttpStatusCode.InternalServerError /*500*/:
                        if (excecao.Message.Contains("A potentially dangerous"))
                        {
                            routeData.Values["controller"] = "Erro";
                            routeData.Values["action"] = "RequisicaoInvalida";
                            Response.StatusCode = (int)HttpStatusCode.Conflict;/*400*/
                        }
                        break;
                        
                }//switch
            }
            else {

                //falha da própria aplicação
                routeData.Values["controller"] = "Erro";
                routeData.Values["action"] = "FalhaNaApliacacao";

                routeData.Values.Add("Erro", excecaoOriginal.GetType().FullName);
                routeData.Values.Add("Descrição", excecaoOriginal.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError; /*500*/


            }

            //Executa o controller de erro onthefly!
            IController controller = new ErroController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            controller.Execute(rc);

            Response.Flush();
            Response.End();


        }
    }
}
