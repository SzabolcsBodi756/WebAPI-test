using MySql.Data.MySqlClient;
using Mysqlx.Connection;

namespace Test
{
    public class Connect
    {

        public MySqlConnection ? Connection;

        private string _host;
        private string _db;
        private string _user;
        private string _password;

        private string ConnectionString;

        public Connect()
        {
            _host = "localhost";
            _db = "libary";
            _user = "root";
            _password = "";

            ConnectionString = $"SERVER = {_host}; DATABASE = {_db}; UID = {_user}; PASSWORD = {_password}; SslMode = none";

            Connection = new MySqlConnection(ConnectionString);
        }
    }
}
