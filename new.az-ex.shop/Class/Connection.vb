Imports System.Data.SqlClient

Public Class Connection

    Public Shared Function LocalDB()

        Return "Data Source = ALI;Initial Catalog=Balogna;Integrated Security=True"

    End Function

    Public Shared Function NewUser()

        Return "Data Source = wpl36.hosting.reg.ru;Initial Catalog=u1602830_NewBalogna;User ID=u1602830_Moderator;Password=4750816Aa"

    End Function
    Public Shared Function User()

        Return "Data Source = wpl36.hosting.reg.ru;Initial Catalog=u1602830_Balogna;User ID=u1602830_Moderator;Password=4750816Aa"

    End Function
    Public Shared Function Cavab()

        Return "Data Source = wpl36.hosting.reg.ru;Initial Catalog=u1602830_Answers;User ID=u1602830_Moderator;Password=4750816Aa"

    End Function

    Public Shared Function Get_value(ByVal w_parameter As String, ByVal W_value As String, ByVal g_parameter As String, ByVal table As String, ByVal Base As String)
        Try
            If Base = "User" Then
                Base = User()
            ElseIf Base = "Cavab" Then
                Base = Cavab()
            ElseIf Base = "nUser" Then
                Base = NewUser()
            ElseIf Base = "Local" Then
                Base = LocalDB()
            End If
            Dim connectionString As String = Base
            Dim query As String
            query = "SELECT " & g_parameter & "
    From           " & table & "
    WHERE        " & w_parameter & " = '" & W_value & "'"
            Dim datas As String
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Connection = conn
                    conn.Open()
                    Dim myreade As SqlDataReader
                    myreade = cmd.ExecuteReader
                    myreade.Read()
                    datas = myreade(g_parameter)
                End Using
            End Using
            Return datas
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function Update_value(ByVal get_parameter As String, ByVal get_value As String, ByVal set_parameter As String, ByVal set_value As String, ByVal where As String, ByVal Base As String)
        Try
            If Base = "User" Then
                Base = User()
            ElseIf Base = "Cavab" Then
                Base = Cavab()
            ElseIf Base = "nUser" Then
                Base = NewUser()
            End If
            Using connection As New SqlConnection(Base)
                connection.Open()
                Dim command As SqlCommand = connection.CreateCommand()
                command.CommandText = "
                UPDATE " & where & "
                  SET " & set_parameter & " = N'" & set_value & "'
 WHERE " & get_parameter & " = N'" & get_value & "'"
                command.ExecuteNonQuery()
                connection.Close()
            End Using

        Catch ex As Exception

        End Try
        Return ""
    End Function
    Public Shared Function Get_Count(ByVal command As String)
        Dim connect As String = Connection.NewUser
        Dim query As String
        query = command
        Dim result As Integer = 0
        Using conn As New SqlConnection(connect)
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                result = DirectCast(cmd.ExecuteScalar(), Integer)
            End Using
        End Using
        Return result
    End Function
End Class


