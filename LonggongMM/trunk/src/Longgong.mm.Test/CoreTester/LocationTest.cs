using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Longgong.mm.Core;
using NUnit.Framework;

namespace Longgong.mm.Models.Test
{
    [TestFixture]
    public class LocationTest
    {
        [Test]
        public void NewLocationShouldHaveDefaultType()
        {
            var location = new Location();
            Assert.IsNotNull(location.LocationType);
            Assert.AreEqual(location.LocationType, LocationType.Source);
        }

        [Test]
        public void NewLocationShouldHaveZeroId()
        {
            var location = new Location();

            Assert.IsTrue(location.ID == 0);
        }
    }
}
