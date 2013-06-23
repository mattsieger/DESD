Imports System.Math
Imports DESD.Math


Partial Public Class RehrAlbers


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="l"></param>
    ''' <param name="mu"></param>
    ''' <param name="nu"></param>
    ''' <param name="rho"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function LGammaTwid(ByVal l As Integer, ByVal mu As Integer, ByVal nu As Integer, ByVal rho As Double) As Complex
        '// LGamma ---------------------------------------------------
        '// RA lower-case gamma (gtype = 0) and gamma-twiddle (gtype = 1)
        '// functions

        '// l must be greater than zero:
        If l < 0 Then Throw New ArgumentOutOfRangeException("l", "must be greater than or equal to zero.")

        '// Take the absolute value of the mu parameter:
        Dim absmu As Integer = System.Math.Abs(mu)

        '// Do some checking on input values:
        '// abs(mu) must be less than or equal to l:
        'If absmu > l Then Throw New ArgumentOutOfRangeException("mu", "|mu| must be less than or equal to l.")
        If absmu > l Then Return New Complex

        '// nu must be between 0 and absmu:
        'If (nu < 0) Or (nu > absmu) Then Throw New ArgumentOutOfRangeException("nu", "must be positive and less than or equal to |mu|.")

        Dim temp1 As Double
        Dim temp2 As Double
        Dim temp3 As Double

        Dim dum1 As Complex
        Dim dum2 As Complex
        Dim dum3 As Complex

        Dim z As New Complex(0.0, -1.0 / rho)

        Dim myNLM As Double = RehrAlbers.Nlm(l, absmu)


        temp1 = CDbl(2 * l + 1)
        temp2 = myNLM * System.Math.Exp(Functions.FactorialLn(nu))
        temp3 = temp1 / temp2


        dum1 = Cnlz(nu, l, z)
        dum2 = Complex.CPow(z, nu)
        dum3 = dum1 * dum2

        Return temp3 * dum3

    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="l"></param>
    ''' <param name="mu"></param>
    ''' <param name="nu"></param>
    ''' <param name="rho"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function LGammaTwid(ByVal l As Integer, ByVal mu As Integer, ByVal nu As Integer, ByVal rho As Complex) As Complex
        '// LGamma ---------------------------------------------------
        '// RA lower-case gamma (gtype = 0) and gamma-twiddle (gtype = 1)
        '// functions

        '// l must be greater than zero:
        If l < 0 Then Throw New ArgumentOutOfRangeException("l", "must be greater than or equal to zero.")

        '// Take the absolute value of the mu parameter:
        Dim absmu As Integer = System.Math.Abs(mu)

        '// Do some checking on input values:
        '// abs(mu) must be less than or equal to l:
        'If absmu > l Then Throw New ArgumentOutOfRangeException("mu", "|mu| must be less than or equal to l.")
        If absmu > l Then Return New Complex

        '// nu must be between 0 and absmu:
        'If (nu < 0) Or (nu > absmu) Then Throw New ArgumentOutOfRangeException("nu", "must be positive and less than or equal to |mu|.")

        Dim temp1 As Double
        Dim temp2 As Double
        Dim temp3 As Double

        Dim dum1 As Complex
        Dim dum2 As Complex
        Dim dum3 As Complex

        'Dim z As New Complex(0.0, -1.0 / rho)
        Dim z as Complex = -Complex.i / rho

        Dim myNLM As Double = RehrAlbers.Nlm(l, absmu)


        temp1 = CDbl(2 * l + 1)
        temp2 = myNLM * System.Math.Exp(Functions.FactorialLn(nu))
        temp3 = temp1 / temp2


        dum1 = Cnlz(nu, l, z)
        dum2 = Complex.CPow(z, nu)
        dum3 = dum1 * dum2

        Return temp3 * dum3

    End Function



End Class
