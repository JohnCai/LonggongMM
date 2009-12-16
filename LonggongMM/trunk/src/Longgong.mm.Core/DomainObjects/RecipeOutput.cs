using System;
using Mavis.Core;

namespace Longgong.mm.Core
{
    public class RecipeOutput : Entity
    {
        private Product _outputProduct;
        private int _outputQuantity;
        private Recipe _recipe;

        public RecipeOutput()
        {
            OutputProduct = new Product();
        }
        
        public virtual Product OutputProduct
        {
            get { return _outputProduct; }
            set
            {
                _outputProduct = value;
                NotifyPropertyChanged("OutputProduct");
            }
        }

        public virtual int OutputQuantity
        {
            get { return _outputQuantity; }
            set
            {
                _outputQuantity = value;
                NotifyPropertyChanged("OutputQuantity");
            }
        }

        public virtual Recipe Recipe
        {
            get {
                return _recipe;
            }
            set {
                _recipe = value;
                NotifyPropertyChanged("Recipe");
            }
        }
    }
}