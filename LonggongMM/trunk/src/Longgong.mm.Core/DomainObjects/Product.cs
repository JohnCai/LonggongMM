using Mavis.Core;

namespace Longgong.mm.Core
{
    public class Product : Entity
    {
        private string _description;
        private string _drawingNumber;
        private string _finishGoodConfigure;
        private string _finishGoodMode;
        private string _name;
        private double _netWeight;
        private ProductType _productType;
        private string _spec;

        private readonly SimpleRule _finishGoodModeNotEmptyRule;

        public Product()
        {
            _finishGoodModeNotEmptyRule = new SimpleRule("FinishGoodMode", "机型不能为空！",
                                                         () => string.IsNullOrEmpty(FinishGoodMode));
            AddRule(new SimpleRule("Name", "产品名称不能为空！", () => string.IsNullOrEmpty(Name)));
            AddRule(new SimpleRule("ProductType", "产品类型不能为空！", () => ProductType < 0));
        }

        public virtual ProductType ProductType
        {
            get { return _productType; }
            set
            {
                _productType = value;
                if (value == ProductType.FinishGood)
                    AddRule(_finishGoodModeNotEmptyRule);
                else
                    RemoveRule(_finishGoodModeNotEmptyRule);
                NotifyPropertyChanged("ProductType");
            }
        }

        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (value == "abc")
                    AddRule(_finishGoodModeNotEmptyRule);
                else
                    RemoveRule(_finishGoodModeNotEmptyRule);
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

        /// <summary>
        /// 图号
        /// </summary>
        public virtual string DrawingNumber
        {
            get { return _drawingNumber; }
            set
            {
                _drawingNumber = value;
                NotifyPropertyChanged("DrawingNumber");
            }
        }

        public virtual string Spec
        {
            get { return _spec; }
            set
            {
                _spec = value;
                NotifyPropertyChanged("Spec");
            }
        }

        public virtual double NetWeight
        {
            get { return _netWeight; }
            set
            {
                _netWeight = value;
                NotifyPropertyChanged("NetWeight");
            }
        }

        /// <summary>
        /// 机型，对于FinishiGood，不能为空。
        /// </summary>
        public virtual string FinishGoodMode
        {
            get { return _finishGoodMode; }
            set
            {
                _finishGoodMode = value;
                NotifyPropertyChanged("FinishGoodMode");
            }
        }

        /// <summary>
        /// 配置
        /// </summary>
        public virtual string FinishGoodConfigure
        {
            get { return _finishGoodConfigure; }
            set
            {
                _finishGoodConfigure = value;
                NotifyPropertyChanged("FinishGoodConfigure");
            }
        }
    }

    public enum ProductType
    {
        [DisplayString("None")]
        None = -1,

        [DisplayString("原材料")]
        RawMaterial,

        [DisplayString("中间件")]
        Immediate,

        [DisplayString("成品")]
        FinishGood
    }
}