
Imports System.Math
Imports DESD.Math
Imports DESD


Namespace Rehr_Albers

    <TestClass> _
    Partial Public Class Gamma1

        <TestMethod()> _
        Public Sub Test1()

            Dim lvalues() As Integer = {2, 4, 2, 2}
            Dim mvalues() As Integer = {1, 3, -1, -2}
            Dim muvalues() As Integer = {1, 0, 1, 0}
            Dim nuvalues() As Integer = {0, 0, 0, 0}
            Dim rhovalues() As Double = {10.0, 10.0, 10.0, 10.0}
            Dim thetavalues() As Double = {0.3, DESD.Math.Constants.Pi / 7.0, 0.3, 0.3}
            Dim phivalues() As Double = {1.2, DESD.Math.Constants.Pi / 3.0, 1.2, 1.2}
            Dim expectvalues_re() As Double = {-0.244928391859825, 0.183020951809162, 0.0153012500775891, -0.061302970189541}
            Dim expectvalues_im() As Double = {0.0429016215509866, 0.292245766046744, 0.0097686323685937, -0.10480596251727}

            Dim maxULPs As Long = 250

            Dim testval As Complex

            Dim expectval_re As Double
            Dim expectval_im As Double

            For i As Integer = 0 To lvalues.Length - 1
                testval = RehrAlbers.Gamma1(lvalues(i), mvalues(i), muvalues(i), nuvalues(i), rhovalues(i), thetavalues(i), phivalues(i))
                expectval_re = expectvalues_re(i)
                expectval_im = expectvalues_im(i)
                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))
            Next


        End Sub


        <TestMethod()> _
        Public Sub Test2()

            Dim lvalues() As Integer = {2, 4}
            Dim mvalues() As Integer = {1, 3}
            Dim muvalues() As Integer = {1, 0}
            Dim nuvalues() As Integer = {0, 0}
            Dim rhovalues() As Double = {10.0, 10.0}
            Dim thetavalues() As Double = {0.3, DESD.Math.Constants.Pi / 7.0}
            Dim phivalues() As Double = {1.2, DESD.Math.Constants.Pi / 3.0}
            Dim expectvalues_re() As Double = {-0.244928391859825, 0.183020951809162}
            Dim expectvalues_im() As Double = {0.0429016215509866, 0.292245766046744}

            Dim maxULPs As Long = 250

            Dim testval As Complex

            Dim expectval_re As Double
            Dim expectval_im As Double
            'Dim v As New Vector

            For i As Integer = 0 To lvalues.Length - 1
                'v.SetValuesSpherical(rhovalues(i), thetavalues(i), phivalues(i))
                testval = RehrAlbers.Gamma1(lvalues(i), mvalues(i), muvalues(i), nuvalues(i), rhovalues(i), thetavalues(i), phivalues(i))
                expectval_re = expectvalues_re(i)
                expectval_im = expectvalues_im(i)
                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))
            Next


        End Sub


        ''' <summary>
        ''' Tests l = 0 and l = 1 combinations at fixed rho and (mu,nu) = (0,0)
        ''' </summary>
        ''' <remarks></remarks>
        <TestMethod()> _
        Public Sub Test3()

            Dim lvalues() As Integer = {0, 1, 1, 1}
            Dim mvalues() As Integer = {0, -1, 0, 1}
            Dim mu As Integer = 0
            Dim nu As Integer = 0

            Dim k As Double = 1.8842
            Dim rho As New Vector(0, 0, 7.5589 * k)
            Dim rhoprime As New Vector(2.5653 * k, -2.5653 * k, 2.5653 * k)

            Dim expectvalues_re() As Double = {1.0, 0.0, 1.73205080756888, 0.0}
            Dim expectvalues_im() As Double = {0.0, 0.0, 0.121611607175722, 0.0}

            Dim maxULPs As Long = 250

            Dim testval As Complex

            Dim expectval_re As Double
            Dim expectval_im As Double

            For i As Integer = 0 To lvalues.Length - 1
                testval = RehrAlbers.Gamma1(lvalues(i), mvalues(i), mu, nu, rho.R, rho.Theta, rho.phi)
                expectval_re = expectvalues_re(i)
                expectval_im = expectvalues_im(i)
                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))
            Next


        End Sub

        ''' <summary>
        ''' Tests combinations at l = 2, fixed rho and (mu,nu) = (0,0)
        ''' </summary>
        ''' <remarks></remarks>
        <TestMethod()> _
        Public Sub Test4()

            Dim lvalues() As Integer = {2, 2, 2, 2, 2}
            Dim mvalues() As Integer = {-2, -1, 0, 1, 2}
            Dim mu As Integer = 0
            Dim nu As Integer = 0

            Dim k As Double = 1.8842
            Dim rho As New Vector(0, 0, 7.5589 * k)
            Dim rhoprime As New Vector(2.5653 * k, -2.5653 * k, 2.5653 * k)

            Dim expectvalues_re() As Double = {0.0, 0.0, 2.20299791176682, 0.0, 0.0}
            Dim expectvalues_im() As Double = {0.0, 0.0, 0.470999729297089, 0.0, 0.0}

            Dim maxULPs As Long = 250

            Dim testval As Complex

            Dim expectval_re As Double
            Dim expectval_im As Double

            For i As Integer = 0 To lvalues.Length - 1
                testval = RehrAlbers.Gamma1(lvalues(i), mvalues(i), mu, nu, rho.R, rho.Theta, rho.phi)
                expectval_re = expectvalues_re(i)
                expectval_im = expectvalues_im(i)
                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))
            Next


        End Sub


        <TestMethod()> _
        Public Sub Test5()

            Dim lvalues() As Integer = {3, 3, 3, 3, 3, 3, 3}
            Dim mvalues() As Integer = {-3, -2, -1, 0, 1, 2, 3}
            Dim mu As Integer = 0
            Dim nu As Integer = 0

            Dim k As Double = 1.8842
            Dim rho As New Vector(0, 0, 7.5589 * k)
            Dim rhoprime As New Vector(2.5653 * k, -2.5653 * k, 2.5653 * k)

            Dim expectvalues_re() As Double = {0.0, 0.0, 0.0, 2.45010616375598, 0.0, 0.0, 0.0}
            Dim expectvalues_im() As Double = {0.0, 0.0, 0.0, 1.10085205677713, 0.0, 0.0, 0.0}

            Dim maxULPs As Long = 250

            Dim testval As Complex

            Dim expectval_re As Double
            Dim expectval_im As Double

            For i As Integer = 0 To lvalues.Length - 1
                testval = RehrAlbers.Gamma1(lvalues(i), mvalues(i), mu, nu, rho.R, rho.Theta, rho.phi)
                expectval_re = expectvalues_re(i)
                expectval_im = expectvalues_im(i)
                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))
            Next


        End Sub


        <TestMethod()> _
        Public Sub Test6()

            Dim lvalues() As Integer = {4, 4, 4, 4, 4, 4, 4, 4, 4}
            Dim mvalues() As Integer = {-4, -3, -2, -1, 0, 1, 2, 3, 4}
            Dim mu As Integer = 0
            Dim nu As Integer = 0

            Dim k As Double = 1.8842
            Dim rho As New Vector(0, 0, 7.5589 * k)
            Dim rhoprime As New Vector(2.5653 * k, -2.5653 * k, 2.5653 * k)

            Dim expectvalues_re() As Double = {0.0, 0.0, 0.0, 0.0, 2.34213316973929, 0.0, 0.0, 0.0, 0.0}
            Dim expectvalues_im() As Double = {0.0, 0.0, 0.0, 0.0, 1.99734288012811, 0.0, 0.0, 0.0, 0.0}

            Dim maxULPs As Long = 250

            Dim testval As Complex

            Dim expectval_re As Double
            Dim expectval_im As Double

            For i As Integer = 0 To lvalues.Length - 1
                testval = RehrAlbers.Gamma1(lvalues(i), mvalues(i), mu, nu, rho.R, rho.Theta, rho.phi)
                expectval_re = expectvalues_re(i)
                expectval_im = expectvalues_im(i)
                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))
            Next


        End Sub



        <TestMethod()> _
        Public Sub Test7()

            Dim lvalues() As Integer = {5, 6, 7, 8, 9, 10}
            Dim m As Integer = 0
            Dim mu As Integer = 0
            Dim nu As Integer = 0

            Dim k As Double = 1.8842
            Dim rho As New Vector(0, 0, 7.5589 * k)
            Dim rhoprime As New Vector(2.5653 * k, -2.5653 * k, 2.5653 * k)

            Dim expectvalues_re() As Double = {1.67601590551011, 0.28241870365335, -1.77617058700907, -3.9365901782165, -4.98062901169255, -3.40510409004115}
            Dim expectvalues_im() As Double = {3.01621818952483, 3.8077227117227, 3.79908542023917, 2.3628486180364, -0.691747444453697, -4.35914026271205}

            Dim maxULPs As Long = 500

            Dim testval As Complex

            Dim expectval_re As Double
            Dim expectval_im As Double

            For i As Integer = 0 To lvalues.Length - 2
                testval = RehrAlbers.Gamma1(lvalues(i), m, mu, nu, rho.R, rho.Theta, rho.phi)
                expectval_re = expectvalues_re(i)
                expectval_im = expectvalues_im(i)
                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))
            Next


        End Sub

        <TestMethod()> _
        Public Sub Gamma2_Test1()

            Dim lvalues() As Integer = {2, 4, 2, 2}
            Dim mvalues() As Integer = {1, 3, -1, -2}
            Dim muvalues() As Integer = {1, 0, 1, 0}
            Dim nuvalues() As Integer = {0, 0, 0, 0}
            Dim rhovalues() As Double = {10.0, 10.0, 10.0, 10.0}
            Dim thetavalues() As Double = {0.3, DESD.Math.Constants.Pi / 7.0, 0.3, 0.3}
            Dim phivalues() As Double = {1.2, DESD.Math.Constants.Pi / 3.0, 1.2, 1.2}
            Dim expectvalues_re() As Double = {-3.07759809981061, 0.183020951809162, -0.0255891907132942, -0.109767938228084}
            Dim expectvalues_im() As Double = {3.8786814528125708, 0.292245766046744, -0.360573883859449, 0.051897434140685}

            Dim maxULPs As Long = 250

            Dim testval As Complex

            Dim expectval_re As Double
            Dim expectval_im As Double

            For i As Integer = 0 To lvalues.Length - 1
                testval = RehrAlbers.Gamma2(lvalues(i), mvalues(i), muvalues(i), nuvalues(i), rhovalues(i), thetavalues(i), phivalues(i))
                expectval_re = expectvalues_re(i)
                expectval_im = expectvalues_im(i)
                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))
            Next


        End Sub


        <TestMethod()> _
        Public Sub Gamma2_Test2()

            Dim lvalues() As Integer = {2, 4}
            Dim mvalues() As Integer = {1, 3}
            Dim muvalues() As Integer = {1, 0}
            Dim nuvalues() As Integer = {0, 0}
            Dim rhovalues() As Double = {10.0, 10.0}
            Dim thetavalues() As Double = {0.3, DESD.Math.Constants.Pi / 7.0}
            Dim phivalues() As Double = {1.2, DESD.Math.Constants.Pi / 3.0}
            Dim expectvalues_re() As Double = {-3.07759809981061, 0.183020951809162}
            Dim expectvalues_im() As Double = {3.8786814528125708, 0.292245766046744}

            Dim maxULPs As Long = 250

            Dim testval As Complex

            Dim expectval_re As Double
            ' Dim v As New Vector
            Dim expectval_im As Double

            For i As Integer = 0 To lvalues.Length - 1
                'v.SetValuesSpherical(rhovalues(i), thetavalues(i), phivalues(i))
                testval = RehrAlbers.Gamma2(lvalues(i), mvalues(i), muvalues(i), nuvalues(i), rhovalues(i), thetavalues(i), phivalues(i))
                expectval_re = expectvalues_re(i)
                expectval_im = expectvalues_im(i)
                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))
            Next


        End Sub


        <TestMethod()> _
        Public Sub Gamma2_Test3()

            Dim lvalues() As Integer = {0, 1, 1, 1}
            Dim mvalues() As Integer = {0, -1, 0, 1}
            Dim mu As Integer = 0
            Dim nu As Integer = 0

            Dim k As Double = 1.8842
            Dim rho As New Vector(0, 0, 7.5589 * k)
            Dim rhoprime As New Vector(2.5653 * k, -2.5653 * k, 2.5653 * k)

            Dim expectvalues_re() As Double = {1.0, 0.791568363676196, 1.0, -0.622645198696899}
            Dim expectvalues_im() As Double = {0.0, -0.622645198696899, 0.119446715456355, -0.435914026271205}

            Dim maxULPs As Long = 250

            Dim testval As Complex

            Dim expectval_re As Double
            Dim expectval_im As Double

            For i As Integer = 0 To lvalues.Length - 1
                testval = RehrAlbers.Gamma2(lvalues(i), mvalues(i), mu, nu, rho.R, rho.Theta, rho.phi)
                expectval_re = expectvalues_re(i)
                expectval_im = expectvalues_im(i)
                Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
                Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))
            Next


        End Sub


        <TestMethod()> _
        Public Sub Gamma2L00lamda00()

            Dim expectval_re As Double = 1.0
            Dim expectval_im As Double = 0.0

            Dim testval As Complex = RehrAlbers.Gamma2(0, 0, 0, 0, 10.0, 1.0, 2.0)

            Assert.AreEqual(expectval_re, testval.Real, 0.000000000000001)
            Assert.AreEqual(expectval_im, testval.Imag, 0.000000000000001)

        End Sub

        <TestMethod()> _
        Public Sub Gamma1L00lamda00()

            Dim expectval_re As Double = 1.0
            Dim expectval_im As Double = 0.0

            Dim testval As Complex = RehrAlbers.Gamma1(0, 0, 0, 0, 10.0, 1.0, 2.0)

            Assert.AreEqual(expectval_re, testval.Real, 0.000000000000001)
            Assert.AreEqual(expectval_im, testval.Imag, 0.000000000000001)

        End Sub

        <TestMethod()> _
        Public Sub Gamma2L10lamda00()

            '			Dim expectval_re As Double = 1.0
            '			Dim expectval_im As Double = 0.0
            Dim expectval As Complex = Sqrt(3.0) * Cos(1.0) * (1.0 + 0.1 * complex.i)

            Dim testval As Complex = RehrAlbers.Gamma2(1, 0, 0, 0, 10.0, 1.0, 2.0)

            Assert.AreEqual(expectval.Real, testval.Real, 0.000000000000001)
            Assert.AreEqual(expectval.Imag, testval.Imag, 0.000000000000001)

        End Sub

        <TestMethod()> _
        Public Sub Gamma1L10lamda00()

            '			Dim expectval_re As Double = 1.0
            '			Dim expectval_im As Double = 0.0
            Dim expectval As Complex = Sqrt(3.0) * Cos(1.0) * (1.0 + 0.1 * complex.i)

            Dim testval As Complex = RehrAlbers.Gamma1(1, 0, 0, 0, 10.0, 1.0, 2.0)

            Assert.AreEqual(expectval.Real, testval.Real, 0.000000000000001)
            Assert.AreEqual(expectval.Imag, testval.Imag, 0.000000000000001)

        End Sub

        <TestMethod()> _
        Public Sub Gamma1L1m1lamda00()

            '			Dim expectval_re As Double = 1.0
            '			Dim expectval_im As Double = 0.0
            Dim expectval As Complex = Sqrt(1.5) * Sin(1.0) * complex.CExp(-2.0 * complex.i) * (1.0 + 0.1 * complex.i)

            Dim testval As Complex = RehrAlbers.Gamma1(1, -1, 0, 0, 10.0, 1.0, 2.0)

            Assert.AreEqual(expectval.Real, testval.Real, 0.000000000000001)
            Assert.AreEqual(expectval.Imag, testval.Imag, 0.000000000000001)

        End Sub

        <TestMethod()> _
        Public Sub Gamma2L1m1lamda00()

            '			Dim expectval_re As Double = 1.0
            '			Dim expectval_im As Double = 0.0
            Dim expectval As Complex = Sqrt(1.5) * Sin(1.0) * complex.CExp(2.0 * complex.i) * (1.0 + 0.1 * complex.i)

            Dim testval As Complex = RehrAlbers.Gamma2(1, -1, 0, 0, 10.0, 1.0, 2.0)

            Assert.AreEqual(expectval.Real, testval.Real, 0.000000000000001)
            Assert.AreEqual(expectval.Imag, testval.Imag, 0.000000000000001)

        End Sub

        <TestMethod()> _
        Public Sub Gamma1L11lamda00()

            '			Dim expectval_re As Double = 1.0
            '			Dim expectval_im As Double = 0.0
            Dim expectval As Complex = -Sqrt(1.5) * Sin(1.0) * complex.CExp(2.0 * complex.i) * (1.0 + 0.1 * complex.i)

            Dim testval As Complex = RehrAlbers.Gamma1(1, 1, 0, 0, 10.0, 1.0, 2.0)

            Assert.AreEqual(expectval.Real, testval.Real, 0.000000000000001)
            Assert.AreEqual(expectval.Imag, testval.Imag, 0.000000000000001)

        End Sub

        <TestMethod()> _
        Public Sub Gamma2L11lamda00()

            '			Dim expectval_re As Double = 1.0
            '			Dim expectval_im As Double = 0.0
            Dim expectval As Complex = -Sqrt(1.5) * Sin(1.0) * complex.CExp(-2.0 * complex.i) * (1.0 + 0.1 * complex.i)

            Dim testval As Complex = RehrAlbers.Gamma2(1, 1, 0, 0, 10.0, 1.0, 2.0)

            Assert.AreEqual(expectval.Real, testval.Real, 0.000000000000001)
            Assert.AreEqual(expectval.Imag, testval.Imag, 0.000000000000001)

        End Sub


    End Class


End Namespace
