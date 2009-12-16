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
            _finishGoodModeNotEmptyRule = new SimpleRule("FinishGoodMode", "���Ͳ���Ϊ�գ�",
                                                         () => string.IsNullOrEmpty(FinishGoodMode));
            AddRule(new SimpleRule("Name", "��Ʒ���Ʋ���Ϊ�գ�", () => string.IsNullOrEmpty(Name)));
            AddRule(new SimpleRule("ProductType", "��Ʒ���Ͳ���Ϊ�գ�", () => ProductType < 0));
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
        /// ͼ��
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
        /// ���ͣ�����FinishiGood������Ϊ�ա�
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
        /// ����
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

        [DisplayString("ԭ����")]
        RawMaterial,

        [DisplayString("�м��")]
        Immediate,

        [DisplayString("��Ʒ")]
        FinishGood
    }
}