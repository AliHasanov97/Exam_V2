Imports System.Data.SqlClient

Public Class WebForm2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim grid1 As New GridView
        Dim connectionStrings As String = Connection.User
        Dim sqls As String = "SELECT
      [Date]
      ,[Time]
      ,[Operation]
      ,[Sum]
      ,[Message]
      ,[Balance]
  FROM [Balance]
 Where [User_id] = '2'
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
        grid1.DataBind()
        grid1.HeaderRow.TableSection = TableRowSection.TableHeader
        grid1.HeaderRow.Cells(0).Text = "Tarix"
        grid1.HeaderRow.Cells(1).Text = "Vaxt"
        grid1.HeaderRow.Cells(2).Text = "Əməliyyat"
        grid1.HeaderRow.Cells(3).Text = "Məbləq"
        grid1.HeaderRow.Cells(4).Text = "Qeyd"
        grid1.HeaderRow.Cells(5).Text = "Balans"
        PlaceHolder1.Controls.Add(grid1)
    End Sub

End Class