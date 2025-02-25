using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomCheckboxGroup
{
    /// <summary>
    /// Логика взаимодействия для CheckboxGroupTopLabel.xaml
    /// </summary>
    public partial class CheckboxGroupTopLabel : UserControl
    {
        public ObservableCollection<CheckboxItem> CheckBoxItemCollection { get; set; }


        public List<string> ContentText
        {
            get { return (List<string>)GetValue(ContentTextProperty); }
            set { SetValue(ContentTextProperty, value); }
        }
        public List<bool> ContentStatus
        {
            get { return (List<bool>)GetValue(ContentStatusProperty); }
            set { SetValue(ContentStatusProperty, value); }
        }
        public string MainText
        {
            get { return (string)GetValue(MainTextProperty); }
            set { SetValue(MainTextProperty, value); }
        }
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }


        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.Register(nameof(ContentText), typeof(List<string>), typeof(CheckboxGroupTopLabel), new PropertyMetadata(new List<string>(), OnContentTextChanged));
        public static readonly DependencyProperty ContentStatusProperty =
            DependencyProperty.Register(nameof(ContentStatus), typeof(List<bool>), typeof(CheckboxGroupTopLabel), new PropertyMetadata(new List<bool>(), OnContentStatusChanged));
        public static readonly DependencyProperty MainTextProperty =
            DependencyProperty.Register(nameof(MainText), typeof(string), typeof(CheckboxGroupTopLabel), new PropertyMetadata("All", OnMainTextChanged));
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(CheckboxGroupTopLabel), new PropertyMetadata(null));


        public CheckboxGroupTopLabel()
        {
            InitializeComponent();
            CheckBoxItemCollection = new ObservableCollection<CheckboxItem>();
        }


        private void UpdateCheckboxItem()
        {
            CheckBoxItemCollection.Clear();
            List<bool> tmpList = new List<bool>();

            bool checkStatus = true;
            for (int i = 0; ContentText.Count > i; i++)
            {
                CheckboxItem tmpCheckbox = new CheckboxItem(ContentText[i], checkStatus);
                tmpCheckbox.ChangStatus += OnStatusCheckboxChanged;
                CheckBoxItemCollection.Add(tmpCheckbox);
                tmpList.Add(checkStatus);
            }
            MainCheckbox.IsChecked = checkStatus;
        }


        private void UpdateCheckboxStatuses(List<bool> newCheckedList)
        {
            List<CheckboxItem> CheckBoxItemsCopy = CheckBoxItemCollection.Select(x => x).ToList();
            if (newCheckedList.Count == CheckBoxItemsCopy.Count)
            {
                CheckBoxItemCollection.Clear();
                for (int i = 0; CheckBoxItemsCopy.Count > i; i++)
                {
                    CheckboxItem tmpCheckbox = new CheckboxItem(CheckBoxItemsCopy[i].Content, newCheckedList[i]);
                    tmpCheckbox.ChangStatus += OnStatusCheckboxChanged;
                    CheckBoxItemCollection.Add(tmpCheckbox);
                }
                UpdateMainCheckboxStatus();
            }
        }


        public void UpdateMainCheckboxStatus()
        {
            if (CheckBoxItemCollection.All(x => x.Status == true))
            { MainCheckbox.IsChecked = true; }
            else { MainCheckbox.IsChecked = false; }
        }


        private void InvokeCommand()
        {
            if (Command != null && Command.CanExecute(null))
            { Command.Execute(null); }
        }


        public void OnStatusCheckboxChanged(string content)
        {
            int indexToUpdate = ContentText.IndexOf(content);

            if (indexToUpdate >= 0)
            {
                // 1. Update ContentStatus
                if (ContentStatus.Count > indexToUpdate)
                {
                    ContentStatus[indexToUpdate] = !ContentStatus[indexToUpdate];
                    // 2. Update status main checkbox
                    UpdateMainCheckboxStatus();
                }
                // 3. Execute command
                InvokeCommand();
            }
        }


        private static void OnContentTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckboxGroupTopLabel myClass = d as CheckboxGroupTopLabel;
            myClass.UpdateCheckboxItem();


        }
        private static void OnContentStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckboxGroupTopLabel myClass = d as CheckboxGroupTopLabel;
            myClass.UpdateCheckboxStatuses((List<bool>)e.NewValue);
        }


        private static void OnMainTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckboxGroupTopLabel myClass = d as CheckboxGroupTopLabel;
            myClass.MainCheckbox.Content = (string)e.NewValue;
        }


        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            bool test;

            if (bool.TryParse(checkbox.IsChecked.ToString(), out test))
            {
                List<bool> values = ContentText.Select(x => test).ToList();
                if (ContentStatus.Count == values.Count)
                {
                    for (int i = 0; ContentText.Count > i; i++)
                    { ContentStatus[i] = values[i]; }
                }
                // 1. Update status checkbox on the form
                UpdateCheckboxStatuses(values);
                // 2. Execute command
                InvokeCommand();
            }
        }
    }
}
