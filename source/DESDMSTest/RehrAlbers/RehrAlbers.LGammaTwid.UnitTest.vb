


Imports System.Math
Imports DESD.Math
Imports DESD

Namespace Rehr_Albers


    <TestClass()> _
    Partial Public Class LGammaTwid
        Dim maxULPs As Long = 1000

        <TestMethod()> _
        Public Sub LGammaTwid000()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            '            Dim Z as Complex

            Dim expectval As New Complex(1.0, 0.0)
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(0, 0, 0, Rho)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid100()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(1, 0, 0, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = Sqrt(3.0) * (1.0 - Z)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub


        <TestMethod()> _
        Public Sub LGammaTwid110()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(1, 1, 0, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = Sqrt(6.0) * (1.0 - Z)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid101()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(1, 0, 1, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = -Sqrt(3.0) * Z
                'console.WriteLine(testval.ToString & " = " & expectval.tostring)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid200()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(2, 0, 0, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = Sqrt(5.0) * (1.0 - 3.0 * Z + 3.0 * Z ^ 2)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid210()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(2, 1, 0, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = Sqrt(30.0) * (1.0 - 3.0 * Z + 3.0 * Z ^ 2)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid201()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(2, 0, 1, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = -3.0 * Sqrt(5.0) * Z * (1.0 - 2.0 * Z)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid211()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(2, 1, 1, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = -3.0 * Sqrt(30.0) * Z * (1.0 - 2.0 * Z)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid220()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(2, 2, 0, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = 2.0 * Sqrt(30.0) * (1.0 - 3.0 * Z + 3.0 * Z ^ 2)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid202()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(2, 0, 2, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = 3.0 * Sqrt(5.0) * Z ^ 2
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid300()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(3, 0, 0, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = Sqrt(7.0) * (1.0 - 6.0 * Z + 15.0 * Z ^ 2 - 15.0 * Z ^ 3)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub


        <TestMethod()> _
        Public Sub LGammaTwid310()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(3, 1, 0, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = Sqrt(84.0) * (1.0 - 6.0 * Z + 15.0 * Z ^ 2 - 15.0 * Z ^ 3)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid301()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(3, 0, 1, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = -Sqrt(7.0) * Z * (6.0 - 30.0 * Z + 45.0 * Z ^ 2)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid311()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(3, 1, 1, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = -Sqrt(84.0) * Z * (6.0 - 30.0 * Z + 45.0 * Z ^ 2)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid320()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(3, 2, 0, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = Sqrt(840.0) * (1.0 - 6.0 * Z + 15.0 * Z ^ 2 - 15.0 * Z ^ 3)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid302()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(3, 0, 2, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = 15.0 * Sqrt(7.0) * Z ^ 2 * (1.0 - 3.0 * Z)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid321()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(3, 2, 1, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = -Sqrt(840.0) * Z * (6.0 - 30.0 * Z + 45.0 * Z ^ 2)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid312()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(3, 1, 2, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = 30.0 * Sqrt(21.0) * Z ^ 2 * (1.0 - 3.0 * Z)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid330()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(3, 3, 0, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = Sqrt(7.0 * 720.0) * (1.0 - 6.0 * Z + 15.0 * Z ^ 2 - 15.0 * Z ^ 3)
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub

        <TestMethod()> _
        Public Sub LGammaTwid303()

            Dim minRho As Double = 0.001
            Dim maxRho As Double = 1000.0
            Dim nRho As Integer = 10000
            Dim Rho As Double
            Dim Z As Complex

            Dim expectval As Complex
            Dim testval As Complex

            For i As Integer = 0 To nRho
                Rho = minRho + CDbl(i) * (maxRho + minRho) / nRho
                testval = RehrAlbers.LGammaTwid(3, 0, 3, Rho)
                Z = New Complex(0.0, -1.0 / Rho)
                expectval = -15.0 * Sqrt(7.0) * Z ^ 3
                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub
    End Class


End Namespace