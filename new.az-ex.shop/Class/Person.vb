Public Class Person
    Public Property FirstName As String
    Public Property LastName As String
    Public Property Age As Integer

    Public ask

    ' Constructor
    Public Sub New(FN As String, LN As String, AG As Integer)
        ask = FN
        FirstName = FN
        LastName = LN
        Age = AG
    End Sub

    ' Method
    Public Sub DisplayInfo()
        Console.WriteLine("Name: " & FirstName & " " & LastName & ", Age: " & Age)
    End Sub
End Class
