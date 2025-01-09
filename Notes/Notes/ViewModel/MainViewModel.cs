using Oracle.ManagedDataAccess;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Notes.Model;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System;
using System.Windows.Input;

namespace Notes.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly DBContext dBContext;

        public ObservableCollection<Tasks_Notes> Task_Note { get; set; }

        public MainViewModel()
        {
            string connectionString = "User Id=;Password=1111;Data Source=//localhost:1521/orcl;DBA Privilege=SYSDBA"; 
            dBContext = new DBContext(connectionString);
            Task_Note = new ObservableCollection<Tasks_Notes>();
           LoadTask();
             

            
        }

        private void LoadTask()
        {
            var task = dBContext.GetTask();
            foreach (var _task in task)
            {
                Task_Note.Add(_task);
            }
        }
        private void AddTask()
        {
            var newTaskNotes = new Tasks_Notes
            {
              
            };

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
