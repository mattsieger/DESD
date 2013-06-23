Imports System.Math
Namespace Math


    Partial Public Class Functions

        ''' <summary>
        ''' Returns the spherical bessel functions j_n(x), y_n(x) and their derivatives for real argument x.
        ''' </summary>
        ''' <param name="n"></param>
        ''' <param name="x"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function SphericalBessel(ByVal n As Integer, ByVal x As Double) As Double()
            '// Compute from relation to fractional order bessel functions:
            Dim sqrtpiover2 As Double = Sqrt(PI / 2.0)
            Dim sqrtx As Double = Sqrt(x)

            Dim retval(3) As Double


            If n = 0 Then
                If x = 0.0 Then
                    retval(0) = 1.0
                    retval(1) = Double.NegativeInfinity
                    retval(2) = 0.0
                    retval(3) = Double.PositiveInfinity
                Else
                    retval(0) = Sin(x) / x
                    retval(1) = -Cos(x) / x
                    retval(2) = (1.0 / x) * (-retval(0) + Cos(x))
                    retval(3) = (1.0 / x) * (-retval(1) + Sin(x))
                End If
                Return retval
            End If

            If x = 0.0 Then
                retval(0) = 0.0
                retval(1) = Double.NegativeInfinity
                retval(2) = 0.0
                retval(3) = Double.PositiveInfinity
                Return retval
            End If

            Dim fb As Double() = FractionalBessel(CDbl(n) + 0.5, x)
            retval(0) = sqrtpiover2 * fb(0) / sqrtx
            retval(1) = sqrtpiover2 * fb(1) / sqrtx
            retval(2) = (-1.0 / (2.0 * x)) * retval(0) + sqrtpiover2 * fb(2) / sqrtx
            retval(3) = (-1.0 / (2.0 * x)) * retval(1) + sqrtpiover2 * fb(3) / sqrtx
            Return retval

        End Function

    End Class

End Namespace