Imports DESD.Math

<TestClass> _
Public Class CubicSplineTest

    <TestMethod> _
    Public Sub TestSine()

        Dim X(100) As Double
        Dim Y(100) As Double

        For i As Integer = 0 To 100
            X(i) = CDbl(i) / 10.0
            Y(i) = System.Math.Sin(X(i))
        Next
        Dim DYDXN As Double = (Y(100) - Y(99)) / (X(100) - X(99))
        Dim CS As New CubicSpline(X, Y, 1.0, DYDXN)

        Dim xx As Double
        Dim yy As Double
        Dim expectedyy As Double
        Dim delta As Double = 0.0005

        For i As Integer = 0 To 1000
            xx = CDbl(i) / 100.0
            yy = CS.Y(xx)
            expectedyy = System.Math.Sin(xx)

            Assert.AreEqual(expectedyy, yy, delta)

        Next

    End Sub

End Class
