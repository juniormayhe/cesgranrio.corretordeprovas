// *** Updated 7/8/2017 6:58 PM
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
            UnityConfig.RegisterComponents();//gestor de injecao de dependencias
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;
            
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
            routeData.Values.Add("Erro", excecaoOriginal.GetType().FullName);
            routeData.Values.Add("Descrição", excecaoOriginal.Message);
            
            if (excecaoOriginal is HttpException)
            {
                /*aqui podemos tratar erros http*/
                HttpException excecao = excecaoOriginal as HttpException;
                var codigoHttp = excecao.GetHttpCode();

                switch (codigoHttp)
                {
                    case (int)HttpStatusCode.InternalServerError /*500*/:

                        if (excecao is HttpRequestValidationException)
                        {
                            //tentativa de injection
                            if (excecao.Message.Contains("danger") || excecao.Message.Contains("perigo"))
                            {
                                routeData.Values["controller"] = "Erro";
                                routeData.Values["action"] = "RequisicaoInvalida";
                                //Response.StatusCode = (int)HttpStatusCode.Conflict;/*400*/
                            }
                        }
                        else
                        {
                            //ex: excecao is HttpCompileException....
                            routeData.Values["controller"] = "Erro";
                            routeData.Values["action"] = "FalhaNaAplicacao";
                        }
                        break;

                    case (int)HttpStatusCode.BadRequest /*400*/:
                        //pode ser um erro numa view
                        routeData.Values["controller"] = "Erro";
                        routeData.Values["action"] = "FalhaNaAplicacao";
                        
                        break;

                }//switch
            }
            else {

                //falha da própria aplicação
                routeData.Values["controller"] = "Erro";
                routeData.Values["action"] = "FalhaNaAplicacao";
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
