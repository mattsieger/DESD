
Imports System.Math
Imports DESD.Math

<TestClass> _
Public Class Functions_Legendre_UnitTest

    <TestMethod> _
    Public Sub P1m1A()

        Dim N As Integer = 1000

        Dim x As Double
        Dim L As Integer = 1
        Dim M As Integer = -1

        Dim testVal As Double
        Dim expectVal As Double

        Dim delta As Double = 0.00000000000001

        For i As Integer = 0 To N

            x = 2.0 * (CDbl(i) / CDbl(N)) - 1.0

            testVal = Functions.Legendre(L, M, x)

            expectVal = 0.5 * System.Math.Sqrt(1.0 - x ^ 2)

            Assert.AreEqual(expectVal, testVal, delta)

        Next
    End Sub

    <TestMethod> _
    Public Sub P1m1B()

        Dim N As Integer = 1000

        Dim x As Double
        Dim L As Integer = 1
        Dim M As Integer = -1

        Dim testVal As Double
        Dim expectVal As Double

        Dim delta As Double = 0.00000000000001

        For i As Integer = 0 To N

            x = 2.0 * (CDbl(i) / CDbl(N)) - 1.0

            testVal = Functions.Legendre(L, M, x)

            expectVal = -0.5 * Functions.Legendre(L, -M, x)

            Assert.AreEqual(expectVal, testVal, delta)

        Next
    End Sub


    <TestMethod> _
    Public Sub P11()

        Dim N As Integer = 1000

        Dim x As Double
        Dim L As Integer = 1
        Dim M As Integer = 1

        Dim testVal As Double
        Dim expectVal As Double

        Dim delta As Double = 0.00000000000001

        For i As Integer = 0 To N

            x = 2.0 * (CDbl(i) / CDbl(N)) - 1.0

            testVal = Functions.Legendre(L, M, x)

            expectVal = -System.Math.Sqrt(1.0 - x ^ 2)

            Assert.AreEqual(expectVal, testVal, delta)

        Next
    End Sub

    <TestMethod> _
    Public Sub P22()

        Dim N As Integer = 1000

        Dim x As Double
        Dim L As Integer = 2
        Dim M As Integer = 2

        Dim testVal As Double
        Dim expectVal As Double

        Dim delta As Double = 0.00000000000001

        For i As Integer = 0 To N

            x = 2.0 * (CDbl(i) / CDbl(N)) - 1.0

            testVal = Functions.Legendre(L, M, x)

            expectVal = 3.0 - 3.0 * x * x

            Assert.AreEqual(expectVal, testVal, delta)

        Next
    End Sub

    <TestMethod> _
    Public Sub P33()

        Dim N As Integer = 1000

        Dim x As Double
        Dim L As Integer = 3
        Dim M As Integer = 3

        Dim testVal As Double
        Dim expectVal As Double

        Dim delta As Double = 0.00000000000001

        For i As Integer = 0 To N

            x = 2.0 * (CDbl(i) / CDbl(N)) - 1.0

            testVal = Functions.Legendre(L, M, x)

            expectVal = -15.0 * Pow((1.0 - x * x), 1.5)

            Assert.AreEqual(expectVal, testVal, delta)

        Next
    End Sub

    <TestMethod> _
    Public Sub P44()

        Dim N As Integer = 1000

        Dim x As Double
        Dim L As Integer = 4
        Dim M As Integer = 4

        Dim testVal As Double
        Dim expectVal As Double

        Dim delta As Double = 0.0000000000001

        For i As Integer = 0 To N

            x = 2.0 * (CDbl(i) / CDbl(N)) - 1.0

            testVal = Functions.Legendre(L, M, x)

            expectVal = 105.0 * Pow((x * x - 1.0), 2.0)

            Assert.AreEqual(expectVal, testVal, delta)

        Next
    End Sub

    <TestMethod> _
    Public Sub P55()

        Dim N As Integer = 1000

        Dim x As Double
        Dim L As Integer = 5
        Dim M As Integer = 5

        Dim testVal As Double
        Dim expectVal As Double

        Dim delta As Double = 0.000000000001

        For i As Integer = 0 To N

            x = 2.0 * (CDbl(i) / CDbl(N)) - 1.0

            testVal = Functions.Legendre(L, M, x)

            expectVal = -945.0 * Pow(1.0 - x * x, 2.5)

            Assert.AreEqual(expectVal, testVal, delta)

        Next
    End Sub

    <TestMethod> _
    Public Sub P5m5()

        Dim N As Integer = 1000

        Dim x As Double
        Dim L As Integer = 5
        Dim M As Integer = -5

        Dim testVal As Double
        Dim expectVal As Double

        Dim delta As Double = 0.000000000001

        For i As Integer = 0 To N

            x = 2.0 * (CDbl(i) / CDbl(N)) - 1.0

            testVal = Functions.Legendre(L, M, x)

            expectVal = -Exp(-Functions.FactorialLn(10)) * Functions.Legendre(5, 5, x)

            Assert.AreEqual(expectVal, testVal, delta)

        Next
    End Sub


    ''' <summary>
    ''' Tests return values of the Legendre(l,m,x) function for l = 0.
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod> _
    Public Sub P00()

        Dim XValues As Double() = {0.0R, _
                           0.1R, _
                           0.2R, _
                           0.3R, _
                           0.4R, _
                           0.5R, _
                           0.6R, _
                           0.7R, _
                           0.75R, _
                           0.77R, _
                           0.79R, _
                           0.81R, _
                           0.83R, _
                           0.85R, _
                           0.87R, _
                           0.89R, _
                           0.93R, _
                           0.9375R, _
                           0.94R, _
                           0.95R, _
                           0.96R, _
                           0.97R, _
                           0.98R, _
                           0.99R, _
                           0.999R, _
                           0.9999R, _
                           0.99999R, _
                           0.999999R, _
                           0.99999999R, _
                           0.999999999999999R}

        Dim testval As Double
        Dim expectval As Double

        Dim maxULPs As Long = 1

        Dim x As Double

        For i As Integer = 0 To XValues.Length - 1
            x = XValues(i)
            testval = Functions.Legendre(0, 0, x)
            expectval = 1.0
            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
        Next

    End Sub



    ''' <summary>
    ''' Tests return values of the Legendre(l,m,x) function for l = 1 and m = 0.
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod> _
    Public Sub Legendre_l1m0()

        Dim XValues As Double() = {0.0R, _
                           0.1R, _
                           0.2R, _
                           0.3R, _
                           0.4R, _
                           0.5R, _
                           0.6R, _
                           0.7R, _
                           0.75R, _
                           0.77R, _
                           0.79R, _
                           0.81R, _
                           0.83R, _
                           0.85R, _
                           0.87R, _
                           0.89R, _
                           0.93R, _
                           0.9375R, _
                           0.94R, _
                           0.95R, _
                           0.96R, _
                           0.97R, _
                           0.98R, _
                           0.99R, _
                           0.999R, _
                           0.9999R, _
                           0.99999R, _
                           0.999999R, _
                           0.99999999R, _
                           0.999999999999999R}

        Dim testval As Double
        Dim expectval As Double

        Dim maxULPs As Long = 1

        Dim x As Double

        For i As Integer = 0 To XValues.Length - 1
            x = XValues(i)
            testval = Functions.Legendre(1, 0, x)
            expectval = x
            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
        Next

    End Sub




    ''' <summary>
    ''' Tests return values of the Legendre(l,m,x) function for l = 1 and m = 1.
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod> _
    Public Sub Legendre_l1m1()

        Dim XValues As Double() = {0.0R, _
                           0.1R, _
                           0.2R, _
                           0.3R, _
                           0.4R, _
                           0.5R, _
                           0.6R, _
                           0.7R, _
                           0.75R, _
                           0.77R, _
                           0.79R, _
                           0.81R, _
                           0.83R, _
                           0.85R, _
                           0.87R, _
                           0.89R, _
                           0.93R, _
                           0.9375R, _
                           0.94R, _
                           0.95R, _
                           0.96R, _
                           0.97R, _
                           0.98R, _
                           0.99R, _
                           0.999R, _
                           0.9999R, _
                           0.99999R, _
                           0.999999R, _
                           0.99999999R, _
                           0.999999999999999R}

        Dim testval As Double
        Dim expectval As Double

        Dim maxULPs As Long = 1

        Dim x As Double

        For i As Integer = 0 To XValues.Length - 1
            x = XValues(i)
            testval = Functions.Legendre(1, 0, x)
            expectval = x
            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
        Next

    End Sub



    ''' <summary>
    ''' Tests return values of the Legendre(l,m,x) function.
    ''' </summary>
    ''' <remarks>Values taken from the POLPAK test library.</remarks>
    <TestMethod> _
    Public Sub Legendre_GridValues()

        Dim expectedvalues() As Double = {0.0, _
                                  -0.5, _
     0.0, _
     0.375, _
     0.0, _
    -0.8660254037844386, _
    -1.299038105676658, _
    -0.3247595264191645, _
     1.3531646934131849, _
    -0.28, _
     1.175755076535925, _
     2.88, _
    -14.10906091843111, _
    -3.955078125, _
    -9.99755859375, _
     82.653114441004846, _
     20.244428368151521, _
    -423.79975318908691, _
     1638.3206248283391, _
    -20256.873892272251}


        Dim mvalues() As Integer = {0, 0, 0, 0, _
    0, 1, 1, 1, _
    1, 0, 1, 2, _
    3, 2, 2, 3, _
    3, 4, 4, 5}


        Dim lvalues() As Integer = {1, 2, 3, 4, _
    5, 1, 2, 3, _
    4, 3, 3, 3, _
    3, 4, 5, 6, _
    7, 8, 9, 10}

        Dim xvalues() As Double = {0.0, _
            0.0, _
            0.0, _
            0.0, _
            0.0, _
            0.5, _
            0.5, _
            0.5, _
            0.5, _
            0.2, _
            0.2, _
            0.2, _
            0.2, _
            0.25, _
            0.25, _
            0.25, _
            0.25, _
            0.25, _
            0.25, _
            0.25}


        Dim testval As Double
        Dim expectval As Double

        Dim maxULPs As Long = 4

        Dim x As Double

        For i As Integer = 0 To xvalues.Length - 1
            x = xvalues(i)
            testval = Functions.Legendre(lvalues(i), mvalues(i), x)
            expectval = expectedvalues(i)
            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
        Next

    End Sub


End Class
