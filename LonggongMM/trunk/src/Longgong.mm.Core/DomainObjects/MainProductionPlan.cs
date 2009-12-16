using Mavis.Core;
using System;

namespace Longgong.mm.Core
{
    public class MainProductionPlan: Entity
    {
        public MainProductionPlan()
        {
            AddRule(new SimpleRule("PlanProduct", "��Ʒ����Ϊ�գ�", () => PlanProduct == null));
            AddRule(new SimpleRule("PlanQuantity", "�����������0��", () => PlanQuantity <= 0));
            PlanDate = DateTime.Today;
        }

        private Product _planProduct;
        public virtual Product PlanProduct
        {
            get { return _planProduct; }
            set
            {
                _planProduct = value;
                NotifyPropertyChanged("PlanProduct");
            }
        }

        private int _planQuantity;
        public virtual int PlanQuantity
        {
            get { return _planQuantity; }
            set
            {
                _planQuantity = value;
                NotifyPropertyChanged("PlanQuantity");
            }
        }

        private DateTime _planDate;
        public virtual DateTime PlanDate
        {
            get { return _planDate; }
            set
            {
                _planDate = value;
                NotifyPropertyChanged("PlanDate");
            }
        }

        private string _notes;
        public virtual string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                NotifyPropertyChanged("Notes");
            }
        }
    }
}