Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Threading
Imports System.Web.Services.Description

Public Class WebForm4
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ID = Entrance.Authenticator
        Try
            If IsPostBack Then
                Dim subs As String = Request("__EVENTTARGET")
                Dim parameter As String = Request("__EVENTARGUMENT")
                If subs = "Exam" Then
                    Dim lab As New Label
                    lab.Text = exam(parameter)
                    PlaceHolder2.Controls.Add(lab)
                ElseIf subs = "Read" Then
                    Dim lab As New Label
                    lab.Text = read(parameter)
                    PlaceHolder2.Controls.Add(lab)
                End If
            End If
            Status()
            Dim connect As String = Connection.NewUser
            Dim grid1 As New GridView
            grid1.ID = "ERT"
            Dim connectionStrings As String = Connection.NewUser
            Dim sqls As String = "SELECT
       [ID]
      ,[Subject]
      ,[Amount]
  FROM [Questions]
  where [Status] = 'Active'"
            Dim connections As New SqlConnection(connectionStrings)
            Dim dataadapters As New SqlDataAdapter(sqls, connections)
            Dim dss As New DataSet()
            connections.Open()
            dataadapters.Fill(dss)
            connections.Close()
            grid1.DataSource = dss
            grid1.CssClass = "table table-hover table-centered mb-0"
            grid1.Attributes.Add("style", "border-left:none;border-top:none;border-right:none;vertical-align: middle;")
            AddHandler grid1.RowDataBound, AddressOf Siyahı
            grid1.DataBind()
            grid1.HeaderRow.TableSection = TableRowSection.TableHeader
            grid1.HeaderRow.Cells(0).Text = "ID"
            grid1.HeaderRow.Cells(1).Text = "Fənn"
            grid1.HeaderRow.Cells(2).Text = ""
            grid1.HeaderRow.Visible = True
            grid1.EnableViewState = True
            PlaceHolder1.Controls.Add(grid1)
        Catch ex As Exception

        End Try
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
                Dim limit = datas(1)
                Dim message = ""
                If limit > 0 Then
                    message = "<i class='fa-solid fa-check'></i> " & limit & " ödənişsiz imtahan."
                Else
                    message = "<i class='fa-solid fa-xmark'></i> Ödənişsiz imtahan limitiniz bitib."
                End If
                Dim pan1 As New HtmlGenericControl("div")
                pan1.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-regular fa-gem' style='font-size:30px'></i>
                </div>
                <div class='col'>
                        <span style='font-size:18px'>Pro premium özəlliyi</span>
                    <br><span><i class='fa-regular fa-clock'></i> " & datas(2) & " tarixinədək aktivdir.</span>             
                    <br>
                    <br><span><i class='fa-solid fa-check'></i> Bütün suallar aktivdir.</span>
                    <br><span>" & message & "</span></div>
            </div>"
                PlaceHolder2.Controls.Add(pan1)
            ElseIf status = "Enterprise" Then
                Dim pan1 As New HtmlGenericControl("div")
                pan1.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-regular fa-gem' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <span style='font-size:18px'>Enterprise premium özəlliyi</span>
                    <br><span><i class='fa-regular fa-clock'></i> " & datas(2) & " tarixinədək aktivdir.</span>             
                    <br>
                    <br><span><i class='fa-solid fa-check'></i> Bütün suallar aktivdir.</span>
                    <br><span><i class='fa-solid fa-check'></i> Ödənişsiz imtahan</span></div>
            </div>"
                PlaceHolder2.Controls.Add(pan1)
            End If

        End If
    End Sub
    Function read(ByVal parameter As Integer)
        Session("Read") = parameter
        Response.Redirect("Read")

    End Function
    Function exam(ByVal parameter As Integer)
        Dim ID = Entrance.Authenticator
        Dim exam_s = Connection.Get_value("ID", parameter, "Exam", "Questions", "nUser")
        Dim Balance = Double.Parse(Connection.Get_value("ID", ID, "Balance", "Users", "nUser"))
        Dim Cost = Double.Parse(Connection.Get_value("ID", parameter, "P_Exam", "Questions", "nUser"))
        Dim G_balance
        Dim query
        If exam_s = "True" Then
            Dim nov = Connection.Get_value("ID", ID, "Position", "Users", "nUser")
            If nov = "Basic" Then
                ' < Satın almaq >
                query = "SELECT COUNT(*)
                 FROM [Purchase]
                 where [UserID]= " & ID & " and [Action] = 'Buy' and [Type] = 'Subject' and [Note] = '" & parameter & "'"
                Dim Result = Connection.Get_Count(query)
                If Result = 1 Then
                    If Balance > Cost Or Balance = Cost Then
                        If Cost = 0 Then
                            Dim pan1 As New HtmlGenericControl("div")
                            pan1.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-circle-check' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <span style='font-size:18px;'>İmtahan başladı</span><!-- Breadcrumb -->
                    <br><span>Ödənişsiz imtahan</span>
                </div>
            </div>
        </div>"
                            PlaceHolder2.Controls.Add(pan1)
                        Else
                            G_balance = Balance - Cost
                            Connection.Update_value("ID", ID, "Balance", G_balance, "Users", "nUser")
                            Dim pan1 As New HtmlGenericControl("div")
                            pan1.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-circle-check' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <span style='font-size:18px;'>İmtahan başladı</span><!-- Breadcrumb -->
                    <br><span>Sizin balansınız : " & G_balance & " ₼</span>
                </div>
            </div>
        </div>"
                            PlaceHolder2.Controls.Add(pan1)
                        End If
                    Else
                        Dim pan As New HtmlGenericControl("div")
                        pan.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-ban' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <p style='font-size:18px;'><b>" & Connection.Get_value("ID", parameter, "Subject", "Questions", "nUser") & "</b> fənnindən imtahan vermək üçün balansınızda kifayət qədər vəsait yoxdur.</p><!-- Breadcrumb -->              
                <span>Sizin balansınız : " & Balance & " ₼</span></div>
            </div>
<hr>
<a href='Balance' class='btn btn-secondary' style='width100px;width: auto;'>Balans</a></div>"
                        PlaceHolder2.Controls.Add(pan)
                    End If
                    'Return "Basic : " & Connection.Get_value("ID", parameter, "P_Exam", "Questions", "nUser")
                End If

                ' < icarə >

                query = "SELECT COUNT(*)
            FROM [Purchase]
            where [UserID]= " & ID & " and [Action] = 'Rent' and [Type] = 'Subject' and [Note] = '" & parameter & "'"
                Result = Connection.Get_Count(query)
                If Result > 0 Then
                    Dim query1 As String
                    query1 = "SELECT TOP (1) *
                FROM [Purchase]
                where [UserID]= " & ID & " and [Action] = 'Rent' and [Type] = 'Subject' and [Note] = '" & parameter & "'
                 ORDER
                  BY ID DESC "
                    Dim datas As String
                    Using conn As New SqlConnection(Connection.NewUser)
                        Using cmd As New SqlCommand(query1, conn)
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
                        If Balance > Cost Or Balance = Cost Then
                            If Cost = 0 Then
                                Dim pan1 As New HtmlGenericControl("div")
                                pan1.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-circle-check' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <span style='font-size:18px;'>İmtahan başladı</span><!-- Breadcrumb -->
                    <br><span>Ödənişsiz imtahan</span>
                </div>
            </div>
        </div>"
                                PlaceHolder2.Controls.Add(pan1)
                            Else
                                G_balance = Balance - Cost
                                Connection.Update_value("ID", ID, "Balance", G_balance, "Users", "nUser")
                                Dim pan1 As New HtmlGenericControl("div")
                                pan1.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-circle-check' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <span style='font-size:18px;'>İmtahan başladı</span><!-- Breadcrumb -->
                    <br><span>Sizin balansınız : " & G_balance & " ₼</span>
                </div>
            </div>
        </div>"
                                PlaceHolder2.Controls.Add(pan1)
                            End If
                        Else
                            Dim pan As New HtmlGenericControl("div")
                            pan.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-ban' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <p style='font-size:18px;'><b>" & Connection.Get_value("ID", parameter, "Subject", "Questions", "nUser") & "</b> fənnindən imtahan vermək üçün balansınızda kifayət qədər vəsait yoxdur.</p><!-- Breadcrumb -->              
                <span>Sizin balansınız : " & Balance & " ₼</span></div>
            </div>
<hr>
<a href='Balance' class='btn btn-secondary' style='width: auto;'>Balans</a></div>"
                            PlaceHolder2.Controls.Add(pan)
                        End If
                        'Return "Basic Rent: " & Connection.Get_value("ID", parameter, "P_Exam", "Questions", "nUser")
                    End If
                End If

            ElseIf nov = "Pro" Then
                query = "SELECT TOP (1) *
                FROM [Purchase]
                where [UserID]= " & ID & " and [Type] = 'Pro'
                 ORDER
                  BY ID DESC "
                Dim datas() As String
                Using conn As New SqlConnection(Connection.NewUser)
                    Using cmd As New SqlCommand(query, conn)
                        cmd.Connection = conn
                        conn.Open()
                        Dim myreade As SqlDataReader
                        myreade = cmd.ExecuteReader
                        myreade.Read()
                        datas = {myreade("Note"), myreade("ID")}
                    End Using
                End Using
                If datas(0) > 0 Then
                    If Cost > 0 Then
                        Dim ferq = datas(0) - 1
                        Connection.Update_value("ID", datas(1), "Note", ferq, "Purchase", "nUser")
                    End If

                    Dim pan1 As New HtmlGenericControl("div")
                    pan1.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-circle-check' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <span style='font-size:18px;'>İmtahan başladı</span><!-- Breadcrumb -->
                    <br><span>Ödənişsiz imtahan</span>
                </div>
            </div>
        </div>"
                    PlaceHolder2.Controls.Add(pan1)
                    'Return "Pro Pulsuz"
                Else
                    If Balance > Cost Or Balance = Cost Then
                        If Cost = 0 Then
                            Dim pan1 As New HtmlGenericControl("div")
                            pan1.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-circle-check' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <span style='font-size:18px;'>İmtahan başladı</span><!-- Breadcrumb -->
                    <br><span>Ödənişsiz imtahan</span>
                </div>
            </div>
        </div>"
                            PlaceHolder2.Controls.Add(pan1)
                        Else
                            G_balance = Balance - Cost
                            Connection.Update_value("ID", ID, "Balance", G_balance, "Users", "nUser")
                            Dim pan1 As New HtmlGenericControl("div")
                            pan1.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-circle-check' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <span style='font-size:18px;'>İmtahan başladı</span><!-- Breadcrumb -->
                    <br><span>Sizin balansınız : " & G_balance & " ₼</span>
                </div>
            </div>
        </div>"
                            PlaceHolder2.Controls.Add(pan1)
                        End If
                    Else
                        Dim pan As New HtmlGenericControl("div")
                        pan.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-ban' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <p style='font-size:18px;'><b>" & Connection.Get_value("ID", parameter, "Subject", "Questions", "nUser") & "</b> fənnindən imtahan vermək üçün balansınızda kifayət qədər vəsait yoxdur.</p><!-- Breadcrumb -->              
                <span>Sizin balansınız : " & Balance & " ₼</span></div>
            </div>
<hr>
<a href='Balance' class='btn btn-secondary' style='width100px;width: auto;'>Balans</a></div>"
                        PlaceHolder2.Controls.Add(pan)
                    End If
                    'Return "Pro : " & Connection.Get_value("ID", parameter, "P_Exam", "Questions", "nUser")
                End If
            ElseIf nov = "Enterprise" Then
                Dim pan1 As New HtmlGenericControl("div")
                pan1.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-circle-check' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <span style='font-size:18px;'>İmtahan başladı</span><!-- Breadcrumb -->
                    <br><span>Ödənişsiz imtahan</span>
                </div>
            </div>
        </div>"
                PlaceHolder2.Controls.Add(pan1)
                'Return "Enterprise Pulsuz"
            End If
        Else
            Return "Müvəqqəti olaraq imtahan vermək aktiv deyil"
        End If
    End Function

    Protected Sub Siyahı(sender As Object, e As GridViewRowEventArgs)
        Dim ID = Entrance.Authenticator
        Dim ID_Q As TableCell = e.Row.Cells(0)
        Dim ID_S As TableCell = e.Row.Cells(1)
        Dim ID_C As TableCell = e.Row.Cells(2)
        Dim connectionString As String = Connection.NewUser

        Try
            e.Row.Visible = False
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim Cost = Double.Parse(Connection.Get_value("ID", ID_Q.Text, "P_Exam", "Questions", "nUser"))
                Dim query As String
                Dim result As Integer = 0

                '<icare>

                query = "SELECT COUNT(*)
            FROM [Purchase]
            where [UserID]= " & ID & " and [Action] = 'Rent' and [Type] = 'Subject' and [Note] = '" & ID_Q.Text & "'"
                result = Connection.Get_Count(query)
                If result > 0 Then
                    Dim query1 As String
                    query1 = "SELECT TOP (1) *
                FROM [Purchase]
                where [UserID]= " & ID & " and [Action] = 'Rent' and [Type] = 'Subject' and [Note] = '" & ID_Q.Text & "'
                 ORDER
                  BY ID DESC "
                    Dim datas As String
                    Using conn As New SqlConnection(connectionString)
                        Using cmd As New SqlCommand(query1, conn)
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
                        e.Row.Visible = True
                        Cont.Visible = True
                        Empty.Visible = False
                        ID_S.Controls.Clear()
                        ID_C.Controls.Clear()
                        Dim SUBJ As New HtmlGenericControl("span")
                        SUBJ.InnerHtml = Connection.Get_value("ID", ID_Q.Text, "Subject", "Questions", "nUser")
                        Dim but_tip As New HtmlGenericControl("span")
                        but_tip.ID = ID_Q.Text & "Read"
                        but_tip.Attributes.Add("style", "padding: 10px;margin:2px;")
                        but_tip.Attributes.Add("data-bs-toggle", "tooltip")
                        but_tip.Attributes.Add("data-bs-placement", "right")
                        but_tip.Attributes.Add("data-bs-title", Connection.Get_value("ID", datas, "Expire", "Purchase", "nUser") & " tarixinədək aktivdir.Son " & day & " gün")
                        but_tip.InnerHtml = "<i class='fa-solid fa-circle-info'></i>"
                        ID_S.Controls.Add(SUBJ)
                        ID_S.Controls.Add(but_tip)
                        Dim Action_pan As New Panel
                        Dim read_s = Connection.Get_value("ID", ID_Q.Text, "Reading", "Questions", "nUser")
                        If read_s = "True" Then
                            Dim but_pre As New HtmlGenericControl("span")
                            but_pre.ID = ID_Q.Text & "Read"
                            but_pre.Attributes.Add("class", "btn btn-primary")
                            but_pre.Attributes.Add("style", "padding: 5px;margin:2px;width:100px")
                            but_pre.Attributes.Add("data-bs-toggle", "modal")
                            but_pre.Attributes.Add("data-bs-target", "#staticBackdrop")
                            but_pre.Attributes.Add("onclick", "__doPostBack('Read', " & ID_Q.Text & ")")
                            but_pre.InnerHtml = "<i class='fa-regular fa-file'></i> Oxumaq"
                            Action_pan.Controls.Add(but_pre)
                        End If
                        Dim exam_s = Connection.Get_value("ID", ID_Q.Text, "Exam", "Questions", "nUser")
                        If exam_s = "True" Then
                            Dim but_about As New HtmlGenericControl("span")
                            but_about.ID = ID_Q.Text & "Read"
                            but_about.Attributes.Add("style", "padding: 5px;margin:2px;width: 100px;")
                            but_about.Attributes.Add("data-bs-toggle", "modal")
                            but_about.Attributes.Add("data-bs-target", "#staticBackdrop")
                            but_about.Attributes.Add("onclick", "__doPostBack('Exam', " & ID_Q.Text & ")")
                            If Cost = 0 Then
                                but_about.Attributes.Add("class", "btn btn-success")
                                but_about.InnerHtml = "<i class='fa-solid fa-ticket'></i> Ödənişsiz"
                            Else
                                but_about.Attributes.Add("class", "btn btn-secondary")
                                but_about.InnerHtml = "<i Class='fa-solid fa-ticket'></i> Sınaq " & Connection.Get_value("ID", ID_Q.Text, "P_Exam", "Questions", "nUser") & " ₼"
                            End If
                            Action_pan.Controls.Add(but_about)
                        End If
                        ID_C.Controls.Add(Action_pan)
                    End If
                End If

                '<Satın alma>

                query = "SELECT COUNT(*)
                 FROM [Purchase]
                 where [UserID]= " & ID & " and [Action] = 'Buy' and [Type] = 'Subject' and [Note] = '" & ID_Q.Text & "'"
                result = Connection.Get_Count(query)
                If result = 1 Then
                    e.Row.Visible = True
                    Empty.Visible = False
                    Cont.Visible = True
                    ID_S.Controls.Clear()
                    ID_C.Controls.Clear()
                    Dim SUBJ As New HtmlGenericControl("span")
                    SUBJ.InnerHtml = Connection.Get_value("ID", ID_Q.Text, "Subject", "Questions", "nUser")
                    ID_S.Controls.Add(SUBJ)
                    Dim Action_pan As New Panel
                    Dim read_s = Connection.Get_value("ID", ID_Q.Text, "Reading", "Questions", "nUser")
                    If read_s = "True" Then
                        Dim but_pre As New HtmlGenericControl("span")
                        but_pre.ID = ID_Q.Text & "Read"
                        but_pre.Attributes.Add("class", "btn btn-primary")
                        but_pre.Attributes.Add("style", "padding: 5px;margin:2px;width:100px")
                        but_pre.Attributes.Add("data-bs-toggle", "modal")
                        but_pre.Attributes.Add("data-bs-target", "#staticBackdrop")
                        but_pre.Attributes.Add("onclick", "__doPostBack('Read', " & ID_Q.Text & ")")
                        but_pre.InnerHtml = "<i class='fa-regular fa-file'></i> Oxumaq"
                        Action_pan.Controls.Add(but_pre)
                    End If
                    Dim exam_s = Connection.Get_value("ID", ID_Q.Text, "Exam", "Questions", "nUser")
                    If exam_s = "True" Then
                        Dim but_about As New HtmlGenericControl("span")
                        but_about.ID = ID_Q.Text & "Read"
                        but_about.Attributes.Add("style", "padding: 5px;margin:2px;width: 100px;")
                        but_about.Attributes.Add("data-bs-toggle", "modal")
                        but_about.Attributes.Add("data-bs-target", "#staticBackdrop")
                        but_about.Attributes.Add("onclick", "__doPostBack('Exam', " & ID_Q.Text & ")")
                        If Cost = 0 Then
                            but_about.Attributes.Add("class", "btn btn-success")
                            but_about.InnerHtml = "<i class='fa-solid fa-ticket'></i> Ödənişsiz"
                        Else
                            but_about.Attributes.Add("class", "btn btn-secondary")
                            but_about.InnerHtml = "<i Class='fa-solid fa-ticket'></i> Sınaq " & Connection.Get_value("ID", ID_Q.Text, "P_Exam", "Questions", "nUser") & " ₼"
                        End If
                        Action_pan.Controls.Add(but_about)
                    End If
                    ID_C.Controls.Add(Action_pan)
                End If

                ' <Premium>

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
                    Using conn As New SqlConnection(connectionString)
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
                        Dim nodv = Connection.Get_value("ID", datas, "Type", "Purchase", "nUser")

                        e.Row.Visible = True
                        Cont.Visible = True
                        Empty.Visible = False
                        ID_S.Controls.Clear()
                        ID_C.Controls.Clear()
                        Dim SUBJ As New HtmlGenericControl("span")
                        SUBJ.InnerHtml = Connection.Get_value("ID", ID_Q.Text, "Subject", "Questions", "nUser")
                        ID_S.Controls.Add(SUBJ)
                        Dim nov = Connection.Get_value("ID", datas, "Type", "Purchase", "nUser")
                        Dim Action_pan As New Panel
                        Dim read_s = Connection.Get_value("ID", ID_Q.Text, "Reading", "Questions", "nUser")
                        If read_s = "True" Then
                            Dim but_pre As New HtmlGenericControl("span")
                            but_pre.ID = ID_Q.Text & "Read"
                            but_pre.Attributes.Add("class", "btn btn-primary")
                            but_pre.Attributes.Add("style", "padding: 5px;margin:2px;width:100px")
                            but_pre.Attributes.Add("data-bs-toggle", "modal")
                            but_pre.Attributes.Add("data-bs-target", "#staticBackdrop")
                            but_pre.Attributes.Add("onclick", "__doPostBack('Read', " & ID_Q.Text & ")")
                            but_pre.InnerHtml = "<i class='fa-regular fa-file'></i> Oxumaq"
                            Action_pan.Controls.Add(but_pre)
                        End If
                        Dim exam_s = Connection.Get_value("ID", ID_Q.Text, "Exam", "Questions", "nUser")
                        If exam_s = "True" Then
                            Dim but_about As New HtmlGenericControl("span")
                            but_about.ID = ID_Q.Text & "Read"
                            but_about.Attributes.Add("style", "padding: 5px;margin:2px;width: 100px;")
                            but_about.Attributes.Add("data-bs-toggle", "modal")
                            but_about.Attributes.Add("data-bs-target", "#staticBackdrop")
                            but_about.Attributes.Add("onclick", "__doPostBack('Exam', " & ID_Q.Text & ")")
                            If nov = "Pro" Then
                                Dim limit = Connection.Get_value("ID", datas, "Note", "Purchase", "nUser")
                                If limit > 0 Then
                                    If Cost = 0 Then
                                        but_about.Attributes.Add("class", "btn btn-success")
                                    Else
                                        but_about.Attributes.Add("class", "btn btn-secondary")
                                    End If
                                    but_about.InnerHtml = "<i class='fa-solid fa-ticket'></i> Ödənişsiz"
                                Else
                                    If Cost = 0 Then
                                        but_about.Attributes.Add("class", "btn btn-success")
                                        but_about.InnerHtml = "<i class='fa-solid fa-ticket'></i> Ödənişsiz"
                                    Else
                                        but_about.Attributes.Add("class", "btn btn-secondary")
                                        but_about.InnerHtml = "<i Class='fa-solid fa-ticket'></i> Sınaq " & Connection.Get_value("ID", ID_Q.Text, "P_Exam", "Questions", "nUser") & " ₼"
                                    End If
                                End If
                            ElseIf nov = "Enterprise" Then
                                but_about.Attributes.Add("class", "btn btn-secondary")
                                but_about.InnerHtml = "<i class='fa-solid fa-ticket'></i> Ödənişsiz"
                            End If
                            Action_pan.Controls.Add(but_about)
                        End If
                        ID_C.Controls.Add(Action_pan)
                    End If
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("store")
    End Sub

    Function expire(ByVal ID As Integer)
        Dim guy = Connection.Get_value("ID", ID, "Expire", "Purchase", "nUser")
        Dim lastdate As DateTime = DateTime.ParseExact(guy, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)
        Dim today As DateTime = DateTime.Today
        Dim daysBetween As Double = (lastdate - today).TotalDays
        Return daysBetween
    End Function

End Class