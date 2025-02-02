using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Proxies;
using System.Windows.Input;
using Notes.Command;
using Notes.Model;
using Prism.Commands;

namespace Notes.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly DBContext dBContext;
        public ICommand AddCommand { get; set; } 
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public DBContext DBContext;
        public MainViewModel()
        {
            var connectionString =
                "Data Source= (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl)));Password=q;User ID=ad_l";
            dBContext = new DBContext(connectionString);
            Task_Note = new ObservableCollection<Tasks_Notes>();

            AddCommand = new ReplayCommand(Add, CanAdd);
            UpdateCommand = new ReplayCommand(Update,CanUpdate);
            DeleteCommand = new ReplayCommand(Delete, CanDelete);

            LoadTask();
        }

        private bool CanDelete(object obj)
        {
            return true;
        }

        private void Delete(object obj)
        {
            
        }

        private bool CanUpdate(object obj)
        {
            return true;
        }

        private void Update(object obj)
        {
           UpdateTask();
        }

        private bool CanAdd(object obj)
        {
            return true;
        }

        private void Add(object obj)
        {
            AddTask();
        }




        public ObservableCollection<Tasks_Notes> Task_Note { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

       

        private void LoadTask()
        {
            var task = dBContext.GetTask();
            foreach (var _task in task) Task_Note.Add(_task);
        }

        private void AddTask()
        {
            var newTaskNotes = new Tasks_Notes();
        }

        private void UpdateTask()
        {
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}