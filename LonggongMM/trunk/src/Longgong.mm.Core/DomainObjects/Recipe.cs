using System;
using System.Collections.Generic;
using Mavis.Core;

namespace Longgong.mm.Core
{
    public class Recipe : Entity
    {
        private IList<RecipeInput> _recipeInputs;
        private string _description;
        private string _recipeName;
        private WorkingProcedure _workingProcedure;
        private Product _outputProduct;
        private int _outputQuantity;

        public Recipe()
        {
            //_recipeInputs = new List<RecipeInput>();
            //MainRecipeOutput = new RecipeOutput();
            AddRule(new SimpleRule("Name", "名称不能为空！", () => string.IsNullOrEmpty(Name)));
            AddRule(new SimpleRule("WorkingProcedure", "工序不能为空！", () => WorkingProcedure == null));
            AddRule(new SimpleRule("OutputProduct", "产出不能为空！", () => OutputProduct == null));
            AddRule(new SimpleRule("OutputQuantity", "产出数量必须大于0！", () => OutputQuantity <= 0));
            AddRule(new SimpleRule("RecipeInputs", "投入产品不能为空！", () => RecipeInputs.Count == 1));
        }

        #region Properties

        public virtual string Name
        {
            get { return _recipeName; }
            set
            {
                _recipeName = value;
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

        public virtual IList<RecipeInput> RecipeInputs
        {
            get
            {
                if (_recipeInputs == null)
                {
                    _recipeInputs = new List<RecipeInput>();
                }
                return _recipeInputs;
            }
            set
            {
                _recipeInputs = value;
                if (value != null)
                {
                    foreach (var recipeInput in value)
                    {
                        recipeInput.Recipe = this;
                    }
                }
                NotifyPropertyChanged("RecipeInputs");
            }
        }

        public virtual WorkingProcedure WorkingProcedure
        {
            get { return _workingProcedure; }
            set
            {
                _workingProcedure = value;
                NotifyPropertyChanged("WorkingProcedure");
            }
        }

        public virtual Product OutputProduct
        {
            get {
                return _outputProduct;
            }
            set {
                _outputProduct = value;
                NotifyPropertyChanged("OutputProduct");
            }
        }

        public virtual int OutputQuantity
        {
            get {
                return _outputQuantity;
            }
            set {
                _outputQuantity = value;
                NotifyPropertyChanged("OutputQuantity");
            }
        }

        #endregion

        public virtual void AddRecipeInput(RecipeInput recipeInput)
        {
            RecipeInputs.Add(recipeInput);
            recipeInput.Recipe = this;
        }

        public virtual void RemoveRecipeInput(RecipeInput recipeInput)
        {
            RecipeInputs.Remove(recipeInput);
            recipeInput.Recipe = null;
        }
    }
}