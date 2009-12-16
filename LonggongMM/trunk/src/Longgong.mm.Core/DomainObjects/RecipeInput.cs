using System;
using Mavis.Core;

namespace Longgong.mm.Core
{
    public class RecipeInput : Entity
    {
        private Product _inputProduct;

        private int _inputQuantity;

        private Recipe _recipe;

        public RecipeInput()
        {
            InputProduct = new Product();
        }
        
        public virtual Product InputProduct
        {
            get { return _inputProduct; }
            set
            {
                _inputProduct = value;
                NotifyPropertyChanged("InputProduct");
            }
        }

        public virtual int InputQuantity
        {
            get { return _inputQuantity; }
            set
            {
                _inputQuantity = value;
                NotifyPropertyChanged("InputQuantity");
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