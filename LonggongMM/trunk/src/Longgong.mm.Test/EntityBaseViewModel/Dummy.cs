using System;
using Longgong.mm.ViewModels;
using Mavis.Core;
using Mavis.MVVM;

namespace Longgong.mm.Test
{
    public class DummyViewModel : EntityViewModelBase<Dummy, IDummyRepository, DummyBrowserViewModel, DummyDetailViewModel>
    {
        public DummyViewModel(IDummyRepository entityRepository, IMessageBoxService messageBoxService, IContainerFacade containerFacade) 
            : base(entityRepository, messageBoxService, containerFacade)
        {
        }
    }

    public class DummyBrowserViewModel : EntityBrowserViewModel<Dummy, IDummyRepository>
    {
        public DummyBrowserViewModel(IDummyRepository entityReposity)
            : base(entityReposity)
        {
        }
    }

    public class DummyDetailViewModel : EntityDetailViewModel<Dummy>
    {
        #region Overrides of EntityDetailViewModel<Dummy,IDummyRepository>

        protected override void SetPropertiesEditableViaCurrentViewMode()
        {
            
        }

        #endregion
    }

    public interface IDummyRepository : INHibernateRepository<Dummy>
    {

    }

    public class Dummy : Entity
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}