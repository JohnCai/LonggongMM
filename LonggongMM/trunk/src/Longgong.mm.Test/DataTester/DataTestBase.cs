using System;
using System.IO;
using FluentNHibernate.Cfg.Db;
using Longgong.mm.Data;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using NHibernate;
using Mavis.DataAccess;

namespace Longgong.mm.Test
{
    public class DataTestBase
    {
        private const string DBFile = "Test.db";

        public static void InitializeNHibernateSessionForTest()
        {
            NHibernateSession.Init<LocationMap>(new SimpleSessionStorage(),
                                                SQLiteConfiguration.Standard.UsingFile(DBFile).ShowSql(),
                                                BuildSchema);
        }

        private static void BuildSchema(Configuration config)
        {
            if (File.Exists(DBFile))
            {
                File.Delete(DBFile);
            }
            new SchemaExport(config).Create(false, true);
        }

        protected ISession Session
        {
            get { return NHibernateSession.Current; }
        }

        [SetUp]
        public virtual void SetUp()
        {
            InitializeNHibernateSessionForTest();
            LoadTestData();
        }

        [TearDown]
        public virtual void TearDown()
        {
            NHibernateSession.Clear();
            //Session.Dispose();
           
        }

        protected virtual void LoadTestData()
        {}

        protected void FlushAndClearSession()
        {
            // Commits any changes up to this point to the database
            Session.Flush();

            Session.Clear();
        }

        
    }

}