using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Cesgranrio.CorretorDeProvas.DAL;
using Cesgranrio.CorretorDeProvas.DAL.Model;

namespace Cesgranrio.CorretorDeProvas.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            
            //injecao de dependencias builtin
            container.RegisterType<ICorretorDeProvasDbContext, CorretorDeProvasDbContext>(new PerThreadLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IQuestaoRepository, QuestaoRepository>();
            //container.RegisterType<IRepository<Questao>, QuestaoRepository>();
            container.RegisterType<IRepository<Resposta>, RespostaRepository>();
            container.RegisterType<ILoginUsuarioRepository<Usuario>, UsuarioRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}