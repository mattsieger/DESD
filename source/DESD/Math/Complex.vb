Imports System.ComponentModel
Imports System.Math


Namespace Math



    ''' <summary>
    ''' Encodes a complex value, with the real and imaginary parts represented
    ''' as double-precision floating-point values.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Structure Complex

        Implements IComparable(Of Complex)
        Implements IFormattable

        Private m_RealPart As Double
        Private m_ImagPart As Double


#Region "Constructors"


        '    Shared Sub New()
        '
        '    End Sub


        Public Sub New(ByVal realPart As Double, ByVal imaginaryPart As Double)
            m_RealPart = realPart
            m_ImagPart = imaginaryPart
        End Sub

#End Region


#Region "Properties"

        ''' <summary>
        ''' The real part of the imaginary number Re(Z).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Description("")> _
        Public Property Real() As Double
            Get
                Return m_RealPart
            End Get
            Set(ByVal value As Double)
                m_RealPart = value
            End Set
        End Property

        ''' <summary>
        ''' The imaginary part of the imaginary number Im(Z).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Description("")> _
        Public Property Imag() As Double
            Get
                Return m_ImagPart
            End Get
            Set(ByVal value As Double)
                m_ImagPart = value
            End Set
        End Property

#End Region


#Region "Instance Methods"

        ''' <summary>
        ''' Returns the length or magnitude of the complex vector.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Magnitude() As Double
            Return System.Math.Sqrt(Me.Real * Me.Real + Me.Imag * Me.Imag)
        End Function

        ''' <summary>
        ''' Returns the phase angle the complex number makes with the real axis.
        ''' Defined on the interval (-pi/2,pi/2].
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Argument() As Double
            Dim arg As Double = System.Math.Atan2(m_ImagPart, m_RealPart)
            '// Force -pi result onto the interval including +pi:
            If arg = -System.Math.PI / 2.0 Then arg = System.Math.PI / 2.0
            Return arg
        End Function

        ''' <summary>
        ''' Returns the complex conjugate of the complex number.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Conjugate() As Complex
            Return New Complex(Me.Real, -Me.Imag)
        End Function

        ''' <summary>
        ''' Returns the value of the complex number formatted as a string
        ''' Re + Im i
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String

            Dim sgn As String = " + "

            If m_ImagPart < 0.0R Then sgn = " - "

            Return m_RealPart.ToString & sgn & System.Math.Abs(m_ImagPart).ToString & "i"

        End Function


#End Region


#Region "Shared Methods"

        ''' <summary>
        ''' Returns a complex number from polar coordinates.
        ''' </summary>
        ''' <param name="r"></param>
        ''' <param name="phi"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FromPolar(ByVal r As Double, ByVal phi As Double) As Complex
            Return New Complex(r * System.Math.Cos(phi), r * System.Math.Sin(phi))
        End Function


        ''' <summary>
        ''' Returns e raised to the power z, where z is a complex number.
        ''' </summary>
        ''' <param name="z">A complex-valued exponent.</param>
        ''' <returns>Complex</returns>
        ''' <remarks></remarks>
        Public Shared Function CExp(ByVal z As Complex) As Complex

            Dim temp As Double = System.Math.Exp(z.Real)
            '		Dim realpart As Double = System.Math.Cos(z.Imag)
            '		Dim imagpart As Double = System.Math.Sin(z.Imag)
            '		If system.Math.abs(realpart) < 1.0E-15 Then realpart = 0.0
            '		if system.Math.Abs(imagpart) < 1.0E-15 Then imagpart = 0.0
            '        Dim temp2 As New Complex(realpart, imagpart)
            Dim temp2 As New Complex(System.Math.Cos(z.Imag), System.Math.Sin(z.Imag))

            Return temp * temp2

        End Function

        Shared Function CExp(ByVal realpart As Double, ByVal imagpart As Double) As Complex

            Dim temp As Double = System.Math.Exp(realpart)

            '		Dim realprt As Double = System.Math.Cos(imagpart)
            '		Dim imagprt As Double = System.Math.Sin(imagpart)
            '		If system.Math.abs(realprt) < 1.0E-15 Then realprt = 0.0
            '		if system.Math.Abs(imagprt) < 1.0E-15 Then imagprt = 0.0
            '        Dim temp2 As New Complex(realprt, imagprt)
            Dim temp2 As New Complex(System.Math.Cos(imagpart), System.Math.Sin(imagpart))

            Return temp * temp2

        End Function

        ''' <summary>
        ''' Returns the complex number z raised to the (real) power of y.
        ''' </summary>
        ''' <param name="z">A complex number</param>
        ''' <param name="y">A real number</param>
        ''' <returns></returns>
        ''' <remarks>This routine has some precison issues.</remarks>
        Public Shared Function CPow(ByVal z As Complex, ByVal y As Double) As Complex

            If y = 0.0 Then Return New Complex(1.0, 0.0)

            Return Complex.FromPolar(System.Math.Pow(z.Magnitude, y), y * z.Argument)

        End Function

        Public Shared Function CPow(ByVal z As Complex, ByVal n As Integer) As Complex

            If n = 0.0 Then Return New Complex(1.0, 0.0)

            If n < 0 Then z = 1.0 / z
            Dim retval As Complex = z

            For i As Integer = 2 To System.Math.Abs(n)
                retval *= z
            Next

            Return retval

        End Function


        '// Future Extension - an overload for complex exponents.
        'Overloads Shared Function CPow(ByVal z1 As Complex, ByVal z2 As Complex) As Complex

        '    Dim r As Double = z1.Magnitude
        '    Dim phi As Double = z1.Argument

        '    r = System.Math.Pow(r, x)
        '    phi = x * phi

        '    Return Complex.ConvertPolarForm(r, phi)

        'End Function


        ''' <summary>
        ''' Returns the real part of the complex number z.
        ''' </summary>
        ''' <param name="z">A complex number.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Re(ByVal z As Complex) As Double
            Return z.Real
        End Function


        ''' <summary>
        ''' Returns the imaginary part of the complex number z.
        ''' </summary>
        ''' <param name="z">A complex number.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Im(ByVal z As Complex) As Double
            Return z.Imag
        End Function


        ''' <summary>
        ''' Returns the absolute value (magnitude) of the complex number z.
        ''' </summary>
        ''' <param name="z"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CAbs(ByVal z As Complex) As Double

            Return System.Math.Sqrt(z.Real * z.Real + z.Imag * z.Imag)

        End Function


        ''' <summary>
        ''' Returns the complex conjugate of the complex number z.
        ''' </summary>
        ''' <param name="z"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Conjugate(ByVal z As Complex) As Complex
            Return New Complex(z.Real, -z.Imag)
        End Function

        ''' <summary>
        ''' Returns the principal value of the natural logarithm of z, lying on the interval (-pi/2,pi/2].
        ''' Note that CLn(0) is undefined.
        ''' </summary>
        ''' <param name="z"></param>
        ''' <returns></returns>
        Public Shared Function CLn(ByVal z As Complex) As Complex
            Return New Complex(Log(z.Magnitude), z.Argument)
        End Function


        Public Shared Function CSin(z As Complex) As Complex
            Return New Complex(Sin(z.Real) * Cosh(z.Imag), Cos(z.Real) * Sinh(z.Imag))
        End Function

        Public Shared Function CCos(z As Complex) As Complex
            Return New Complex(Cos(z.Real) * Cosh(z.Imag), -Sin(z.Real) * Sinh(z.Imag))
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="z"></param>
        ''' <returns></returns>
        ''' <remarks>http://en.wikipedia.org/wiki/Complex_square_root#Square_roots_of_negative_and_complex_numbers</remarks>
        Public Shared Function CSqrt(z As Complex) As Complex
            Dim realpart As Double = Sqrt(0.5 * (z.Magnitude + z.Real))
            Dim imagpart As Double = Sqrt(0.5 * (z.Magnitude - z.Real))
            Dim sign As Double = 1.0
            If z.Imag < 0.0 Then sign = -1.0

            Return New Complex(realpart, sign * imagpart)

        End Function


        Public Shared Function i() As Complex
            Return New Complex(0.0, 1.0)
        End Function

        Public Shared Function Arg(z As Complex) As Double
            '// x + iy = r*cos(arg) + i*r*sin(arg)
            Return z.Argument
        End Function

#End Region


#Region "Operator Definitions"

#Region "* operators"

        ''' <summary>
        ''' Provides an implementation for z1 * z2 = z3.
        ''' </summary>
        ''' <param name="z1">A complex number</param>
        ''' <param name="z2">A complex number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator *(ByVal z1 As Complex, ByVal z2 As Complex) As Complex
            Return New Complex( _
                z1.Real * z2.Real - z1.Imag * z2.Imag, _
                z1.Real * z2.Imag + z1.Imag * z2.Real)
        End Operator

        ''' <summary>
        ''' Provides an implementation for x * z = z1
        ''' </summary>
        ''' <param name="x">A pure real number</param>
        ''' <param name="z">A complex number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator *(ByVal x As Double, ByVal z As Complex) As Complex
            Return New Complex(x * z.Real, x * z.Imag)
        End Operator

        ''' <summary>
        ''' Provides an implementation for z * x = z1
        ''' </summary>
        ''' <param name="z">A complex number</param>
        ''' <param name="x">A pure real number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator *(ByVal z As Complex, ByVal x As Double) As Complex
            Return New Complex(x * z.Real, x * z.Imag)
        End Operator

#End Region

#Region "+ Operators"

        ''' <summary>
        ''' Returns the sum of two complex numbers z1 + z2 = z3
        ''' </summary>
        ''' <param name="z1">A complex number</param>
        ''' <param name="z2">A complex number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator +(ByVal z1 As Complex, ByVal z2 As Complex) As Complex
            Return New Complex(z1.Real + z2.Real, z1.Imag + z2.Imag)
        End Operator

        ''' <summary>
        ''' Returns the sum of a real and complex number x + z = z1
        ''' </summary>
        ''' <param name="x">A pure real number</param>
        ''' <param name="z">A complex number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator +(ByVal x As Double, ByVal z As Complex) As Complex
            Return New Complex(x + z.Real, z.Imag)
        End Operator

        ''' <summary>
        ''' Returns the sum of a complex and a real number z + x = z1
        ''' </summary>
        ''' <param name="z">A complex number</param>
        ''' <param name="x">A pure real number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator +(ByVal z As Complex, ByVal x As Double) As Complex
            Return New Complex(x + z.Real, z.Imag)
        End Operator

#End Region

#Region "- Operators"

        ''' <summary>
        ''' Returns the difference of two complex numbers z1 - z2 = z3
        ''' </summary>
        ''' <param name="z1">A complex number</param>
        ''' <param name="z2">A complex number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator -(ByVal z1 As Complex, ByVal z2 As Complex) As Complex
            Return New Complex(z1.Real - z2.Real, z1.Imag - z2.Imag)
        End Operator

        ''' <summary>
        ''' Returns the difference of a real number and a complex number x - z = z1
        ''' </summary>
        ''' <param name="x">A pure real number</param>
        ''' <param name="z">A complex number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator -(ByVal x As Double, ByVal z As Complex) As Complex
            Return New Complex(x - z.Real, -z.Imag)
        End Operator

        ''' <summary>
        ''' Returns the difference of a complex number and a real number z - x = z1
        ''' </summary>
        ''' <param name="z">A complex number</param>
        ''' <param name="x">A pure real number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator -(ByVal z As Complex, ByVal x As Double) As Complex
            Return New Complex(z.Real - x, z.Imag)
        End Operator

#End Region

#Region "/ Operators"

        ''' <summary>
        ''' Returns the quotient of two complex numbers z1 / z2 = z3
        ''' </summary>
        ''' <param name="z1">A complex number</param>
        ''' <param name="z2">A complex number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator /(ByVal z1 As Complex, ByVal z2 As Complex) As Complex

            Dim num As Double = z2.Real * z2.Real + z2.Imag * z2.Imag
            Return New Complex((z1.Real * z2.Real + z1.Imag * z2.Imag) / num, _
                               (z1.Imag * z2.Real - z1.Real * z2.Imag) / num)

        End Operator

        ''' <summary>
        ''' Returns the quotient of a complex number divided by a real number z / x = z1
        ''' </summary>
        ''' <param name="z">A complex number</param>
        ''' <param name="x">A pure real number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator /(ByVal z As Complex, ByVal x As Double) As Complex
            Return New Complex(z.Real / x, z.Imag / x)
        End Operator

        ''' <summary>
        ''' Returns a real number divided by a complex number x / z = z1
        ''' </summary>
        ''' <param name="x">A pure real number</param>
        ''' <param name="z">A complex number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator /(ByVal x As Double, ByVal z As Complex) As Complex

            Dim denom As Double = z.Real * z.Real + z.Imag * z.Imag
            Return New Complex(x * z.Real / denom, -x * z.Imag / denom)

        End Operator

        ''' <summary>
        ''' Returns a complex number divided by a real integer z / n = z1
        ''' </summary>
        ''' <param name="z">A complex number</param>
        ''' <param name="n">A real integer</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator /(ByVal z As Complex, ByVal n As Integer) As Complex
            Return New Complex(z.Real / CDbl(n), z.Imag / CDbl(n))
        End Operator

#End Region

#Region "= Operators"

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="z1">A complex number</param>
        ''' <param name="z2">A complex number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator =(ByVal z1 As Complex, ByVal z2 As Complex) As Boolean
            Return (z1.Real = z2.Real) AndAlso (z1.Imag = z2.Imag)
        End Operator

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="x">A real number</param>
        ''' <param name="z">A complex number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator =(ByVal x As Double, ByVal z As Complex) As Boolean
            Return (z.Real = x) AndAlso (z.Imag = 0.0R)
        End Operator

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="z">A complex number</param>
        ''' <param name="x">A real number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator =(ByVal z As Complex, ByVal x As Double) As Boolean
            Return (z.Real = x) AndAlso (z.Imag = 0.0R)
        End Operator

#End Region

#Region "<> Operators"

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="z1">A complex number</param>
        ''' <param name="z2">A complex number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator <>(ByVal z1 As Complex, ByVal z2 As Complex) As Boolean
            Return (z1.Real <> z2.Real) Or (z1.Imag <> z2.Imag)
        End Operator

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="z">A complex number</param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator <>(ByVal x As Double, ByVal z As Complex) As Boolean
            Return (z.Real <> x) Or (z.Imag <> 0.0R)
        End Operator

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="z">A complex number</param>
        ''' <param name="x"></param>
        ''' <returns>A complex number</returns>
        ''' <remarks></remarks>
        Public Shared Operator <>(ByVal z As Complex, ByVal x As Double) As Boolean
            Return (z.Real <> x) Or (z.Imag <> 0.0R)
        End Operator

#End Region

        Public Shared Operator <(ByVal z1 As Complex, ByVal z2 As Complex) As Boolean
            Return (z1.Magnitude < z2.Magnitude)
        End Operator

        Public Shared Operator >(ByVal z1 As Complex, ByVal z2 As Complex) As Boolean
            Return (z1.Magnitude > z2.Magnitude)
        End Operator

        Public Shared Operator <=(ByVal z1 As Complex, ByVal z2 As Complex) As Boolean
            Return (z1.Magnitude <= z2.Magnitude)
        End Operator

        Public Shared Operator >=(ByVal z1 As Complex, ByVal z2 As Complex) As Boolean
            Return (z1.Magnitude >= z2.Magnitude)
        End Operator

        Public Shared Operator ^(ByVal z As Complex, ByVal x As Double) As Complex
            Return CPow(z, x)
        End Operator

        Public Shared Operator ^(ByVal z As Complex, ByVal n As Integer) As Complex
            Return CPow(z, n)
        End Operator

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="z"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Operator -(ByVal z As Complex) As Complex
            Return New Complex(-z.Real, -z.Imag)
        End Operator

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="z"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Narrowing Operator CType(ByVal z As Complex) As Double
            Return z.Magnitude
        End Operator

        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean

            If Not (TypeOf (obj) Is Complex) Then Return False

            Dim z2 As Complex = DirectCast(obj, Complex)

            Return (Me.Real = z2.Real) AndAlso (Me.Imag = z2.Imag)

        End Function

#End Region


        Public Function CompareTo(ByVal other As Complex) As Integer Implements System.IComparable(Of Complex).CompareTo

            Dim z As New Complex(Me.Real, Me.Imag)

            If (z = other) Then
                Return 0
            ElseIf z > other Then
                Return 1
            Else
                Return -1
            End If

        End Function

        '    Public Shared Function Parse() As Complex
        '
        '    End Function

        Public Overloads Function ToString(ByVal format As String, ByVal formatProvider As System.IFormatProvider) As String Implements System.IFormattable.ToString

            ' If no format specifier is passed, display like this: (x, y).
            If String.IsNullOrEmpty(format) Then
                If m_ImagPart < 0.0R Then
                    Return String.Format("{0} - {1}i", m_RealPart, -m_ImagPart)
                Else
                    Return String.Format("{0} + {1}i", m_RealPart, m_ImagPart)
                End If
            End If

            '' For "x" formatting, return just the x value as a string
            'If format.ToLower = "x" Then _
            '    Return x.ToString()

            '' For "y" formatting, return just the y value as a string
            'If format.ToLower = "y" Then _
            '    Return y.ToString()

            ' For any unrecognized format, throw an exception.
            Throw New FormatException(String.Format("Invalid format string: '{0}'.", format))

        End Function

    End Structure





End Namespace