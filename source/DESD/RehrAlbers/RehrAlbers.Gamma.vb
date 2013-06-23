'Imports Math.Functions
Imports DESD.Math
Imports System.Math


Partial Public Class RehrAlbers


	''' <summary>
	''' 
	''' </summary>
	''' <param name="l"></param>
	''' <param name="m"></param>
	''' <param name="mu"></param>
	''' <param name="nu"></param>
	''' <param name="rho"></param>
	''' <param name="theta"></param>
	''' <param name="phi"></param>
	''' <returns></returns>
    Public Shared Function Gamma1(ByVal l As Integer, ByVal m As Integer, ByVal mu As Integer, ByVal nu As Integer, ByVal rho As Double, ByVal theta As Double, ByVal phi As Double) As Complex

        '// Capital-gamma function
        Return Functions.Rotm(l, mu, m, 0.0, theta, Pi - phi) * LGamma(l, mu, nu, rho)

'        Dim alpha As Double = 0.0
'        Dim beta As Double = theta
'        Dim gamma As Double = system.Math.Pi - phi
'
'        Return Functions.Rotm(l, mu, m, alpha, beta, gamma) * LGamma(l, mu, nu, rho)

    End Function


	''' <summary>
	''' 
	''' </summary>
	''' <param name="l"></param>
	''' <param name="m"></param>
	''' <param name="mu"></param>
	''' <param name="nu"></param>
	''' <param name="rho"></param>
	''' <param name="theta"></param>
	''' <param name="phi"></param>
	''' <returns></returns>
    Public Shared Function Gamma1(ByVal l As Integer, ByVal m As Integer, ByVal mu As Integer, ByVal nu As Integer, ByVal rho As Complex, ByVal theta As Double, ByVal phi As Double) As Complex

        '// Capital-gamma function
        Return Functions.Rotm(l, mu, m, 0.0, theta, Pi - phi) * LGamma(l, mu, nu, rho)
        
        '// these for performance testing..
        'Return LGamma(l, mu, nu, rho)
        'Return Functions.Rotm(l, mu, m, 0.0, theta, Pi - phi)


'        Dim alpha As Double = 0.0
'        Dim beta As Double = theta
'        Dim gamma As Double = system.Math.Pi - phi
'
'        Return Functions.Rotm(l, mu, m, alpha, beta, gamma) * LGamma(l, mu, nu, rho)

    End Function


'	''' <summary>
'	''' 
'	''' </summary>
'	''' <param name="l"></param>
'	''' <param name="m"></param>
'	''' <param name="mu"></param>
'	''' <param name="nu"></param>
'	''' <param name="v"></param>
'	''' <returns></returns>
'    Public Shared Function Gamma1(ByVal l As Integer, ByVal m As Integer, ByVal mu As Integer, ByVal nu As Integer, ByVal v As Vector) As Complex
'
'        '// Capital-gamma function
'        Return Gamma1(l, m, mu, nu, v.R, v.Theta, v.Phi)
'
'    End Function



	''' <summary>
	''' 
	''' </summary>
	''' <param name="l"></param>
	''' <param name="m"></param>
	''' <param name="mu"></param>
	''' <param name="nu"></param>
	''' <param name="rho"></param>
	''' <param name="theta"></param>
	''' <param name="phi"></param>
	''' <returns></returns>
    Public Shared Function Gamma2(ByVal l As Integer, ByVal m As Integer, ByVal mu As Integer, ByVal nu As Integer, ByVal rho As Double, ByVal theta As Double, ByVal phi As Double) As Complex

        '// Capital-gamma-twiddle function
        Return Functions.Rotm(l, m, mu, phi - pi, -theta, 0.0) * LGammaTwid(l, mu, nu, rho)

'        Dim alpha As Double = 0.0
'        Dim beta As Double = theta
'        Dim gamma As Double = system.Math.Pi - phi
'
'        Return Functions.Rotm(l, m, mu, -gamma, -beta, -alpha) * LGammaTwid(l, mu, nu, rho)

    End Function


	''' <summary>
	''' 
	''' </summary>
	''' <param name="l"></param>
	''' <param name="m"></param>
	''' <param name="mu"></param>
	''' <param name="nu"></param>
	''' <param name="rho"></param>
	''' <param name="theta"></param>
	''' <param name="phi"></param>
	''' <returns></returns>
	''' <remarks>Messian (page 1073, bottom) remarks that alpha should be restricted to be in the interval 0 to 2*pi.
	''' However, here in this function call alpha is phi - pi, which can take on value less than zero.</remarks>
    Public Shared Function Gamma2(ByVal l As Integer, ByVal m As Integer, ByVal mu As Integer, ByVal nu As Integer, ByVal rho As Complex, ByVal theta As Double, ByVal phi As Double) As Complex

        '// Capital-gamma-twiddle function
        Return Functions.Rotm(l, m, mu, phi - pi, -theta, 0.0) * LGammaTwid(l, mu, nu, rho)

'        Dim alpha As Double = 0.0
'        Dim beta As Double = theta
'        Dim gamma As Double = system.Math.Pi - phi
'
'        Return Functions.Rotm(l, m, mu, -gamma, -beta, -alpha) * LGammaTwid(l, mu, nu, rho)

    End Function



'    Public Shared Function Gamma2(ByVal l As Integer, ByVal m As Integer, ByVal mu As Integer, ByVal nu As Integer, ByVal v As Vector) As Complex
'
'        '// Capital-gamma function
'        Return Gamma2(l, m, mu, nu, v.R, v.Theta, v.Phi)
'
'    End Function


End Class
