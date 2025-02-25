
namespace CustomCheckboxGroup
{
    public class CheckboxItem
    {
        public Action<string> ChangStatus;
        private bool _status;
        public string Content { get; set; }
        public bool Status
        {
            get { return _status; }
            set
            {
                _status = value;
                ChangStatus?.Invoke(Content);
            }
        }


        public CheckboxItem()
        {
            Content = "";
            Status = true;
        }


        public CheckboxItem(string contentText, bool isChecked)
        {
            Content = contentText;
            Status = isChecked;
        }
    }
}
