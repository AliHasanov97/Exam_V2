Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Security.Cryptography

Public Class Entrance

    Public Shared Function Authenticator()
        Try
            Dim ID
            IP_check()
            If (HttpContext.Current.Session("UserID") IsNot Nothing) Then
                ID = HttpContext.Current.Session("UserID").ToString
                Dim connect As String = Connection.NewUser
                Dim nov = "Basic"
                Dim query As String
                query = "SELECT COUNT(*)
            FROM [Users]
            WHERE ID = '" & ID & "'"
                Dim result As Integer = 0
                Using conn As New SqlConnection(connect)
                    Using cmd As New SqlCommand(query, conn)
                        conn.Open()
                        result = DirectCast(cmd.ExecuteScalar(), Integer)
                    End Using
                End Using
                If result > 0 Then
                    query = "SELECT COUNT(*)
            FROM [Purchase]
            where [UserID]= " & ID & " and [Action] = 'Premium'"
                    result = Connection.Get_Count(query)
                    If result > 0 Then
                        query = "SELECT TOP (1) *
                FROM [Purchase]
                where [UserID]= " & ID & " and [Action] = 'Premium'
                 ORDER
                  BY ID DESC "
                        Dim datas As String
                        Using conn As New SqlConnection(connect)
                            Using cmd As New SqlCommand(query, conn)
                                cmd.Connection = conn
                                conn.Open()
                                Dim myreade As SqlDataReader
                                myreade = cmd.ExecuteReader
                                myreade.Read()
                                datas = myreade("ID")
                            End Using
                        End Using
                        Dim day = expire(datas)
                        If day > 0 Then
                            nov = Connection.Get_value("ID", datas, "Type", "Purchase", "nUser")
                        Else
                            nov = "Basic"
                        End If
                    End If
                    Connection.Update_value("ID", ID, "Position", nov, "Users", "nUser")
                    Return ID
                End If
            Else
                HttpContext.Current.Response.Redirect("Login")
            End If
        Catch ex As Exception
        End Try
    End Function

    Public Shared Function IP_check()
        Try
            Dim IP = HttpContext.Current.Request.UserHostAddress
            Dim connect As String = Connection.NewUser
            Dim query As String
            query = "SELECT COUNT(*)
            FROM [IP_Block]
            WHERE [IP_address] = '" & IP & "'"
            Dim result As Integer = 0
            Using conn As New SqlConnection(connect)
                Using cmd As New SqlCommand(query, conn)
                    conn.Open()
                    result = DirectCast(cmd.ExecuteScalar(), Integer)
                End Using
            End Using
            If result > 0 Then
                query = "SELECT TOP (1) *
                FROM [IP_Block]
                where [IP_address]= '" & IP & "'
                 ORDER
                  BY ID DESC "
                Dim datas As String
                Using conn As New SqlConnection(connect)
                    Using cmd As New SqlCommand(query, conn)
                        cmd.Connection = conn
                        conn.Open()
                        Dim myreade As SqlDataReader
                        myreade = cmd.ExecuteReader
                        myreade.Read()
                        datas = myreade("Expire")
                    End Using
                End Using

                Dim lastdate As DateTime = DateTime.ParseExact(datas, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)
                Dim today As DateTime = DateTime.Today
                Dim daysBetween As Double = (lastdate - today).TotalDays

                If daysBetween > 0 Then
                    HttpContext.Current.Response.Redirect("IP_Block.html")
                End If
            End If

        Catch ex As Exception

        End Try
    End Function

    Public Shared Function Encrypt(clearText As String)
        Dim _DES As New TripleDESCryptoServiceProvider()
        Dim _HashMD5 As New MD5CryptoServiceProvider()

        _DES.Key = _HashMD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(clearText))
        _DES.Mode = CipherMode.ECB
        Dim _DESEncrypt As ICryptoTransform = _DES.CreateEncryptor()
        Dim _Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(clearText)
        clearText = Convert.ToBase64String(_DESEncrypt.TransformFinalBlock(_Buffer, 0, _Buffer.Length))
        Return clearText
    End Function
    Public Shared Function Check(ByVal email As String)
        Dim connect As String = Connection.NewUser
        Dim query As String
        query = "SELECT COUNT(*)
FROM Users
WHERE [Email] = '" & email & "'"
        Dim result As Integer = 0
        Using conn As New SqlConnection(connect)
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                result = DirectCast(cmd.ExecuteScalar(), Integer)
            End Using
        End Using
        If result > 0 Then
            Dim connecte As String = Connection.NewUser
            Dim querye As String
            querye = "SELECT        [ID]
FROM            Users
WHERE        [Email] = '" & email & "'"


            Dim datas As String
            Using conni As New SqlConnection(connecte)
                Using cmdi As New SqlCommand(querye, conni)
                    cmdi.Connection = conni
                    conni.Open()
                    Dim myreade As SqlDataReader

                    myreade = cmdi.ExecuteReader
                    myreade.Read()

                    datas = myreade("ID")
                End Using

                Return datas

            End Using
        Else
            Return "False"
        End If
    End Function

    Public Shared Function expire(ByVal ID As Integer)
        Dim guy = Connection.Get_value("ID", ID, "Expire", "Purchase", "nUser")
        Dim lastdate As DateTime = DateTime.ParseExact(guy, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)
        Dim today As DateTime = DateTime.Today
        Dim daysBetween As Double = (lastdate - today).TotalDays
        Return daysBetween
    End Function

End Class
