
Imports DESD.Math


<TestClass()> _
Partial Public Class Types_


    <TestMethod()> _
    Public Sub Complex_Addition()

        Dim c1 As New Complex(1.0, 1.0)
        Dim c2 As New Complex(0.5, 0.5)

        Dim c3 As Complex = c1 + c2

        Assert.AreEqual(c3.Real, 1.5R)
        Assert.AreEqual(c3.Imag, 1.5R)

        Dim C4 As Complex

        C4 = 1.0R + c1

        Assert.AreEqual(C4.Real, 2.0R)
        Assert.AreEqual(C4.Imag, 1.0R)

        C4 = c1 + 1.0R

        Assert.AreEqual(C4.Real, 2.0R)
        Assert.AreEqual(C4.Imag, 1.0R)

    End Sub

    <TestMethod()> _
    Public Sub Complex_Multiplication()

        Dim c1 As New Complex(1.0, 1.0)
        Dim c2 As New Complex(0.5, 0.5)

        Dim c3 As Complex = c1 * c2

        Assert.AreEqual(c3.Real, 0.0R)

    End Sub

    <TestMethod()> _
    Public Sub Complex_Division()

        Dim c1 As New Complex(1.0, 0.5)
        Dim c2 As New Complex(2.0, 0.7)

        Dim c3 As Complex = c1 / c2

        Assert.AreEqual(c3.Real, 0.523385301, 0.000000001)
        Assert.AreEqual(c3.Imag, 0.066815145, 0.000000001)

        c3 = 1.0 / c2

        Assert.AreEqual(c3.Real, 2.0 / 4.49)
        Assert.AreEqual(c3.Imag, -0.7 / 4.49)

        c3 = 1 / c2

        Assert.AreEqual(c3.Real, 2.0 / 4.49)
        Assert.AreEqual(c3.Imag, -0.7 / 4.49)

        c3 = c2 / 3.0

        Assert.AreEqual(c3.Real, 2.0 / 3.0)
        Assert.AreEqual(c3.Imag, 0.7 / 3.0)

        c3 = c2 / 3

        Assert.AreEqual(c3.Real, 2.0 / 3.0)
        Assert.AreEqual(c3.Imag, 0.7 / 3.0)


        c3 = 1.0 / (1.0 / c2)

        Assert.AreEqual(c3.Real, c2.Real)
        Assert.AreEqual(c3.Imag, c2.Imag)


        c3 = 1.0 / (1.0 / c2 + 1.0 / c2)


        Assert.AreEqual(c3.Real, c2.Real / 2)
        Assert.AreEqual(c3.Imag, c2.Imag / 2)

        c2 = c2 / 2
        Assert.AreEqual(c2.Real, 1.0)
        Assert.AreEqual(c2.Imag, 0.35)

    End Sub

    <TestMethod()> _
    Public Sub Complex_Magnitude()

        Dim c1 As New Complex(2.0, 0.7)

        Assert.AreEqual(c1.Magnitude, System.Math.Sqrt(4.49))

    End Sub

    <TestMethod()> _
    Public Sub Complex_Argument1()

        Dim c1 As New Complex(1.0, 1.0)

        '// In this case, r = sqrt(2) and theta should be pi/4
        Assert.AreEqual(System.Math.Sqrt(2), c1.Magnitude, 0.0000000001)
        Assert.AreEqual(System.Math.PI / 4.0, c1.Argument, 0.0000000001)

    End Sub

    <TestMethod()> _
    Public Sub Complex_Argument2()

        Dim c1 As New Complex(1.0, 0.0)

        '// In this case, r = 1 and theta should be 0
        Assert.AreEqual(1.0, c1.Magnitude, 0.0000000001)
        Assert.AreEqual(0.0, c1.Argument, 0.0000000001)

    End Sub

    <TestMethod()> _
    Public Sub Complex_Argument3()

        Dim c1 As New Complex(1.0, -1.0)

        '// In this case, r = 1 and theta should be 0
        Assert.AreEqual(System.Math.Sqrt(2), c1.Magnitude, 0.0000000001)
        Assert.AreEqual(-System.Math.PI / 4.0, c1.Argument, 0.0000000001)

    End Sub

    <TestMethod()> _
    Public Sub Complex_Argument4()

        Dim c1 As New Complex(-1.0, 1.0)

        '// In this case, r = 1 and theta should be 0
        Assert.AreEqual(System.Math.Sqrt(2), c1.Magnitude, 0.0000000001)
        Assert.AreEqual(3.0 * System.Math.PI / 4.0, c1.Argument, 0.0000000001)

    End Sub

    <TestMethod()> _
    Public Sub Complex_Argument5()

        Dim c1 As New Complex(-1.0, -1.0)

        '// In this case, r = 1 and theta should be 0
        Assert.AreEqual(System.Math.Sqrt(2), c1.Magnitude, 0.0000000001)
        Assert.AreEqual(-3.0 * System.Math.PI / 4.0, c1.Argument, 0.0000000001)

    End Sub

    <TestMethod()> _
    Public Sub Complex_Argument6()

        Dim c1 As New Complex(0.0, -1.0)

        '// In this case, r = 1 and theta should be 0
        Assert.AreEqual(1.0, c1.Magnitude, 0.0000000001)
        Assert.AreEqual(System.Math.PI / 2.0, c1.Argument, 0.0000000001)

    End Sub

    <TestMethod()> _
    Public Sub Complex_Argument7()

        Dim c1 As New Complex(0.0, 1.0)

        '// In this case, r = 1 and theta should be 0
        Assert.AreEqual(1.0, c1.Magnitude, 0.0000000001)
        Assert.AreEqual(System.Math.PI / 2.0, c1.Argument, 0.0000000001)

    End Sub


    <TestMethod()> _
    Public Sub Complex_Ln1()

        Dim z As Complex = New Complex(1.0, 2.0)
        Dim lnz As Complex = Complex.CLn(z)

        Assert.AreEqual(0.80471895621705014, lnz.real, 0.0000000001)
        Assert.AreEqual(1.1071487177940904, lnz.imag, 0.0000000001)

    End Sub

    <TestMethod()> _
    Public Sub Complex_Ln2()

        Dim z As Complex = New Complex(1.0, -2.0)
        Dim lnz As Complex = Complex.CLn(z)

        Assert.AreEqual(0.80471895621705014, lnz.real, 0.0000000001)
        Assert.AreEqual(-1.1071487177940904, lnz.imag, 0.0000000001)

    End Sub

    '<TestMethod()> _
    '	Public Sub Complex_Power1()
    '		Dim Rho as Double = 0.01
    '		Dim Z As New Complex(0.0, -1 / Rho)
    '		
    '		Dim testVal As Complex = Z^4
    '		Dim testVal2 as Complex = complex.CPow(Z,4)
    '		
    '		Dim expectVal As Complex = New Complex(1.0/(Rho^4),0.0)
    '		
    ''		console.WriteLine(testval.ToString & " = " & expectval.ToString)
    ''		console.WriteLine(testval2.ToString & " = " & expectval.ToString)
    '
    '	End Sub

    <TestMethod()> _
    Public Sub Complex_Cexp01()

        Dim phi As Double = System.Math.PI / 2.0

        Dim testval As Complex = Complex.CExp(complex.i * phi)
        Dim expectval As Complex = Complex.i
        Dim delta As Double = 0.000000000000001

        Assert.AreEqual(expectval.Real, testval.real, delta)
        Assert.AreEqual(expectval.imag, testval.imag, delta)

    End Sub

    <TestMethod()> _
    Public Sub Complex_Cexp02()

        Dim phi As Double = -System.Math.PI / 2.0

        Dim testval As Complex = Complex.CExp(complex.i * phi)
        Dim expectval As Complex = -Complex.i
        Dim delta As Double = 0.000000000000001

        Assert.AreEqual(expectval.Real, testval.real, delta)
        Assert.AreEqual(expectval.imag, testval.imag, delta)

    End Sub

    <TestMethod()> _
    Public Sub Complex_Cexp03()

        Dim phi As Double = System.Math.PI / 2.0

        Dim testval As Complex = Complex.CExp(0.0, phi)
        Dim expectval As Complex = Complex.i
        Dim delta As Double = 0.000000000000001

        Assert.AreEqual(expectval.Real, testval.real, delta)
        Assert.AreEqual(expectval.imag, testval.imag, delta)

    End Sub

    <TestMethod()> _
    Public Sub Complex_Cexp04()

        Dim phi As Double = -System.Math.PI / 2.0

        Dim testval As Complex = Complex.CExp(0.0, phi)
        Dim expectval As Complex = -Complex.i
        Dim delta As Double = 0.000000000000001

        Assert.AreEqual(expectval.Real, testval.real, delta)
        Assert.AreEqual(expectval.imag, testval.imag, delta)

    End Sub


End Class

