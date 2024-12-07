Imports System.Data.SqlClient
Imports System.Security
Imports Microsoft.VisualBasic.ApplicationServices
Imports System.Data
Imports System.Text
Imports System.Reflection
Imports System.Security.Policy
Imports System.IO
Public Class Result
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim postData As String = Request.Form("data")
        MsgBox("Received data: " & postData)

        Dim ie = GetLast()
        Tarix.Text = Connection.Get_value("ID", ie, "Date", "Exams", "Local") & " " & Connection.Get_value("ID", GetLast, "Time", "Exams", "Local")
        'Fen.Text = Connection.Get_value("ID", GetLast, "SubjectID", "Exams", "Local")
        Fen.Text = "Epidemiologiya"
        Araliq.Text = Connection.Get_value("ID", GetLast, "Type", "Exams", "Local")



        Dim input As String = Connection.Get_value("ID", ie, "Question", "[Exams]", "Local")
        Dim result As String() = input.Split(";"c)

        Dim C_trues As Integer = 0
        Dim C_mistakes As Integer = 0
        Dim C_unans As Integer = 0

        Dim real As String
        Dim metn_sual As String = Connection.Get_value("ID", ie, "Question", "[Exams]", "Local")
        Dim metn_cavab As String = Connection.Get_value("ID", ie, "Answers", "[Exams]", "Local")
        Dim result_sual As String() = metn_sual.Split(","c)
        Dim result_cavab As String() = metn_cavab.Split(","c)
        Dim count As Integer
        count = metn_sual.Split(",").Length - 1
        Dim status(count) As String

        Dim dt As DataTable = New DataTable
        dt.Columns.AddRange(New DataColumn() {New DataColumn("#", GetType(System.Int32)), New DataColumn("Status", GetType(System.String)), New DataColumn("Bax", GetType(System.String))})

        For i = 0 To count - 1
            If result_cavab(i) = "-" Then
                status(i) = "-"
                C_unans += 1
            Else
                real = Connection.Get_value("ID", result_sual(i), "Answers", "no3", "Cavab")
                If result_cavab(i) = real Then
                    status(i) = "<i class='fa fa-check'></i>"
                    C_trues += 1
                Else
                    status(i) = "<i class='fa fa-ban'></i>"
                    C_mistakes += 1
                End If
            End If
            dt.Rows.Add(i + 1, status(i), "<a href='#' id='" & i + 1 & "' onclick='preview(" & i + 1 & ")' data-id='" & result_sual(i) & "' data-Yans='" & result_cavab(i) & "' style='padding:0px' class='btn btn-secondary'><i class='fa fa-eye'></i></a>")
        Next
        Label1.Text = "Suallara ətraflı baxmaq üçün sol xanadan müvafiq xananı seçin."

        Trues.InnerText = C_trues
        Wrong.InnerText = C_mistakes
        Unans.InnerText = C_unans

        Dim sb As StringBuilder = New StringBuilder()
        'Table start.
        sb.Append("<table class='table table-hover table-centered mb-0' cellspacing='0' rules='all' border='1' id='ERT' style='border-collapse: collapse;border-left:none;border-top:none;border-right:none;vertical-align: middle;text-align:center'>")
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
        Literal1.Text = sb.ToString
    End Sub
    Function GetLast()
        Dim US_ID As String = 1
        Dim datas As String
        Dim query As String
        query = "SELECT  [ID]
    FROM         [Exams]
    WHERE        [UserID] = '" & US_ID & "'
 order by [ID] Desc
    OFFSET 0 ROWS
    FETCH NEXT 1 ROWS ONLY;"
        Dim connect As String = Connection.LocalDB
        Using conn As New SqlConnection(connect)
            Using cmd As New SqlCommand(query, conn)
                cmd.Connection = conn
                conn.Open()
                Dim myreade As SqlDataReader

                myreade = cmd.ExecuteReader
                myreade.Read()

                datas = myreade("ID")
                Return datas
            End Using
        End Using
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function preview(ByVal ID As String, ByVal ans As String) As String
        Dim trues = Connection.Get_value("ID", ID, "Answers", "no3", "Cavab")
        Dim texts = File.ReadAllText(HttpContext.Current.Server.MapPath("App_Data\1\" & ID & ".html"))

        Return "<div class='col-2'>" & ID & "</div><div class='col-10 text-end'><p class='m-1'>Düzgün cavab : " & trues & "</p><p class='m-1'>Sizin cavabınız : " & ans & "</p></div><hr><div>" & texts & "</div></div>"
    End Function
End Class