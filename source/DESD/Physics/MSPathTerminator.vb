
'Public Class MSPathTerminator
'	
'	
'	
'	''' <summary>
'	''' The Termination matrix Q.
'	''' </summary>
'	''' <param name="R1"></param>
'	''' <param name="R2"></param>
'	''' <param name="k"></param>
'	''' <param name="khat"></param>
'	''' <param name="T"></param>
'	''' <param name="zv0"></param>
'	''' <param name="raorder"></param>
'	''' <returns></returns>
'	Public Shared Function Q(R1 As Vector, R2 As Vector, k As Complex, khat As Vector, T As Complex(), zv0 as Double, raorder As Integer) As ComplexMatrix
'		'// Compute the bond vectors:
'		Dim R12 As Vector = R1 - R2
'		
'		Dim theta12 As Double = R12.Theta
'		Dim phi12 As Double = R12.Phi
'		Dim rho12 As Complex = k * R12.R
'
'		Dim RAvalues As Integer(,) = MuNu(raorder)
'		Dim LMvalues As Integer(,) = LM(T.Length - 1)
'
'		Dim Retval(RAvalues.Length\2 - 1,0) As Complex
'		Dim Gamma1 As Complex
'		Dim Ylm as Complex
'		
'		Dim l As Integer
'		Dim m As Integer
'		Dim mu As Integer
'		Dim nu As Integer
'		
'		'// Compute the plane wave factor:
'		Dim eikdotr As Complex = Complex.CExp(complex.i * Vector.ScalarProduct(khat,R12))
'		
'		'// Compute the attenuation factor:
'		Dim attenuation as Complex = Complex.CExp(-k.Imag*(zv0-R2.Z)/cos(constants.Pi-khat.Theta))
'		
'		'// Perform the sums over L
'		For lamda As Integer = 0 To RAvalues.Length\2 - 1
'			mu = RAvalues(lamda,0)
'			nu = RAvalues(lamda,1)
'			
'			'// Clear the accumulator:
'			Retval(lamda,0) = New Complex()
'				
'			'// Begin sum over L-values:
'			For ilm As Integer = 0 To LMvalues.Length\2 - 1
'				l = LMvalues(ilm,0)
'				m = LMvalues(ilm,1)
'				Gamma1 = RehrAlbers.Gamma1(l,m,mu,nu,rho12,theta12,phi12)
'				Ylm = Functions.SphHarmonic(l,m,khat.Theta,khat.Phi).Conjugate 
'				Retval(lamda,0) += Gamma1 * T(l) * Ylm
'			Next
'		Next
'		
'		Dim temp As New ComplexMatrix(Retval)
'		
'		temp *= (attenuation * eikdotr * Complex.CExp(Complex.i * rho12) / (k * rho12))
'		
'		Return temp
'
'	End Function
'
'	
'	Public Shared Function MuNu(raorder As Integer) As Integer(,)
'		
'		Dim mu() As Integer = {0, -1, 1, 0, -2, 2, -1, 1, -3, 3, 0, -2, 2, -4, 4}
'        Dim nu() As Integer = {0,  0, 0, 1,  0, 0,  1, 1,  0, 0, 2,  1, 1,  0, 0}
'
'        Dim imax As Integer
'        Select Case raorder
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
'        Dim retval(imax,1) As Integer
'        
'        For i As Integer = 0 To imax
'        	retval(i,0) = mu(i)
'        	retval(i,1) = nu(i)
'        Next
'		
'		Return retval
'		
'	End Function
'	
'	Public Shared Function LM(lmax As Integer) As Integer(,)
'		
'		'// Determine the number of distinct (l,m) values:
'		Dim NLvalues As Integer = lmax * lmax + 2 * lmax + 1
'
'		Dim retval(NLValues-1,1) As Integer
'		Dim count As Integer = -1
'		
'		For l As Integer = 0 To lmax
'			For m As Integer = -l To l
'				count += 1
'				retval(count,0) = l
'				retval(count,1) = m
'			Next
'		Next
'		
'		Return retval
'		
'	End Function
'
'
'	Public Shared Function Eikdotr(k As Complex, khat As Vector, R As Vector) As Complex
'		
'		Return Complex.CExp(complex.i * Vector.ScalarProduct(khat,R))
'		
'	End Function
'	
'
'End Class
