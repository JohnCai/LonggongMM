using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;

namespace Longgong.mm.Test
{
    [TestFixture]
    public class EntityBrowserViewModelTest
    {
        private DummyBrowserViewModel _dummyBrowserViewModel;
        private IDummyRepository _stubDummyRepository;

        [SetUp]
        public void SetUp()
        {
            _stubDummyRepository = MockRepository.GenerateStub<IDummyRepository>();
            _dummyBrowserViewModel = new DummyBrowserViewModel(_stubDummyRepository);
        }

        [Test]
        public void CanSearchIntoMatchedEntitList()
        {
            var entitiesFound = new List<Dummy> {new Dummy {Number = 1}, new Dummy {Number = 2}};

            _stubDummyRepository.Stub(x => x.FindAll(new Dictionary<string, object>())).IgnoreArguments().Return(
                entitiesFound);

            _dummyBrowserViewModel.SearchEntityCommand.Execute(null);
            Assert.AreEqual(2, _dummyBrowserViewModel.MatchedEntities.Count);
            Assert.IsTrue(_dummyBrowserViewModel.MatchedEntities.Contains(entitiesFound[0]));
            Assert.IsTrue(_dummyBrowserViewModel.MatchedEntities.Contains(entitiesFound[1]));
        }

        [Test]
        public void MatchedEntitiesShouldNotBeNullAllTheTime()
        {
            Assert.IsNotNull(_dummyBrowserViewModel.MatchedEntities);

            var entitiesFound = new List<Dummy>();
            _stubDummyRepository.Stub(x => x.FindAll(new Dictionary<string, object>())).IgnoreArguments().Return(
                entitiesFound);
            Assert.IsNotNull(_dummyBrowserViewModel.MatchedEntities);
        }

        [Test]
        public void CanMove()
        {
            var d1 = new Dummy();
            var d2 = new Dummy();
            var d3 = new Dummy();
            _dummyBrowserViewModel.MatchedEntities.Add(d1);
            _dummyBrowserViewModel.MatchedEntities.Add(d2);
            _dummyBrowserViewModel.MatchedEntities.Add(d3);

            _dummyBrowserViewModel.MoveToFirst();
            Assert.AreSame(d1, _dummyBrowserViewModel.CurrentEntity);

            _dummyBrowserViewModel.MoveToNext();
            Assert.AreSame(d2, _dummyBrowserViewModel.CurrentEntity);

            //loop for next
            _dummyBrowserViewModel.MoveToNext();
            _dummyBrowserViewModel.MoveToNext();
            Assert.AreSame(d1, _dummyBrowserViewModel.CurrentEntity);

            _dummyBrowserViewModel.MoveToLast();
            Assert.AreSame(d3, _dummyBrowserViewModel.CurrentEntity);

            //loop for previous
            for (int i = 0; i < 3; i++)
            {
                _dummyBrowserViewModel.MoveToPrevious();
            }
            Assert.AreSame(d3, _dummyBrowserViewModel.CurrentEntity);
        }
    }
}