Imports DESD.Math
Imports System.Math

<TestClass> _
Public Class Functions_RotationMatrices_UnitTest

    ''' <summary>
    ''' Tests the symmetry relation from Messiah, page 1072, section 12, first equation.
    ''' </summary>
    <TestMethod> _
    Public Sub Symmetry01()

        Dim NBeta As Integer = 100
        Dim Lmax As Integer = 10

        Dim Beta As Double
        Dim expectVal1 As Double
        Dim testVal As Double
        Dim expectVal2 As Double
        Dim delta As Double = 0.000000000001

        For ib As Integer = 0 To NBeta
            '// Beta runs from zero to Pi
            Beta = (CDbl(ib) / CDbl(NBeta)) * System.Math.PI
            For L As Integer = 0 To Lmax
                For M As Integer = -L To L
                    For Mprime As Integer = -L To L
                        testVal = Functions.LRotm(L, M, Mprime, Beta)
                        expectVal1 = Functions.LRotm(L, Mprime, M, -Beta)
                        expectVal2 = (-1.0) ^ (M - Mprime) * Functions.LRotm(L, -M, -Mprime, Beta)
                        Assert.AreEqual(expectVal1, testVal, delta)
                        Assert.AreEqual(expectVal2, testVal, delta)
                    Next
                Next
            Next
        Next
    End Sub


    <TestMethod> _
    Public Sub Symmetry02()

        Dim NBeta As Integer = 100
        Dim Lmax As Integer = 10

        Dim Beta As Double
        Dim expectVal1 As Double
        Dim testVal As Double
        Dim expectVal2 As Double
        Dim expectVal3 As Double
        Dim delta As Double = 0.00000000000001

        For ib As Integer = 0 To NBeta
            '// Beta runs from zero to Pi
            Beta = (CDbl(ib) / CDbl(NBeta)) * System.Math.PI
            For L As Integer = 0 To Lmax
                For M As Integer = -L To L
                    testVal = Functions.LRotm(L, M, L, Beta)
                    expectVal1 = (-1.0) ^ (L - M) * Functions.LRotm(L, L, M, Beta)
                    expectVal2 = Functions.LRotm(L, -L, -M, Beta)
                    expectVal3 = (-1.0) ^ (L - M) * Functions.LRotm(L, -M, -L, Beta)
                    Assert.AreEqual(expectVal1, testVal, delta)
                    Assert.AreEqual(expectVal2, testVal, delta)
                    Assert.AreEqual(expectVal3, testVal, delta)
                Next
            Next
        Next
    End Sub


    <TestMethod> _
    Public Sub YlmRotation01()

        Dim L As Integer = 3
        Dim M As Integer = -1

        Dim alpha As Double = 0.0
        Dim beta As Double = System.Math.PI / 2.0
        Dim gamma As Double = System.Math.PI / 2.0

        Dim delta As Double = 0.00000000000001

        Dim expectVal As Complex = Functions.SphericalHarmonic(L, M, 0.0, 0.0)

        Dim testVal As New Complex

        For Mprime As Integer = -L To L
            testVal += Functions.Rotm(L, Mprime, M, alpha, beta, gamma) * Functions.SphericalHarmonic(L, Mprime, System.Math.PI / 2.0, 0.0)
        Next

        Assert.AreEqual(expectVal.Real, testVal.Real, delta)
        Assert.AreEqual(expectVal.Imag, testVal.Imag, delta)

    End Sub

    <TestMethod> _
    Public Sub YlmRotation02()

        Dim L As Integer = 1
        Dim M As Integer = -1

        Dim alpha As Double = PI / 4.0
        Dim beta As Double = 0.0
        Dim gamma As Double = 0.0

        Dim delta As Double = 0.00000000000001

        Dim theta As Double = PI / 2.0
        Dim phi As Double = PI / 4.0
        Dim expectVal As Complex = Functions.SphericalHarmonic(L, M, theta, phi)

        Dim testVal As New Complex

        For Mprime As Integer = -L To L
            testVal += Functions.Rotm(L, Mprime, M, alpha, beta, gamma) * Functions.SphericalHarmonic(L, Mprime, PI / 2.0, PI / 2.0)
        Next

        Assert.AreEqual(expectVal.Real, testVal.Real, delta)
        Assert.AreEqual(expectVal.Imag, testVal.Imag, delta)

    End Sub


    <TestMethod> _
    Public Sub LRotm000()

        '// If l, m, and mprime all = 0, then LRitn = 1 for all beta. - test for 10k different rho.
        Dim minBeta As Double = 0.0
        Dim maxBeta As Double = System.Math.PI
        Dim nBeta As Integer = 10000
        Dim Beta As Double

        Dim expectval As Double = 1.0
        Dim testval As Double

        Dim maxULPs As Long = 1

        For i As Integer = 0 To nBeta
            Beta = minBeta + CDbl(i) * (maxBeta + minBeta) / nBeta
            testval = Functions.LRotm(0, 0, 0, Beta)
            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
        Next

    End Sub

    <TestMethod> _
    Public Sub LRotm100()

        '// If l, m, and mprime all = 0, then LRitn = 1 for all beta. - test for 10k different rho.
        Dim minBeta As Double = 0.0
        Dim maxBeta As Double = System.Math.PI
        Dim nBeta As Integer = 10000
        Dim Beta As Double

        Dim expectval As Double
        Dim testval As Double

        'Dim maxULPs As Long = 10000
        Dim delta As Double = 0.000000000000001

        For i As Integer = 0 To nBeta
            Beta = minBeta + CDbl(i) * (maxBeta + minBeta) / nBeta
            testval = Functions.LRotm(1, 0, 0, Beta)
            expectval = Cos(Beta)
            'Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Assert.AreEqual(expectval, testval, delta)
        Next

    End Sub

    <TestMethod> _
    Public Sub LRotm1m10()

        Dim minBeta As Double = 0.0
        Dim maxBeta As Double = System.Math.PI
        Dim nBeta As Integer = 10000
        Dim Beta As Double

        Dim expectval As Double
        Dim testval As Double

        'Dim maxULPs As Long = 10000
        Dim delta As Double = 0.000000000000001

        For i As Integer = 0 To nBeta
            Beta = minBeta + CDbl(i) * (maxBeta + minBeta) / nBeta
            testval = Functions.LRotm(1, -1, 0, Beta)
            expectval = (1.0 / Sqrt(2.0)) * Sin(Beta)
            'Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Assert.AreEqual(expectval, testval, delta)
        Next

    End Sub


    <TestMethod> _
    Public Sub LRotm110()

        Dim minBeta As Double = 0.0
        Dim maxBeta As Double = System.Math.PI
        Dim nBeta As Integer = 10000
        Dim Beta As Double

        Dim expectval As Double
        Dim testval As Double

        'Dim maxULPs As Long = 10000
        Dim delta As Double = 0.000000000000001

        For i As Integer = 0 To nBeta
            Beta = minBeta + CDbl(i) * (maxBeta + minBeta) / nBeta
            testval = Functions.LRotm(1, 1, 0, Beta)
            expectval = -(1.0 / Sqrt(2.0)) * Sin(Beta)
            'Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Assert.AreEqual(expectval, testval, delta)
        Next

    End Sub

End Class
