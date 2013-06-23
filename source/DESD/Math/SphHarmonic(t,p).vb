

Imports system.math
Namespace Math


    Partial Public Class Functions


        ''' <summary>
        ''' Returns the spherical harmonic Ylm(theta, phi) with Laplace normalization and the Condon-Shortley phase convention (included in the Plm).
        ''' </summary>
        ''' <param name="l"></param>
        ''' <param name="m"></param>
        ''' <param name="theta"></param>
        ''' <param name="phi"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function SphericalHarmonic(ByVal l As Integer, ByVal m As Integer, ByVal theta As Double, ByVal phi As Double) As Complex

            '// Trap some trivial solutions:
            If (l = 0) AndAlso (m = 0) Then Return New Complex(0.5 * Constants.InvSqrtPi, 0.0)
            If (l = 1) AndAlso (m = 0) Then Return New Complex(System.Math.Sqrt(0.75) * Constants.InvSqrtPi * System.Math.Cos(theta), 0.0)

            '// Compute the coefficient on the associated legendre polynomial:

            Dim temp1 As Double = Functions.FactorialLn(l - m)
            Dim temp2 As Double = Functions.FactorialLn(l + m)
            Dim temp3 As Double = System.Math.Exp(temp1 - temp2)
            Dim temp4 As Double = 0.5 * Constants.InvSqrtPi * System.Math.Sqrt(CDbl(2 * l + 1) * temp3)
            Dim temp5 As New Complex(System.Math.Cos(CDbl(m) * phi), System.Math.Sin(CDbl(m) * phi))

            Return temp4 * Legendre(l, m, Cos(theta)) * temp5

        End Function

        ''' <summary>
        ''' Returns the spherical harmonic Ylm(theta, phi) with Laplace normalization and the Condon-Shortley phase convention (included in the Plm).
        ''' </summary>
        ''' <param name="l"></param>
        ''' <param name="m"></param>
        ''' <param name="theta"></param>
        ''' <param name="phi"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        '    Public Shared Function SphericalHarmonic(ByVal l As Integer, ByVal m As Integer, ByVal theta As Double, ByVal phi As Double) As Complex
        '
        '        '// Trap some trivial solutions:
        '        If (l = 0) AndAlso (m = 0) Then Return New Complex(0.5 * Constants.InvSqrtPi, 0.0)
        '        If (l = 1) AndAlso (m = 0) Then Return New Complex(System.Math.Sqrt(0.75) * Constants.InvSqrtPi * System.Math.Cos(theta), 0.0)
        '
        '        '// Compute the coefficient on the associated legendre polynomial:
        '
        '        Dim temp1 As Double = Functions.FactorialLn(l - m)
        '        Dim temp2 As Double = Functions.FactorialLn(l + m)
        '        Dim temp3 as Double = 0.5*(System.Math.Log(2.0*cdbl(l) + 1.0) - System.Math.Log(4.0*System.Math.PI) + temp1 - temp2)
        '        Dim temp4 As Double = System.Math.Exp(temp3)
        '        'Dim temp4 As Double = 0.5 * Constants.InvSqrtPi * System.Math.Sqrt(CDbl(2 * l + 1) * temp3)
        '        'Dim temp5 as Complex = Complex.CExp(cdbl(m) * complex.i * phi)
        '        Dim temp5 As New Complex(System.Math.Cos(CDbl(m) * phi), System.Math.Sin(CDbl(m) * phi))
        '
        '        Return temp4 * Legendre(l, m, Cos(theta)) * temp5
        '
        '    End Function



    End Class

    'Public Enum SphericalHarmonicNormalization
    '	Laplace = 0
    '	LaplaceWithCondonShortley = 1
    'End Enum
End Namespace