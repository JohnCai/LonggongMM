using Mavis.Core;
using NHibernate.Validator.Constraints;

namespace Longgong.mm.Core
{
    public class Location : Entity
    {
        private string _description;
        private LocationType _locationType;
        private string _name;

        public Location()
        {
            AddRule(new SimpleRule("Name", "名称不能为空！", () => string.IsNullOrEmpty(Name)));
        }

        public virtual LocationType LocationType
        {
            get { return _locationType; }
            set
            {
                _locationType = value;
                NotifyPropertyChanged("LocationType");
            }
        }

        //[NotNullNotEmpty]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public virtual string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }
    }

    /// <summary>
    /// LocationType
    /// Source: a location from which products are shipped. eg. a manufacturing plant.
    /// Destination: a location to which products are finally shipped. eg. a retail store.
    /// Replenishment point: a place at which products are received, stored and distributed. eg: a warehouse/distribution center.
    /// </summary>
    public enum LocationType
    {
        Source,
        Destination,
        ReplenishmentPoint
    }
}