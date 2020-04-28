using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Added

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MedicalDatabaseApplication
{
    class SQLHelper
    {
        // String Holds The Connection Stuff For The Database
        private string _Conn;

        public SQLHelper()
        {
            //get connection string from App.config file
            _Conn = ConfigurationManager.ConnectionStrings["TheSand"].ConnectionString;

        }

        public DataTable executeSQL(string sql)
        {
            //create connection object and get the connection string from private variable _Conn
            SqlConnection objConnection = new SqlConnection(_Conn);

            //create command object get the SQL to execute from the sql parameter passed as input to this function
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //open database connection
            objConnection.Open();

            //execute SQL and return dataReader
            SqlDataReader objDataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);

            //create data table
            DataTable objDataTable = new DataTable();
            objDataTable.Load(objDataReader);

            return objDataTable;
        }

        public DataTable executeSQL(string sql, SqlParameter[] parameters)
        {
            //create connection object get connection string from private variable _Conn
            SqlConnection objConnection = new SqlConnection(_Conn);

            //create command object get SQL to execute from the sql parameter passed as input to this function
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //fill parameters – this is the method we created to attach the parameters to the command object
            fillParameters(objCommand, parameters);

            //open database connection
            objConnection.Open();

            //execute SQL and store in dataReader
            SqlDataReader objDataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);

            //create data table
            DataTable objDataTable = new DataTable();

            objDataTable.Load(objDataReader);

            return objDataTable;
        }

        public object scalarSQL(string sql)
        {
            //create the connection object and get the connection string from the private variable _Conn
            SqlConnection objConnection = new SqlConnection(_Conn);

            //create command object get SQL to execute from the sql parameter passed as input to this function
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //create a variable that will hold the return value
            object objRetValue;

            //open database connection
            objConnection.Open();

            //execute SQL
            objRetValue = objCommand.ExecuteScalar();

            //close connection
            objConnection.Close();

            //execute SQL and return value
            return objRetValue;
        }

        public object scalarSQL(string sql, SqlParameter[] parameters)
        {
            //create connection object get connection string from private variable _Conn
            SqlConnection objConnection = new SqlConnection(_Conn);

            //create command object get SQL to execute from the sql parameter passed as input to this method
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //fill parameters
            fillParameters(objCommand, parameters);

            //create a variable that will hold the return value
            object objRetValue;

            //open database connection
            objConnection.Open();

            //execute SQL
            objRetValue = objCommand.ExecuteScalar();

            //close connection
            objConnection.Close();

            //execute SQL and return value
            return objRetValue;
        }

        public int NonQuerySQL(string sql)
        {
            //create connection object and get the connection string from private variable _Conn
            SqlConnection objConnection = new SqlConnection(_Conn);

            //create command object get SQL to execute from the sql parameter passed as input to this function
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //create a variable that will hold the return value
            int intRetValue;

            //open database connection
            objConnection.Open();

            //execute SQL
            intRetValue = objCommand.ExecuteNonQuery();

            //close connection
            objConnection.Close();

            //execute SQL and return value
            return intRetValue;
        }

        public int NonQuerySQL(string sql, SqlParameter[] parameters)
        {
            //create connection object get connection string from private variable _Conn
            SqlConnection objConnection = new SqlConnection(_Conn);

            //create command object get SQL to execute from the sql parameter passed as input to this function
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //fill parameters
            fillParameters(objCommand, parameters);

            //create a variable that will hold the return value
            int intRetValue;

            //open database connection
            objConnection.Open();

            //execute SQL
            intRetValue = objCommand.ExecuteNonQuery();

            //close connection
            objConnection.Close();

            //execute SQL and return value
            return intRetValue;
        }

        private void fillParameters(SqlCommand objCommand, SqlParameter[] parameters)
        {
            int i;
            //for each parameter passed in add it to the command object parameters collection
            for (i = 0; i < parameters.Length; i++)
            {
                objCommand.Parameters.Add(parameters[i]);
            }
        }
    }
}
