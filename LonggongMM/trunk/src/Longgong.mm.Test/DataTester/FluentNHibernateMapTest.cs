using System;
using FluentNHibernate.Testing;
using Longgong.mm.Data;
using NUnit.Framework;
using Longgong.mm.Core;
using System.Collections;
using System.Collections.Generic;

namespace Longgong.mm.Test
{
    [TestFixture]
    public class FluentNHibernateMapTest: DataTestBase
    {
        private Product _finishGood1;
        private Product _finishGood2;
        private Product _rawMaterial1;
        private Product _rawMaterial2;
        private WorkingProcedure _workingProcedure;

        private void PrepareProducts()
        {
            _finishGood1 = new Product { ProductType = ProductType.FinishGood, Name = "F1", DrawingNumber = "6606" };
            _finishGood2 = new Product { ProductType = ProductType.FinishGood, Name = "F2", DrawingNumber = "9393" };
            _rawMaterial1 = new Product { ProductType = ProductType.RawMaterial, Name = "R1" };
            _rawMaterial2 = new Product { ProductType = ProductType.RawMaterial, Name = "R2" };

            _finishGood1.SetIdTo(1);
            _finishGood2.SetIdTo(2);
            _rawMaterial1.SetIdTo(3);
            _rawMaterial2.SetIdTo(4);
        }

        private void PrepareWorkingProcedure()
        {
            
            _workingProcedure = new WorkingProcedure { Name = "w1" };
            _workingProcedure.SetIdTo(1);
        }


        protected override void LoadTestData()
        {
            //PrepareProducts();
        }

        [Test]
        public void CanCorrectlyMapLocation()
        {
            new PersistenceSpecification<Location>(Session)
                .CheckProperty(c => c.ID, 1)
                .CheckProperty(c => c.LocationType, LocationType.ReplenishmentPoint)
                .CheckProperty(c => c.Name, "Location1")
                .CheckProperty(c => c.Description, "Location1 Desc")
                .VerifyTheMappings();
        }

        [Test]
        public void CanCorrectlyMapProduct()
        {
            new PersistenceSpecification<Product>(Session)
                .CheckProperty(c => c.Description, "product1desc")
                .CheckProperty(c => c.DrawingNumber, "c01-00")
                .CheckProperty(c => c.FinishGoodConfigure, "config1")
                .CheckProperty(c => c.FinishGoodMode, "mode1")
                .CheckProperty(c => c.Name, "product1")
                .CheckProperty(c => c.NetWeight, 80.9)
                .CheckProperty(c => c.ProductType, ProductType.Immediate)
                .CheckProperty(c => c.Spec, "20*30*20")
                .VerifyTheMappings();
        }

        [Test]
        public void CanCorrectlyMapRecipeInput()
        {
            new PersistenceSpecification<RecipeInput>(Session)
                .CheckProperty(c => c.InputQuantity, 2)
                .CheckReference(c => c.InputProduct, new Product {Name = "product1"})
                .VerifyTheMappings();
        }

        [Test]
        public void CanCorrectlyMapRecipe()
        {
            PrepareProducts();
            PrepareWorkingProcedure();
            new PersistenceSpecification<Recipe>(Session)
                .CheckProperty(c => c.Name, "recipe1")
                .CheckProperty(c => c.Description, "recipe1 desc")
                .CheckList(c => c.RecipeInputs,
                           new List<RecipeInput>
                               {
                                   new RecipeInput {InputProduct = _rawMaterial1, InputQuantity = 1},
                                   new RecipeInput {InputProduct = _rawMaterial2, InputQuantity = 2}
                               })
                .CheckReference(c => c.OutputProduct, _finishGood2)
                .CheckProperty(c => c.OutputQuantity, 1)
                .CheckReference(c => c.WorkingProcedure, _workingProcedure)
                .VerifyTheMappings();
        }

        [Test]
        public void CanCorrectlyMapWorkingProcedure()
        {
            new PersistenceSpecification<WorkingProcedure>(Session)
                .CheckProperty(c => c.Name, "workingprocedure1")
                .CheckProperty(c => c.Description, "des1")
                .VerifyTheMappings();
        }

        [Test]
        public void CanCorrectlyMapMainProductionPlan()
        {
            PrepareProducts();
            new PersistenceSpecification<MainProductionPlan>(Session)
                .CheckProperty(c => c.Notes, "notes")
                .CheckProperty(c => c.PlanDate, DateTime.Today)
                .CheckProperty(c => c.PlanQuantity, 20)
                .CheckReference(c => c.PlanProduct, _finishGood2)
                .VerifyTheMappings();
        }

    }
    
}