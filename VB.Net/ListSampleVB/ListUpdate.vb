Imports System.Collections.Generic
Imports System.Text
Imports System.Data.SqlClient

'Project Requirements: 
'.Net Runtime 3.5 or earilier
'Set to run on x86 platform

'Square 9® Softworks 2013

Namespace ListSample
    Public Class ListUpdate
        Public Function ListFieldData(SQLColumn As [String]) As List(Of String)
            'Create our list that will return our data to SmartSearch. This must be named 'FieldData'.
            Dim FieldData As New List(Of [String])()

            'Creating SQL related objects  
            Dim oConnection As New SqlConnection()
            Dim oReader As SqlDataReader = Nothing
            Dim oCmd As New SqlCommand()

            'Setting our connection string for connection to the database, this will be different depending on your enviroment.
            oConnection.ConnectionString = "Data Source=(local)\FULL2008;Initial Catalog=GrapeLanes;Integrated Security=SSPI;"

            'Check for valid column name
            If (SQLColumn.ToUpper() = "VENDNAME") OrElse (SQLColumn.ToUpper() = "CSTCTR") Then
                'Setting SQL query to return a list of data
                oCmd.CommandText = "SELECT DISTINCT " & SQLColumn.ToUpper() & " FROM INVDATA"
            Else
                'Returns an empty list in case of invalid column
                FieldData.Clear()
                Return FieldData
            End If

            Try
                'Attempt to open the SQL connection and get the data.
                oConnection.Open()
                oCmd.Connection = oConnection
                oReader = oCmd.ExecuteReader()

                'Take our SQL return and place it into our return object
                While oReader.Read()
                    FieldData.Add(oReader.GetString(0))
                End While

                'Clean up
                oReader.Close()
                oConnection.Close()
            Catch generatedExceptionName As Exception
                'Returns an empty list in case of error 
                FieldData.Clear()
                Return FieldData
            End Try

            'Return our list data to SmartSearch if successful
            Return FieldData
        End Function

    End Class
End Namespace
