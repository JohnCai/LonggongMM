using System;
using Microsoft.Practices.Unity;

namespace Mavis.MVVM
{
    public class UnityContainerAdapter: IContainerFacade
    {
        private readonly IUnityContainer _unityContainer;

        public UnityContainerAdapter(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        #region Implementation of IContainerFacade

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public void AddInstance<T>(T instance)
        {
            _unityContainer.RegisterInstance(instance, new ContainerControlledLifetimeManager());
        }

        public void AddComponent<I, T>() where T : class, I
        {
            AddComponent<I, T>(LifetimeType.Singleton);
        }

        public void AddComponent<I, T>(LifetimeType lifetimeType) where T : class, I
        {
            AddComponent<I, T>(null, lifetimeType);
        }

        public void AddComponent<I, T>(string key) where T : class, I
        {
            AddComponent<I, T>(key, LifetimeType.Singleton);
        }

        public void AddComponent<I, T>(string key, LifetimeType lifetimeType) where T : class, I
        {
            _unityContainer.RegisterType<I, T>(key, lifetimeType.ToLifetimeManager());
        }

        public void AddComponent<T>() where T : class
        {
            AddComponent<T>(LifetimeType.Singleton);
        }

        public void AddComponent<T>(LifetimeType lifetimeType) where T : class
        {
            AddComponent<T>(null, lifetimeType);
        }

        public void AddComponent<T>(string key) where T : class
        {
            AddComponent<T>(key, LifetimeType.Singleton);
        }

        public void AddComponent<T>(string key, LifetimeType lifetimeType) where T : class
        {
            _unityContainer.RegisterType<T>(key, lifetimeType.ToLifetimeManager());
        }

        public void Release(object instance)
        {
            _unityContainer.Teardown(instance);
        }

        #endregion
    }


    static class LifestyleTypeMapper
    {
        public static LifetimeManager ToLifetimeManager(this LifetimeType lifetimeType)
        {
            switch (lifetimeType)
            {
                case LifetimeType.Singleton:
                    return new ContainerControlledLifetimeManager();
                case LifetimeType.Transient:
                    return new TransientLifetimeManager();
                default:
                    return new ContainerControlledLifetimeManager();
            }
        }
    }


}