'Imports NUnit.Framework

'
' Created by SharpDevelop.
' User: Matt
' Date: 2/5/2012
' Time: 5:25 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Imports DESD.Math

<TestClass> _
Public Class PolynomialInterpolation_UnitTest

    <TestMethod()> _
    Public Sub LinearTest01()

        ' y = 3x + 1

        Dim xa As Double() = {0.0, 2.0}
        Dim ya As Double() = {1.0, 7.0}

        Dim expectval As Double = 4.0
        Dim testval As Double() = Functions.PolynomialInterpolation(xa, ya, 1.0)

        Dim tolerance As Double = 0.000001

        Assert.AreEqual(expectval, testval(0), tolerance)


    End Sub

    <TestMethod()> _
    Public Sub LinearTest02()

        ' y = 3x + 1

        Dim xa As Double() = {0.0, 2.0}
        Dim ya As Double() = {1.0, 7.0}

        Dim expectval As Double = 2.5
        Dim testval As Double() = Functions.PolynomialInterpolation(xa, ya, 0.5)

        Dim tolerance As Double = 0.000001

        Assert.AreEqual(expectval, testval(0), tolerance)


    End Sub

    <TestMethod()> _
    Public Sub QuadraticTest01()

        ' y = -2x^2 + 3x + 1

        Dim xa As Double() = {0.0, 2.0, 3.0}
        Dim ya As Double() = {1.0, -1.0, -8.0}

        Dim expectval As Double = 2.0
        Dim testval As Double() = Functions.PolynomialInterpolation(xa, ya, 1.0)

        Dim tolerance As Double = 0.000001

        Assert.AreEqual(expectval, testval(0), tolerance)


    End Sub

    <TestMethod()> _
    Public Sub QuadraticTest02()

        ' y = -2x^2 + 3x + 1

        Dim xa As Double() = {0.0, 2.0, 3.0}
        Dim ya As Double() = {1.0, -1.0, -8.0}

        Dim expectval As Double = 2.0
        Dim testval As Double() = Functions.PolynomialInterpolation(xa, ya, 0.5)

        Dim tolerance As Double = 0.000001

        Assert.AreEqual(expectval, testval(0), tolerance)


    End Sub

    <TestMethod()> _
    Public Sub CubicTest01()

        ' y = x^3 - 2x^2 + 3x + 1

        Dim xa As Double() = {0.0, 1.0, 3.0, 4.0}
        Dim ya As Double() = {1.0, 3.0, 19.0, 45.0}

        Dim expectval As Double = 7.0
        Dim testval As Double() = Functions.PolynomialInterpolation(xa, ya, 2.0)

        Dim tolerance As Double = 0.000001

        Assert.AreEqual(expectval, testval(0), tolerance)


    End Sub

    <TestMethod()> _
    Public Sub CubicTest02()

        ' y = x^3 - 2x^2 + 3x + 1

        Dim xa As Double() = {0.0, 0.5, 3.0, 6.0}
        Dim ya As Double() = {1.0, 2.125, 19.0, 163.0}

        Dim expectval As Double = 7.0
        Dim testval As Double() = Functions.PolynomialInterpolation(xa, ya, 2.0)

        Dim tolerance As Double = 0.000001

        Assert.AreEqual(expectval, testval(0), tolerance)


    End Sub


End Class
