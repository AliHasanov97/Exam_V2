Imports System.Security
Imports System.IO
Imports System.Data.SqlClient
Public Class exam
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Image1.ImageUrl = Connection.Get_value("ID", 1, "Avatar", "Users", "nUser")
        Label1.Text = Connection.Get_value("ID", 1, "Full_Name", "Users", "nUser")

        Avatar.Src = Connection.Get_value("ID", 1, "Avatar", "Users", "nUser")
        Avatar1.Src = Connection.Get_value("ID", 1, "Avatar", "Users", "nUser")
        Name1.InnerText = Connection.Get_value("ID", 1, "Full_Name", "Users", "nUser")

        Fenn.InnerHtml = Connection.Get_value("ID", 1, "Subject", "Questions", "nUser")
        Max.Text = Connection.Get_value("ID", 1, "Amount", "Questions", "nUser")
        Max.Attributes.Add("style", "visibility:hidden")

    End Sub


    Sub start(tip As Integer, say As Integer, min_a As Integer, max_a As Integer)
        Try

            Dim nc As String = 1

            Dim max As Integer = say

            Dim i As Integer


            Dim bolgu As Integer = max_a / max
            Dim min_r As Integer
            Dim max_r As Integer

            Dim rand(max) As String
            Dim r = New Random
            Dim res
            If tip = 0 Then
                For i = 0 To max
start:
                    res = r.Next(min_a, max_a)
                    If rand.Contains(res) Then
                        GoTo start
                    Else
                        rand(i) = res
                        ListBox1.Items.Add(res)
                    End If
                Next
            ElseIf tip = 1 Then
                For i = 0 To max
start1:
                    min_r = i * bolgu
                    If min_r = 0 Then
                        min_r = 1
                    End If
                    max_r = min_r + bolgu
                    res = r.Next(min_r, max_r)
                    If rand.Contains(res) Then
                        GoTo start1
                    Else
                        rand(i) = res
                        ListBox1.Items.Add(res)
                    End If
                Next
            End If
            Try

                For i = 0 To max

                    Dim card As New Panel
                    card.ID = i & "pans"
                    card.CssClass = "tool card card-line card-body bg-body-tertiary border-transparent mb-7"
                    PlaceHolder1.Controls.Add(card)

                    Dim rowB As New Panel
                    rowB.CssClass = "row align-items-center"
                    PlaceHolder1.Controls.Item(i).Controls.Add(rowB)

                    Dim lab6 As New Label
                    lab6.CssClass = "col-6 num" & i
                    lab6.Text = i + 1
                    PlaceHolder1.Controls.Item(i).Controls.Item(0).Controls.Add(lab6)

                    Dim hr As New HtmlGenericControl("hr")
                    PlaceHolder1.Controls.Item(i).Controls.Add(hr)

                    Dim row As New Panel
                    row.ID = i & "pan"
                    row.CssClass = "row d-flex justify-content-between"
                    PlaceHolder1.Controls.Item(i).Controls.Add(row)

                    Dim col10 As New Panel
                    col10.CssClass = "col-10"
                    PlaceHolder1.Controls.Item(i).Controls.Item(2).Controls.Add(col10)

                    Dim sual As New Label
                    sual.Text = File.ReadAllText(Server.MapPath("App_Data\1\" & ListBox1.Items(i).ToString() & ".html"))
                    PlaceHolder1.Controls.Item(i).Controls.Item(2).Controls.Item(0).Controls.Add(sual)

                    Dim question As New RadioButtonList
                    question.Enabled = True
                    question.ID = i
                    question.Items.Add("A")
                    question.Items.Add("B")
                    question.Items.Add("C")
                    question.Items.Add("D")
                    question.Items.Add("E")
                    question.CssClass = "col-1"
                    question.Attributes.Add("style", "border-collapse: separate;")
                    PlaceHolder1.Controls.Item(i).Controls.Item(2).Controls.Add(question)

                Next

                panel2.Visible = True


            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim min = S_min.Value
        Dim max = S_max.Value
        say.Text = DropDownList1.SelectedValue
        start(DropDownList2.SelectedValue, DropDownList1.SelectedValue, min, max)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim nc = 3
        Dim Ques As String
        Dim Ans As String
        Dim i As Integer

        Dim tip_s
        If DropDownList2.SelectedValue = 0 Then
            tip_s = "Avtomatik"
        Else
            tip_s = "Seçmə"
        End If
        Dim tip As String = tip_s & " (" & S_min.Value & "-" & S_max.Value & ")"

        For i = 0 To say.Text
            Dim a As Integer = ListBox1.Items(i).ToString()
            Ques += a & ","
            If (Request.Form.Get(i.ToString()) IsNot Nothing) Then
                Dim message As String = Request.Form.Get(i.ToString)
                Ans += message & ","
            Else
                Ans += "-,"
            End If

        Next

        Dim base = "Data Source=ALI;Initial Catalog=Balogna;Integrated Security=True"
        Using connection As New SqlConnection(base)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            command.CommandText = "INSERT INTO [Exams]
                             ([UserID]
            ,[Date]
            ,[Time]
            ,[Type]
            ,[SubjectID]
            ,[Question]
            ,[Answers])
                       VALUES
                             ('1'
                             ,'" & DateTime.Today.ToString("dd.MM.yyyy") & "'
                             ,'" & DateTime.Now.ToString("HH:mm") & "'
                             ,N'" & tip & "'
                             ,N'1'
                             ,'" & Ques & "'
                             ,'" & Ans & "')"
            command.ExecuteNonQuery()


            connection.Close()
        End Using
        Response.Redirect("Result")
    End Sub
End Class