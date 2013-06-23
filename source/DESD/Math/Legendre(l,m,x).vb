
Imports system.math

Namespace Math


    Partial Public Class Functions



        ''' <summary>
        ''' Returns the associated Legendre polynomial Pl(m)(x).
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns>Double</returns>
        ''' <remarks>
        ''' Implements the algorithm taken from Numerical Recipes for Fortran.
        ''' That implementation includes the Condon-Shortley phase in Plm.
        ''' </remarks>
        ''' <example>
        '''</example>
        Public Shared Function Legendre(ByVal l As Integer, ByVal m As Integer, ByVal x As Double) As Double

            '// For negative values of m, apply the identity relation:
            If m < 0 Then
                Dim absm = System.Math.Abs(m)
                Dim coeff As Double = Pow(-1.0, absm) * Exp(FactorialLn(l - absm) - FactorialLn(l + absm))
                Return coeff * Legendre(l, absm, x)
            End If


            '// Trap some trivial solutions:
            If (l = 0) AndAlso (m = 0) Then Return 1.0
            If (l = 1) AndAlso (m = 0) Then Return x

            '// Apply symmetry relation to negative l values:
            If (l < 0) Then l = System.Math.Abs(l) - 1

            '// Check inputs for validity:
            '// |x| must be less than or equal to one:
            If (System.Math.Abs(x) > 1.0) Then Throw New ArgumentOutOfRangeException("x", "|x| must be less than or equal to 1.")
            '// |m| must be <= l.
            If (System.Math.Abs(m) > l) Then Throw New ArgumentOutOfRangeException("m", "|m| must be less than or equal to l.")



            Dim PMM As Double = 1.0
            Dim SOMX2 As Double
            Dim FACT As Double

            '// Does this work for -ve m values??
            If m > 0 Then
                SOMX2 = System.Math.Sqrt((1.0 - x) * (1.0 + x))
                FACT = 1.0
                For i As Integer = 1 To m
                    PMM = -PMM * FACT * SOMX2
                    FACT += 2.0
                Next
            End If

            Dim PMMP1 As Double
            Dim PLL As Double

            If (l = m) Then
                Return PMM
            Else
                PMMP1 = x * CDbl(2 * m + 1) * PMM
                If (l = m + 1) Then
                    Return PMMP1
                Else
                    For ll As Integer = m + 2 To l
                        PLL = (x * CDbl(2 * ll - 1) * PMMP1 - CDbl(ll + m - 1) * PMM) / CDbl(ll - m)
                        PMM = PMMP1
                        PMMP1 = PLL
                    Next
                    Return PLL
                End If
            End If

            Return 0.0

        End Function



    End Class
End Namespace