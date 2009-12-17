using System;
using System.Diagnostics;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Longgong.mm.Core.DataInferfaces;
using Longgong.mm.Data;
using Longgong.mm.ViewModels;
using Mavis.Core;
using Mavis.DataAccess;
using Mavis.MVVM;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Longgong.mm.app.Views;

namespace Longgong.mm.app
{
    public class LonggongMMShellBuilder:UnityShellBuilder
    {
        private const string DBFile = "Test.db";

        private static readonly ISessionStorage _simpleSessionStorage = new SimpleSessionStorage();

        public LonggongMMShellBuilder()
            : this(InitializeNHibernateSession)
        {
            
        }

        //for testing injection
        public LonggongMMShellBuilder(Action nHibernateInitializer)
        {
            Singleton<RunOnce>.Instance.Run(nHibernateInitializer);

        }

        
        #region Overrides of UnityShellBuilder

        protected override void ConfigureIocContainer()
        {
            base.ConfigureIocContainer();

            //register Repositories
            IocContainer.AddComponent<ILocationRepository, LocationRepository>();
            IocContainer.AddComponent<IWorkingProcedureRepository, WorkingProcedureRepository>();
            IocContainer.AddComponent<IProductRepository, ProductRepository>();
            IocContainer.AddComponent<IRecipeRepository, RecipeRepository>();
            IocContainer.AddComponent<IMainProductionPlanRepository, MainProductionPlanRepository>();

            //register UI Services
            IocContainer.AddComponent<IMessageBoxService, VPFMessageBoxService>();
            IocContainer.AddComponent<IShowDialogService, VPFShowDialogService>();

            //register Popup Windows.
            var showDialogService = IocContainer.Resolve<IShowDialogService>();
            showDialogService.Register("ProductsLookupPopup", typeof(ProductsLookupPopup));
        }
        
        protected override object CreateShell()
        {
            return new MainWindowViewModel(IocContainer);
        }

        #endregion

       
        private static void BuildSchema(Configuration config)
        {
            if (File.Exists(DBFile))
            {
                File.Delete(DBFile);
            }
            new SchemaExport(config).Create(true, true);
        }

        private static void InitializeNHibernateSession()
        {
            NHibernateSession.Init<LocationMap>(_simpleSessionStorage, "NHibernate.config");

            //for test
            //NHibernateSession.Init<LocationMap>(_simpleSessionStorage, SQLiteConfiguration.Standard.UsingFile(DBFile), BuildSchema);
        }
    }
}