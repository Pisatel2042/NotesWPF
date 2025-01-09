using Notes.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class DBContext
    {
        private string connectionString;

        public DBContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Tasks_Notes> GetTask()
        {
            var tasks_notes = new List<Tasks_Notes>();

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand("select Id,datetime, Name , countnumber from timeevents", connection))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks_notes.Add(new Tasks_Notes
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

            return tasks_notes;
        }
        public void AddTask(Tasks_Notes tasks_notes)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand("insert into timeevents (datetime,name,countnumber) values (:datetime, :name, :number)", connection))
                {
                    command.Parameters.Add("datetime", tasks_notes.DateTime);
                    command.Parameters.Add("name", tasks_notes.Name);
                    command.Parameters.Add("number", tasks_notes.Number);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateTask(Tasks_Notes tasks_notes)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand("update timeevents set datetime = :datetime, name = :name, countnumber = :number WHERE id = :id", connection))
                {
                    command.Parameters.Add("id", tasks_notes.Id);
                    command.Parameters.Add("datetime", tasks_notes.DateTime);
                    command.Parameters.Add("name", tasks_notes.Name);
                    command.Parameters.Add("number", tasks_notes.Number);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteTask(Tasks_Notes tasks_notes)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand("delete from timeevents where id = :id", connection))
                {
                    command.Parameters.Add("id", tasks_notes.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }

}
