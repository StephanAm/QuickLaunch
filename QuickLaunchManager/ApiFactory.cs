using QuickLaunchManager.Handlers;
using QuickLaunchManager.Repo;
using QuickLaunchManager.Repo.XmlRepo;
using QuickLaunchManager.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using QuickLaunchManager.Config;
using System.IO;
using Unity.Lifetime;

namespace QuickLaunchManager
{
    public class ApiFactory
    {
        
        private readonly IUnityContainer _container;
        public ApiFactory()
        {
            _container = CreateContainer();
        }
        public QuickLaunchApi GetApi()
        {
            return _container.Resolve<QuickLaunchApi>();
        }
        private QuickLaunchAppConfig GetConfig()
        {
            var config = new QuickLaunchAppConfig();
            config.AppName = "QuickLaunch";
            config.DataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                config.AppName);
            config.XmlRepoFileName = "quickLaunch.xml";
            return config;

        }

        private IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<QuickLaunchAppConfig>(
                new SingletonLifetimeManager(),
                new InjectionFactory(c=>GetConfig())
                );
            var config = container.Resolve<QuickLaunchAppConfig>();
            var handlerTypes = GetHandlers<BaseHandler>();
            
            container.RegisterType<BaseHandler[]>(
                new InjectionFactory(
                    c=> {
                        var r = handlerTypes.Select(t => c.Resolve(t)).Cast<BaseHandler>();
                        return r.ToArray();
                        })
                );
            container.RegisterType<WebUrlHandler>();
            container.RegisterType<IItemValidator, ItemValidator>();
            container.RegisterType<IRepo>(new InjectionFactory(c => 
                new XmlRepo(Path.Combine(config.DataPath,config.XmlRepoFileName))
            ));

            container.RegisterType<QuickLaunchApi>();
            return container;
        }
        private IEnumerable<Type> GetHandlers<T>()
        {
            var baseType = typeof(T);
            var types = Assembly
                .GetAssembly(baseType)
                .GetTypes()
                .Where(t => t.IsSubclassOf(baseType));
            return types;
        }
    }
}
