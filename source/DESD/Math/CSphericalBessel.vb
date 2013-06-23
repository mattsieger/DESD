Imports System.Math
Namespace Math


    Partial Public Class Functions

        Public Shared Function CSphericalBesselJ(ByVal n As Integer, ByVal z As Complex) As Complex

            If z = 0.0 Then
                If n = 0 Then
                    Return New Complex(1.0, 0.0)
                Else
                    Return New Complex(0.0, 0.0)
                End If
            End If

            If n = 0 Then
                Return complex.csin(z) / z
            ElseIf n = 1 Then
                If z.Magnitude < 0.001 Then
                    Return z / 3.0
                Else
                    Return (complex.csin(z) / z - complex.ccos(z)) / z
                End If
            Else
                If z.Magnitude < 0.0248 * n ^ 2 - 0.1148 * n + 0.1896 Then
                    Return z ^ n / doublefactorial(2 * n + 1)
                Else
                    Dim f0 As Complex = CSphericalBesselJ(n - 2, z)
                    Dim f1 As Complex = CSphericalBesselJ(n - 1, z)
                    Return CDbl(2 * n - 1) * f1 / z - f0
                End If
            End If



        End Function


        Public Shared Function SphericalBesselJ(ByVal n As Integer, ByVal x As Double) As Double

            If x = 0.0 Then
                If n = 0 Then
                    Return 1.0
                Else
                    Return 0.0
                End If
            End If

            If n = 0 Then
                Return Sin(x) / x
            ElseIf n = 1 Then
                If x < 0.001 Then
                    Return x / 3.0
                Else
                    Return (Sin(x) / x - Cos(x)) / x
                End If
            Else
                If x < 0.0248 * n ^ 2 - 0.1148 * n + 0.1896 Then
                    Return x ^ n / doublefactorial(2 * n + 1)
                Else
                    Dim f0 As Double = SphericalBesselJ(n - 2, x)
                    Dim f1 As Double = SphericalBesselJ(n - 1, x)
                    Return CDbl(2 * n - 1) * f1 / x - f0
                End If
            End If

        End Function

        '    Public Shared Function SphericalBesselJSeries(ByVal n As Integer, ByVal x As Double) As Double
        '	
        '        Dim x2over2 As Double = x^2 / 2.0
        '        Dim coeff As Double = x^n / DoubleFactorial(2*n + 1)
        '        Dim lastsum As Double = 0.0
        '        Dim sum As Double = 1.0
        '        Dim dbl2n as Double = 2.0 * cdbl(n)
        '        Dim term As Double = 1.0
        '        Dim b as Double = 0.0
        '        
        '        '// This seems like a crazy algorithm, but let's try it:
        '        Do Until sum = lastsum
        '        	lastsum = sum
        '        	'// Compute the next term in the sum:
        '        	b += 1.0
        '        	term *= -x2over2 / (b * (dbl2n + 2.0*b + 1.0))
        '        	sum += term
        '        Loop
        '        
        '        return coeff * sum
        '        
        '    End Function
    End Class

End Namespace