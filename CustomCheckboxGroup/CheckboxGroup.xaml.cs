using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CustomCheckboxGroup
{
    /// <summary>
    /// Логика взаимодействия для CheckboxGroup.xaml
    /// </summary>
    public partial class CheckboxGroup : UserControl
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
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.Register(nameof(ContentText), typeof(List<string>), typeof(CheckboxGroup), new PropertyMetadata(new List<string>(), OnContentTextChanged));
        public static readonly DependencyProperty ContentStatusProperty =
            DependencyProperty.Register(nameof(ContentStatus), typeof(List<bool>), typeof(CheckboxGroup), new PropertyMetadata(new List<bool>(), OnContentStatusChanged));
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(CheckboxGroup), new PropertyMetadata(null));

        private static void OnContentTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckboxGroup myClass = d as CheckboxGroup;
            myClass.UpdateCheckboxItem();
        }
        private static void OnContentStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckboxGroup myClass = d as CheckboxGroup;
            myClass.UpdateCheckboxStatuses((List<bool>)e.NewValue);
        }


        public CheckboxGroup()
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
            }
        }


        public void OnStatusCheckboxChanged(string content)
        {
            int indexToUpdate = ContentText.IndexOf(content);
            if (indexToUpdate >= 0)
            {
                if (ContentStatus.Count > indexToUpdate)
                {
                    // 1. Update Item ContentStatus
                    ContentStatus[indexToUpdate] = !ContentStatus[indexToUpdate];
                }
            }
            // 2. Execute a command
            InvokeCommand();
        }


        private void InvokeCommand()
        {
            if (Command != null && Command.CanExecute(null))
            { Command.Execute(null); }
        }
    }
}
