using Oracle.ManagedDataAccess;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Notes.Model;
using Oracle.ManagedDataAccess.Client;

namespace Notes.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Tasks_Notes> _task_notes;
        public ObservableCollection<Tasks_Notes> tasks_Notes
        {
            get => _task_notes;
            set
            {
                if (_task_notes != value)
                {
                    _task_notes = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public MainViewModel()
        {
           tasks_Notes =  new ObservableCollection<Tasks_Notes>();
           LoadTasks(); 
        }

        public void LoadTasks()
        {
           

            string connectionString = "Server =DESKTOP-2LQI6HI; DateBase= Dummy; Trusted_Connection=True";

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (var command = new OracleCommand("Select * from Dummy",connection)) 
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks_Notes.Add(new Tasks_Notes
                            {
                                Id = reader.GetInt32(0),
                                DateTime = reader.GetDateTime(1),
                                Name = reader.GetString(2),
                                Number = reader.GetInt32(3)
                            });
                        }
                    }
                }
            }

        }
    }
}
