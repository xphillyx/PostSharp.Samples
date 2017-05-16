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

namespace PostSharp.Samples.Xaml
{
    /// <summary>
    /// Interaction logic for FancyTextBlock.xaml
    /// </summary>
    public partial class FancyTextBlock : UserControl
    {
        public FancyTextBlock()
        {
            InitializeComponent();
        }

        public string Text { get; set; }


    }
}
