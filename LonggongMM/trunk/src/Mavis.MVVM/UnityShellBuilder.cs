using System;
using Microsoft.Practices.Unity;
using System.Diagnostics;

namespace Mavis.MVVM
{
    public abstract class UnityShellBuilder
    {
        private static readonly IUnityContainer _container;

        public IContainerFacade IocContainer { get; protected set; }

        static UnityShellBuilder()
        {
            _container = CreateIocContainer();
            _container.RegisterInstance(_container);
            _container.RegisterType<IContainerFacade, UnityContainerAdapter>(new ContainerControlledLifetimeManager());
        }

        public object BuildShell()
        {
            ConfigureIocContainer();
            return CreateShell();
        }

        protected abstract object CreateShell();
        
        /// <summary>
        /// Register default types
        /// </summary>
        protected virtual void ConfigureIocContainer()
        {
            IocContainer = _container.Resolve<IContainerFacade>();
            IocContainer.AddComponent<IEventAggregator, EventAggregator>();
        }

        protected static IUnityContainer CreateIocContainer()
        {
            return new UnityContainer();
        }
    }

}