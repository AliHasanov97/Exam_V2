Imports System.Data.SqlClient
Imports System.Security

Public Class WebForm1
    Inherits Base

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ID = Entrance.Authenticator
        Name.InnerText = Connection.Get_value("ID", ID, "Full_Name", "Users", "nUser")
        Avatar.ImageUrl = Connection.Get_value("ID", ID, "Avatar", "Users", "nUser")
        GTX.Attributes.Add("style", "background: no-repeat url(" & Connection.Get_value("ID", ID, "Background_Image", "Users", "nUser") & ") center center / cover")
        About.InnerText = Connection.Get_value("ID", ID, "About", "Users", "nUser")
        Job.InnerText = Connection.Get_value("ID", ID, "Job", "Users", "nUser")
        Email.InnerText = Connection.Get_value("ID", ID, "Email", "Users", "nUser")
        'About.InnerText = Session("Username").ToString
        Try
            Dim row As New Panel
            row.CssClass = "row"
            Dim col4 As New Panel
            col4.CssClass = "col-lg-8"
            Dim card As New Panel
            card.CssClass = "card"
            Dim query As String
            Dim connect As String = Connection.User
            query = "SELECT COUNT (*)
  FROM [HistoryOfexams]
where [User_id] = '150'"
            Dim result As Integer = 0
            Using conn As New SqlConnection(connect)
                Using cmd As New SqlCommand(query, conn)
                    conn.Open()
                    result = DirectCast(cmd.ExecuteScalar(), Integer)
                End Using
            End Using
            If result = 0 Then
                Dim netice As New HtmlGenericControl("p")
                netice.InnerText = "Sizin imtahan keçmişiniz yoxdur"
                netice.Attributes.Add("style", "text-align: center;font-family: 'times New Roman', Times, serif;font-size: 26px;font-style: italic;")
                PlaceHolder1.Controls.Add(netice)
            Else
                Dim grid1 As New GridView
                Dim connectionStrings As String = Connection.User
                Dim sqls As String = "SELECT TOP (10)
       [ID]
      ,[Date]
      ,[Type]
      ,[Subject]      
    FROM [HistoryOfexams]
    Where [User_id]= '1'
    Order By ID DESC"
                Dim connections As New SqlConnection(connectionStrings)
                Dim dataadapters As New SqlDataAdapter(sqls, connections)
                Dim dss As New DataSet()
                connections.Open()
                dataadapters.Fill(dss)
                connections.Close()
                grid1.DataSource = dss
                grid1.CssClass = "table table-hover table-centered mb-0"
                grid1.Attributes.Add("border", 0)
                AddHandler grid1.RowDataBound, AddressOf history
                grid1.DataBind()
                grid1.HeaderRow.TableSection = TableRowSection.TableHeader
                grid1.HeaderRow.Cells(0).Text = "ID"
                grid1.HeaderRow.Cells(1).Text = "Tarix"
                grid1.HeaderRow.Cells(2).Text = "Tip"
                grid1.HeaderRow.Cells(3).Text = "Fənn"
                PlaceHolder1.Controls.Add(grid1)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub history(sender As Object, e As GridViewRowEventArgs)
        e.Row.Cells(0).Attributes.Add("data-label", "ID")
        e.Row.Cells(1).Attributes.Add("data-label", "Tarix")
        e.Row.Cells(2).Attributes.Add("data-label", "Tip")
        e.Row.Cells(3).Attributes.Add("data-label", "Fən")
    End Sub

End Class