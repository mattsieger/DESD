
Imports System.Math
Imports DESD.Math

<TestClass> _
Public Class Functions_FractionalBessel_UnitTest

    <TestMethod> _
    Public Sub FractionalBessel_Test1()

        Dim Nu() As Double = {0.0, 0.5, 0.5}
        Dim X() As Double = {0.1, 3.0, 1.0}

        Dim ExpectedJ() As Double = {0.99750156206604, 0.065008182877375781, 0.67139670714180311}
        Dim ExpectedY() As Double = {-1.5342386513503667, 0.45604882079463316, -0.4310988680183761}
        Dim ExpectedJPrime() As Double
        Dim ExpectedYPrime() As Double

        Dim testval As Double()
        Dim maxULPs As Long = 10
        Dim expectJval As Double
        Dim expectYval As Double
        Dim Jpercentdiff As Double
        Dim Ypercentdiff As Double
        'Dim tolerance as Double = 2.0
        For i As Integer = 0 To Nu.Length - 1
            testval = Functions.FractionalBessel(Nu(i), X(i))
            Jpercentdiff = Abs((ExpectedJ(i) - testval(0)) / ExpectedJ(i))
            Ypercentdiff = Abs((ExpectedY(i) - testval(1)) / ExpectedY(i))
            expectJval = ExpectedJ(i)
            expectYval = ExpectedY(i)
            '            Assert.LessOrEqual(Jpercentdiff,tolerance)
            '            Assert.Lessorequal(Ypercentdiff, tolerance)
            Assert.IsTrue(TestUtilities.AreEqual(expectJval, testval(0), maxULPs))
            Assert.IsTrue(TestUtilities.AreEqual(expectYval, testval(1), maxULPs))
        Next

    End Sub

    <TestMethod> _
    Public Sub FractionalBessel_TestJ0()

        Dim Nu As Double = 0.0
        Dim X() As Double = {0.0001, 0.001, 0.01, 0.1, 1.0, 5.0, 10.0, 100.0}

        Dim ExpectedJ() As Double = {0.9999999975, 0.99999975000001562, 0.99997500015624952, 0.99750156206604, _
        0.76519768655796661, -0.17759677131433829, -0.24593576445134835, 0.019985850304223122}

        Dim testval As Double
        Dim maxULPs As Long = 2000  '// Problem is for X = 100, 1000.  Looks like fractinal error is OK, but ULPS are off.
        Dim expectval As Double
        For i As Integer = 0 To X.Length - 1
            testval = Functions.FractionalBessel(Nu, X(i))(0)
            expectval = ExpectedJ(i)
            Assert.IsTrue(TestUtilities.AreEqual(expectval, testval, maxULPs))
        Next

    End Sub

    <TestMethod> _
    Public Sub FractionalBessel_TestY0()

        Dim Nu As Double = 0.0
        Dim X() As Double = {0.0001, 0.001, 0.01, 0.1, 1.0, 5.0, 10.0, 100.0}

        Dim Expected() As Double = {-5.9372890697093368, -4.4714166113759228, -3.0054556370836458, -1.5342386513503667, _
        0.088256964215676956, -0.30851762524903376, 0.055671167283599395, -0.077244313365083153}

        Dim testval As Double
        Dim maxULPs As Long = 90  '// Problem for X = 1000.  Looks like fractinal error is OK, but ULPS are off.
        Dim expectval As Double
        For i As Integer = 0 To X.Length - 1
            testval = Functions.FractionalBessel(Nu, X(i))(1)
            expectval = Expected(i)
            Assert.IsTrue(TestUtilities.AreEqual(expectval, testval, maxULPs))
        Next

    End Sub

End Class
