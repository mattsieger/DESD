
Imports System.Math
Imports DESD.Math.Functions



Partial Public Class RehrAlbers

    ''' <summary>
    ''' Returns the spherical harmonic normalization factors N(l)m for the reduced, 
    ''' dimensionless, z-axis propagator g.
    ''' By the definition of the gamma functions in Rehr and Albers, PRB 41, 8139 (1990),
    ''' Equations 9 and 10, 
    ''' this function is only ever called with m greater than or = 0.
    ''' </summary>
    ''' <param name="l">l must be greater than or equal to zero.</param>
    ''' <param name="m">Abs(m) must be less than or equal to l.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Nlm(ByVal l As Integer, ByVal m As Integer) As Double

        If l < 0 Then Throw New ArgumentOutOfRangeException("l", "must be greater than or equal to zero.")

        Dim absm As Integer = Abs(m)

        If absm > l Then Throw New ArgumentOutOfRangeException("m", "The absolute value of m must be less than or equal to l.")

        Dim temp1 As Double = FactorialLn(l - absm)
        Dim temp2 As Double = FactorialLn(l + absm)
        Dim temp3 As Double = System.Math.Log(CDbl(2 * l + 1))

        Return System.Math.Exp(0.5 * (temp1 + temp3 - temp2))

    End Function


End Class


