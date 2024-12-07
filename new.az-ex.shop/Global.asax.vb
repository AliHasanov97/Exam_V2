Imports System.Web.Routing
Imports System.Web.SessionState

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Private Sub Application_Start(sender As Object, e As EventArgs)
        RegisterRoutes(RouteTable.Routes)
    End Sub

    Private Shared Sub RegisterRoutes(routes As RouteCollection)
        routes.MapPageRoute("Default", "Default", "~/Default.aspx")
        routes.MapPageRoute("Account", "Account", "~/Upanel/account.aspx")
        routes.MapPageRoute("Balance", "Balance", "~/Upanel/Balance.aspx")
        routes.MapPageRoute("Store", "Store", "~/Upanel/store.aspx")
        routes.MapPageRoute("Login", "Login", "~/Entrance/login.aspx")
        routes.MapPageRoute("index", "Index", "~/Upanel/index.aspx")
        routes.MapPageRoute("question", "question", "~/Upanel/Question.aspx")
        routes.MapPageRoute("tarifs", "tarifs", "~/Upanel/tarifs.aspx")
        routes.MapPageRoute("Read", "Read", "~/Read.aspx")
        routes.MapPageRoute("Result", "Result", "~/Result.aspx")

    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class