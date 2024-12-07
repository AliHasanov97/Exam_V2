Imports System.Data.SqlClient
Imports System.IO

Public Class read
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim ID = Entrance.Authenticator
            Dim IDS
            If Session("Read") IsNot Nothing Then
                IDS = Session("Read").ToString()
            Else
                Response.Redirect("index")
            End If

            Dim permission = Connection.Get_value("ID", IDS, "Reading", "Questions", "nUser")
            If permission = "False" Then
                Response.Redirect("Login")
            End If

            Avatar.Src = Connection.Get_value("ID", ID, "Avatar", "Users", "nUser")
            Name.InnerText = Connection.Get_value("ID", ID, "Full_Name", "Users", "nUser")
            List1.Attributes.Add("onclick", "Requests(" & IDS & ")")

            Avatar1.Src = Connection.Get_value("ID", ID, "Avatar", "Users", "nUser")
            Name1.InnerText = Connection.Get_value("ID", ID, "Full_Name", "Users", "nUser")
            List2.Attributes.Add("onclick", "Requests(" & IDS & ")")

            Subject.Text = Connection.Get_value("ID", IDS, "Subject", "Questions", "nUser")
            Max.Text = Connection.Get_value("ID", IDS, "Amount", "Questions", "nUser")
            Max.Attributes.Add("style", "visibility:hidden")

            If Not IsPostBack Then
                start(1, 10, 0)
            Else
                Dim min = S_min.Value
                Dim max = S_max.Value
                start(min, max, 1)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim min = S_min.Value
        Dim max = S_max.Value
        start(min, max, 0)
        panel1.Visible = False
    End Sub

    Protected Sub start(ByVal min As Integer, ByVal max As Integer, ByVal parameter As Integer)
        PlaceHolder1.Controls.Clear()

        Dim p1 = min
        Dim p2 = max
        Dim ID = Session("Read").ToString
        Dim say As Integer = p2 - p1
        For i = 0 To say

            Dim card As New Panel
            card.ID = i & "pans"
            card.CssClass = "tool card card-line card-body bg-body-tertiary border-transparent mb-7"
            If parameter = 0 Then
                card.Attributes.Add("data-ans", Connection.Get_value("ID", i + p1, "Answers", "no3", "Cavab"))
                card.Attributes.Add("onmousemove", "Answer(this)")
                card.Attributes.Add("onmouseleave", "Leave()")
            End If
            PlaceHolder1.Controls.Add(card)

            Dim rowB As New Panel
            rowB.CssClass = "row align-items-center"
            PlaceHolder1.Controls.Item(i).Controls.Add(rowB)

            Dim lab6 As New Label
            lab6.CssClass = "col-6 num" & i
            lab6.Text = i + p1
            PlaceHolder1.Controls.Item(i).Controls.Item(0).Controls.Add(lab6)

            If parameter = 0 Then
                Dim lab_6 As New Label
                lab_6.CssClass = "col-5 text-end"
                lab_6.Text = "Düzgün cavab : " & Connection.Get_value("ID", i + p1, "Answers", "no3", "Cavab")
                PlaceHolder1.Controls.Item(i).Controls.Item(0).Controls.Add(lab_6)

                Dim bug As New HtmlGenericControl("span")
                bug.Attributes.Add("class", "col-1")
                bug.Attributes.Add("data-bs-toggle", "modal")
                bug.Attributes.Add("data-bs-target", "#staticBackdrop")
                bug.Attributes.Add("onclick", "Report(" & ID & "," & i + p1 & ")")
                bug.InnerHtml = "<i class='fa-solid fa-bug'></i>"
                PlaceHolder1.Controls.Item(i).Controls.Item(0).Controls.Add(bug)
            End If

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
            sual.Text = File.ReadAllText(Server.MapPath("App_Data\" & ID & "\" & i + p1 & ".html"))
            PlaceHolder1.Controls.Item(i).Controls.Item(2).Controls.Item(0).Controls.Add(sual)

            If parameter = 1 Then
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
                question.Attributes.Add("onclick", "get(" & i & ")")
                question.Attributes.Add("data", p1 + i)
                question.AutoPostBack = False
                PlaceHolder1.Controls.Item(i).Controls.Item(2).Controls.Add(question)
            End If

        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim min = S_min.Value
        Dim max = S_max.Value
        start(min, max, 1)
        Dim say As Integer = max - min
        panel1.Visible = True
        unans.InnerText = say + 1
    End Sub
    'Public Sub ddlFM_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim id As String = (TryCast(sender, RadioButtonList)).ID
    '    Dim text As String = (TryCast(sender, RadioButtonList)).Text
    '    Dim attr As String = (TryCast(sender, RadioButtonList)).Attributes("data")
    '    If text = "A" Then
    '        ListBox1.Items.Add(id)
    '    End If
    'End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function Query(ByVal ID As String, ByVal ans As String) As String
        Dim IDS = HttpContext.Current.Session("Read").ToString
        Dim texts = File.ReadAllText(HttpContext.Current.Server.MapPath("App_Data\" & IDS & "\" & ID & ".html"))
        Dim cavab = Connection.Get_value("ID", ID, "Answers", "no3", "Cavab")
        If cavab = ans Then
            'Return "<p><i class='fa-solid fa-check'></i> Cavabınız doğrudur</p>"
            Return 1
        ElseIf ans = "undefined" Then
            Return 2
        Else
            Return "
<div class='accordion' id='accordionExample" & ID & "'>
                    <div class='accordion-item'>
                      <h2 class='accordion-header'>
                        <button class='accordion-button collapsed' type='button' data-bs-toggle='collapse' data-bs-target='#collapseOne" & ID & "' aria-expanded='false' aria-controls='collapseOne" & ID & "'>
                         <i class='fa-solid fa-xmark' style='padding-right:10px'></i> Cavabınız yanlışdır
                        </button>
                      </h2>
                      <div id='collapseOne" & ID & "' class='accordion-collapse collapse' data-bs-parent='#accordionExample" & ID & "' style=''>
                        <div class='accordion-body' data-ans='" & cavab & "' onmousemove='Answer(this)' onmouseleave='Leave()'>
                      <p class='text-end'>Düzgün cavab : " & cavab & "</p>
                          " & texts & "
                        </div>
                      </div>
                    </div>
           </div>
            "

        End If
    End Function



    <System.Web.Services.WebMethod()>
    Public Shared Function Report(ByVal ID As String, ByVal que As String) As String

        Return "
                        <div Class='modal-header'>
                            <h1 Class='modal-title fs-5' id='staticBackdropLabel'>Report : " & que & "</h1>
                            <Button type = 'button' Class='btn-close' data-bs-dismiss='modal' aria-label='Close'></button>
                        </div>
                        <div Class='modal-body'>
                            <div Class='row align-items-center mb-2'>
                                <div Class='col-4'>Düzgün cavab : </div>
                                <div Class='col-8'>
                                    <Select name = 'DropDownList1' id='DropDownList1' Class='form-select'>
	<option value = 'A' >A</Option>
	<option value = 'B' >B</Option>
	<option value = 'C' >C</Option>
	<option value = 'D' >D</Option>
	<option value = 'E' >E</Option>
</select>
                                </div>
                            </div>
                            <textarea id='floatingTextarea' Class='form-control' placeholder='Cavabı əsaslandırın'></textarea>
                        </div>
                        <div Class='modal-footer'>
                            <Button type = 'button' Class='btn btn-secondary' data-bs-dismiss='modal'>Close</button>
                            <Button type = 'button' onclick='Offer(" & ID & ", " & que & ")' Class='btn btn-primary'>Sorğu yolla</button>
                        </div>
"

    End Function


    <System.Web.Services.WebMethod()>
    Public Shared Function Offer(ByVal subs As String, ByVal num As String, ByVal ans As String, ByVal note As String) As String
        Dim check = Connection.Get_Count("SELECT COUNT(*)
        FROM [AnsQuery]
        WHERE [UserID] = '" & 1 & "' and [SubjectID] = '" & subs & "' and [Question] = '" & num & "' and [Status] = 'Waiting'")
        If check = 1 Then
            Return "<div class='modal-body p-4'>
                               <div class='text-center'>
                                   <i class='fas fa-ban' style='font-size: 30px'></i>
                                   <h4 class='mt-2'>Xəbərdarlıq</h4>
                                   <p class='mt-3'>Bu sual üçün artıq müraciət olunub.Yenidən sorğu üçün əvvəlki sorğunuzu ləğv edin</p>
                                   <button type='button' class='btn btn-light my-2' data-bs-dismiss='modal'>Davam etmək</button>
                               </div>
                           </div>
            "
        Else
            Dim base = Connection.NewUser
            Using connection As New SqlConnection(base)
                connection.Open()
                Dim command As SqlCommand = connection.CreateCommand()

                command.CommandText = "INSERT INTO [AnsQuery]
                  ([Date]
                  ,[UserID]
                  ,[SubjectID]
                  ,[Question]
                  ,[Answer]
                  ,[Note]
                  ,[Status])
            VALUES
                  ('" & DateTime.Today.ToString("dd.MM.yyyy") & " " & DateTime.Now.ToString("HH:mm") & "'
                  ," & 1 & "
                  ," & subs & "
                  ," & num & "
                  ,'" & ans & "'
                  ,N'" & note & "'
                  ,'Waiting')"
                command.ExecuteNonQuery()
                connection.Close()
            End Using
            Return "<div class='modal-body p-4'>
                               <div class='text-center'>
                                   <i class='fas fa-info-circle' style='font-size: 30px'></i>
                                   <h4 class='mt-2'>Bildiriş!</h4>
                                   <p class='mt-3'>Sorğunuz göndərildi</p>
                                   <button type='button' class='btn btn-info my-2' data-bs-dismiss='modal'>Davam etmək</button>
                               </div>
                             </div>"
        End If
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function Requests(ByVal subs As String) As String
        Dim query
        Dim check = Connection.Get_Count("SELECT COUNT(*)
        FROM [AnsQuery]
        WHERE [UserID] = '" & 1 & "' and [SubjectID] = '" & subs & "'")
        If Not check = 0 Then
            Dim dt As DataTable = New DataTable
            dt.Columns.AddRange(New DataColumn() {New DataColumn("Sual", GetType(System.String)), New DataColumn("Status", GetType(System.String)), New DataColumn("Əməliyyat", GetType(System.String))})
            Dim connectionString As String = Connection.NewUser
            For i = 0 To check - 1
                query = "SELECT *
    From           [AnsQuery]
        WHERE [UserID] = '" & 1 & "' and [SubjectID] = '" & subs & "'
    order by [ID] Desc
    OFFSET " & i & " ROWS
    FETCH NEXT 1 ROWS ONLY;"

                Dim datas() As String
                Dim base = Connection.NewUser
                Using conn As New SqlConnection(base)
                    Using cmd As New SqlCommand(query, conn)
                        cmd.Connection = conn
                        conn.Open()
                        Dim myreade As SqlDataReader

                        myreade = cmd.ExecuteReader
                        myreade.Read()

                        datas = {myreade("Date"), myreade("Question"), myreade("Answer"), myreade("Status"), myreade("ID"), myreade("UserID")}

                    End Using
                    If Not datas(3) = "Waiting" Then
                        dt.Rows.Add(datas(1), "<span class='badge bg-success'>" & datas(3) & "</span>", "<button type='button' onclick='Bax(" & datas(4) & ")' class='btn btn-outline-secondary rounded-pill'><i class='fas fa-eye'></i> </button>")
                    Else
                        dt.Rows.Add(datas(1), "<span class='badge bg-warning'>" & datas(3) & "</span>", "<button type='button' onclick='Bax(" & datas(4) & ")' class='btn btn-outline-secondary rounded-pill'><i class='fas fa-eye'></i> </button><button type='button' onclick='Del(" & datas(4) & ",1)'  class='btn btn-outline-secondary rounded-pill'><i class='fas fa-trash-alt'></i> </button>")
                    End If
                End Using
            Next
            Dim sb As StringBuilder = New StringBuilder()
            'Table start.
            sb.Append("<table cellpadding='5' border='0' cellspacing='0' class='table table-hover table-centered mb-0  text-center' style='vertical-align:middle'>")
            'Adding HeaderRow.
            sb.Append("<tr>")
            For Each column As DataColumn In dt.Columns
                sb.Append(("<th>" _
                                    + (column.ColumnName + "</th>")))
            Next
            sb.Append("</tr>")
            'Adding DataRow.
            For Each row As DataRow In dt.Rows
                sb.Append("<tr>")
                For Each column As DataColumn In dt.Columns
                    sb.Append(("<td>" _
                                        + (row(column.ColumnName).ToString + "</td>")))
                Next
                sb.Append("</tr>")
            Next
            'Table end.
            sb.Append("</table>")
            Return "  <div Class='modal-header'>
                            <h1 Class='modal-title fs-5' id='staticBackdropLabel'>Sorğularınız</h1>
                            <Button type = 'button' Class='btn-close' data-bs-dismiss='modal' aria-label='Close'></button>
                        </div>
                        <div Class='modal-body'>
                            " & sb.ToString & "
                         </div>
                        <div Class='modal-footer'>
                            <Button type = 'button' Class='btn btn-secondary' data-bs-dismiss='modal'>Close</button>
                        </div>"
        Else
            Return "  <div Class='modal-header'>
                            <h1 Class='modal-title fs-5' id='staticBackdropLabel'>Sorğularınız</h1>
                            <Button type = 'button' Class='btn-close' data-bs-dismiss='modal' aria-label='Close'></button>
                        </div>
                        <div Class='modal-body'>
                            Mövcud fənn üzrə sorğu mövcud deyil
                         </div>
                        <div Class='modal-footer'>
                            <Button type = 'button' Class='btn btn-secondary' data-bs-dismiss='modal'>Close</button>
                        </div>"

        End If
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function DEL(ByVal ID As String) As String
        Try
            Dim base = Connection.NewUser
            Using connection As New SqlConnection(base)
                connection.Open()
                Dim command As SqlCommand = connection.CreateCommand()
                command.CommandText = "DELETE FROM [AnsQuery] WHERE [ID]= " & ID
                command.ExecuteNonQuery()
                connection.Close()
            End Using

        Catch

        End Try
        Return ""
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function Bax(ByVal ID As String) As String
        Dim subs = Connection.Get_value("ID", ID, "SubjectID", "AnsQuery", "nUser")
        Dim num = Connection.Get_value("ID", ID, "Question", "AnsQuery", "nUser")
        Dim Dates = Connection.Get_value("ID", ID, "Date", "AnsQuery", "nUser")
        Dim status = Connection.Get_value("ID", ID, "Status", "AnsQuery", "nUser")
        Dim ans = Connection.Get_value("ID", ID, "Answer", "AnsQuery", "nUser")
        Dim note = Connection.Get_value("ID", ID, "Note", "AnsQuery", "nUser")
        Dim texts = File.ReadAllText(HttpContext.Current.Server.MapPath("App_Data\" & subs & "\" & num & ".html"))

        Dim C_status
        If status = "Waiting" Then
            C_status = "<span class='badge bg-warning'>Waiting</span>"
        Else
            C_status = "<span class='badge bg-success'>Accepted</span>"
        End If

        Return "<div Class='modal-header'>
                            <h1 Class='modal-title fs-5' id='staticBackdropLabel'>Sual #" & num & "</h1>
                            <Button type = 'button' Class='btn-close' data-bs-dismiss='modal' aria-label='Close'></button>
                        </div>
                        <div Class='modal-body'>
                            <p>Müraciət tarixi : " & Dates & "</p>
                            <p>Status : " & C_status & "</p>
                            <p>Sizin təklif etdiyiniz cavab : " & ans & "</p>
                            <p>Açıqlama : " & note & "</p>
                            <hr>


<div class='accordion' id='accordionExample" & ID & "'>
                    <div class='accordion-item'>
                      <h2 class='accordion-header'>
                        <button class='accordion-button collapsed' type='button' data-bs-toggle='collapse' data-bs-target='#collapseOne" & ID & "' aria-expanded='false' aria-controls='collapseOne" & ID & "'>
                         Sual
                        </button>
                      </h2>
                      <div id='collapseOne" & ID & "' class='accordion-collapse collapse' data-bs-parent='#accordionExample" & ID & "' style=''>
                        <div class='accordion-body'>                   
                          " & texts & "
                        </div>
                      </div>
                    </div>
           </div>                            
                        </div>
                        <div Class='modal-footer'>
                            <Button type = 'button' Class='btn btn-secondary' data-bs-dismiss='modal'>Close</button>
                        </div>"

    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function Preview(ByVal param As String)
        Try
            Dim texts = File.ReadAllText(HttpContext.Current.Server.MapPath("App_Data\" & param & "\1.html"))
            Return "<div class='modal-content'>
                <div class='modal-header'>
                    <h1 class='modal-title fs-5' id='staticBackdropLabel'>" & Connection.Get_value("ID", param, "Subject", "Questions", "nUser") & " : Ön izləmə</h1>
                    <button type='button' class='btn-close' data-bs-dismiss='modal' aria-label='Close'></button>
                </div>
                <div class='modal-body'>
                    " & texts & "
                </div>
                <div class='modal-footer'>
                    <button type='button' class='btn btn-secondary' data-bs-dismiss='modal'>Close</button>
                </div>
            </div>"
        Catch ex As Exception
            MsgBox(ex.Message)
            Return "<div Class='modal-content'>
                <div Class='modal-header'>
                    <h1 Class='modal-title fs-5' id='staticBackdropLabel'>" & Connection.Get_value("ID", param, "Subject", "Questions", "nUser") & " : Ön izləmə</h1>
                    <Button type ='button' class='btn-close' data-bs-dismiss='modal' aria-label='Close'></button>
                </div>
            <div Class='modal-body'>
                    Müvəqqəti olaraq " & Connection.Get_value("ID", param, "Subject", "Questions", "nUser") & " fənni üzrə ön izləmə aktiv deyil.                  
                </div>
                <div Class='modal-footer'>
                    <Button type ='button' class='btn btn-secondary' data-bs-dismiss='modal'>Close</button>
                </div>
            </div>"
        End Try
    End Function
End Class