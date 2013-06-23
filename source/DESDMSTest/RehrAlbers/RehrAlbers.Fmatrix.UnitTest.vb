
Imports DESD.Math
Imports DESD

<TestClass> _
Partial Public Class RehrAlbers_

    <TestMethod()> _
    Public Sub Fmatrix_Test1()

        Dim mu() As Integer = {0}
        Dim nu() As Integer = {0}
        Dim muprime() As Integer = {0}
        Dim nuprime() As Integer = {0}

        Dim k As Double = 1.8842
        Dim rho As New Vector(0, 0, 7.5589 * k)
        Dim rhoprime As New Vector(2.5653 * k, -2.5653 * k, 2.5653 * k)

        Dim magrho As Double = rho.Magnitude
        Dim magrhoprime As Double = rhoprime.Magnitude

        '// Set up the t-matrix:
        'Dim delta() As Double = {-0.4004146, _
        '                          0.6112068, _
        '                          1.329133, _
        '                          0.3146167, _
        '                          0.04819505, _
        '                          0.005902354, _
        '                          0.0005629561, _
        '                          0.00004174712, _
        '                          0.000002449426, _
        '                          0.0000001161656}
        Dim delta() As Double = {-0.4004146, _
                                  0.6112068}

        Dim tmatrix(delta.Length - 1) As Complex

        'Dim expecttmvalues_Re() As Double = {-0.358966776709757, _
        '                                      0.469963021771605, _
        '                                      0.232363669888565, _
        '                                      0.294262575501045, _
        '                                      0.0481204542144784, _
        '                                      0.00590221691767092, _
        '                                      0.000562955981058804, _
        '                                      0.0000417471199514948, _
        '                                      0.0000024494259999902, _
        '                                      0.000000116165599999999}
        Dim expecttmvalues_Re() As Double = {-0.358966776709757, _
                                              0.469963021771605}

        Dim maxULPs As Long = 250

        For i As Integer = 0 To delta.Length - 1
            tmatrix(i) = System.Math.Sin(delta(i)) * Complex.CExp(0.0, delta(i))
            Assert.IsTrue(TestUtilities.AreEqual(tmatrix(i).Real, expecttmvalues_Re(i), maxULPs))

        Next

        'Dim expectvalues_Re() As Double = {0.319195260806378}
        'Dim expectvalues_Im() As Double = {-0.184988316565414}
        Dim expectvalues_Re() As Double = {0.340027882401079}
        Dim expectvalues_Im() As Double = {0.87192640421321}


        Dim testval As Complex

        Dim expectval_re As Double
        Dim expectval_im As Double

        For i As Integer = 0 To mu.Length - 1

            testval = RehrAlbers.F(mu(i), nu(i), muprime(i), nuprime(i), rho, rhoprime, tmatrix)

            expectval_re = expectvalues_Re(i)
            expectval_im = expectvalues_Im(i)

            Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
            Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))

        Next


    End Sub


    <TestMethod()> _
    Public Sub Fmatrix_Test2()

        Dim mu() As Integer = {0}
        Dim nu() As Integer = {0}
        Dim muprime() As Integer = {0}
        Dim nuprime() As Integer = {0}

        Dim k As Double = 1.8842
        Dim rho As New Vector(0, 0, 7.5589 * k)
        Dim rhoprime As New Vector(2.5653 * k, -2.5653 * k, 2.5653 * k)

        Dim magrho As Double = rho.Magnitude
        Dim magrhoprime As Double = rhoprime.Magnitude

        '// Set up the t-matrix:
        Dim delta() As Double = {-0.4004146, 0.0}

        Dim tmatrix(delta.Length - 1) As Complex

        Dim expecttmvalues_Re() As Double = {-0.358966776709757, 0.0}

        Dim maxULPs As Long = 250

        For i As Integer = 0 To delta.Length - 1
            tmatrix(i) = System.Math.Sin(delta(i)) * Complex.CExp(0.0, delta(i))
            Assert.IsTrue(TestUtilities.AreEqual(tmatrix(i).Real, expecttmvalues_Re(i), maxULPs))

        Next

        Dim expectvalues_Re() As Double = {-0.358966776709757}
        Dim expectvalues_Im() As Double = {0.151944180886733}


        Dim testval As Complex

        Dim expectval_re As Double
        Dim expectval_im As Double

        For i As Integer = 0 To mu.Length - 1

            testval = RehrAlbers.F(mu(i), nu(i), muprime(i), nuprime(i), rho, rhoprime, tmatrix)

            expectval_re = expectvalues_Re(i)
            expectval_im = expectvalues_Im(i)

            Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
            Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))

        Next


    End Sub


    <TestMethod()> _
    Public Sub Fmatrix_Test3()

        Dim mu() As Integer = {0}
        Dim nu() As Integer = {0}
        Dim muprime() As Integer = {0}
        Dim nuprime() As Integer = {0}

        Dim k As Double = 1.8842
        Dim rho As New Vector(0, 0, 7.5589 * k)
        Dim rhoprime As New Vector(2.5653 * k, -2.5653 * k, 2.5653 * k)

        Dim magrho As Double = rho.Magnitude
        Dim magrhoprime As Double = rhoprime.Magnitude

        '// Set up the t-matrix:
        Dim delta() As Double = {0.0, 0.6112068}

        Dim tmatrix(delta.Length - 1) As Complex
        For i As Integer = 0 To delta.Length - 1
            tmatrix(i) = System.Math.Sin(delta(i)) * Complex.CExp(0.0, delta(i))
        Next


        Dim maxULPs As Long = 500


        Dim expectvalues_Re() As Double = {0.698994659110836}
        Dim expectvalues_Im() As Double = {0.719982223326477}


        Dim testval As Complex

        Dim expectval_re As Double
        Dim expectval_im As Double

        For i As Integer = 0 To mu.Length - 1

            testval = RehrAlbers.F(mu(i), nu(i), muprime(i), nuprime(i), rho, rhoprime, tmatrix)

            expectval_re = expectvalues_Re(i)
            expectval_im = expectvalues_Im(i)

            Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
            Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))

        Next


    End Sub

    '<Test()> _
    Public Sub Fmatrix_Test4()

        Dim mu() As Integer = {0, 0, 0}
        Dim nu() As Integer = {0, 0, 0}
        Dim muprime() As Integer = {0, -1, 1}
        Dim nuprime() As Integer = {0, 0, 0}

        Dim k As Double = 1.8842
        Dim rho As New Vector(0, 0, 7.5589 * k)
        Dim rhoprime As New Vector(2.5653 * k, -2.5653 * k, 2.5653 * k)

        Dim magrho As Double = rho.Magnitude
        Dim magrhoprime As Double = rhoprime.Magnitude

        '// Set up the t-matrix:
        Dim delta() As Double = {-0.4004146, _
                                  0.6112068, _
                                  1.329133, _
                                  0.3146167, _
                                  0.04819505, _
                                  0.005902354, _
                                  0.0005629561, _
                                  0.00004174712, _
                                  0.000002449426, _
                                  0.0000001161656}

        Dim tmatrix(delta.Length - 1) As Complex

        Dim expecttmvalues_Re() As Double = {-0.358966776709757, _
                                              0.469963021771605, _
                                              0.232363669888565, _
                                              0.294262575501045, _
                                              0.0481204542144784, _
                                              0.00590221691767092, _
                                              0.000562955981058804, _
                                              0.0000417471199514948, _
                                              0.0000024494259999902, _
                                              0.000000116165599999999}

        Dim maxULPs As Long = 250

        For i As Integer = 0 To delta.Length - 1
            tmatrix(i) = System.Math.Sin(delta(i)) * Complex.CExp(0.0, delta(i))
            Assert.IsTrue(TestUtilities.AreEqual(tmatrix(i).Real, expecttmvalues_Re(i), maxULPs))
        Next

        Dim expectvalues_Re() As Double = {0.319195260806378, -0.763487481064857, 0.763487481064857}
        Dim expectvalues_Im() As Double = {-0.184988316565414, 9.20783943879568, -9.20783943879568}


        Dim testval As Complex

        Dim expectval_re As Double
        Dim expectval_im As Double

        For i As Integer = 0 To mu.Length - 1

            testval = RehrAlbers.F(mu(i), nu(i), muprime(i), nuprime(i), rho, rhoprime, tmatrix)

            expectval_re = expectvalues_Re(i)
            expectval_im = expectvalues_Im(i)

            Assert.IsTrue(TestUtilities.AreEqual(testval.Real, expectval_re, maxULPs))
            Assert.IsTrue(TestUtilities.AreEqual(testval.Imag, expectval_im, maxULPs))

        Next


    End Sub



End Class
