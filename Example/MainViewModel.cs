using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Example
{
    public class MainViewModel
    {
        public List<People> _peoples;

        public ObservableCollection<People> Peoples { get; set; }
        public List<string> Names { get; set; }

        public List<bool> Statuses { get; set; }


        private ICommand _updateTable;
        public ICommand UpdateTable
        {
            get { return _updateTable; }
            set { _updateTable = value; }
        }
        

        public MainViewModel()
        {
            _peoples = new List<People>()
            {
                new People(){ Name = "Tom", Age = 25, Weight = 76, Height = 190 },
                new People(){ Name = "Ann", Age = 16, Weight = 40, Height = 150 },
                new People(){ Name = "Bob", Age = 63, Weight = 65, Height = 170 },
                new People(){ Name = "Poll", Age = 13, Weight = 42, Height = 155 },
                new People(){ Name = "Mary", Age = 7, Weight = 25, Height = 105 },
                new People(){ Name = "Henry", Age = 33, Weight = 66, Height = 175 },
                new People(){ Name = "Elizabeth", Age = 27, Weight = 62, Height = 185 },
                new People(){ Name = "James", Age = 27, Weight = 85, Height = 170 },
                new People(){ Name = "Sarah", Age = 15, Weight = 52, Height = 170 },
                new People(){ Name = "Jack", Age = 19, Weight = 78, Height = 177 },
            };
            Peoples = new ObservableCollection<People>(_peoples); 

            Names = _peoples.Select(x => x.Name).ToList();
            Statuses = _peoples.Select(x => true).ToList();
            _updateTable = new CustomCommand(UpdatePeoples);
        }


        public void UpdatePeoples(object sender)
        {
            Peoples.Clear();
            for (int i = 0; _peoples.Count > i; i++)
            {
                if (Statuses[i] == true)
                { Peoples.Add(_peoples[i]); }
            }
        }
    }



    public class People
    { 
        public string Name { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
    }
}
