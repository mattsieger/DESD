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
    Public Shared Function LGamma(ByVal l As Integer, ByVal mu As Integer, ByVal nu As Integer, ByVal rho As Double) As Complex
		Return LGamma(l,mu,nu,New Complex(rho,0.0))
		
        '// l must be greater than zero:
        'If l < 0 Then Throw New ArgumentOutOfRangeException("l", "must be greater than or equal to zero.")

        '// Take the absolute value of the mu parameter:
'        Dim absmu As Integer = System.Math.Abs(mu)
'
'        '// Do some checking on input values:
'        '// abs(mu) must be less than or equal to l:
'        'If absmu > l Then Throw New ArgumentOutOfRangeException("mu", "|mu| must be less than or equal to l.")
'        'If absmu > l Then Return New Complex
'
'        '// nu must be between 0 and absmu: (WHY?)
'        'If (nu < 0) Or (nu > absmu) Then Throw New ArgumentOutOfRangeException("nu", "must be positive and less than or equal to |mu|.")
'
'        '// Rho must be greater than zero:
'        'If (rho <= 0.0) Then Throw New ArgumentOutOfRangeException("rho", "must be greater than zero.")
'
'        '// mu + nu cannot be less than zero:
'        'If (absmu + nu) < 0 Then Throw New ArgumentOutOfRangeException("mu + nu", "must be greater than or equal to zero.")
'
'
'        Dim temp1 As Double
'
'        Dim dum1 As Complex
'
'        Dim z As New Complex(0.0, -1.0 / rho)
'
'        temp1 = Pow(-1.0, absmu) * RehrAlbers.Nlm(l, absmu) / Exp(Functions.FactorialLn(absmu + nu))
'
'        dum1 = Cnlz(absmu + nu, l, z) * z ^ (absmu + nu)
'
'        Return temp1 * dum1

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
    Public Shared Function LGamma_old(ByVal l As Integer, ByVal mu As Integer, ByVal nu As Integer, ByVal rho As Complex) As Complex

        '// l must be greater than zero:
        'If l < 0 Then Throw New ArgumentOutOfRangeException("l", "must be greater than or equal to zero.")

        '// Take the absolute value of the mu parameter:
        Dim absmu As Integer = System.Math.Abs(mu)

        '// Do some checking on input values:
        '// abs(mu) must be less than or equal to l:
        
        '// 2010.11.15 - Really?  Why?
        'If absmu > l Then Throw New ArgumentOutOfRangeException("mu", "|mu| must be less than or equal to l.")
        'If absmu > l Then Return New Complex
        If (absmu + nu) > l Then Return New Complex(0.0,0.0)
		If (l = 0) Then Return New Complex(1.0,0.0)
		
        '// nu must be between 0 and absmu: (WHY?)
        'If (nu < 0) Or (nu > absmu) Then Throw New ArgumentOutOfRangeException("nu", "must be positive and less than or equal to |mu|.")

        '// Rho must be greater than zero:
        'If (rho.Magnitude <= 0.0) Then Throw New ArgumentOutOfRangeException("rho", "The magnitude must be greater than zero.")

        '// mu + nu cannot be less than zero:
        'If (absmu + nu) < 0 Then Throw New ArgumentOutOfRangeException("absmu + nu", "must be greater than or equal to zero.")


        Dim temp1 As Double

        Dim dum1 As Complex

        'Dim z As New Complex(0.0, -1.0 / rho)
        Dim z as Complex = -Complex.i / rho

        temp1 = Pow(-1.0, absmu) * RehrAlbers.Nlm(l, absmu) / Exp(Functions.FactorialLn(absmu + nu))

        dum1 = Cnlz(absmu + nu, l, z) * z ^ (absmu + nu)

        Return temp1 * dum1

    End Function



    Public Shared Function LGamma(ByVal l As Integer, ByVal mu As Integer, ByVal nu As Integer, ByVal rho As Complex) As Complex

        '// Take the absolute value of the mu parameter:
        Dim absmu As Integer = System.Math.Abs(mu)
		
		'Console.WriteLine("l = " & l.ToString)
		
        If (absmu + nu) > l Then Return New Complex(0.0,0.0)

		If (l = 0) Then Return New Complex(1.0,0.0)
        
        Dim z as Complex = -Complex.i / rho
		
		If (l = 1) Then
			If (absmu = 0) Then
				If (nu = 0) Then Return sqrt(3.0)*(1.0-z)
				If (nu = 1) Then Return -sqrt(3.0)*z
			ElseIf (absmu = 1) Then
				Return sqrt(1.5)*z
			End If
		ElseIf (l = 2) Then
			If (absmu = 0) Then
				If (nu = 0) Then Return sqrt(5.0)*(1.0-3.0*z+3.0*z^2)
				If (nu = 1) Then Return sqrt(5.0)*(-3.0*z+6.0*z^2)
				If (nu = 2) Then Return 3.0*sqrt(5.0)*z^2
			ElseIf (absmu = 1) Then
				If (nu = 0) Then Return -sqrt(5.0/6.0)*(-3.0*z+6.0*z^2)
				If (nu = 1) then return -0.5*sqrt(30.0)*z^2
			ElseIf (absmu = 2) Then
				Return 0.25 * sqrt(30.0) *z^2
			End If
		ElseIf (l = 3) Then
			If (absmu = 0) Then
				If (nu = 0) Then Return sqrt(7.0)*(1.0-6.0*z+15.0*z^2-15.0*z^3)
				If (nu = 1) Then Return -sqrt(7.0)*(6.0*z-30.0*z^2+45.0*z^3)
				If (nu = 2) Then Return 15.0*sqrt(7.0)*z^2*(1.0-3.0*z)
				If (nu = 3) Then Return -15.0*sqrt(7.0)*z^3
			ElseIf (absmu = 1) Then
				If (nu = 0) Then Return sqrt(7.0/12.0)*(6.0*z - 30.0*z^2 + 45.0*z^3)
				If (nu = 1) Then Return -15.0*sqrt(7.0/12.0)*z^2*(1.0-3.0*z)
				If (nu = 2) Then Return 15.0*sqrt(7.0/12.0)*z^3
			ElseIf (absmu = 2) Then
				If (nu = 0) Then Return 15.0*sqrt(7.0/120.0)*z^2*(1.0-3.0*z)
				If (nu = 1) Then Return -15.0*sqrt(7.0/120.0)*z^3
			ElseIf (absmu = 3) Then
				Return 15.0*sqrt(7.0/720.0)*z^3
			End If
		End If
		
		Return LGamma_old(l,mu,nu,rho)
		
    End Function


'    Public Shared Function LGammaB(ByVal l As Integer, ByVal mu As Integer, ByVal nu As Integer, ByVal rho As Complex) As Complex
'
'        '// Take the absolute value of the mu parameter:
'        Dim absmu As Integer = System.Math.Abs(mu)
'			
'        If (absmu + nu) > l Then Return New Complex(0.0,0.0)
'
'		If (l = 0) Then Return New Complex(1.0,0.0)
'        
'		
'		Dim coeffs(,) as Double = {{1.73205080756888,-1.73205080756888,0,0} _
'		{0,	-1.73205080756888,	0,	0}
'{0,	1.22474487139159,	0,	0}
'{2.23606797749979,	-6.70820393249937,	6.70820393249937,	0}
'{0,	-6.70820393249937,	13.4164078649987,	0}
'{0,	0,	6.70820393249937,	0}
'{0,	2.73861278752583,	-5.47722557505166,	0}
'{0,	0,	-2.73861278752583,	0}
'{0,	0,	1.36930639376292,	0}
'{2.64575131106459,	-15.8745078663875,	39.6862696659689,	-39.6862696659689}
'{0,	-15.8745078663875,	79.3725393319377,	-119.058808997907}
'{0,	0,	39.6862696659689,	-119.058808997907}
'{0,	0,	0,	-39.6862696659689}
'{0,	4.58257569495584,	-22.9128784747792,	34.3693177121688}
'{0,	0,	-11.4564392373896,	34.3693177121688}
'{0,	0,	0,	11.4564392373896}
'{0,	0,	3.62284418654736,	-10.8685325596421}
'{0,	0,	0,	-3.62284418654736}
'{0,	0,	0,	1.47901994577490}}
'
'
'        Dim z as Complex = -Complex.i / rho
'
'		If l < 3 Then
'			
'			Dim temp as Complex = New Complex(0.0,0.0)
'			For i As Integer = (absmu + nu) To l
'				temp += coeffs(row,i) * z^i
'			Next
'			
'			Return temp
'			
'		Else
'			
'			Return LGamma_old(l,mu,nu,rho)
'			
'		End If
'		
'
'    End Function

End Class
