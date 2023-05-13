using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CSVExport
{
    class DatabaseConnection
    {
        public SqlConnection sqlConnection = new SqlConnection();
        public DatabaseConnection()
        {
            this.sqlConnection.ConnectionString = "Server=MANOJ-LENOVO\\MSSQLSERVER2022;database=Tiger;User Id=sa;Password=12345";
        }
        public DataTable GetEmployeeData()
        {
            this.sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "select * from EMPLOYEE";
            DataTable employeeTable = new DataTable();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            employeeTable.Load(sqlDataReader);
            this.sqlConnection.Close();
            return employeeTable;
        }
       public void SaveData(DataTable dataTable)
        {
            this.sqlConnection.Open();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {

                string InsertQuery = string.Empty;

                InsertQuery = "INSERT INTO EMPLOYEE " +
                              "(EMP_ID,EMP_NAME) " +
                              "VALUES ('" + dataTable.Rows[i]["EMP_ID"].ToString() + "','" + dataTable.Rows[i]["EMP_NAME"].ToString() + "')";
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = InsertQuery;
                sqlCommand.Connection = sqlConnection;

                sqlCommand.ExecuteNonQuery();
                
            }
            this.sqlConnection.Close();


        }
    }
}
