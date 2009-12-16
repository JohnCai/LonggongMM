using System.Collections.Generic;
using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;
using Mavis.Core;

namespace Longgong.mm.ViewModels
{
    public class LocationBrowserViewModel: EntityBrowserViewModel<Location, ILocationRepository>
    {
        public LocationBrowserViewModel(ILocationRepository entityReposity) : base(entityReposity)
        {
        }

        protected override List<SearchCriteria<Location>> SearchCriterias
        {
            get
            {
                var result = new List<SearchCriteria<Location>>
                                 {new SearchCriteria<Location>(x => x.Name, NameFilter, Operator.Like)};
                return result;
            }
        }

        private string _nameFilter;
        public string NameFilter
        {
            get { return _nameFilter; }
            set
            {
                _nameFilter = value;
                NotifyPropertyChanged("NameFilter");
            }
        }
    }
}