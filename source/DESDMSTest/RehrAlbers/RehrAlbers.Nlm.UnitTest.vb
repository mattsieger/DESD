
Imports System.Math
Imports DESD.Math
Imports DESD

Namespace Rehr_Albers

    <TestClass> _
    Partial Public Class Nlm


        <TestMethod(), ExpectedException(GetType(ArgumentOutOfRangeException))> _
        Public Sub TestErrorCondition1()

            Dim testval As Double = RehrAlbers.Nlm(2, 3)

        End Sub


        <TestMethod(), ExpectedException(GetType(ArgumentOutOfRangeException))> _
        Public Sub TestErrorCondition2()

            Dim testval As Double = RehrAlbers.Nlm(2, -3)

        End Sub



        <TestMethod()> _
        Public Sub TestNl0()

            Dim maxULPs As Long = 110

            Dim testval As Double
            Dim expectval As Double

            For i As Integer = 0 To 100
                testval = RehrAlbers.Nlm(i, 0)
                expectval = Sqrt(2.0 * CDbl(i) + 1.0)

                Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))
            Next

        End Sub


        <TestMethod()> _
        Public Sub TestN00()

            Dim maxULPs As Long = 110

            Dim testval As Double
            Dim expectval As Double

            testval = RehrAlbers.Nlm(0, 0)
            expectval = 1.0

            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        End Sub


        <TestMethod()> _
        Public Sub TestN10()

            Dim maxULPs As Long = 110

            Dim testval As Double
            Dim expectval As Double

            testval = RehrAlbers.Nlm(1, 0)
            expectval = Sqrt(3.0)

            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        End Sub



        <TestMethod()> _
        Public Sub TestN11()

            Dim maxULPs As Long = 110

            Dim testval As Double
            Dim expectval As Double

            testval = RehrAlbers.Nlm(1, 1)
            expectval = Sqrt(1.5)

            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        End Sub



        <TestMethod()> _
        Public Sub TestN20()

            Dim maxULPs As Long = 110

            Dim testval As Double
            Dim expectval As Double

            testval = RehrAlbers.Nlm(2, 0)
            expectval = Sqrt(5.0)

            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        End Sub


        <TestMethod()> _
        Public Sub TestN21()

            Dim maxULPs As Long = 110

            Dim testval As Double
            Dim expectval As Double

            testval = RehrAlbers.Nlm(2, 1)
            expectval = Sqrt(5.0 / 6.0)

            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        End Sub


        <TestMethod()> _
        Public Sub TestN22()

            Dim maxULPs As Long = 110

            Dim testval As Double
            Dim expectval As Double

            testval = RehrAlbers.Nlm(2, 2)
            expectval = Sqrt(5.0 / 24.0)

            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        End Sub



        <TestMethod()> _
        Public Sub TestN30()

            Dim maxULPs As Long = 110

            Dim testval As Double
            Dim expectval As Double

            testval = RehrAlbers.Nlm(3, 0)
            expectval = Sqrt(7.0)

            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        End Sub


        <TestMethod()> _
        Public Sub TestN31()

            Dim maxULPs As Long = 110

            Dim testval As Double
            Dim expectval As Double

            testval = RehrAlbers.Nlm(3, 1)
            expectval = Sqrt(7.0 / 12.0)

            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        End Sub


        <TestMethod()> _
        Public Sub TestN32()

            Dim maxULPs As Long = 110

            Dim testval As Double
            Dim expectval As Double

            testval = RehrAlbers.Nlm(3, 2)
            expectval = Sqrt(7.0 / 120.0)

            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        End Sub


        <TestMethod()> _
        Public Sub TestN33()

            Dim maxULPs As Long = 110

            Dim testval As Double
            Dim expectval As Double

            testval = RehrAlbers.Nlm(3, 3)
            expectval = Sqrt(7.0 / 720.0)

            Assert.IsTrue(TestUtilities.AreEqual(testval, expectval, maxULPs))

        End Sub


    End Class


End Namespace
