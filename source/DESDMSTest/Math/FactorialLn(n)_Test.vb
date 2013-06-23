
Imports System.Math
Imports DESD.Math


<TestClass> _
Partial Public Class Functions_


    ''' <summary>
    ''' Tests the FactorialLn(n) function at several values of x, a, and b and compares results
    ''' against expected values computed elsewhere.
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub FactorialLn()

        Dim testval As Double
        Dim expectval As Double

        '// Test values are taken from Excel:

        '1	0.0000000000000000000000000000
        '2	0.6931471805599450000000000000
        '3	1.7917594692280500000000000000
        '4	3.1780538303479500000000000000
        '5	4.7874917427820500000000000000
        '6	6.5792512120101000000000000000
        '7	8.5251613610654100000000000000
        '8	10.6046029027453000000000000000
        '9	12.8018274800815000000000000000
        '10	15.1044125730755000000000000000
        '11	17.5023078458739000000000000000
        '12	19.9872144956619000000000000000
        '13	22.5521638531234000000000000000
        '14	25.1912211827387000000000000000
        '15	27.8992713838409000000000000000
        '16	30.6718601060807000000000000000
        '17	33.5050734501369000000000000000
        '18	36.3954452080331000000000000000
        '19	39.3398841871995000000000000000
        '20	42.3356164607535000000000000000
        '21	45.3801388984769000000000000000
        '22	48.4711813518352000000000000000
        '23	51.6066755677644000000000000000
        '24	54.7847293981123000000000000000
        '25	58.0036052229805000000000000000
        '26	61.2617017610020000000000000000
        '27	64.5575386270063000000000000000
        '28	67.8897431371815000000000000000
        '29	71.2570389671680000000000000000
        '30	74.6582363488302000000000000000
        'TODO:  Revamp this test section

        testval = Functions.FactorialLn(0)
        expectval = 0.0R
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err15))

        testval = Functions.FactorialLn(1)
        expectval = 0.0R
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err15))

        testval = Functions.FactorialLn(2)
        expectval = 0.693147180559945R
        Assert.IsTrue(TestFractionalPrecision(testval, expectval, err15))
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err15))

        testval = Functions.FactorialLn(3)
        expectval = 1.79175946922805R
        Assert.IsTrue(TestFractionalPrecision(testval, expectval, err14))
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err14))

        testval = Functions.FactorialLn(4)
        expectval = 3.17805383034795R
        Assert.IsTrue(TestFractionalPrecision(testval, expectval, err14))
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err14))

        testval = Functions.FactorialLn(5)
        expectval = 4.78749174278205R
        Assert.IsTrue(TestFractionalPrecision(testval, expectval, err14))
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err14))

        testval = Functions.FactorialLn(6)
        expectval = 6.5792512120101R
        Assert.IsTrue(TestFractionalPrecision(testval, expectval, err14))
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err13))

        testval = Functions.FactorialLn(7)
        expectval = 8.52516136106541R
        Assert.IsTrue(TestFractionalPrecision(testval, expectval, err13))
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err13))

        testval = Functions.FactorialLn(8)
        expectval = 10.6046029027453R
        Assert.IsTrue(TestFractionalPrecision(testval, expectval, err13))
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err12))

        testval = Functions.FactorialLn(9)
        expectval = 12.8018274800815R
        Assert.IsTrue(TestFractionalPrecision(testval, expectval, err13))
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err12))

        testval = Functions.FactorialLn(10)
        expectval = 15.1044125730755R
        Assert.IsTrue(TestFractionalPrecision(testval, expectval, err13))
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err12))

        testval = Functions.FactorialLn(15)
        expectval = 27.8992713838409R
        Assert.IsTrue(TestFractionalPrecision(testval, expectval, err12))
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err11))

        testval = Functions.FactorialLn(20)
        expectval = 42.3356164607535R
        Assert.IsTrue(TestFractionalPrecision(testval, expectval, err15))
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err13))

        testval = Functions.FactorialLn(25)
        expectval = 58.0036052229805R
        Assert.IsTrue(TestFractionalPrecision(testval, expectval, err15))
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err13))

        testval = Functions.FactorialLn(30)
        expectval = 74.6582363488302R
        Assert.IsTrue(TestFractionalPrecision(testval, expectval, err15))
        Assert.IsTrue(TestAbsolutePrecision(testval, expectval, err13))


        '// Try some invalid values:
        Dim testflag As Boolean

        '// The following call should throw an argumentoutofrange exception:
        testflag = False
        Try
            testval = Functions.FactorialLn(-1)
        Catch oorex As ArgumentOutOfRangeException
            testflag = True
        Catch ex As Exception

        End Try
        Assert.IsTrue(testflag)

    End Sub



End Class
