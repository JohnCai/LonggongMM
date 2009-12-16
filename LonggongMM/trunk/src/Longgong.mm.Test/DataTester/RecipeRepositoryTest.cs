using System;
using System.Collections.Generic;
using Longgong.mm.Data;
using NHibernate;
using NUnit.Framework;
using Longgong.mm.Core;

namespace Longgong.mm.Test
{
    [TestFixture]
    public class RecipeRepositoryTest: DataTestBase
    {
        private Product _finishGood1; 
        private Product _finishGood2; 
        private Product _rawMaterial1;
        private Product _rawMaterial2;

        //SUT
        private RecipeRepository _recipeRepository;

        private WorkingProcedure _workingProcedure;

        #region Overrides of DataTestBase

        protected override void LoadTestData()
        {
            _recipeRepository = new RecipeRepository();
            PrepareProducts();
            PrepareWorkingProcedure();
        }
        
        #endregion

        #region Private helper

        private void PrepareProducts()
        {
            var productRepository = new ProductRepository();

            _finishGood1 = new Product { ProductType = ProductType.FinishGood, Name = "F1", DrawingNumber = "6606"};
            _finishGood2 = new Product { ProductType = ProductType.FinishGood, Name = "F2", DrawingNumber = "9393"};
            _rawMaterial1 = new Product { ProductType = ProductType.RawMaterial,Name = "R1"};
            _rawMaterial2 = new Product { ProductType = ProductType.RawMaterial,Name = "R2"};

            productRepository.SaveOrUpdate(_finishGood1);
            productRepository.SaveOrUpdate(_finishGood2);
            productRepository.SaveOrUpdate(_rawMaterial1);
            productRepository.SaveOrUpdate(_rawMaterial2);

            FlushAndClearSession();
        }

        private void PrepareWorkingProcedure()
        {
            var workingProcedureRepository = new WorkingProcedureRepository();

            _workingProcedure = new WorkingProcedure {Name = "w1"};

            workingProcedureRepository.SaveOrUpdate(_workingProcedure);

            FlushAndClearSession();
        }

        private int CreateAndSaveARecipe()
        {
            var recipeInputs = new List<RecipeInput>
                               {
                                   new RecipeInput {InputProduct = _rawMaterial1, InputQuantity = 1},
                                   new RecipeInput {InputProduct = _rawMaterial2, InputQuantity = 2}
                               };

            var recipe = new Recipe
            {
                Name = "recipe1",
                OutputProduct = _finishGood1, 
                OutputQuantity = 1,
                RecipeInputs = recipeInputs,
                WorkingProcedure = _workingProcedure
            };

            //todo:why using transaction and flush will fail?
            //using (Session.BeginTransaction())
            //{
                _recipeRepository.Save(recipe);
            //}
            
            FlushAndClearSession();

            Assert.IsTrue(recipe.ID > 0);

            return recipe.ID;

        }

        #endregion

        [Test]
        public void CanSaveNewAddedRecipe()
        {
            int recipeId = CreateAndSaveARecipe();
            
            var savedRecipe = _recipeRepository.Get(recipeId);

            //Assert.IsTrue(NHibernateUtil.IsInitialized(savedRecipe.RecipeInputs));

            Assert.AreEqual(_finishGood1.ID, savedRecipe.OutputProduct.ID);
            Assert.AreEqual(2, savedRecipe.RecipeInputs.Count);
            Assert.AreEqual(1, savedRecipe.OutputQuantity);
            Assert.AreEqual(_workingProcedure.ID, savedRecipe.WorkingProcedure.ID);
        }

        [Test]
        public void CanUpdateRecipe()
        {
            int recipeId = CreateAndSaveARecipe();
            
            var savedRecipe = _recipeRepository.Get(recipeId);
            //select another output
            savedRecipe.OutputQuantity = 2;
            savedRecipe.OutputProduct = _finishGood2;
            //delete an input
            savedRecipe.RecipeInputs.RemoveAt(0);
            _recipeRepository.SaveOrUpdate(savedRecipe);
            FlushAndClearSession();

            savedRecipe = _recipeRepository.Get(recipeId);
            Assert.AreEqual(1, savedRecipe.RecipeInputs.Count);
            Assert.AreEqual(_finishGood2.ID, savedRecipe.OutputProduct.ID);
            Assert.AreEqual(2, savedRecipe.OutputQuantity);
        }

    }
}