
Imports DESD.Math

<TestClass> _
Public Class Functions_CSphericalBessel_UnitTest


    <TestMethod()> _
    Public Sub CSphericalBesselJ1()

        Dim n As Integer = 5
        Dim M As Integer = 100
        Dim X(M) As Double
        Dim XMax As Double = 40.0
        Dim XMin As Double = 0.0

        For i As Integer = 0 To M
            X(i) = XMin + CDbl(i) * ((XMax - XMin) / CDbl(M))
        Next

        Dim testval As Complex
        'Dim expectval As Complex
        'Dim tolerance as Double = 1.0E-10

        For i As Integer = 0 To X.Length - 1
            testval = Functions.CSphericalBesselJ(n, New Complex(X(i), 1.0))
            'assert.AreEqual(expectval,testval,tolerance)
            'console.WriteLine(X(i).ToString & ", "  & testval.Real.ToString & ", "  & testval.imag.ToString)
        Next

    End Sub

End Class
