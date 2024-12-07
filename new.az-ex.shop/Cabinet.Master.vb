Imports System.Globalization
Imports System.Threading
Public Class Cabinet
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim ID = Entrance.Authenticator
            Avatar.Src = Connection.Get_value("ID", ID, "Avatar", "Users", "nUser")
            Name.InnerText = Connection.Get_value("ID", ID, "Full_Name", "Users", "nUser")

            Avatar1.Src = Connection.Get_value("ID", ID, "Avatar", "Users", "nUser")
            Name1.InnerText = Connection.Get_value("ID", ID, "Full_Name", "Users", "nUser")
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Session.Remove("UserID")
        Response.Redirect("Login")
    End Sub
    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Session.Remove("UserID")
        Response.Redirect("Login")
    End Sub
End Class