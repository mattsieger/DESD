Imports system.math

Namespace Math


    Partial Public Class Functions


        ''' <summary>
        ''' Returns the matrix element for the spherical rotation matrix R(j)mm'(alpha,beta,gamma),
        ''' per Messiah, Quantum Mechanics, pp 1070.
        ''' </summary>
        ''' <param name="j"></param>
        ''' <param name="m"></param>
        ''' <param name="mprime"></param>
        ''' <param name="alpha"></param>
        ''' <param name="beta"></param>
        ''' <param name="gamma"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Rotm(ByVal j As Double, ByVal m As Double, ByVal mprime As Double, ByVal alpha As Double, ByVal beta As Double, ByVal gamma As Double) As Complex

            Dim term1 As Complex = Complex.CExp(0.0, -alpha * CDbl(m))
            Dim term2 As Double = LRotm(CInt(j), CInt(m), CInt(mprime), beta)
            Dim term3 As Complex = Complex.CExp(0.0, -gamma * CDbl(mprime))

            Return term1 * term2 * term3

        End Function


    End Class
End Namespace