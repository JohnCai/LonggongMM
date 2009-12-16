using System.Collections.Generic;
using Longgong.mm.Core;
using Longgong.mm.Core.DataInferfaces;
using System;
using Mavis.Core;

namespace Longgong.mm.ViewModels
{
    public class MainProductionPlanBrowserViewModel : EntityBrowserViewModel<MainProductionPlan, IMainProductionPlanRepository>
    {
        public MainProductionPlanBrowserViewModel(IMainProductionPlanRepository entityReposity)
            : base(entityReposity)
        {
        }

        protected override List<SearchCriteria<MainProductionPlan>> SearchCriterias
        {
            get
            {
                var result = new List<SearchCriteria<MainProductionPlan>>();

                if (StartDateFilter.HasValue)
                    result.Add(new SearchCriteria<MainProductionPlan>(x => x.PlanDate, StartDateFilter.Value.Date,
                                                                      Operator.GreaterEqual));
                if (EndDateFilter.HasValue)
                    result.Add(new SearchCriteria<MainProductionPlan>(x => x.PlanDate, EndDateFilter.Value.Date.AddDays(1),
                                                                      Operator.LessThan));
                if (!string.IsNullOrEmpty(FinishGoodModeFilter))
                {
                    result.Add(new SearchCriteria<MainProductionPlan>("FinishGoodMode", "PlanProduct", FinishGoodModeFilter, Operator.Like));
                }

                return result;
            }
        }


        #region Filter

        private DateTime? _startDateFilter;
        public DateTime? StartDateFilter
        {
            get { return _startDateFilter; }
            set
            {
                _startDateFilter = value;
                NotifyPropertyChanged("StartDateFilter");
            }
        }

        private DateTime? _endDateFilter;
        public DateTime? EndDateFilter
        {
            get { return _endDateFilter; }
            set
            {
                _endDateFilter = value;
                NotifyPropertyChanged("EndDateFilter");
            }
        }

        private string _finishGoodModerFilter;
        public string FinishGoodModeFilter
        {
            get { return _finishGoodModerFilter; }
            set
            {
                _finishGoodModerFilter = value;
                NotifyPropertyChanged("FinishGoodModeFilter");
            }
        }

        #endregion

    }
}