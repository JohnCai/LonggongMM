using Mavis.Core;

namespace Longgong.mm.Core
{
    public class WorkingProcedure : Entity
    {
        private string _descriptoin;
        private string _name;

        public WorkingProcedure()
        {
            AddRule(new SimpleRule("Name", "�������Ʋ���Ϊ�գ�", () => string.IsNullOrEmpty(Name)));
        }

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
            get { return _descriptoin; }
            set
            {
                _descriptoin = value;
                NotifyPropertyChanged("Description");
            }
        }
    }
}