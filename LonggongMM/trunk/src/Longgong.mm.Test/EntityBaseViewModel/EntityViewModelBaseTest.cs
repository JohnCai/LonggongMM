using Longgong.mm.ViewModels;
using Mavis.Core;
using NUnit.Framework;
using Rhino.Mocks;
using Mavis.MVVM;

namespace Longgong.mm.Test
{
    /// <summary>
    /// This class tests the EnityViewModel for the following:
    /// 1.the behavior of Execute Commands
    /// 2.the CanExecute of Commands
    /// 3.for simple entity(Dummy in the test), EntityViewModel should do all the works(Add, Edit, Save, Cancel...) well.
    /// </summary>
    [TestFixture]
    public class EntityViewModelBaseTest
    {
        private IDummyRepository _stubDummyRepository;
        private IMessageBoxService _stubMessageBoxService;
        private IContainerFacade _containerFacade;
        private DummyViewModel _dummyViewModel;

        [SetUp]
        public void SetUp()
        {
            _stubDummyRepository = MockRepository.GenerateStub<IDummyRepository>();
            _stubMessageBoxService = MockRepository.GenerateStub<IMessageBoxService>();
            _containerFacade = MockRepository.GenerateStub<IContainerFacade>();

            _containerFacade.Stub(x => x.Resolve<DummyBrowserViewModel>()).Return(
                new DummyBrowserViewModel(_stubDummyRepository));
            _containerFacade.Stub(x => x.Resolve<DummyDetailViewModel>()).Return(
                new DummyDetailViewModel());
            //EntityBrowserViewModel = (TBrowser) Activator.CreateInstance(typeof (TBrowser), entityRepository);
            //EntityBrowserViewModel = ContainerFacade.Resolve<TBrowser>();
            ////EntityDetailViewModel = (TDetail) Activator.CreateInstance(typeof (TDetail));
            //EntityDetailViewModel = ContainerFacade.Resolve<TDetail>();

            _dummyViewModel = new DummyViewModel(_stubDummyRepository, _stubMessageBoxService, _containerFacade);
            Assert.AreEqual(ViewMode.BrowseMode, _dummyViewModel.CurrentViewMode);
        }

        #region Command CanExecute Tester

        [Test]
        public void CanMoveItemOnlyFromViewOnlyOrBrowseMode()
        {
            _dummyViewModel.EntityBrowserViewModel.MatchedEntities.Add(new Dummy());
            _dummyViewModel.CurrentViewMode = ViewMode.BrowseMode;
            Assert.IsTrue(_dummyViewModel.MoveToFirstCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.ViewOnlyMode;
            Assert.IsTrue(_dummyViewModel.MoveToFirstCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.AddMode;
            Assert.IsFalse(_dummyViewModel.MoveToFirstCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.EditMode;
            Assert.IsFalse(_dummyViewModel.MoveToFirstCommand.CanExecute(null));
        }

        [Test]
        public void CanAddOnlyFromBrowseOrViewOnlyMode()
        {
            _dummyViewModel.CurrentViewMode = ViewMode.BrowseMode;
            Assert.IsTrue(_dummyViewModel.AddEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.ViewOnlyMode;
            Assert.IsTrue(_dummyViewModel.AddEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.AddMode;
            Assert.IsFalse(_dummyViewModel.AddEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.EditMode;
            Assert.IsFalse(_dummyViewModel.AddEntityCommand.CanExecute(null));
        }

        [Test]
        public void CanEditOnlyFromBrowseOrViewOnlyModeAndCurrentEntityIsNotNull()
        {
            //CurrentEntity is not Null
            _dummyViewModel.CurrentViewMode = ViewMode.BrowseMode;
            _dummyViewModel.CurrentEntity = new Dummy();
            Assert.IsTrue(_dummyViewModel.EditEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.ViewOnlyMode;
            _dummyViewModel.CurrentEntity = new Dummy();
            Assert.IsTrue(_dummyViewModel.EditEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.AddMode;
            _dummyViewModel.CurrentEntity = new Dummy();
            Assert.IsFalse(_dummyViewModel.EditEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.EditMode;
            _dummyViewModel.CurrentEntity = new Dummy();
            Assert.IsFalse(_dummyViewModel.EditEntityCommand.CanExecute(null));

            //CurrentEntity is Null
            _dummyViewModel.CurrentViewMode = ViewMode.BrowseMode;
            _dummyViewModel.CurrentEntity = null;
            Assert.IsFalse(_dummyViewModel.EditEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.ViewOnlyMode;
            _dummyViewModel.CurrentEntity = null;
            Assert.IsFalse(_dummyViewModel.EditEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.AddMode;
            _dummyViewModel.CurrentEntity = null;
            Assert.IsFalse(_dummyViewModel.EditEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.EditMode;
            _dummyViewModel.CurrentEntity = null;
            Assert.IsFalse(_dummyViewModel.EditEntityCommand.CanExecute(null));
        }

        [Test]
        public void CanSaveOnlyFromAddOrEditMode()
        {
            _dummyViewModel.CurrentViewMode = ViewMode.AddMode;
            Assert.IsTrue(_dummyViewModel.SaveEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.EditMode;
            Assert.IsTrue(_dummyViewModel.SaveEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.BrowseMode;
            Assert.IsFalse(_dummyViewModel.SaveEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.ViewOnlyMode;
            Assert.IsFalse(_dummyViewModel.SaveEntityCommand.CanExecute(null));
        }

        [Test]
        public void CanCancelOnlyFromAddOrEditMode()
        {
            _dummyViewModel.CurrentViewMode = ViewMode.AddMode;
            Assert.IsTrue(_dummyViewModel.ReloadEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.EditMode;
            Assert.IsTrue(_dummyViewModel.ReloadEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.BrowseMode;
            Assert.IsFalse(_dummyViewModel.ReloadEntityCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.ViewOnlyMode;
            Assert.IsFalse(_dummyViewModel.ReloadEntityCommand.CanExecute(null));
        }

        [Test]
        public void CanSwitchOnlyFromBrowseOrViewOnlyModeAndCurrentEnitityIsNotNull()
        {
            _dummyViewModel.CurrentViewMode = ViewMode.BrowseMode;
            Assert.IsFalse(_dummyViewModel.SwitchModeCommand.CanExecute(null));
            _dummyViewModel.CurrentEntity = new Dummy();
            Assert.IsTrue(_dummyViewModel.SwitchModeCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.ViewOnlyMode;
            _dummyViewModel.CurrentEntity = new Dummy();
            Assert.IsTrue(_dummyViewModel.SwitchModeCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.AddMode;
            Assert.IsFalse(_dummyViewModel.SwitchModeCommand.CanExecute(null));

            _dummyViewModel.CurrentViewMode = ViewMode.EditMode;
            Assert.IsFalse(_dummyViewModel.SwitchModeCommand.CanExecute(null));

        }

        [Test]
        public void CannotSwitchWhenCurrentEntityIsNull()
        {
            _dummyViewModel.CurrentViewMode = ViewMode.BrowseMode;
            _dummyViewModel.EntityBrowserViewModel.CurrentEntity = null;

            Assert.IsFalse(_dummyViewModel.SwitchModeCommand.CanExecute(null));
        }

        #endregion

        #region Command Execute Tester

        [Test]
        public void CanAdd()
        {
            var originalCurrentEntity = new Dummy { Number = 1, Name = "name1", Description = "desc1" };
            _dummyViewModel.CurrentEntity = originalCurrentEntity;
            _dummyViewModel.AddEntityCommand.Execute(null);

            Assert.AreEqual(ViewMode.AddMode, _dummyViewModel.CurrentViewMode);
            Assert.IsNotNull(_dummyViewModel.CurrentEntity);
            Assert.AreEqual(0, _dummyViewModel.CurrentEntity.Number);
        }

        [Test]
        public void CanAddFromBrowseModeAndCancelToBrowseMode()
        {
            var originalCurrentEntity = new Dummy { Number = 1, Name = "name1", Description = "desc1" };
            _dummyViewModel.CurrentViewMode = ViewMode.BrowseMode;
            _dummyViewModel.CurrentEntity = originalCurrentEntity;

            _dummyViewModel.AddEntityCommand.Execute(null);
            _dummyViewModel.ReloadEntityCommand.Execute(null);

            Assert.AreEqual(ViewMode.BrowseMode, _dummyViewModel.CurrentViewMode);
            Assert.AreEqual(originalCurrentEntity, _dummyViewModel.CurrentEntity);
        }

        [Test]
        public void CanAddFromViewOnlyModeAndCancelToViewOnlyMode()
        {
            var originalCurrentEntity = new Dummy { Number = 1, Name = "name1", Description = "desc1" };
            _dummyViewModel.CurrentViewMode = ViewMode.ViewOnlyMode;
            _dummyViewModel.CurrentEntity = originalCurrentEntity;

            _dummyViewModel.AddEntityCommand.Execute(null);
            _dummyViewModel.ReloadEntityCommand.Execute(null);

            Assert.AreEqual(ViewMode.ViewOnlyMode, _dummyViewModel.CurrentViewMode);
            Assert.AreEqual(originalCurrentEntity, _dummyViewModel.CurrentEntity);
        }

        [Test]
        public void CanAddAndSave()
        {
            var originalCurrentEntity = new Dummy { Number = 1, Name = "name1", Description = "desc1" };
            var newEntity = new Dummy() {Number = 2, Name = "name2", Description = "desc2"};
            _dummyViewModel.CurrentEntity = originalCurrentEntity;
            _dummyViewModel.AddEntityCommand.Execute(null);

            _dummyViewModel.CurrentEntity = newEntity;
            _dummyViewModel.SaveEntityCommand.Execute(null);

            Assert.AreEqual(ViewMode.ViewOnlyMode, _dummyViewModel.CurrentViewMode);
            Assert.AreEqual(newEntity, _dummyViewModel.CurrentEntity);

            //also confirm the new added entity has been synchronized with the CurrentEntity in Grid.
            Assert.AreEqual(newEntity, _dummyViewModel.EntityBrowserViewModel.CurrentEntity, "didn't sychronize to Grid");
        }
        
        
        [Test]
        public void CanEdit()
        {
            _dummyViewModel.CurrentEntity = new Dummy();
            _dummyViewModel.EditEntityCommand.Execute(null);

            Assert.AreEqual(ViewMode.EditMode, _dummyViewModel.CurrentViewMode);
        }

        [Test]
        public void CanEditFromBrowseModeAndCacelToBrowseMode()
        {
            var currentEntity = new Dummy();
            _dummyViewModel.CurrentViewMode = ViewMode.BrowseMode;
            _dummyViewModel.CurrentEntity = currentEntity;
            _dummyViewModel.EditEntityCommand.Execute(null);
            _dummyViewModel.ReloadEntityCommand.Execute(null);

            Assert.AreEqual(ViewMode.BrowseMode, _dummyViewModel.CurrentViewMode);
            Assert.AreEqual(currentEntity, _dummyViewModel.CurrentEntity);
        }

        [Test]
        public void CanEditFromViewOnlyModeAndCacelToViewOnlyMode()
        {
            var currentEntity = new Dummy();
            _dummyViewModel.CurrentViewMode = ViewMode.ViewOnlyMode;
            _dummyViewModel.CurrentEntity = currentEntity;
            _dummyViewModel.EditEntityCommand.Execute(null);
            _dummyViewModel.ReloadEntityCommand.Execute(null);

            Assert.AreEqual(ViewMode.ViewOnlyMode, _dummyViewModel.CurrentViewMode);
            Assert.AreEqual(currentEntity, _dummyViewModel.CurrentEntity);
        }

        [Test]
        public void CanEditAndSaveFromBrowserMode()
        {
            var currentEntity = new Dummy { Number = 1, Name = "name1", Description = "desc1" };
            _dummyViewModel.CurrentViewMode = ViewMode.BrowseMode;
            _dummyViewModel.CurrentEntity = currentEntity;
            _dummyViewModel.EditEntityCommand.Execute(null);
            _dummyViewModel.CurrentEntity.Number = 2;
            _dummyViewModel.SaveEntityCommand.Execute(null);

            Assert.AreEqual(ViewMode.ViewOnlyMode, _dummyViewModel.CurrentViewMode);
            Assert.AreEqual(2, _dummyViewModel.CurrentEntity.Number);
            Assert.AreEqual(currentEntity, _dummyViewModel.EntityBrowserViewModel.CurrentEntity);
            Assert.AreEqual(currentEntity, _dummyViewModel.EntityDetailViewModel.CurrentEntity);
        }

        [Test]
        public void CanEditAndSaveFromViewOnlyMode()
        {
            _dummyViewModel.CurrentViewMode = ViewMode.ViewOnlyMode;
            _dummyViewModel.CurrentEntity = new Dummy { Number = 1, Name = "name1", Description = "desc1" };
            _dummyViewModel.EditEntityCommand.Execute(null);
            _dummyViewModel.CurrentEntity.Number = 2;
            _dummyViewModel.SaveEntityCommand.Execute(null);

            Assert.AreEqual(ViewMode.ViewOnlyMode, _dummyViewModel.CurrentViewMode);
            Assert.AreEqual(2, _dummyViewModel.CurrentEntity.Number);
        }

        [Test]
        public void CanSwitchBetweenTwoMode()
        {
            _dummyViewModel.CurrentViewMode = ViewMode.BrowseMode;
            _dummyViewModel.EntityBrowserViewModel.CurrentEntity = new Dummy();
            _dummyViewModel.SwitchModeCommand.Execute(null);

            Assert.AreEqual(ViewMode.ViewOnlyMode, _dummyViewModel.CurrentViewMode);
            Assert.AreSame(_dummyViewModel.EntityBrowserViewModel.CurrentEntity, _dummyViewModel.EntityDetailViewModel.CurrentEntity);
        }

        #endregion

        #region Test Can Save/Update the simple entity

       [Test]
       public void CanAddNewDummySuccessfully()
       {
           _dummyViewModel.EntityBrowserViewModel.MatchedEntities.Clear();
           

           var dummy1 = new Dummy {Number = 1, Name = "name1", Description = "desc1"};
           var dummy2 = new Dummy {Number = 2, Name = "name2", Description = "desc2"};


           _dummyViewModel.AddEntityCommand.Execute(null);
           _dummyViewModel.CurrentEntity = dummy1;
           _dummyViewModel.SaveEntityCommand.Execute(null);

           _dummyViewModel.AddEntityCommand.Execute(null);
           _dummyViewModel.CurrentEntity = dummy2;
           _dummyViewModel.SaveEntityCommand.Execute(null);

           Assert.AreEqual(2, _dummyViewModel.EntityBrowserViewModel.MatchedEntities.Count);
           Assert.AreSame(dummy1, _dummyViewModel.EntityBrowserViewModel.MatchedEntities[0]);
           Assert.AreSame(dummy2, _dummyViewModel.EntityBrowserViewModel.MatchedEntities[1]);
           Assert.AreSame(dummy2, _dummyViewModel.EntityDetailViewModel.CurrentEntity);
           
        }

        #endregion
    }

    
}