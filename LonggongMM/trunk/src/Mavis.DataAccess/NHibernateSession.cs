using System;
using System.Collections.Generic;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using Mavis.Core;

namespace Mavis.DataAccess
{
    public static class NHibernateSession
    {
        static NHibernateSession()
        {
            SessionStorages = new Dictionary<string, ISessionStorage>();
            SessionFactories = new Dictionary<string, ISessionFactory>();
        }

        public static readonly string DefaultFactoryKey = "nhibernate.current_session";

        public static Configuration Init<T>(ISessionStorage storage, IPersistenceConfigurer persistenceConfigurer, Action<Configuration> exposeConfiguration)
        {
            return Init<T>(storage, string.Empty, persistenceConfigurer, exposeConfiguration);
        }

        public static Configuration Init<T>(ISessionStorage storage, string configFile)
        {
            return Init<T>(storage, configFile, null, null);
        }

        public static Configuration Init<T>(
            ISessionStorage storage, 
            string configFile, 
            IPersistenceConfigurer persistenceConfigurer, 
            Action<Configuration> exposeConfiguration)
        {
            Check.Require(storage != null, "storage mechanism was null but must be provided");
            Check.Require(!SessionFactories.ContainsKey(storage.FactoryKey),
                "A session factory has already been configured with the key of " + storage.FactoryKey);

            var config = ConfigureNHibernate(configFile);

            SessionFactories.Add(storage.FactoryKey, CreateSessionFactoryFor<T>(config, persistenceConfigurer, exposeConfiguration));

            SessionStorages.Add(storage.FactoryKey, storage);

            return config;
        }

        private static ISessionFactory CreateSessionFactoryFor<T>(Configuration config, IPersistenceConfigurer persistenceConfigurer, Action<Configuration> exposeConfiguration)
        {
            var fluentConfig =  Fluently.Configure(config);

            if (persistenceConfigurer != null)
                fluentConfig.Database(persistenceConfigurer);

            fluentConfig.Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>());

            if (exposeConfiguration != null)
                fluentConfig.ExposeConfiguration(exposeConfiguration);

            return fluentConfig.BuildSessionFactory();
        }

        private static Configuration ConfigureNHibernate(string configFile)
        {
            var cfg = new Configuration();

            if (!string.IsNullOrEmpty(configFile))
                cfg.Configure(configFile);

            return cfg;
        }

        //public static Configuration Init(ISessionStorage storage, string configFile)
        //{
            
        //}

        public static IDictionary<string, ISessionStorage> SessionStorages { get; set; }
        public static IDictionary<string, ISessionFactory> SessionFactories { get; set; }

        public static ISession Current
        {
            get {
                Check.Require(SessionStorages.Count == 1, "The NHibernateSession.Current property may " +
                    "only be invoked if you only have one NHibernate session factory; i.e., you're " + 
                    "only communicating with one database.  Since you're configured communications " +
                    "with multiple databases, you should instead call Current(string factoryKey)");

                return CurrentFor(DefaultFactoryKey);
			}
        }

        public static ISession CurrentFor(string factoryKey)
        {
            Check.Require(!string.IsNullOrEmpty(factoryKey), "factoryKey may not be null or empty");
            Check.Require(SessionStorages.ContainsKey(factoryKey), "An ISessionStorage does not exist with a factory key of " + factoryKey);
            Check.Require(SessionFactories.ContainsKey(factoryKey), "An ISessionFactory does not exist with a factory key of " + factoryKey);

            ISession session = SessionStorages[factoryKey].Session;

            if (session == null || ! session.IsOpen)
            {
                session = SessionFactories[factoryKey].OpenSession();
                SessionStorages[factoryKey].Session = session;
            }


            return session;
        }

        public static void Clear()
        {
            SessionStorages.Clear();
            SessionFactories.Clear();
        }
    }
}