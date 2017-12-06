using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TestDatabaze
{
    public sealed class DbCommands
    {
        private static SqlConnection instance = null;
        private static readonly object padlock = new object();

        public static SqlConnection Connection
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SqlConnection(connectionString: @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Vaclav.Zidek\Documents\Visual Studio 2017\Projects\MVC\TestDatabaze\TestDatabaze\App_Data\Database.mdf; Integrated Security = True");
                    }
                    return instance;
                }
            }
        }

        public static void Connect()
        {
            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("SQL Connection Failed", ex);
            }
        }

        public static void Close()
        {
            Connection.Close();
        }

        public static ConnectionState State()
        {
            return Connection.State;
        }

        public static List<UserProfile> SelectPersons(String SqlSelect)
        {
            var ListP = new List<UserProfile>();

            try
            {
                using (SqlCommand cmd = new SqlCommand(SqlSelect, Connection))
                {
                    Connect();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserProfile dtb = new UserProfile();
                            dtb.Id = reader.GetInt32(0);
                            dtb.IsAdmin = reader.GetInt32(1);
                            dtb.Username = reader.GetString(2);
                            dtb.Password = reader.GetString(3);
                            dtb.Date = reader.GetDateTime(4);

                            ListP.Add(dtb);
                        }
                    }
                    Close();
                }

                return ListP;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("SQL Command (Select) Failed", ex);
            }
        }


        public static UserProfile SelectPerson(String Username)
        {
            UserProfile dtb = new UserProfile();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Id, IsAdmin, Name, Password, Date FROM Users WHERE Name = @Name", Connection);
                cmd.Parameters.AddWithValue("@Name", Username);
                Connect();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dtb.Id = reader.GetInt32(0);
                            dtb.IsAdmin = reader.GetInt32(1);
                            dtb.Username = reader.GetString(2);
                            dtb.Password = reader.GetString(3);
                            dtb.Date = reader.GetDateTime(4);
                        }
                    }
                    Close();

                return dtb;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("SQL Command (Select) Failed", ex);
            }
        }

        public static Boolean UserExists(String Username)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Users WHERE Name = @Name", Connection);
                cmd.Parameters.AddWithValue("@Name", Username);
                Connection.Open();
                var result = cmd.ExecuteScalar();

                Close();

                if (result != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("SQL Command (Select) Failed", ex);
            }
        }

        public static void InsertPerson(UserProfile p)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Users (IsAdmin, Name, Password, Date) VALUES (@IsAdmin, @Name, @Password, @Date)", Connection))
                {
                    Connect();

                    cmd.Parameters.AddWithValue("@IsAdmin", 0);
                    cmd.Parameters.AddWithValue("@Name", p.Username);
                    cmd.Parameters.AddWithValue("@Password", Md5.CalculateMD5Hash(p.Password));
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.ExecuteNonQuery();

                    Close();
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("SQL Command (Insert) Failed", ex);
            }
        }

    }
}