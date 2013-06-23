
Imports DESD.Math


Partial Public Class Types_


    <TestMethod()> _
    Public Sub Vector_Test1()

        Dim k As Double = 1.8842
        Dim rho As New Vector(0, 0, 7.5589 * k)
        Dim rhoprime As New Vector(2.5653 * k, -2.5653 * k, 2.5653 * k)

        Dim maxULPs As Long = 250

        Dim testval As Double = rho.Magnitude
        Dim expectval As Double = 14.24247938
        Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        testval = rho.Theta
        expectval = 0.0
        Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        testval = rho.Phi
        expectval = 0.0
        Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))


        testval = rhoprime.Magnitude
        expectval = 8.37193384664807
        Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        testval = rhoprime.Theta
        expectval = 0.955316618124509
        Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        testval = rhoprime.Phi
        expectval = -0.785398163397448
        Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

    End Sub


End Class

