using Longgong.mm.Data;
using NUnit.Framework;
using Longgong.mm.Core;

namespace Longgong.mm.Test
{
    [TestFixture]
    public class LocationRepositoryTest: DataTestBase
    {
        private LocationRepository _locationRepository;

        [Test]
        public void CanGetAll()
        {
            var locations = _locationRepository.GetAll();

            Assert.IsNotNull(locations);
            Assert.AreEqual(3, locations.Count);
        }

        [Test]
        public void CanGetOne()
        {
            var location = _locationRepository.Get(2);

            Assert.IsNotNull(location);
            Assert.AreEqual(location.LocationType, LocationType.Destination);
        }

        [Test]
        public void CanFindByType()
        {
            var locations = _locationRepository.FindByType(LocationType.Source);

            Assert.IsNotNull(locations);
            Assert.AreEqual(2, locations.Count);
        }

        #region Overrides of DataTestBase

        protected override void LoadTestData()
        {
            _locationRepository = new LocationRepository();

            var location1 = new Location {LocationType = LocationType.Source};
            var location2 = new Location {LocationType = LocationType.Destination};
            var location3 = new Location {LocationType = LocationType.Source};

            _locationRepository.SaveOrUpdate(location1);
            _locationRepository.SaveOrUpdate(location2);
            _locationRepository.SaveOrUpdate(location3);

            FlushAndClearSession();
        }

        #endregion
    }
}