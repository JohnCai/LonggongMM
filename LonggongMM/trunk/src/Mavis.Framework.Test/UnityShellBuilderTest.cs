using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Mavis.MVVM.Test
{
    [TestFixture]
    public class UnityShellBuilderTest
    {
        [Test]
        public void CheckIocContainerIsSingleton()
        {
            var builder1 = new UnityShellBuilder1();
            var builder2 = new UnityShellBuilder2();
            builder1.BuildShell();
            builder2.BuildShell();
            
            //check IocContainer is static so that all instance can share.
            Assert.IsTrue(ReferenceEquals(builder1.IocContainer, builder2.IocContainer));

            Assert.IsTrue(ReferenceEquals(builder1.IocContainer.Resolve<IEventAggregator>(), builder2.IocContainer.Resolve<IEventAggregator>()));
        }
    }

    public class UnityShellBuilder1: UnityShellBuilder
    {
        protected override object CreateShell()
        {
            return null;
        }

    }
    
    public class UnityShellBuilder2: UnityShellBuilder
    {
        protected override object CreateShell()
        {
            return null;
        }

    }
}
