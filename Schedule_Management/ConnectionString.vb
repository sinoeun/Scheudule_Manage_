Imports System.Data.SqlClient
Module ConnectionString
    Public connStr As SqlConnection = New SqlConnection("Data Source=10.0.11.23;Initial Catalog=Schedule_DB ;Trusted_Connection=Yes")
End Module
