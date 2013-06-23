


Imports System.Math
Imports DESD.Math



Partial Public Class Functions_


    ''' <summary>
    ''' Tests the GammaLn(x) function at several values of x and compares results
    ''' against expected values computed elsewhere.
    ''' Test values taken from 
    ''' http://www.codecogs.com/d-ox/maths/special/gamma/log_gamma.php
    ''' 
    ''' Test results show the routine is good to 1 ULP.
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub GammaLn()

        '// Compare with test values taken from:
        '// 

        '// This routine is advertised to be accurate to machine precison:
        Dim XValues As Double() = {0.5R, _
                                   1.0R, _
                                   1.5R, _
                                   2.0R, _
                                   2.5R, _
                                   3.0R, _
                                   3.5R, _
                                   4.0R, _
                                   4.5R, _
                                   5.0R, _
                                   5.5R, _
                                   6.0R, _
                                   6.5R, _
                                   7.0R, _
                                   7.5R, _
                                   8.0R, _
                                   8.5R, _
                                   9.0R, _
                                   9.5R, _
                                   10.0R}


        Dim ExpectedValues As Double() = {0.5723649429247R, _
         0.0R, _
        -0.12078223763524526R, _
         0.0R, _
         0.28468287047291918R, _
         0.6931471805599454R, _
        1.2009736023470743R, _
        1.7917594692280552R, _
        2.4537365708424423R, _
        3.1780538303479453R, _
        3.9578139676187161R, _
        4.7874917427820449R, _
        5.6625620598571409R, _
        6.5792512120101012R, _
        7.5343642367587336R, _
        8.5251613610654129R, _
        9.5492672573009987R, _
        10.604602902745247R, _
        11.689333420797267R, _
        12.801827480081469R}


        Dim testval As Double
        Dim TestValues(XValues.Length - 1) As Double
        Dim expectval As Double
        Dim AbsDelta(XValues.Length - 1) As Double
        Dim AbsFract(XValues.Length - 1) As Double

        Dim maxULPs As Long = 1

        For i As Integer = 0 To XValues.Length - 1
            testval = Functions.GammaLn(XValues(i))
            TestValues(i) = testval
            expectval = ExpectedValues(i)
            Assert.IsTrue(TestUtilities.AreEqual(expectval, testval, maxULPs))
            AbsDelta(i) = System.Math.Abs(testval - expectval)
            AbsFract(i) = System.Math.Abs((testval - expectval) / expectval)
        Next

        '// Test for the infinite result:
        testval = Functions.GammaLn(0.0R)
        Assert.IsTrue(Double.IsPositiveInfinity(testval))

        '// Try some invalid values:
        Dim testflag As Boolean

        '// The following call should throw an argumentoutofrange exception:
        testflag = False
        Try
            testval = Functions.GammaLn(-0.5)
        Catch oorex As ArgumentOutOfRangeException
            testflag = True
        Catch ex As Exception

        End Try
        Assert.IsTrue(testflag)


    End Sub



End Class
