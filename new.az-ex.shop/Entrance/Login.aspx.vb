Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Web.Services.Description
Imports System.Text


Public Class Login
    Inherits System.Web.UI.Page




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Dim postData As String = "exampleData"
        Dim url As String = "Result"
        Dim script As String = "<script language='javascript'>" & _
                           "var form = document.createElement('form');" & _
                           "form.setAttribute('method', 'post');" & _
                           "form.setAttribute('action', '" & url & "');" & _
                           "var hiddenField = document.createElement('input');" & _
                           "hiddenField.setAttribute('type', 'hidden');" & _
                           "hiddenField.setAttribute('name', 'data');" & _
                           "hiddenField.setAttribute('value', '" & postData & "');" & _
                           "form.appendChild(hiddenField);" & _
                           "document.body.appendChild(form);" & _
                           "form.submit();" & _
                           "</script>"

    ClientScript.RegisterStartupScript(Me.GetType(), "redirect", script)


        Try
            Entrance.IP_check()
            If Session("UserID") Is Nothing Then

                'Session("UserID") = 1
                'Response.Redirect("read")
                Email.Attributes.Add("placeholder", "Email ünvanınızı qeyd edin...")
                Password.Attributes.Add("placeholder", "Şifrənizi qeyd edin...")
                Email.Attributes.Add("type", "email")
                Password.Attributes.Add("type", "password")
                Second.Visible = False
            Else
                Response.Redirect("index")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result = Entrance.Check(Email.Text)
        If result = "False" Then
            MSG.InnerText = "Email mövcud deyil"
        Else
            First.Visible = False
            Second.Visible = True
            Name1.InnerText = Connection.Get_value("ID", result, "Full_Name", "Users", "nUser")
            Avatar.Src = Connection.Get_value("ID", result, "Avatar", "Users", "nUser")
        End If
        If Second.Visible = True Then
            Dim pas = Password.Text
            Dim val = Connection.Get_value("ID", result, "Password", "Users", "nUser")
            If Not pas = "" Then
                If val = pas Then
                    Session("UserID") = result
                    Entrance.Authenticator()
                    Response.Redirect("index")
                Else
                    MSG1.Visible = True
                    Dim limit As Integer = Label1.Text
                    Label1.Text += 1
                    MSG1.InnerText = "Daxil etdiyiniz şifrə yanlışdır.Son " & 3 - Label1.Text & " cəhd"
                    If Label1.Text = 2 Then
                        Dim startDate As DateTime = DateTime.Today
                        Dim leaveNo As Integer = 1
                        Dim exp As String = startDate.AddDays(leaveNo)

                        Dim connectionStrings As String = Connection.NewUser
                        Dim connections As New SqlConnection(connectionStrings)
                        connections.Open()
                        Dim command As SqlCommand = connections.CreateCommand()
                        command.CommandText = "INSERT INTO [u1602830_Moderator].[IP_Block]
                      ([IP_address]
                      ,[Expire])
                VALUES
                      ( '" & Request.UserHostAddress & "'
                        ,'" & exp & "')"
                        command.ExecuteNonQuery()
                        connections.Close()
                    End If
                End If
            Else
                MSG1.Visible = False
            End If
        End If
    End Sub
End Class