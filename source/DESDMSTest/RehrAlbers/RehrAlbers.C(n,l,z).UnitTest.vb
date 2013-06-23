
Imports System.Math
Imports DESD.Math
Imports DESD

Namespace Rehr_Albers

    <TestClass> _
    Partial Public Class Cnlz

        <TestMethod()> _
        Public Sub Test1()

            Dim nvalues() As Integer = {0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2}
            Dim lvalues() As Integer = {0, 1, 2, 0, 1, 2, 3, 0, 1, 2, 3}

            Dim expectvals_re() As Double = {1.0, 0.0, 1.0, 0.0, -1.0, 3.0, -21.0, 0.0, 0.0, 6.0, -60.0}

            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval_re As Double
            Dim z As New Complex(1.0, 0.0)

            For i As Integer = 0 To nvalues.Length - 1
                testval = RehrAlbers.Cnlz(nvalues(i), lvalues(i), z)
                expectval_re = expectvals_re(i)


                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))

            Next

        End Sub


        <TestMethod()> _
        Public Sub Test2()

            Dim nvalues() As Integer = {0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3}
            Dim lvalues() As Integer = {0, 1, 2, 0, 1, 2, 3, 0, 1, 2, 3, 3}

            Dim expectvals_re() As Double = {1.0, 1.0, -2.0, 0.0, -1.0, -3.0, 39.0, 0.0, 0.0, 6.0, 30.0, -90.0}
            Dim expectvals_im() As Double = {0.0, -1.0, -3.0, 0.0, 0.0, 6.0, 30.0, 0.0, 0.0, 0.0, -90.0, 0.0}

            Dim maxULPs As Long = 10

            Dim testval As Complex
            Dim expectval_re As Double
            Dim expectval_im As Double
            Dim z As New Complex(0.0, 1.0)

            Dim q As Complex = 1.0 - z

            For i As Integer = 0 To nvalues.Length - 1
                testval = RehrAlbers.Cnlz(nvalues(i), lvalues(i), z)
                expectval_re = expectvals_re(i)
                expectval_im = expectvals_im(i)

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))

            Next

        End Sub



        <TestMethod()> _
        Public Sub Test3()

            Dim nvalues() As Integer = {0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3}
            Dim lvalues() As Integer = {0, 1, 2, 0, 1, 2, 3, 0, 1, 2, 3, 3}

            Dim expectvals_re() As Double = {1.0, -0.5, -12.62, 0.0, -1.0, 6.0, 175.8, 0.0, 0.0, 6.0, -105.0, -90.0}
            Dim expectvals_im() As Double = {0.0, -2.3, 13.8, 0.0, 0.0, 13.8, -241.5, 0.0, 0.0, 0.0, -207, 0.0}

            Dim maxULPs As Long = 10

            Dim testval As Complex
            Dim expectval_re As Double
            Dim expectval_im As Double
            Dim z As New Complex(1.5, 2.3)

            For i As Integer = 0 To nvalues.Length - 1
                testval = RehrAlbers.Cnlz(nvalues(i), lvalues(i), z)
                expectval_re = expectvals_re(i)
                expectval_im = expectvals_im(i)

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))

            Next

        End Sub

        <TestMethod()> _
        Public Sub TestC00()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As New Complex(1.0, 0.0)
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(0, 0, Z)

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub


        <TestMethod()> _
        Public Sub TestC10()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As New Complex(0.0, 0.0)
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(1, 0, Z)

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub

        <TestMethod()> _
        Public Sub TestC01()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As Complex
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1.0 / Rho)

                testval = RehrAlbers.Cnlz(0, 1, Z)
                expectval = 1.0 - Z

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub


        <TestMethod()> _
        Public Sub TestC01_2()

            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As Complex
            Dim Rho As Double = 1.0
            Z = New Complex(0.0, -1.0 / Rho)

            testval = RehrAlbers.Cnlz(0, 1, Z)
            expectval = 1.0 - Z

            Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
            Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))


        End Sub


        <TestMethod()> _
        Public Sub TestC11()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As New Complex(-1.0, 0.0)
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(1, 1, Z)

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub

        <TestMethod()> _
        Public Sub TestC21()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As New Complex(0.0, 0.0)
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(2, 1, Z)

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub

        <TestMethod()> _
        Public Sub TestC02()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As Complex
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(0, 2, Z)
                expectval = 1.0 - 3.0 * Z + 3.0 * Z ^ 2

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub

        <TestMethod()> _
        Public Sub TestC12()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As Complex
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(1, 2, Z)
                expectval = -3.0 + 6.0 * Z

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub

        <TestMethod()> _
        Public Sub TestC22()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As New Complex(6.0, 0.0)
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(2, 2, Z)

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub



        <TestMethod()> _
        Public Sub TestC32()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As New Complex(0.0, 0.0)
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(3, 2, Z)

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub



        <TestMethod()> _
        Public Sub TestC03()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As Complex
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(0, 3, Z)
                expectval = 1.0 - 6.0 * Z + 15.0 * Z ^ 2 - 15.0 * Z ^ 3

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub


        <TestMethod()> _
        Public Sub TestC13()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As Complex
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(1, 3, Z)
                expectval = -6.0 + 30.0 * Z - 45.0 * Z ^ 2

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub



        <TestMethod()> _
        Public Sub TestC23()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As Complex
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(2, 3, Z)
                expectval = 30.0 - 90.0 * Z

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub


        <TestMethod()> _
        Public Sub TestC33()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As New Complex(-90.0, 0.0)
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(3, 3, Z)

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub


        <TestMethod()> _
        Public Sub TestC43()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 1

            Dim testval As Complex
            Dim expectval As New Complex(0.0, 0.0)
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(4, 3, Z)

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub


        <TestMethod()> _
        Public Sub TestC04()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 10000

            Dim testval As Complex
            Dim expectval As Complex
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(0, 4, Z)
                expectval = 1.0 - 10.0 * Z + 45.0 * Z ^ 2 - 105.0 * Z ^ 3 + 105.0 * Z ^ 4
                '                console.WriteLine(testval.ToString & " = " & expectval.ToString)
                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub


        <TestMethod()> _
        Public Sub TestC14()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 10000

            Dim testval As Complex
            Dim expectval As Complex
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(1, 4, Z)
                expectval = -10.0 + 90.0 * Z - 315.0 * Z ^ 2 + 420.0 * Z ^ 3

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub


        <TestMethod()> _
        Public Sub TestC24()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 10000

            Dim testval As Complex
            Dim expectval As Complex
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(2, 4, Z)
                expectval = 90.0 - 630.0 * Z + 1260.0 * Z ^ 2

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub


        <TestMethod()> _
        Public Sub TestC34()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 10000

            Dim testval As Complex
            Dim expectval As Complex
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(3, 4, Z)
                expectval = -630.0 + 2520.0 * Z

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub


        <TestMethod()> _
        Public Sub TestC44()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 10000

            Dim testval As Complex
            Dim expectval As New Complex(2520.0, 0.0)
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(4, 4, Z)

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub


        <TestMethod()> _
        Public Sub TestC54()

            Dim Ntests As Integer = 10000
            Dim Rhomin As Double = 0.01
            Dim Rhomax As Double = 1000.0
            Dim DeltaRho As Double = (Rhomax - Rhomin) / CDbl(Ntests)


            Dim Z As Complex
            Dim maxULPs As Long = 10000

            Dim testval As Complex
            Dim expectval As New Complex(0.0, 0.0)
            Dim Rho As Double
            For i As Integer = 0 To Ntests - 1
                Rho = Rhomin + CDbl(i) * DeltaRho
                Z = New Complex(0.0, -1 / Rho)

                testval = RehrAlbers.Cnlz(5, 4, Z)

                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval.Real, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval.Imag, maxULPs))

            Next

        End Sub

        <TestMethod(), ExpectedException(GetType(System.ArgumentException))> _
        Public Sub TestErrorCondition()

            Dim z As Complex = RehrAlbers.Cnlz(-1, 1, New Complex(1.0, 0.0))

        End Sub
    End Class

End Namespace
