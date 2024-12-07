Imports System.Threading
Imports System.Globalization

Public Class Base
    Inherits System.Web.UI.Page
    Protected Overrides Sub InitializeCulture()
        Dim language As String = "en-us"

        'Detect User's Language.
        If Request.UserLanguages IsNot Nothing Then
            'Set the Language.
            language = Request.UserLanguages(0)
        End If

        'Check if PostBack is caused by Language DropDownList

        language = Connection.Get_value("ID", 10, "Cur_lang", "Users", "User")
        'Set the Culture.
        Thread.CurrentThread.CurrentCulture = New CultureInfo(language)
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(language)


    End Sub
End Class
