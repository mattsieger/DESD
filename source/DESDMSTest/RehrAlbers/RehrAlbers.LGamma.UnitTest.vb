
Imports System.Math
Imports DESD.Math
Imports DESD

Namespace Rehr_Albers


    <TestClass()> _
    Partial Public Class LGamma

        <TestMethod()> _
        Public Sub LGamma000()

            '// If mu, nu, and l all = 0, then LGamma = 1 for all rho. - test for 10k different rho.
            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double

            Dim expectval As New Complex(1.0, 0.0)
            Dim testval As Complex

            Dim maxULPs As Long = 1

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGamma(0, 0, 0, Rho)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        '        <Test()> _
        '        Public Sub LGamma000a()
        '
        '            '// If mu, nu, and l all = 0, then LGamma = 1 for all rho. - test for 10k different rho.
        '
        '            Dim expectval As New Complex(1.0, 0.0)
        '            'Dim testval As Complex = RehrAlbers.LGamma(0, 0, 0, 1.0)
        '            Dim testval As New Complex(0.0, 0.0)
        '
        '            Dim maxULPs As Long = 1
        '            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
        '
        '
        '        End Sub
        '
        <TestMethod()> _
        Public Sub LGamma100()

            '// If mu, nu = 0 and l = 1.
            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 20 '// 10.31.2009 -- TWO ulps!

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGamma(1, 0, 0, Rho)
                expectval = New Complex(Sqrt(3.0), Sqrt(3.0) / Rho)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub



        <TestMethod()> _
        Public Sub LGamma110()

            '// If mu = 1, nu = 0 and l = 1.
            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 75

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGamma(1, 1, 0, Rho)
                expectval = New Complex(0.0, -Sqrt(1.5) / Rho)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub


        <TestMethod()> _
        Public Sub LGamma101()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double

            Dim expectval As Complex
            Dim testval As Complex
            Dim Z As Complex

            Dim maxULPs As Long = 75

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGamma(1, 0, 1, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = -Sqrt(3.0) * Z
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGamma111()

            '// If mu = 1, nu = 1 and l = 1, LGamma should be zero.
            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double

            Dim expectval As New Complex(0.0, 0.0)
            Dim testval As Complex

            Dim maxULPs As Long = 100

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGamma(1, 1, 1, Rho)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGamma200()

            '// If mu = 0, nu = 0 and l = 2.
            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 100

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(2, 0, 0, Rho)
                expectval = Sqrt(5) * (1.0 - 3.0 * Z + 3.0 * Z ^ 2)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub


        <TestMethod()> _
        Public Sub LGamma210()

            '// If mu = 0, nu = 0 and l = 2.
            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 750

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(2, 1, 0, Rho)
                expectval = -Sqrt(5.0 / 6.0) * (-3.0 * Z + 6.0 * Z ^ 2)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub


        <TestMethod()> _
        Public Sub LGamma201()

            '// If mu = 0, nu = 0 and l = 2.
            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 7500

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(2, 0, 1, Rho)
                expectval = Sqrt(5.0) * (-3.0 * Z + 6.0 * Z ^ 2)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub



        <TestMethod()> _
        Public Sub LGamma211()

            '// If mu = 0, nu = 0 and l = 2.
            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 750

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(2, 1, 1, Rho)
                expectval = -Sqrt(30.0) / 2.0 * Z ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub


        <TestMethod()> _
        Public Sub LGamma220()

            '// If mu = 0, nu = 0 and l = 2.
            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 7500

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(2, 2, 0, Rho)
                expectval = Sqrt(30.0) / 4.0 * Z ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub


        <TestMethod()> _
        Public Sub LGamma202()

            '// If mu = 0, nu = 0 and l = 2.
            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 750

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(2, 0, 2, Rho)
                expectval = 3.0 * Sqrt(5.0) * Z ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub



        <TestMethod()> _
        Public Sub LGamma300()

            '// If mu = 0, nu = 0 and l = 2.
            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 750

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(3, 0, 0, Rho)
                expectval = Sqrt(7.0) * (1.0 - 6.0 * Z + 15.0 * Z ^ 2 - 15.0 * Z ^ 3)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub


        <TestMethod()> _
        Public Sub LGamma310()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 750

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(3, 1, 0, Rho)
                expectval = Sqrt(7.0 / 12.0) * (6.0 * Z - 30.0 * Z ^ 2 + 45.0 * Z ^ 3)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGamma301()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 750

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(3, 0, 1, Rho)
                expectval = -Sqrt(7.0) * (6.0 * Z - 30.0 * Z ^ 2 + 45.0 * Z ^ 3)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub


        <TestMethod()> _
        Public Sub LGamma311()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 750

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(3, 1, 1, Rho)
                expectval = -15.0 * Sqrt(7.0 / 12.0) * Z ^ 2 * (1.0 - 3.0 * Z)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub


        <TestMethod()> _
        Public Sub LGamma320()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 750

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(3, 2, 0, Rho)
                expectval = 15.0 * Sqrt(7.0 / 120.0) * Z ^ 2 * (1.0 - 3.0 * Z)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGamma321()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 750

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(3, 2, 1, Rho)
                expectval = -15.0 * Sqrt(7.0 / 120.0) * Z ^ 3
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGamma312()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 750

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(3, 1, 2, Rho)
                expectval = 15.0 * Sqrt(7.0 / 12.0) * Z ^ 3
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGamma302()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 750

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(3, 0, 2, Rho)
                expectval = 15.0 * Sqrt(7.0) * Z ^ 2 * (1.0 - 3.0 * Z)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGamma330()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 750

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(3, 3, 0, Rho)
                expectval = 15.0 * Sqrt(7.0 / 720.0) * Z ^ 3
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGamma303()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            Dim maxULPs As Long = 750

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.LGamma(3, 0, 3, Rho)
                expectval = -15.0 * Sqrt(7.0) * Z ^ 3
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub


    End Class


End Namespace