using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Example
{
    /// <summary>
    /// Логика взаимодействия для ExampleCustomCheckboxGroup.xaml
    /// </summary>
    public partial class ExampleCustomCheckboxGroup : Window
    {
        private MainViewModel _viewModel;

        public ExampleCustomCheckboxGroup()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            this.DataContext = _viewModel;
        }
    }
}
