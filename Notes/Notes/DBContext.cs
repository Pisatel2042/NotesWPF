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
    }

}
