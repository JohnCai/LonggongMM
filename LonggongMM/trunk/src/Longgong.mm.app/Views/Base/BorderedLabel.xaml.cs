using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Longgong.mm.app.Views.Base
{
    /// <summary>
    /// Interaction logic for BorderedLabel.xaml
    /// </summary>
    public partial class BorderedLabel : UserControl
    {
        public BorderedLabel()
        {
            InitializeComponent();
        }

        public string LabelContent
        {
            get { return label1.Content.ToString(); }
            set { label1.Content = value; }
        }
    }
}
