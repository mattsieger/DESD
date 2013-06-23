'Imports Math.Functions
Imports System.Math
Imports DESD.Math


Partial Public Class RehrAlbers

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="mu"></param>
    ''' <param name="nu"></param>
    ''' <param name="muprime"></param>
    ''' <param name="nuprime"></param>
    ''' <param name="v"></param>
    ''' <param name="vprime"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function F(ByVal mu As Integer, ByVal nu As Integer, ByVal muprime As Integer, ByVal nuprime As Integer, ByVal v As Vector, ByVal vprime As Vector, ByVal tmatrix As Complex()) As Complex

        Dim angles() As Double = Vector.EulerAngles(v, vprime)

        Dim temp As Complex = Complex.CExp(New Complex(0.0, -angles(0) * mu))
        temp *= Complex.CExp(New Complex(0.0, -angles(2) * muprime))
        temp *= LF(mu, nu, muprime, nuprime, v.Magnitude, vprime.Magnitude, angles(1), tmatrix)

        Return temp

    End Function


    Public Shared Function F(ByVal mu As Integer, ByVal nu As Integer, ByVal muprime As Integer, ByVal nuprime As Integer, k as Complex, ByVal r As Vector, ByVal rprime As Vector, ByVal tmatrix As Complex()) As Complex

        Dim angles() As Double = Vector.EulerAngles(r, rprime)

        Dim temp As Complex = Complex.CExp(New Complex(0.0, -angles(0) * mu))
        temp *= Complex.CExp(New Complex(0.0, -angles(2) * muprime))
        temp *= LF(mu, nu, muprime, nuprime, k, r.Magnitude, rprime.Magnitude, angles(1), tmatrix)

        Return temp

    End Function


    ''' <summary>
    ''' The lower-case f matrix.
    ''' </summary>
    ''' <param name="mu"></param>
    ''' <param name="nu"></param>
    ''' <param name="muprime"></param>
    ''' <param name="nuprime"></param>
    ''' <param name="rho"></param>
    ''' <param name="rhoprime"></param>
    ''' <param name="beta"></param>
    ''' <param name="tmatrix"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function LF(ByVal mu As Integer, ByVal nu As Integer, ByVal muprime As Integer, ByVal nuprime As Integer, ByVal rho As Double, ByVal rhoprime As Double, ByVal beta As Double, ByVal tmatrix As Complex()) As Complex


        '// Enter the sum over angular momentum (l,m)
        '// The number of L values to use depends on the dimension of the input t-matrix.

        Dim lmax As Integer = tmatrix.Length - 1
        Dim sum As New Complex

        For l As Integer = 0 To lmax
            sum += tmatrix(l) * LGamma(l, mu, nu, rho) * Functions.LRotm(l, mu, muprime, beta) * LGammaTwid(l, muprime, nuprime, rhoprime)
        Next

        Return sum

    End Function


    Public Shared Function LF(ByVal mu As Integer, ByVal nu As Integer, ByVal muprime As Integer, ByVal nuprime As Integer, ByVal rho As Complex, ByVal rhoprime As Complex, ByVal beta As Double, ByVal tmatrix As Complex()) As Complex


        '// Enter the sum over angular momentum (l,m)
        '// The number of L values to use depends on the dimension of the input t-matrix.

        Dim lmax As Integer = tmatrix.Length - 1
        Dim sum As New Complex

        For l As Integer = 0 To lmax
            sum += tmatrix(l) * LGamma(l, mu, nu, rho) * Functions.LRotm(l, mu, muprime, beta) * LGammaTwid(l, muprime, nuprime, rhoprime)
        Next

        Return sum

    End Function

    Public Shared Function LF(ByVal mu As Integer, ByVal nu As Integer, ByVal muprime As Integer, ByVal nuprime As Integer, k as Complex, ByVal r as double, ByVal rprime As Double, ByVal beta As Double, ByVal tmatrix As Complex()) As Complex


        '// Enter the sum over angular momentum (l,m)
        '// The number of L values to use depends on the dimension of the input t-matrix.

        Dim lmax As Integer = tmatrix.Length - 1
        Dim sum As New Complex
        
        Dim rho As Complex = k * r
        Dim rhoprime as Complex = k * rprime

        For l As Integer = 0 To lmax
            sum += tmatrix(l) * LGamma(l, mu, nu, rho) * Functions.LRotm(l, mu, muprime, beta) * LGammaTwid(l, muprime, nuprime, rhoprime)
        Next

        Return sum

    End Function

'    Public Shared Function LF(ByVal mu As Integer, ByVal nu As Integer, ByVal muprime As Integer, ByVal nuprime As Integer, ByVal rho as ComplexVector, ByVal rhoprime As ComplexVector, ByVal beta As Double, ByVal tmatrix As Complex()) As Complex
'
'
'        '// Enter the sum over angular momentum (l,m)
'        '// The number of L values to use depends on the dimension of the input t-matrix.
'
'        Dim lmax As Integer = tmatrix.Length - 1
'        Dim sum As New Complex
'        
'        Dim rho As Complex = k * r
'        Dim rhoprime as Complex = k * rprime
'
'        For l As Integer = 0 To lmax
'            sum += tmatrix(l) * LGamma(l, mu, nu, rho.Magnitude) * Functions.LRotm(l, mu, muprime, beta) * LGammaTwid(l, muprime, nuprime, rhoprime)
'        Next
'
'        Return sum
'
'    End Function
'
'    Public Shared Function Fmatrix(ByVal v As Vector, ByVal vprime As Vector, ByVal tmatrix As Complex(), ByVal order As Integer) As ComplexMatrix
'
'        Dim mu() As Integer = {0, -1, 1, 0, -2, 2, -1, 1, -3, 3, 0, -2, 2, -4, 4}
'        Dim nu() As Integer = {0,  0, 0, 1,  0, 0,  1, 1,  0, 0, 2,  1, 1,  0, 0}
'
'        Dim imax As Integer
'        Select Case order
'            Case 0
'                imax = 0
'            Case 1
'                imax = 2
'            Case 2
'                imax = 5
'            Case 3
'                imax = 9
'            Case 4
'                imax = 14
'            Case Else
'                Throw New ArgumentOutOfRangeException("order", "must be positive and less than or equal to 4.")
'        End Select
'
'        '// Dimension the return array:
'        Dim retval(imax, imax) As Complex
'
'
'        For i As Integer = 0 To imax
'            For j As Integer = 0 To imax
'                retval(i, j) = F(mu(i), nu(i), mu(j), nu(j), v, vprime, tmatrix)
'            Next
'        Next
'
'        Return New ComplexMatrix(retval)
'
'    End Function

End Class
