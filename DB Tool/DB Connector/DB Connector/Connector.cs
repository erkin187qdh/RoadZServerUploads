using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;


namespace DB_Connector
{
    class Connector
    {
        readonly string connString = "";
        MySqlConnection connection;
        List<IReadDataReturn> listeners = new List<IReadDataReturn>();
        public delegate void readDataReturnDelegate(string data);
        public static readDataReturnDelegate readDataReturn;
        public Connector()
        {
            connection = new MySqlConnection(connString);
        }

        async public void insertUser(string fname, string lname, string username, string email, string pw, string dob, string phonenumber, string secretquestion, string secretanswer)
        {
            await connection.OpenAsync();
            using(var sqlStatement = new MySqlCommand())
            {
                sqlStatement.Connection = connection;
                sqlStatement.CommandText = $"INSERT INTO user(fname, lname, username, email, pw, dob, phonenumber, secretquestion, secretanswer) VALUES ({fname},{lname},{username},{email},{pw},{dob},{phonenumber},{secretquestion},{secretanswer})";
                await sqlStatement.ExecuteNonQueryAsync();
            }
        }

        async public void readLine(string table, string field)
        {
            StringBuilder returnStatement = new StringBuilder();
            using (var sqlStatement = new MySqlCommand($"SELECT {field} from {table}", connection))
            {
                using(var reader = await sqlStatement.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        string tempStringFromSQL = reader.GetString(0);
                        Console.WriteLine(tempStringFromSQL);
                        returnStatement.Append(tempStringFromSQL);

                    }
                }
            }


            string data = returnStatement.ToString();

            foreach (var listener in listeners) //method 1
            {
                listener.receiveData(data);
            }


            readDataReturn?.Invoke(data); //method 2

        }


        public void subscribeListener(IReadDataReturn listener)
        {
            listeners.Add(listener);
        }
    }



    public interface IReadDataReturn
    {
        public void receiveData(string data);
    }
}
