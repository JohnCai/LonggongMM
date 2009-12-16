using System;
using System.Collections.Generic;
using Longgong.mm.Data;
using Mavis.Core;
using NUnit.Framework;
using Longgong.mm.Core;

namespace Longgong.mm.Test
{
    [TestFixture]
    public class MainProductionPlanRepositoryTest: DataTestBase
    {
        private MainProductionPlanRepository _mainProductionPlanRepository;
        private Product _p1;
        private Product _p2;
        private Product _p3;
        private MainProductionPlan _pp1;
        private MainProductionPlan _pp2;
        private MainProductionPlan _pp3;
        private MainProductionPlan _pp4;
        private MainProductionPlan _pp5;

        [Test]
        public void CanFindByCriterias()
        {
            var searchCriterias = new List<SearchCriteria<MainProductionPlan>>
                                      {
                                          new SearchCriteria<MainProductionPlan>("FinishGoodMode", "PlanProduct", "6060",
                                                                                 Operator.Like),
                                          new SearchCriteria<MainProductionPlan>(x => x.PlanDate, DateTime.Today,
                                                                                 Operator.GreaterEqual),
                                          new SearchCriteria<MainProductionPlan>(x => x.PlanDate,
                                                                                 DateTime.Today.AddDays(1),
                                                                                 Operator.LessEqual)
                                      };

            var searchResults = _mainProductionPlanRepository.FindByCriterias(searchCriterias);

            Assert.IsNotNull(searchResults);
            Assert.AreEqual(2, searchResults.Count);
            Assert.IsTrue(searchResults.Contains(_pp1));
            Assert.IsTrue(searchResults.Contains(_pp2));
        }

        protected override void LoadTestData()
        {
            _mainProductionPlanRepository = new MainProductionPlanRepository();
            var productRepository = new ProductRepository();

            _p1 = new Product {FinishGoodMode = "LG6060A", ProductType = ProductType.FinishGood, Name = "fg1"};
            _p2 = new Product {FinishGoodMode = "LG9090A", ProductType = ProductType.FinishGood, Name = "fg2"};
            _p3 = new Product {FinishGoodMode = "LG93A", ProductType = ProductType.FinishGood, Name = "fg3"};

            productRepository.SaveOrUpdate(_p1);
            productRepository.SaveOrUpdate(_p2);
            productRepository.SaveOrUpdate(_p3);

            _pp1 = new MainProductionPlan { PlanDate = DateTime.Today, PlanProduct = _p1, PlanQuantity = 10 };
            _pp2 = new MainProductionPlan { PlanDate = DateTime.Today.AddDays(1), PlanProduct = _p1, PlanQuantity = 10 };
            _pp3 = new MainProductionPlan { PlanDate = DateTime.Today.AddDays(2), PlanProduct = _p1, PlanQuantity = 10 };
            _pp4 = new MainProductionPlan { PlanDate = DateTime.Today, PlanProduct = _p2, PlanQuantity = 10 };
            _pp5 = new MainProductionPlan { PlanDate = DateTime.Today, PlanProduct = _p3, PlanQuantity = 10 };
            _mainProductionPlanRepository.SaveOrUpdate(_pp1);
            _mainProductionPlanRepository.SaveOrUpdate(_pp2);
            _mainProductionPlanRepository.SaveOrUpdate(_pp3);
            _mainProductionPlanRepository.SaveOrUpdate(_pp4);
            _mainProductionPlanRepository.SaveOrUpdate(_pp5);

            FlushAndClearSession();
        }
    }
}