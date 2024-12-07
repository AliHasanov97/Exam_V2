Imports System.Globalization
Imports System.Threading
Public Class WebForm3

    Inherits Base
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ID = Entrance.Authenticator
        entry.InnerText = "Salam " & Connection.Get_value("ID", ID, "Full_Name", "Users", "nUser")
    End Sub
End Class