
Imports System.Math
Imports DESD.Math

<TestClass> _
Public Class Functions_SphericalHarmonic_UnitTest


    <TestMethod> _
    Public Sub Y00()

        Dim nTheta As Integer = 100
        Dim nPhi As Integer = 100

        Dim Theta As Double
        Dim Phi As Double
        Dim L As Integer = 0
        Dim M As Integer = 0

        Dim testVal As Complex
        Dim expectVal As Complex

        Dim delta As Double = 0.00000000000001

        Dim coeff As New Complex(0.5 * System.Math.Sqrt(1.0 / System.Math.PI), 0.0)

        For iT As Integer = 0 To nTheta

            Theta = (CDbl(iT) / CDbl(nTheta)) * System.Math.PI

            For iP As Integer = 0 To nPhi

                Phi = (CDbl(iP) / CDbl(nPhi)) * 2.0 * System.Math.PI

                testVal = Functions.SphericalHarmonic(L, M, Theta, Phi)

                expectVal = coeff

                Assert.AreEqual(expectVal.Real, testVal.Real, delta)
                Assert.AreEqual(expectVal.Imag, testVal.Imag, delta)
            Next
        Next

    End Sub

    <TestMethod> _
    Public Sub Y1m1()

        Dim nTheta As Integer = 100
        Dim nPhi As Integer = 100

        Dim Theta As Double
        Dim Phi As Double
        Dim L As Integer = 1
        Dim M As Integer = -1

        Dim testVal As Complex
        Dim expectVal As Complex

        Dim delta As Double = 0.00000000000001

        Dim coeff As Double = 0.5 * System.Math.Sqrt(3.0 / (2.0 * System.Math.PI))

        For iT As Integer = 0 To nTheta

            Theta = (CDbl(iT) / CDbl(nTheta)) * System.Math.PI

            For iP As Integer = 0 To nPhi

                Phi = (CDbl(iP) / CDbl(nPhi)) * 2.0 * System.Math.PI

                testVal = Functions.SphericalHarmonic(L, M, Theta, Phi)

                expectVal = coeff * Complex.CExp(-Complex.i * Phi) * Sin(Theta)

                Assert.AreEqual(expectVal.Real, testVal.Real, delta)
                Assert.AreEqual(expectVal.Imag, testVal.Imag, delta)
            Next
        Next
    End Sub

    <TestMethod> _
    Public Sub Y10()

        Dim nTheta As Integer = 100
        Dim nPhi As Integer = 100

        Dim Theta As Double
        Dim Phi As Double
        Dim L As Integer = 1
        Dim M As Integer = 0

        Dim testVal As Complex
        Dim expectVal As Complex

        Dim delta As Double = 0.00000000000001

        Dim coeff As Double = 0.5 * System.Math.Sqrt(3.0 / System.Math.PI)

        For iT As Integer = 0 To nTheta

            Theta = (CDbl(iT) / CDbl(nTheta)) * System.Math.PI

            For iP As Integer = 0 To nPhi

                Phi = (CDbl(iP) / CDbl(nPhi)) * 2.0 * System.Math.PI

                testVal = Functions.SphericalHarmonic(L, M, Theta, Phi)

                expectVal = New Complex(coeff * Cos(Theta), 0.0)

                Assert.AreEqual(expectVal.Real, testVal.Real, delta)
                Assert.AreEqual(expectVal.Imag, testVal.Imag, delta)
            Next
        Next
    End Sub


    <TestMethod> _
    Public Sub Y11()

        Dim nTheta As Integer = 100
        Dim nPhi As Integer = 100

        Dim Theta As Double
        Dim Phi As Double
        Dim L As Integer = 1
        Dim M As Integer = 1

        Dim testVal As Complex
        Dim expectVal As Complex

        Dim delta As Double = 0.00000000000001

        Dim coeff As Double = 0.5 * System.Math.Sqrt(3.0 / (2.0 * System.Math.PI))

        For iT As Integer = 0 To nTheta

            Theta = (CDbl(iT) / CDbl(nTheta)) * System.Math.PI

            For iP As Integer = 0 To nPhi

                Phi = (CDbl(iP) / CDbl(nPhi)) * 2.0 * System.Math.PI

                testVal = Functions.SphericalHarmonic(L, M, Theta, Phi)

                expectVal = -coeff * Complex.CExp(Complex.i * Phi) * Sin(Theta)

                Assert.AreEqual(expectVal.Real, testVal.Real, delta)
                Assert.AreEqual(expectVal.Imag, testVal.Imag, delta)
            Next
        Next
    End Sub


    <TestMethod> _
    Public Sub Y2m2()

        Dim nTheta As Integer = 100
        Dim nPhi As Integer = 100

        Dim Theta As Double
        Dim Phi As Double
        Dim L As Integer = 2
        Dim M As Integer = -2

        Dim testVal As Complex
        Dim expectVal As Complex

        Dim delta As Double = 0.00000000000001

        Dim coeff As Double = 0.25 * System.Math.Sqrt(15.0 / (2.0 * System.Math.PI))

        For iT As Integer = 0 To nTheta

            Theta = (CDbl(iT) / CDbl(nTheta)) * System.Math.PI

            For iP As Integer = 0 To nPhi

                Phi = (CDbl(iP) / CDbl(nPhi)) * 2.0 * System.Math.PI

                testVal = Functions.SphericalHarmonic(L, M, Theta, Phi)

                expectVal = coeff * Complex.CExp(-2.0 * Complex.i * Phi) * Sin(Theta) ^ 2

                Assert.AreEqual(expectVal.Real, testVal.Real, delta)
                Assert.AreEqual(expectVal.Imag, testVal.Imag, delta)
            Next
        Next
    End Sub

    <TestMethod> _
    Public Sub Y2m1()

        Dim nTheta As Integer = 100
        Dim nPhi As Integer = 100

        Dim Theta As Double
        Dim Phi As Double
        Dim L As Integer = 2
        Dim M As Integer = -1

        Dim testVal As Complex
        Dim expectVal As Complex

        Dim delta As Double = 0.00000000000001

        Dim coeff As Double = 0.5 * System.Math.Sqrt(15.0 / (2.0 * System.Math.PI))

        For iT As Integer = 0 To nTheta

            Theta = (CDbl(iT) / CDbl(nTheta)) * System.Math.PI

            For iP As Integer = 0 To nPhi

                Phi = (CDbl(iP) / CDbl(nPhi)) * 2.0 * System.Math.PI

                testVal = Functions.SphericalHarmonic(L, M, Theta, Phi)

                expectVal = coeff * Complex.CExp(-1.0 * Complex.i * Phi) * Sin(Theta) * Cos(Theta)

                Assert.AreEqual(expectVal.Real, testVal.Real, delta)
                Assert.AreEqual(expectVal.Imag, testVal.Imag, delta)
            Next
        Next
    End Sub


    <TestMethod> _
    Public Sub Y20()

        Dim nTheta As Integer = 100
        Dim nPhi As Integer = 100

        Dim Theta As Double
        Dim Phi As Double
        Dim L As Integer = 2
        Dim M As Integer = 0

        Dim testVal As Complex
        Dim expectVal As Complex

        Dim delta As Double = 0.00000000000001

        Dim coeff As Double = 0.25 * System.Math.Sqrt(5.0 / System.Math.PI)

        For iT As Integer = 0 To nTheta

            Theta = (CDbl(iT) / CDbl(nTheta)) * System.Math.PI

            For iP As Integer = 0 To nPhi

                Phi = (CDbl(iP) / CDbl(nPhi)) * 2.0 * System.Math.PI

                testVal = Functions.SphericalHarmonic(L, M, Theta, Phi)

                expectVal = New Complex(coeff * (3.0 * Cos(Theta) ^ 2 - 1.0), 0.0)

                Assert.AreEqual(expectVal.Real, testVal.Real, delta)
                Assert.AreEqual(expectVal.Imag, testVal.Imag, delta)
            Next
        Next
    End Sub


    <TestMethod> _
    Public Sub Y21()

        Dim nTheta As Integer = 100
        Dim nPhi As Integer = 100

        Dim Theta As Double
        Dim Phi As Double
        Dim L As Integer = 2
        Dim M As Integer = 1

        Dim testVal As Complex
        Dim expectVal As Complex

        Dim delta As Double = 0.00000000000001

        Dim coeff As Double = -0.5 * System.Math.Sqrt(15.0 / (2.0 * System.Math.PI))

        For iT As Integer = 0 To nTheta

            Theta = (CDbl(iT) / CDbl(nTheta)) * System.Math.PI

            For iP As Integer = 0 To nPhi

                Phi = (CDbl(iP) / CDbl(nPhi)) * 2.0 * System.Math.PI

                testVal = Functions.SphericalHarmonic(L, M, Theta, Phi)

                expectVal = coeff * Sin(Theta) * Cos(Theta) * Complex.CExp(Complex.i * Phi)

                Assert.AreEqual(expectVal.Real, testVal.Real, delta)
                Assert.AreEqual(expectVal.Imag, testVal.Imag, delta)
            Next
        Next
    End Sub

    <TestMethod> _
    Public Sub Y22()

        Dim nTheta As Integer = 100
        Dim nPhi As Integer = 100

        Dim Theta As Double
        Dim Phi As Double
        Dim L As Integer = 2
        Dim M As Integer = 2

        Dim testVal As Complex
        Dim expectVal As Complex

        Dim delta As Double = 0.00000000000001

        Dim coeff As Double = 0.25 * System.Math.Sqrt(15.0 / (2.0 * System.Math.PI))

        For iT As Integer = 0 To nTheta

            Theta = (CDbl(iT) / CDbl(nTheta)) * System.Math.PI

            For iP As Integer = 0 To nPhi

                Phi = (CDbl(iP) / CDbl(nPhi)) * 2.0 * System.Math.PI

                testVal = Functions.SphericalHarmonic(L, M, Theta, Phi)

                expectVal = coeff * Sin(Theta) ^ 2 * Complex.CExp(2.0 * Complex.i * Phi)

                Assert.AreEqual(expectVal.Real, testVal.Real, delta)
                Assert.AreEqual(expectVal.Imag, testVal.Imag, delta)
            Next
        Next
    End Sub



    <TestMethod> _
    Public Sub Y5m5()

        Dim nTheta As Integer = 100
        Dim nPhi As Integer = 100

        Dim Theta As Double
        Dim Phi As Double
        Dim L As Integer = 5
        Dim M As Integer = -5

        Dim testVal As Complex
        Dim expectVal As Complex

        Dim delta As Double = 1.0

        Dim coeff As Double = (3.0 / 32.0) * System.Math.Sqrt(77.0 / System.Math.PI)

        For iT As Integer = 0 To nTheta

            Theta = (CDbl(iT) / CDbl(nTheta)) * System.Math.PI

            For iP As Integer = 0 To nPhi

                Phi = (CDbl(iP) / CDbl(nPhi)) * 2.0 * System.Math.PI

                testVal = Functions.SphericalHarmonic(L, M, Theta, Phi)

                expectVal = coeff * Complex.CExp(-5.0 * Complex.i * Phi) * System.Math.Pow(Sin(Theta) * Sin(Theta), 5.0 / 2.0)

                Assert.AreEqual(expectVal.Real, testVal.Real, delta)
                Assert.AreEqual(expectVal.Imag, testVal.Imag, delta)
            Next
        Next
    End Sub


End Class
