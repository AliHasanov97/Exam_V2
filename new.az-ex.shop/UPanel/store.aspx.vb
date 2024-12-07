Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Security
Imports System.IO

Public Class WebForm5
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ID = Entrance.Authenticator
        If Not IsPostBack Then
            Cedvel()
        Else
            Dim subs As String = Request("__EVENTTARGET")
            Dim parameter As String = Request("__EVENTARGUMENT")
            If subs = "Purchase" Then
                Purchase(parameter)
                Cedvel()
            ElseIf subs = "Rent" Then
                Rent(parameter)
                Cedvel()
            End If
            ViewState.Clear()
        End If

    End Sub
    Protected Sub Cedvel()
        Try
            Dim grid1 As New GridView
            grid1.ID = "ERT"
            Dim connectionStrings As String = Connection.NewUser
            Dim sqls As String = "SELECT
       [ID]
      ,[Subject]
      ,[About]
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
            grid1.HeaderRow.Cells(2).Text = "Əməliyyat"
            grid1.HeaderRow.Visible = True
            grid1.EnableViewState = True
            PlaceHolder1.Controls.Add(grid1)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Siyahı(sender As Object, e As GridViewRowEventArgs)
        Try
            e.Row.Visible = False
            Dim ID_Q As TableCell = e.Row.Cells(0)
            Dim ID = Entrance.Authenticator
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim connect As String = Connection.NewUser
                Dim query As String
                query = "SELECT COUNT(*)
  FROM [Purchase]
  where [UserID]= " & ID & " and [Action] = 'Buy' and [Type] = 'Subject' and [Note] = '" & ID_Q.Text & "'"
                Dim result As Integer = 0
                Using conn As New SqlConnection(connect)
                    Using cmd As New SqlCommand(query, conn)
                        conn.Open()
                        result = DirectCast(cmd.ExecuteScalar(), Integer)
                    End Using
                End Using
                If result = 0 Then
                    e.Row.Visible = True
                    Dim but_pre As New HtmlGenericControl("span")
                    but_pre.ID = ID_Q.Text & "preview"
                    but_pre.Attributes.Add("class", "btn btn-primary")
                    but_pre.Attributes.Add("style", "padding: 5px;margin:2px;width:100px")
                    but_pre.Attributes.Add("data-bs-toggle", "modal")
                    but_pre.Attributes.Add("data-bs-target", "#staticBackdrop")
                    but_pre.Attributes.Add("onclick", "Preview(" & ID_Q.Text & ")")
                    but_pre.InnerHtml = "<i class='fa-solid fa-eye'></i> Ön izləmə"

                    Dim but_about As New HtmlGenericControl("span")
                    but_about.ID = ID_Q.Text & "preview"
                    but_about.Attributes.Add("class", "btn btn-secondary")
                    but_about.Attributes.Add("style", "padding: 5px;margin:2px;width: 100px;")
                    but_about.Attributes.Add("data-bs-toggle", "modal")
                    but_about.Attributes.Add("data-bs-target", "#staticBackdrop")
                    but_about.Attributes.Add("onclick", "Detail(" & ID_Q.Text & ")")
                    but_about.InnerHtml = "<i class='fa-solid fa-cart-shopping'></i> Alış"
                    e.Row.Cells(2).Controls.Add(but_pre)
                    e.Row.Cells(2).Controls.Add(but_about)
                    'but.Attributes.Add("onclick", "__doPostBack('Purchase', " & ID_Q.Text & ")")
                Else
                    e.Row.Visible = False
                End If
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim connect As String = Connection.NewUser
                Dim query As String
                query = "SELECT COUNT(*)
  FROM [Purchase]
  where [UserID]= " & ID & " and [Action] = 'Rent' and [Type] = 'Subject' and [Note] = '" & ID_Q.Text & "'"
                Dim result As Integer = 0
                Using conn As New SqlConnection(connect)
                    Using cmd As New SqlCommand(query, conn)
                        conn.Open()
                        result = DirectCast(cmd.ExecuteScalar(), Integer)
                    End Using
                End Using
                If result > 0 Then
                    Dim connectionString As String = Connection.NewUser
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
                        e.Row.Visible = False
                    Else
                        e.Row.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Purchase(ID_Q As Integer)
        Try
            Dim ID = Entrance.Authenticator

            Dim Balance = Double.Parse(Connection.Get_value("ID", ID, "Balance", "Users", "nUser"))
            Dim Cost = Double.Parse(Connection.Get_value("ID", ID_Q, "Purchase", "Questions", "nUser"))

            If Cost > Balance Then
                Dim pan As New HtmlGenericControl("div")
                pan.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-ban' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <p style='font-size:18px;'><b>" & Connection.Get_value("ID", ID_Q, "Subject", "Questions", "nUser") & "</b> fənninin suallarını almaq üçün balansınızda kifayət qədər vəsait yoxdur.</p><!-- Breadcrumb -->              
                <span>Sizin balansınız : " & Balance & " ₼</span></div>
            </div>
<hr>
<a href='Balance' class='btn btn-secondary' style='width100px;width: auto;'>Balans</a></div>"
                PlaceHolder2.Controls.Add(pan)
            Else

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
                      ( '" & DateTime.Today.ToString("dd.MM.yyyy") & "'
                      ,'" & ID & "'
                      ,'Buy'
                      ,'Subject'
                      ,'-'
                      ,'" & ID_Q & "')"
                command.ExecuteNonQuery()
                connections.Close()

                Dim pan1 As New HtmlGenericControl("div")
                pan1.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-circle-check' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <span style='font-size:18px;'><b>" & Connection.Get_value("ID", ID_Q, "Subject", "Questions", "nUser") & "</b> fənni hesabınıza əlavə edildi.</span><!-- Breadcrumb -->              
                </div>
            </div>
<hr>
<a href='question' class='btn btn-secondary' style='width:100px;width: auto;'>Sualara keç</a></div>"
                PlaceHolder2.Controls.Add(pan1)


            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Rent(ID_Q As Integer)
        Try
            Dim ID = Entrance.Authenticator

            Dim Balance = Double.Parse(Connection.Get_value("ID", ID, "Balance", "Users", "nUser"))
            Dim Cost = Double.Parse(Connection.Get_value("ID", ID_Q, "Rent", "Questions", "nUser"))

            If Cost > Balance Then
                Dim pan1 As New HtmlGenericControl("div")
                pan1.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-ban' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <p style='font-size:18px;'><b>" & Connection.Get_value("ID", ID_Q, "Subject", "Questions", "nUser") & "</b> fənninin suallarını icarəyə götümək üçün balansınızda kifayət qədər vəsait yoxdur.</p><!-- Breadcrumb -->              
                <span>Sizin balansınız : " & Balance & " ₼</span></div>
            </div>
<hr>
<a href='Balance' class='btn btn-secondary' style='width100px;width: auto;'>Balans</a></div>"
                PlaceHolder2.Controls.Add(pan1)
            Else


                Dim startDate As DateTime = DateTime.Today
                Dim leaveNo As Integer = 1
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
                      ,'Rent'
                      ,'Subject'
                      ,'" & exp & "'
                      ,'" & ID_Q & "')"
                command.ExecuteNonQuery()
                connections.Close()

                Dim pan As New HtmlGenericControl("div")
                pan.InnerHtml = "
<div class='card card-line card-body bg-body-tertiary border-transparent mb-7'>
<div class='row align-items-center'>
                <div class='col-auto'>
                    <i class='fa-solid fa-circle-check' style='font-size:30px'></i>
                </div>
                <div class='col'>
                    <p style='font-size:18px;'><b>" & Connection.Get_value("ID", ID_Q, "Subject", "Questions", "nUser") & "</b> fənni hesabınıza əlavə edildi.</p><!-- Breadcrumb -->              
                <span><b>Qeyd : </b>" & startDate.AddDays(1) & " tarixinə qədər istifadə edə bilərsiniz.</span></div>
            </div>
<hr>
<a href='question' class='btn btn-secondary' style='width:100px;width: auto;'>Sualara keç</a></div>"
                PlaceHolder2.Controls.Add(pan)
            End If
        Catch ex As Exception

        End Try

    End Sub



    <System.Web.Services.WebMethod()>
    Public Shared Function detail(ByVal param As String)
        Try

            Return "<div class='modal-content'>
                <div class='modal-header'>
                    <h1 class='modal-title fs-5' id='staticBackdropLabel'>" & Connection.Get_value("ID", param, "Subject", "Questions", "nUser") & "</h1>
                    <button type='button' class='btn-close' data-bs-dismiss='modal' aria-label='Close'></button>
                </div>
                <div class='modal-body'>
                    </br>
                    " & Connection.Get_value("ID", param, "About", "Questions", "nUser") & "
                </div>
                <div class='modal-footer'>
                    <span style='margin:5px' onclick='Rent(" & param & ")' class='col btn btn-primary'><i class='fa-solid fa-business-time'></i> 1 günlük icarə " & Connection.Get_value("ID", param, "Rent", "Questions", "nUser") & " ₼</span>
                    <span style='margin:5px' onclick='Purchase(" & param & ")' class='col btn btn-secondary'><i class='fa-solid fa-cart-shopping'></i> Həmişəlik almaq " & Connection.Get_value("ID", param, "Purchase", "Questions", "nUser") & " ₼</span>
                </div>
            </div>"
        Catch ex As Exception
        End Try
    End Function

    Function expire(ByVal ID As Integer)
        Dim guy = Connection.Get_value("ID", ID, "Expire", "Purchase", "nUser")
        Dim lastdate As DateTime = DateTime.ParseExact(guy, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)
        Dim today As DateTime = DateTime.Today
        Dim daysBetween As Double = (lastdate - today).TotalDays
        Return daysBetween
    End Function

End Class