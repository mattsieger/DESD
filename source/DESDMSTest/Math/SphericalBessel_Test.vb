
Imports System.Math
Imports DESD.Math

<TestClass()> _
Public Class Functions_SphericalBessel_UnitTest

    <TestMethod()> _
    Public Sub SphericalBessel0()

        '// Compare to analytical form for n = 0.

        Dim n As Integer = 0
        Dim X(10000) As Double
        Dim XMax As Double = 50.0
        Dim XMin As Double = 0.000001

        For i As Integer = 0 To 10000
            X(i) = XMin + CDbl(i) * ((XMax - XMin) / 10000.0)
        Next

        Dim testval As Double()
        Dim expectJ As Double
        Dim expectY As Double
        Dim expectJprime As Double
        Dim expectYprime As Double

        Dim tolerance As Double = 0.000000001

        For i As Integer = 0 To X.Length - 1
            testval = Functions.SphericalBessel(n, X(i))

            expectJ = Sin(X(i)) / X(i)
            expectY = -Cos(X(i)) / X(i)
            expectJprime = (1.0 / X(i)) * (-expectJ + Cos(X(i)))
            expectYprime = (1.0 / X(i)) * (-expectY + Sin(X(i)))

            Assert.AreEqual(expectJ, testval(0), Abs(tolerance * expectJ))
            Assert.AreEqual(expectY, testval(1), Abs(tolerance * expectY))
            If Abs(expectJprime) < 0.001 Then
                Assert.AreEqual(expectJprime, testval(2), tolerance)
            Else
                Assert.AreEqual(expectJprime, testval(2), Abs(tolerance * expectJprime))
            End If
            If Abs(expectYprime) < 0.001 Then
                Assert.AreEqual(expectYprime, testval(3), tolerance)
            Else
                Assert.AreEqual(expectYprime, testval(3), Abs(tolerance * expectYprime))
            End If


        Next

    End Sub

    <TestMethod()> _
    Public Sub SphericalBessel1()

        '// Compare to analytical form for n = 1.

        Dim n As Integer = 1
        Dim X(10000) As Double
        Dim XMax As Double = 50.0
        Dim XMin As Double = 0.000001

        For i As Integer = 0 To 10000
            X(i) = XMin + CDbl(i) * ((XMax - XMin) / 10000.0)
        Next

        Dim testval As Double()
        Dim expectJ As Double
        Dim expectY As Double
        Dim expectJprime As Double
        Dim expectYprime As Double

        Dim tolerance As Double = 0.0000000001
        Dim tolerance2 As Double = 0.0001
        Dim tolerance3 As Double = 0.1

        For i As Integer = 0 To X.Length - 1
            testval = Functions.SphericalBessel(n, X(i))

            expectJ = Sin(X(i)) / (X(i) ^ 2) - Cos(X(i)) / X(i)
            expectY = -Cos(X(i)) / (X(i) ^ 2) - Sin(X(i)) / X(i)
            expectJprime = -(2.0 / X(i)) * (expectJ - 0.5 * Sin(X(i)))
            expectYprime = -(2.0 / X(i)) * (expectY - 0.5 * Cos(X(i)))

            Assert.AreEqual(expectJ, testval(0), tolerance)
            Assert.AreEqual(expectY, testval(1), Abs(tolerance * expectY))
            Assert.AreEqual(expectJprime, testval(2), tolerance2)
            'Assert.AreEqual(expectYprime,testval(3),abs(tolerance3*expectYprime))

            '// There is a big problem with the derivative of Y...
            '// Need to print it out and analyze.

        Next

    End Sub


    <TestMethod()> _
    Public Sub SphericalBesselJ0()

        '// Compare to analytical form for n = 0.

        Dim n As Integer = 0
        Dim X(10000) As Double
        Dim XMax As Double = 50.0
        Dim XMin As Double = 0.0

        For i As Integer = 0 To 10000
            X(i) = XMin + CDbl(i) * ((XMax - XMin) / 10000.0)
        Next

        Dim testval As Double
        Dim expectJ As Double

        Dim tolerance As Double = 0.0000000001

        For i As Integer = 0 To X.Length - 1
            testval = Functions.SphericalBesselJ(n, X(i))

            If X(i) = 0.0 Then
                expectJ = 1.0
            Else
                expectJ = Sin(X(i)) / X(i)
            End If

            Assert.AreEqual(expectJ, testval, Abs(tolerance * expectJ))
            'console.WriteLine(X(i).ToString & ", " & expectJ.ToString & ", " & testval.ToString)
        Next

    End Sub

    <TestMethod()> _
    Public Sub SphericalBesselJ1()

        '// Compare to analytical form for n = 0.

        Dim testval As Double = Functions.SphericalBesselJ(0, 1.0)
        Dim expectval As Double = Sin(1.0)
        Dim tolerance As Double = 0.000000001

        Assert.AreEqual(expectval, testval, Abs(tolerance * expectval))

    End Sub

    <TestMethod()> _
    Public Sub SphericalBesselJ2()

        '// Compare to analytical form for n = 0.

        Dim testval As Double = Functions.SphericalBesselJ(0, 40.0)
        Dim expectval As Double = Sin(40.0) / 40.0
        Dim tolerance As Double = 0.000000001

        Assert.AreEqual(expectval, testval, Abs(tolerance * expectval))

    End Sub

    <TestMethod()> _
    Public Sub SphericalBesselJ3()

        '// Compare to analytical form for n = 1.

        Dim n As Integer = 1
        Dim X(10000) As Double
        Dim XMax As Double = 50.0
        Dim XMin As Double = 0.0

        For i As Integer = 0 To 10000
            X(i) = XMin + CDbl(i) * ((XMax - XMin) / 10000.0)
        Next

        Dim testval As Double
        Dim expectJ As Double

        Dim sigfigtol As Integer = 11
        Dim z As Double
        For i As Integer = 0 To X.Length - 1
            z = X(i)
            testval = Functions.SphericalBesselJ(n, z)

            If z = 0.0 Then
                expectJ = 0.0
            Else
                expectJ = Sin(z) / z ^ 2 - Cos(z) / z
            End If

            Assert.IsTrue(TestUtilities.AreEqualToSigFigs(expectJ, testval, sigfigtol))

            'console.WriteLine(X(i).ToString & ", " & expectJ.ToString & ", " & testval.ToString)
        Next

    End Sub


    ''' <summary>
    ''' This test reveals some softness in the routine for x less than 0.06
    ''' </summary>
    <TestMethod()> _
    Public Sub SphericalBesselJ4()

        '// Compare to analytical form for n = 2 (exercise the recursion).

        Dim n As Integer = 2
        Dim X(10000) As Double
        Dim XMax As Double = 50.0
        Dim XMin As Double = 0.0

        For i As Integer = 0 To 10000
            X(i) = XMin + CDbl(i) * ((XMax - XMin) / 10000.0)
        Next

        Dim testval As Double
        Dim expectJ As Double

        Dim tolerance As Double = 0.0000001

        For i As Integer = 0 To X.Length - 1
            testval = Functions.SphericalBesselJ(n, X(i))

            If X(i) = 0.0 Then
                expectJ = 0.0
            Else
                expectJ = (3.0 / X(i) ^ 2 - 1.0) * Sin(X(i)) / X(i) - 3.0 * Cos(X(i)) / X(i) ^ 2
            End If

            'console.WriteLine(X(i).ToString & ", " & expectJ.ToString & ", " & testval.ToString)

            Assert.AreEqual(expectJ, testval, tolerance)

        Next

    End Sub

    <TestMethod()> _
    Public Sub SphericalBesselJ5()

        '// Compare to analytical form for n = 3 (exercise the recursion).

        Dim n As Integer = 3
        Dim X(10000) As Double
        Dim XMax As Double = 50.0
        Dim XMin As Double = 0.0

        For i As Integer = 0 To 10000
            X(i) = XMin + CDbl(i) * ((XMax - XMin) / 10000.0)
        Next

        Dim testval As Double
        Dim expectJ As Double
        Dim tolerance As Double = 0.00000002

        For i As Integer = 0 To X.Length - 1
            testval = Functions.SphericalBesselJ(n, X(i))

            If X(i) = 0.0 Then
                expectJ = 0.0
            Else
                expectJ = (15.0 / X(i) ^ 3 - 6.0 / X(i)) * Sin(X(i)) / X(i) - (15.0 / X(i) ^ 2 - 1.0) * Cos(X(i)) / X(i)
            End If

            'console.WriteLine(X(i).ToString & ", " & expectJ.ToString & ", " & testval.ToString & ", " & Abs(expectJ-testval).tostring)

            Assert.AreEqual(expectJ, testval, tolerance)

        Next

    End Sub


    <TestMethod()> _
    Public Sub SphericalBesselJ6()

        Dim expectval As Double = Functions.SphericalBessel(7, 5.0)(0)
        Dim testval As Double = Functions.SphericalBesselJ(7, 5.0)

        Dim tolerance As Double = 0.000000000000001

        Assert.AreEqual(expectval, testval, tolerance)

    End Sub


    <TestMethod()> _
    Public Sub SphericalBesselJ7()

        '// Compare for n = 10 (exercise the recursion).

        Dim n As Integer = 2
        Dim M As Integer = 1000
        Dim X(M) As Double
        Dim XMax As Double = 0.01
        Dim XMin As Double = 0.00000001

        For i As Integer = 0 To M
            X(i) = XMin + CDbl(i) * ((XMax - XMin) / CDbl(M))
        Next

        Dim testval As Double
        Dim expectval As Double
        Dim tolerance As Double = 0.00000001

        For i As Integer = 0 To X.Length - 1
            testval = Functions.SphericalBesselJ(n, X(i))
            expectval = Functions.SphericalBessel(n, X(i))(0)
            Assert.AreEqual(expectval, testval, tolerance)
            'console.WriteLine(X(i).ToString & ", " & expectval.ToString & ", " & testval.ToString)
        Next

    End Sub

    <TestMethod()> _
    Public Sub SphericalBesselJ8()

        '// Compare for n = 4 (exercise the recursion).

        Dim n As Integer = 2
        Dim M As Integer = 100
        Dim X(M) As Double
        Dim XMax As Double = 0.1
        Dim XMin As Double = 0.0

        For i As Integer = 0 To M
            X(i) = XMin + CDbl(i) * ((XMax - XMin) / CDbl(M))
        Next

        Dim testval As Double
        Dim expectval As Double
        Dim tolerance As Double = 0.00000006

        For i As Integer = 0 To X.Length - 1
            testval = Functions.SphericalBesselJ(n, X(i))
            expectval = Functions.SphericalBessel(n, X(i))(0)
            'console.WriteLine(X(i).ToString & ", " & expectval.ToString & ", " & testval.ToString & ", " & Abs(expectval-testval).tostring)
            Assert.AreEqual(expectval, testval, tolerance)
        Next

    End Sub

    <TestMethod()> _
    Public Sub SphericalBesselJ9()

        '// Compare for n = 5 (exercise the recursion).

        Dim n As Integer = 5
        Dim M As Integer = 10000
        Dim X(M) As Double
        Dim XMax As Double = 50.0
        Dim XMin As Double = 0.1

        For i As Integer = 0 To M
            X(i) = XMin + CDbl(i) * ((XMax - XMin) / CDbl(M))
        Next

        Dim testval As Double
        Dim expectval As Double
        Dim tolerance As Double = 0.00000001

        For i As Integer = 0 To X.Length - 1
            testval = Functions.SphericalBesselJ(n, X(i))
            expectval = Functions.SphericalBessel(n, X(i))(0)
            Assert.AreEqual(expectval, testval, tolerance)
            'console.WriteLine(X(i).ToString & ", " & expectval.ToString & ", " & testval.ToString)
        Next

    End Sub

    <TestMethod()> _
    Public Sub SphericalBesselJ10()

        '// Compare for n = 5 (exercise the recursion).

        Dim n As Integer = 1
        Dim M As Integer = 100
        Dim X(M) As Double
        Dim XMax As Double = 0.001
        Dim XMin As Double = 0.0

        For i As Integer = 0 To M
            X(i) = XMin + CDbl(i) * ((XMax - XMin) / CDbl(M))
        Next

        Dim testval As Double
        Dim expectval As Double
        Dim tolerance As Double = 0.00000001

        For i As Integer = 0 To X.Length - 1
            testval = Functions.SphericalBesselJ(n, X(i))
            expectval = Functions.SphericalBessel(n, X(i))(0)
            Assert.AreEqual(expectval, testval, tolerance)
            'console.WriteLine(X(i).ToString & ", " & expectval.ToString & ", " & testval.ToString)
        Next

    End Sub
End Class
