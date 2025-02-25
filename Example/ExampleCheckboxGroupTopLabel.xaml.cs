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
    /// Логика взаимодействия для ExampoleCheckboxGroupTopLabel.xaml
    /// </summary>
    public partial class ExampleCheckboxGroupTopLabel : Window
    {
        private MainViewModel _viewModel;

        public ExampleCheckboxGroupTopLabel()
        {
            InitializeComponent();

            _viewModel = new MainViewModel();
            this.DataContext = _viewModel;
        }
    }
}
