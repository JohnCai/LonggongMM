using System;
using Longgong.mm.app;
using Longgong.mm.Data;
using Longgong.mm.ViewModels;
using NHibernate;
using NUnit.Framework;
using Longgong.mm.Core.DataInferfaces;
using Rhino.Mocks;

namespace Longgong.mm.Test
{
    [TestFixture]
    public class LonggongMMShellBuilderTest
    {
        //[Test]
        //public void CanResolveLocationRepositoryAfterBuildShell()
        //{
        //    var stubSessionFactory = DatabaseTestHelper.CreateStubSessionFacotry();

        //    var builder = new LonggongMMShellBuilder(stubSessionFactory);

        //    builder.BuildShell();

        //    var locationRepository = builder.IocContainer.Resolve<ILocationRepository>();

        //    Assert.IsNotNull(locationRepository);

        //    Assert.IsInstanceOf(typeof(LocationRepository), locationRepository);

            
        //}

        //[Test]
        //public void CanResolveShellViewModelAfterBuildShell()
        //{
        //    var stubSessionFactory = DatabaseTestHelper.CreateStubSessionFacotry();
        //    var builder = new LonggongMMShellBuilder(stubSessionFactory);
        //    builder.BuildShell();

        //    var locationViewModel = builder.IocContainer.Resolve<LocationViewModel>();

        //    Assert.IsNotNull(locationViewModel);
        //}

    }
}