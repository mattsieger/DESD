
Imports System.Math
Imports DESD.Math



Partial Public Class RehrAlbers


    ''' <summary>
    ''' Returns the nth derivative of the polynomial part of the spherical Hankel function
    ''' of complex argument z.
    ''' The first several Cn(z) (zeroth derivatives) are
    ''' C_0(z) = 1
    ''' C_1(z) = 1-z
    ''' C_2(z) = 1-3z+3z^2
    ''' C_3(z) = 1-6z+15z^2-15z^3
    ''' and the general recursion relation is:
    ''' C_(l+1)(z) = C_(l-1)(z)-(2*l+1)*z*C_l(z), for l >= 1
    ''' </summary>
    ''' <param name="n">The order of derivative</param>
    ''' <param name="l"></param>
    ''' <param name="z">Defined as 1/(i*rho)</param>
    ''' <returns></returns>
    ''' <remarks>This function is only ever used with n = 0, 1 or 2.</remarks>
    Public Shared Function Cnlz2(ByVal n As Integer, ByVal l As Integer, ByVal z As Complex) As Complex

        '// Do some basic validation on inputs:
        If n < 0 Then Throw New ArgumentException("n must be greater than or equal to zero.")

        '// Handle the trivial cases:
        If n > l Then Return New Complex() '// Returns 0

        Dim temp1 As Double
        Dim temp2 As Double
        Dim temp3 As Double
        Dim sum As New Complex()    '// Initializes to 0 + 0i
        Dim dbls As Double

        For s As Integer = 0 To l

            dbls = CDbl(s)

            temp1 = -(dbls * DESD.Math.Constants.Ln2 + Functions.FactorialLn(s)) + Functions.FactorialLn(l + s) - Functions.FactorialLn(l - s)
            temp2 = Pow(-1.0, CDbl(s)) * Exp(temp1)
            temp3 = 1.0
            If n > 0 Then
                For j As Integer = 0 To n - 1
                    temp3 *= (dbls - CDbl(j))
                Next
            End If

            '// 10.25.2009 - changed this line to use the operator ^ instead of CPow.
            'sum += temp2 * temp3 * Complex.CPow(z, CDbl(s - n))
            sum += temp2 * temp3 * z ^ (s - n)

        Next

        Return sum

    End Function


    ''' <summary>
    ''' Returns the nth derivative of the polynomial part of the spherical Hankel function
    ''' of complex argument z.
    ''' The first several Cn(z) (zeroth derivatives) are
    ''' C_0(z) = 1
    ''' C_1(z) = 1-z
    ''' C_2(z) = 1-3z+3z^2
    ''' C_3(z) = 1-6z+15z^2-15z^3
    ''' and the general recursion relation is:
    ''' C_(l+1)(z) = C_(l-1)(z)-(2*l+1)*z*C_l(z), for l >= 1
    ''' </summary>
    ''' <param name="n">The order of derivative</param>
    ''' <param name="l"></param>
    ''' <param name="z">Defined as 1/(i*rho)</param>
    ''' <returns></returns>
    ''' <remarks>This function is only ever used with n = 0, 1 or 2.</remarks>
    Public Shared Function Cnlz(ByVal n As Integer, ByVal l As Integer, ByVal z As Complex) As Complex

        '// Do some basic validation on inputs:
        If n < 0 Then Throw New ArgumentException("n must be greater than or equal to zero.")

        '// Handle the trivial cases:
        If n > l Then Return New Complex() '// Returns 0

        Select Case l
            Case 0
                Return New Complex(1.0, 0.0)
            Case 1
                If n = 0 Then
                    Return (1.0 - z)
                ElseIf n = 1 Then
                    Return New Complex(-1.0, 0.0)
                End If
            Case 2
                If n = 0 Then
                    Return (1.0 - 3.0 * z + 3.0 * z ^ 2)
                ElseIf n = 1 Then
                    Return (-3.0 + 6.0 * z)
                ElseIf n = 2 Then
                    Return New Complex(6.0, 0.0)
                End If
            Case 3
                If n = 0 Then
                    Return (1 - 6.0 * z + 15.0 * z ^ 2 - 15.0 * z ^ 3)
                ElseIf n = 1 Then
                    Return (-6.0 + 30.0 * z - 45.0 * z ^ 2)
                ElseIf n = 2 Then
                    Return (30.0 - 90.0 * z)
                ElseIf n = 3 Then
                    Return New Complex(-90.0, 0.0)
                End If
            Case Else
                Dim temp1 As Double
                Dim temp2 As Double
                Dim temp3 As Double
                Dim sum As New Complex()    '// Initializes to 0 + 0i
                Dim dbls As Double

                For s As Integer = 0 To l

                    dbls = CDbl(s)

                    temp1 = -(dbls * DESD.Math.Constants.Ln2 + Functions.FactorialLn(s)) + Functions.FactorialLn(l + s) - Functions.FactorialLn(l - s)
                    temp2 = Pow(-1.0, CDbl(s)) * Exp(temp1)
                    temp3 = 1.0
                    If n > 0 Then
                        For j As Integer = 0 To n - 1
                            temp3 *= (dbls - CDbl(j))
                        Next
                    End If

                    '// 10.25.2009 - changed this line to use the operator ^ instead of CPow.
                    'sum += temp2 * temp3 * Complex.CPow(z, CDbl(s - n))
                    sum += temp2 * temp3 * z ^ (s - n)

                Next

                Return sum

        End Select

    End Function


End Class
