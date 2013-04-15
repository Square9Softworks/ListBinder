using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

//Project Requirements: 
//.Net Runtime 3.5 or earilier
//Set to run on x86 platform

//Square 9® Softworks 2013

namespace ListSample
{
    public class ListUpdate
    {
        public List<string> ListFieldData(String DataColumn)
        {
            //Create our list that will return our data to SmartSearch. This must be named 'FieldData'.
            List<String> FieldData = new List<String>();
           
            //Creating SQL related objects  
            SqlConnection oConnection = new SqlConnection();
            SqlDataReader oReader = null;
            SqlCommand oCmd = new SqlCommand();
            
            //Setting our connection string for connection to the database, this will be different depending on your enviroment.
            oConnection.ConnectionString = "Data Source=(local)\\FULL2008;Initial Catalog=GrapeLanes;Integrated Security=SSPI;";

            //Check for valid column name
            if ((DataColumn.ToUpper() == "VENDNAME") || (DataColumn.ToUpper() == "CSTCTR"))
            {
                //Setting SQL query to return a list of data
                oCmd.CommandText = "SELECT DISTINCT " + DataColumn.ToUpper() + " FROM INVDATA";
            }
            else
            {
                //Returns an empty list in case of invalid column
                FieldData.Clear();
                return FieldData;
            }

            try
            {
                //Attempt to open the SQL connection and get the data.
                oConnection.Open();
                oCmd.Connection = oConnection;
                oReader = oCmd.ExecuteReader();
                
                //Take our SQL return and place it into our return object
                while (oReader.Read())
                {
                    FieldData.Add(oReader.GetString(0));
                }

                //Clean up
                oReader.Close();
                oConnection.Close();
            }
            catch (Exception)
            {
                //Returns an empty list in case of error 
                FieldData.Clear();
                return FieldData;
            }
            
            //Return our list data to SmartSearch if successful
            return FieldData;
        }

    }
}
