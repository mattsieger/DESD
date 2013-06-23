


Imports System.Math
Imports DESD.Math

Namespace Rehr_Albers

    <TestClass()> _
    Partial Public Class LRotm




        Dim maxULPs As Long = 10000000

        <TestMethod()> _
        Public Sub LRotm000()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double = 1.0
            Dim testval As Double

            For i As Integer = 0 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(0, 0, 0, Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub


        <TestMethod()> _
        Public Sub LRotm1m1m1()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 0 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(1, -1, -1, Beta)
                expectval = 0.5 * (1.0 + Cos(Beta))
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm1m10()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 0 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(1, -1, 0, Beta)
                expectval = Sqrt(0.5) * Sin(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm1m11()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 0 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(1, -1, 1, Beta)
                expectval = 0.5 * (1.0 - Cos(Beta))
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm10m1()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 0 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(1, 0, -1, Beta)
                expectval = -Sqrt(0.5) * Sin(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub


        <TestMethod()> _
        Public Sub LRotm100()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 0 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(1, 0, 0, Beta)
                expectval = Cos(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm101()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 0 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(1, 0, 1, Beta)
                expectval = Sqrt(0.5) * Sin(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm11m1()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 0 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(1, 1, -1, Beta)
                expectval = 0.5 * (1.0 - Cos(Beta))
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm110()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 0 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(1, 1, 0, Beta)
                expectval = -Sqrt(0.5) * Sin(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub




        <TestMethod()> _
        Public Sub LRotm111()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 0 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(1, 1, 1, Beta)
                expectval = 0.5 * (1.0 + Cos(Beta))
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub


        <TestMethod()> _
        Public Sub LRotm2m2m2()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, -2, -2, Beta)
                expectval = 0.25 * (1.0 + Cos(Beta)) ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm2m2m1()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, -2, -1, Beta)
                expectval = 0.5 * (1.0 + Cos(Beta)) * Sin(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm2m20()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, -2, 0, Beta)
                expectval = Sqrt(3.0 / 8.0) * Sin(Beta) ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm2m21()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, -2, 1, Beta)
                expectval = 0.5 * (1.0 - Cos(Beta)) * Sin(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub


        <TestMethod()> _
        Public Sub LRotm2m22()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, -2, 2, Beta)
                expectval = 0.25 * (1.0 - Cos(Beta)) ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub


        <TestMethod()> _
        Public Sub LRotm2m1m2()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, -1, -2, Beta)
                expectval = -0.5 * (1.0 + Cos(Beta)) * Sin(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub


        <TestMethod()> _
        Public Sub LRotm2m1m1()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, -1, -1, Beta)
                expectval = 0.25 * (1.0 + Cos(Beta)) ^ 2 - 0.75 * Sin(Beta) ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub


        <TestMethod()> _
        Public Sub LRotm2m10()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, -1, 0, Beta)
                expectval = 0.5 * Sqrt(6.0) * Cos(Beta) * Sin(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub


        <TestMethod()> _
        Public Sub LRotm2m11()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, -1, 1, Beta)
                expectval = -0.25 * (1.0 - Cos(Beta)) ^ 2 + 0.75 * Sin(Beta) ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub


        <TestMethod()> _
        Public Sub LRotm2m12()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, -1, 2, Beta)
                expectval = 0.5 * (1.0 - Cos(Beta)) * Sin(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm20m2()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 0, -2, Beta)
                expectval = 0.25 * Sqrt(6.0) * Sin(Beta) ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm20m1()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 0, -1, Beta)
                expectval = -0.5 * Sqrt(6.0) * Sin(Beta) * Cos(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm200()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double
            Dim eta As Double
            Dim nu As Double
            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 0, 0, Beta)
                eta = Cos(Beta / 2.0)
                nu = Sin(Beta / 2.0)
                expectval = eta ^ 4 + nu ^ 4 - 4.0 * eta ^ 2 * nu ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm201()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 0, 1, Beta)
                expectval = 0.5 * Sqrt(6.0) * Sin(Beta) * Cos(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm202()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 0, 2, Beta)
                expectval = 0.25 * Sqrt(6.0) * Sin(Beta) ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm21m2()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 1, -2, Beta)
                expectval = -0.5 * (1.0 - Cos(Beta)) * Sin(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm21m1()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 1, -1, Beta)
                expectval = -0.25 * (1.0 - Cos(Beta)) ^ 2 + 0.75 * Sin(Beta) ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm210()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 1, 0, Beta)
                expectval = -0.5 * Sqrt(6.0) * Sin(Beta) * Cos(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm211()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 1, 1, Beta)
                expectval = 0.25 * (1.0 + Cos(Beta)) ^ 2 - 0.75 * Sin(Beta) ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm212()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 1, 2, Beta)
                expectval = 0.5 * (1.0 + Cos(Beta)) * Sin(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm22m2()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 2, -2, Beta)
                expectval = 0.25 * (1.0 - Cos(Beta)) ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub


        <TestMethod()> _
        Public Sub LRotm22m1()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 2, -1, Beta)
                expectval = -0.5 * (1.0 - Cos(Beta)) * Sin(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm220()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 2, 0, Beta)
                expectval = 0.25 * Sqrt(6.0) * Sin(Beta) ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm221()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 2, 1, Beta)
                expectval = -0.5 * (1.0 + Cos(Beta)) * Sin(Beta)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        <TestMethod()> _
        Public Sub LRotm222()

            Dim minBeta As Double = -DESD.Math.Constants.Pi
            Dim maxBeta As Double = DESD.Math.Constants.Pi
            Dim nBeta As Integer = 10000
            Dim Beta As Double

            Dim expectval As Double
            Dim testval As Double

            For i As Integer = 1 To nBeta
                Beta = minBeta + CDbl(i) * (maxBeta - minBeta) / nBeta
                testval = Functions.LRotm(2, 2, 2, Beta)
                expectval = 0.25 * (1.0 + Cos(Beta)) ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs, 0.000000000000001), "Beta = " & Beta.ToString & ", test = " & testval.ToString & ", expect = " & expectval.ToString)
            Next

        End Sub

        '<TestMethod(), ExpectedException(GetType(ArgumentOutOfRangeException))> _
        'Public Sub InvalidMError()

        '    Dim testval As Double = Functions.LRotm(2, 3, 2, 1.0)

        'End Sub

        '<TestMethod(), ExpectedException(GetType(ArgumentOutOfRangeException))> _
        'Public Sub InvalidMPrimeError()

        '    Dim testval As Double = Functions.LRotm(2, 2, -3, 1.0)

        'End Sub


        '<TestMethod(), ExpectedException(GetType(ArgumentOutOfRangeException))> _
        'Public Sub InvalidJError()

        '    Dim testval As Double = Functions.LRotm(-1, -1, -1, 1.0)

        'End Sub

        <TestMethod()> _
        Public Sub LRotm333()

            Dim expectval As Double = 0.456801908504338R
            Dim testval As Double = Functions.LRotm(3, 3, 3, 1.0)
            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs), "Beta = 1.0, test = " & testval.ToString & ", expect = " & expectval.ToString)

        End Sub

        <TestMethod()> _
        Public Sub LRotm332()

            Dim expectval As Double = -0.611275113235054R
            Dim testval As Double = Functions.LRotm(3, 3, 2, 1.0)
            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs), "Beta = 1.0, test = " & testval.ToString & ", expect = " & expectval.ToString)

        End Sub

        <TestMethod()> _
        Public Sub LRotm331()

            Dim expectval As Double = 0.528007266006606R
            Dim testval As Double = Functions.LRotm(3, 3, 1, 1.0)
            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs), "Beta = 1.0, test = " & testval.ToString & ", expect = " & expectval.ToString)

        End Sub

        <TestMethod()> _
        Public Sub LRotm300()

            Dim expectval As Double = -0.416131945674726R
            Dim testval As Double = Functions.LRotm(3, 0, 0, 1.0)
            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs), "Beta = 1.0, test = " & testval.ToString & ", expect = " & expectval.ToString)

        End Sub


        <TestMethod()> _
        Public Sub LRotm5m31()

            Dim expectval As Double = 0.436445496374015R
            Dim testval As Double = Functions.LRotm(5, -3, 1, 1.0)
            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs), "Beta = 1.0, test = " & testval.ToString & ", expect = " & expectval.ToString)

        End Sub

    End Class


End Namespace
