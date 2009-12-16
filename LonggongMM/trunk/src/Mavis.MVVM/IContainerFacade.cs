namespace Mavis.MVVM
{
    public interface IContainerFacade
    {
        T Resolve<T>();

        void AddInstance<T>(T instance);

        void AddComponent<I, T>() where T : class, I;
        void AddComponent<I, T>(LifetimeType lifetimeType) where T : class, I;
        void AddComponent<I, T>(string key) where T : class, I;
        void AddComponent<I, T>(string key, LifetimeType lifetimeType) where T : class, I;

        void AddComponent<T>() where T : class;
        void AddComponent<T>(LifetimeType lifetimeType) where T : class;
        void AddComponent<T>(string key) where T : class;
        void AddComponent<T>(string key, LifetimeType lifetimeType) where T : class;

        void Release(object instance);
    }
}