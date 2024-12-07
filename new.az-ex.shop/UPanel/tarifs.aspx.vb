Imports System.Data.SqlClient

Public Class WebForm6
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Status()
    End Sub

    Protected Sub Status()
        Dim ID = Entrance.Authenticator
        Dim status = Connection.Get_value("ID", ID, "Position", "Users", "nUser")
        If status = "Pro" Or status = "Enterprise" Then
            Dim Query = "SELECT TOP (1) *
                FROM [Purchase]
                where [UserID]= " & ID & " and [Action] = 'Premium'
                 ORDER
                  BY ID DESC "
            Dim datas() As String
            Using conn As New SqlConnection(Connection.NewUser)
                Using cmd As New SqlCommand(Query, conn)
                    cmd.Connection = conn
                    conn.Open()
                    Dim myreade As SqlDataReader
                    myreade = cmd.ExecuteReader
                    myreade.Read()
                    datas = {myreade("ID"), myreade("Note"), myreade("Expire")}
                End Using
            End Using
            If status = "Pro" Then
                Pro.Visible = False
                Enterprise.Text = "Tarifi seç"
                Dim limit = datas(1)
                Dim message = ""
                If limit > 0 Then
                    message = "<i class='fa-solid fa-check'></i> " & limit & " ödənişsiz imtahan."
                    Pro.Visible = False
                Else
                    message = "<i class='fa-solid fa-xmark'></i> Ödənişsiz imtahan limitiniz bitib."
                    Pro.Visible = True
                    Pro.Text = "Tarifi yeniləmək"
                End If
                Dim pan1 As New HtmlGenericControl("div")
                pan1.InnerHtml = "
<div class='card card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-regular fa-gem' style='font-size:30px'></i>
                </div>
                <div class='col'>
                        <span style='font-size:18px'>Pro premium özəlliyi</span>
                    <br><span><i class='fa-regular fa-clock'></i> " & datas(2) & " tarixinədək aktivdir.</span>             
                    <br><span>" & message & "</span></div>
            </div>"
                PlaceHolder1.Controls.Add(pan1)
            ElseIf status = "Enterprise" Then
                Pro.Visible = False
                Enterprise.Visible = False
                Dim pan1 As New HtmlGenericControl("div")
                pan1.InnerHtml = "
<div class='card card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-regular fa-gem' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <span style='font-size:18px'>Enterprise premium özəlliyi</span>
                    <br><span><i class='fa-regular fa-clock'></i> " & datas(2) & " tarixinədək aktivdir.</span>             
                    <br>
                   </div>
            </div>"
                PlaceHolder1.Controls.Add(pan1)
            End If
        Else
            Pro.Visible = True
            Enterprise.Visible = True
            Pro.Text = "Tarifi seç"
            Enterprise.Text = "Tarifi seç"
        End If
    End Sub

    Private Sub Pro_Click(sender As Object, e As EventArgs) Handles Pro.Click
        Try
            Dim ID = Entrance.Authenticator
            Dim startDate As DateTime = DateTime.Today
            Dim leaveNo As Integer = 7
            Dim exp As String = startDate.AddDays(leaveNo)

            Dim connectionStrings As String = Connection.NewUser
            Dim connections As New SqlConnection(connectionStrings)
            connections.Open()
            Dim command As SqlCommand = connections.CreateCommand()
            command.CommandText = "INSERT INTO [u1602830_Moderator].[Purchase]
                      ([Date]
                      ,[UserID]
                      ,[Action]
                      ,[Type]
                      ,[Expire]
                      ,[Note])
                VALUES
                      ( '" & startDate & "'
                      ,'" & ID & "'
                      ,'Premium'
                      ,'Pro'
                      ,'" & exp & "'
                      ,'25')"
            command.ExecuteNonQuery()
            connections.Close()
            Response.Redirect("question")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Enterprise_Click(sender As Object, e As EventArgs) Handles Enterprise.Click
        Try
            Dim ID = Entrance.Authenticator
            Dim startDate As DateTime = DateTime.Today
            Dim leaveNo As Integer = 30
            Dim exp As String = startDate.AddDays(leaveNo)

            Dim connectionStrings As String = Connection.NewUser
            Dim connections As New SqlConnection(connectionStrings)
            connections.Open()
            Dim command As SqlCommand = connections.CreateCommand()
            command.CommandText = "INSERT INTO [u1602830_Moderator].[Purchase]
                      ([Date]
                      ,[UserID]
                      ,[Action]
                      ,[Type]
                      ,[Expire]
                      ,[Note])
                VALUES
                      ( '" & startDate & "'
                      ,'" & ID & "'
                      ,'Premium'
                      ,'Enterprise'
                      ,'" & exp & "'
                      ,'')"
            command.ExecuteNonQuery()
            connections.Close()
            Response.Redirect("question")
        Catch ex As Exception

        End Try
    End Sub
End Class