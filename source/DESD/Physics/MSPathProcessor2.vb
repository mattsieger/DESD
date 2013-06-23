
	
Imports DESD.Math
Imports System.Math

Public Class MSPathProcessor2
	
	Implements IMSPathProcessor
	
	Private _Cluster As Cluster
	Private _ClusterIDs As List(Of Integer)
	
	Private _ToCalculate As New Queue(Of MSPath)
	Private _Calculated as New List(Of MSPath)
	
	Private _RAOrder As Integer = 2
	Private _MaxPathLength As Double = 100.0
	Private _MaxMSOrder As Integer = 10
	
	Private _k As Complex
	Private _khat as Vector
	Private _LMax as integer
	Private _zv0 As Double
	
	Private _LM As Integer(,)
	Private _RA As Integer(,)
	
	Private _NLM As Integer
	Private _NRA As Integer
	
	Private _YlmKhat as Complex()
	
	
#Region "Constructors"
	
	
	Public Sub New(mscluster As Cluster, startingPaths as List(Of MSPath), maxpathlength as Double, k as complex, zv0 as Double, raorder as Integer, maxmsorder as integer, lmax as integer)
		_Cluster = mscluster
		
		'// Cache the cluster atom IDs locally, saves time later on:
		_ClusterIDs = mscluster.GetAtomIDs
		
		'// Add the startingPaths to the ToCalculate queue:
		For Each path As MSPath In startingPaths
			_ToCalculate.Enqueue(path)
		Next
		
		_MaxPathLength = maxpathlength
		_k = k
		_zv0 = zv0
		_RAOrder = raorder
		_MaxMSOrder = maxmsorder
		_LMax = lmax
		
		_LM = LM(lmax)
		_RA = MuNu(raorder)
		
		_NLM = _LM.Length\2
		_NRA = _RA.Length\2
		
		ReDim _YlmKhat(_NLM - 1)
		
		
	End Sub


#End Region
	
	
#Region "Public Properties"
	
	Public Property Khat As Vector Implements IMSPathProcessor.Khat
		Get
			Return _khat
		End Get
		Set (value As Vector)
			_khat = value
		End Set
	End Property
	
	Public ReadOnly Property NPathsCalculated As Integer Implements IMSPathProcessor.NPathsCalculated
		Get
			Return _Calculated.count
		End Get
	End Property
	
#End Region
	
	
	Private Sub ValidateInputs

		'// 1) The _ToCalculate queue must have at least one member:
		If _ToCalculate.Count < 1 Then Throw New InvalidOperationException("There must be at least one input path in order to begin the calculation.")
		
		'// 2) The Cluster object must be non-null:
		If _Cluster Is Nothing Then Throw New InvalidOperationException("The Cluster property has not been set or is Nothing.")
		
		'// 3) The initial Path objects must contain valid atom IDs belonging to the Cluster:
		
		'// 4) The _PhaseShifts object must not be null:
		'If _PhaseShifts Is Nothing Then Throw New InvalidOperationException("The PhaseShifts property has not been set or is Nothing.")

	End Sub
	
	
	Public Sub Calculate() Implements IMSPathProcessor.Calculate
		'// process paths in the "ToCalculate" queue
		
		ValidateInputs()
		
		Dim thisPath As MSPath
		
		Do Until _ToCalculate.Count = 0
			
			'// Pop the next path off of the stack:
			thisPath = _ToCalculate.Dequeue
			
			'// Calculate the path:
			thisPath.RootAmplitude = CalculateAmplitude(thisPath, _k, _lmax, _raorder)
			
			'// Add the path to the _Calculated list:
			_Calculated.Add(thisPath)
			
			'// Extend the path:
			ExtendPath(thisPath)
			
		Loop
		
		'// Now that the paths have been enumerated, prepare for termination by multiplying by the B matrix:
		For Each finalPath As MSPath In _Calculated
			
			'// Calculate the path:
			finalPath.RootAmplitude = PreTerminatePath(finalPath,  _k,  _raorder)
			
		Next

		
	End Sub
	
	
	Public Sub CalculateTermination() Implements IMSPathProcessor.CalculateTermination
		
		'// First, tabulate the Ylm(khat) for the l,m values:
		For i As Integer = 0 To _NLM-1
			_YlmKhat(i) = Functions.SphericalHarmonic(_LM(i,0),_LM(i,1),_khat.Theta,_khat.Phi).Conjugate
		Next
		
		For Each thisPath As MSPath In _Calculated
			
			'// Calculate the path:
			thisPath.Amplitude = TerminatePath(thisPath, _k, _khat, _zv0, _lmax, _raorder)
			
		Next
		
	End Sub
	


	Private Sub ExtendPath(path As MSPath)
		
		'// If this is the direct term, don't extend.
		If path.Order = 0 Then Exit Sub
		
		Dim newPath As MSPath
		
		For Each id As Integer In _ClusterIDs
			
			If (id <> path.TerminalAtom.ID)  and (_Cluster.AtomEnabled(id)) Then
				'// Create a new path, appending this ID onto the chain:
				newPath = New MSPath(path, _Cluster.GetAtom(id))
				'// If the new path has length shorter than or equal to the maximum, and has MS order less than or equal to the maximum, enqueue it:
				if (newPath.Order <= _MaxMSOrder) AndAlso (newPath.pathlength <= _MaxPathLength) Then
					_ToCalculate.Enqueue(newPath)
				end if
			End If
		Next
		
	End Sub
	
	
	Public Function GetPaths() As List(Of MSPath) Implements IMSPathProcessor.GetPaths
		Return New List(Of MSPath)(_Calculated)
	End Function
	
	
	
	''' <summary>
	''' 
	''' </summary>
	''' <param name="path"></param>
	''' <param name="k"></param>
	''' <param name="lmax"></param>
	''' <param name="raorder"></param>
	''' <returns></returns>
	Public Function CalculateAmplitude(path As MSPath, k as Complex, lmax as Integer, raorder as integer) as ComplexMatrix
				
		Dim R1 As Vector
		Dim R2 As Vector
		Dim R3 As Vector
		Dim T As Complex()
		Dim retval As ComplexMatrix
		
		Dim scatteringorder As Integer = path.Order
		Select Case scatteringorder
			Case 0	' Direct term
				'// Not allowed here, since the direct term depends on k-vector
				'Throw new InvalidOperationException("There is no root amplitude for the direct term.")
				retval = nothing
			Case 1	' Single scattering
				R1 = path.GetPosition(1)
				R2 = path.GetPosition(0)
				retval = M(R1, R2, k, lmax, raorder)
			Case Else	' Higher-order (multiple) scattering
				R1 = path.GetPosition(2)
				R2 = path.GetPosition(1)
				R3 = path.GetPosition(0)
				T = path.GetTmatrix(1)
				
				'// If the parent's RootAmplitude is undefined, recurse to compute it.
				If path.Parent.RootAmplitude Is Nothing Then
					'// recurse
					path.Parent.RootAmplitude = CalculateAmplitude(path.Parent, k, lmax, raorder)
				End If

				retval = path.Parent.RootAmplitude * H(R1, R2, R3, k, T, raorder)
				
		End Select
		
		Return retval
		
	End Function
	
	
	''' <summary>
	''' 
	''' </summary>
	''' <param name="path"></param>
	''' <param name="k"></param>
	''' <param name="khat"></param>
	''' <param name="zv0"></param>
	''' <param name="lmax"></param>
	''' <param name="raorder"></param>
	''' <returns></returns>
	Public Function TerminatePath_old(path As MSPath, k As Complex, khat As Vector, zv0 as Double, lmax as Integer, raorder As Integer) As ComplexMatrix
		Dim R1 As Vector
		Dim R2 As Vector
		Dim T As Complex()
		Dim retval As ComplexMatrix
		
		Dim scatteringorder As Integer = path.Order
		
		If path.Order = 0 Then
			'// Direct term:
			R1 = path.GetPosition(0)
			retval = DirectTerm(R1,k,khat,zv0,lmax)
		Else
			'// Higher order (multiple) scattering
			R1 = path.GetPosition(1)
			R2 = path.GetPosition(0)
			T = path.GetTmatrix(0)
				
			'// If the parent's RootAmplitude is undefined, recurse:
			'// This shouldn't be necessary....
			If path.RootAmplitude Is Nothing Then
				'// recurse
				path.RootAmplitude = CalculateAmplitude(path.Parent, k, lmax, raorder)
			End If

			retval = path.RootAmplitude * Q(R1, R2, k, khat, T, zv0, raorder)
	
		End If

		Return retval

	End Function
	

	''' <summary>
	''' 
	''' </summary>
	''' <param name="path"></param>
	''' <param name="k"></param>
	''' <param name="khat"></param>
	''' <param name="zv0"></param>
	''' <param name="lmax"></param>
	''' <param name="raorder"></param>
	''' <returns></returns>
	Public Function TerminatePath(path As MSPath, k As Complex, khat As Vector, zv0 as Double, lmax as Integer, raorder As Integer) As ComplexMatrix
		Dim R As Vector
		Dim retval As ComplexMatrix
		
		Dim scatteringorder As Integer = path.Order
		
		If path.Order = 0 Then
			'// Direct term:
			R = path.GetPosition(0)
			retval = DirectTerm(R,k,khat,zv0,lmax)
		Else
			'// Higher order (multiple) scattering
			R = path.GetPosition(0)
				
			retval = path.RootAmplitude * P(R, k, khat, zv0)
	
		End If

		Return retval

	End Function

	
	Public Function PreTerminatePath(path As MSPath, k As Complex, raorder As Integer) As ComplexMatrix
		Dim R1 As Vector
		Dim R2 As Vector
		Dim T As Complex()
		Dim retval As ComplexMatrix
		
		Dim scatteringorder As Integer = path.Order
		
		If path.Order > 0 Then

			'// Higher order (multiple) scattering
			R1 = path.GetPosition(1)
			R2 = path.GetPosition(0)
			T = path.GetTmatrix(0)
				
			'// If the parent's RootAmplitude is undefined, recurse:
			'// This shouldn't be necessary....
'			If path.RootAmplitude Is Nothing Then
'				'// recurse
'				path.RootAmplitude = CalculateAmplitude(path.Parent, k, _lmax, raorder)
'			End If

			retval = path.RootAmplitude * B(R1, R2, k, T, raorder)
	
		End If

		Return retval


	End Function
	
	
	Public Function B(R1 As Vector, R2 As Vector, k As Complex, T As Complex(), raorder As Integer) As ComplexMatrix
		'// Compute the bond vectors:
		'Dim R12 As Vector = R2 - R1
		Dim R12 As Vector = R1 - R2
		
		Dim theta12 As Double = R12.Theta
		Dim phi12 As Double = R12.Phi
		Dim rho12 As Complex = k * R12.R

		Dim Retval(_NRA - 1,_NLM - 1) As Complex
		Dim Gamma1 As Complex
		
		Dim l As Integer
		Dim m As Integer
		Dim mu As Integer
		Dim nu As Integer
		
		'// Rows in lamda, columns in L
		For lamda As Integer = 0 To _NRA - 1
			mu = _RA(lamda,0)
			nu = _RA(lamda,1)
			
			For ilm As Integer = 0 To _NLM - 1
				l = _LM(ilm,0)
				m = _LM(ilm,1)
				Gamma1 = RehrAlbers.Gamma1(l,m,mu,nu,rho12,theta12,phi12)
				Retval(lamda,ilm) = Gamma1 * T(l)
			Next
		Next
		
		Dim temp As New ComplexMatrix(Retval)
		
		temp *= (Complex.CExp(Complex.i * rho12) / (k * rho12))
		
		Return temp

	End Function
	
	
	Public Function P(R As Vector, k As Complex, khat As Vector, zv0 as Double) As ComplexMatrix
		'// P_L has rows in L
		
		Dim Retval(_NLM - 1,0) As Complex
			
		
		'// Compute the plane wave factor:
		Dim eikdotr As Complex = Complex.CExp(complex.i * k * Vector.ScalarProduct(khat,R))
		
		'// Compute the attenuation factor:
		Dim attenuation as Double = Exp(-k.Imag*(zv0-R.Z)/cos(Pi-khat.Theta))
		
		For ilm As Integer = 0 To _NLM - 1
			Retval(ilm,0) = _YlmKhat(ilm) * attenuation * eikdotr
		Next
		
		Dim temp As New ComplexMatrix(Retval)
			
		Return temp

	End Function

	
	''' <summary>
	''' Returns the DESD termination matrix M.  The dimension of the return array is L rows by Lamda columns.
	''' </summary>
	''' <param name="Ra"></param>
	''' <param name="Rn"></param>
	''' <param name="k"></param>
	''' <param name="lmax"></param>
	''' <param name="raorder"></param>
	''' <returns></returns>
	Public Function M(Ra As Vector, Rn As Vector, k As Complex, lmax as Integer, raorder as integer) As ComplexMatrix
		'// Returns a ComplexMatrix with lmax + 1 rows and the number of columns determined by raorder.
		
		'// Compute the bond vector:
		Dim Ran As Vector = Ra - Rn
		
		Dim theta As Double = Ran.Theta
		Dim phi As Double = Ran.Phi
		Dim rho As Complex = k * Ran.R
		
		'// Determine the number of distinct (l,m) values:
		'// Could declare these as static resources
'		Dim LMvalues As Integer(,) = LM(lmax)
'		Dim RAvalues As Integer(,) = MuNu(raorder)
		
		'// Dimension the array used to store M values:
		Dim Mvalues(_LM.Length\2-1,_NRA-1) As Complex
		
		Dim l As Integer
		Dim ml As Integer
		Dim mu As Integer
		Dim nu As Integer
		
		For ilm As Integer = 0 To _NLM - 1
			For imunu As Integer = 0 To _NRA - 1
				l = _LM(ilm,0)
				ml = _LM(ilm,1)
				mu = _RA(imunu,0)
				nu = _RA(imunu,1)
				Mvalues(ilm,imunu) = RehrAlbers.Gamma2(l,ml,mu,nu,rho,theta,phi)
			Next
		Next
		
		Return New ComplexMatrix(Mvalues)
		
	End Function
	
	
	Public Function F(R1 As Vector, R2 As Vector, R3 As Vector, k As Complex, T As Complex(), raorder As Integer) As ComplexMatrix
		
		'// Compute the bond vectors:
		Dim R12 As Vector = R1 - R2
		Dim R23 As Vector = R2 - R3
		
		Dim theta12 As Double = R12.Theta
		Dim phi12 As Double = R12.Phi
		Dim rho12 As Complex = k * R12.R

		Dim theta23 As Double = R23.Theta
		Dim phi23 As Double = R23.Phi
		Dim rho23 As Complex = k * R23.R

		
		'// Calculate the Euler angles:
		Dim sintheta12 As Double = Sin(theta12)
		Dim costheta12 As Double = Cos(theta12)
		Dim sintheta23 As Double = Sin(theta23)
		Dim costheta23 As Double = Cos(theta23)
		Dim phi12minusphi23 As Double = phi12 - phi23
		
		Dim alpha As Double = Complex.Arg(New Complex(sintheta12*costheta23 - costheta12*sintheta23*Cos(-phi12minusphi23),-sintheta23*Sin(-phi12minusphi23)))
		Dim beta As Double
		Dim argforbeta As Double = costheta12*costheta23 + sintheta12*sintheta23*Cos(phi12minusphi23)
		If argforbeta <= -1.0 Then
			beta = system.Math.pi
		ElseIf argforbeta >= 1.0 Then
			beta = 0.0
		Else
			beta = Acos(argforbeta)
		End If
		Dim gamma As Double = Complex.Arg(New Complex(sintheta12*costheta23*Cos(-phi12minusphi23) - costheta12*sintheta23,sintheta12*Sin(-phi12minusphi23)))


		
		'// Here's a BIG time saver.  Cache these.
		'Dim RAvalues As Integer(,) = MuNu(raorder)
		'Dim LMvalues As Integer(,) = LM(T.Length - 1)
		Dim Lmax As Integer = T.Length - 1
		
		'Dim Retval(RAvalues.Length\2 - 1,RAvalues.Length\2 - 1) As Complex
		Dim Retval(_NRA - 1,_NRA - 1) As Complex
		Dim lgamma As Complex
		Dim lgammatwiddle As Complex
		Dim lrotm As Double
		
		Dim mu As Integer
		Dim nu As Integer
		Dim muprime As Integer
		Dim nuprime As Integer
		
		Dim eminusialphamu As Complex
		Dim eminusigammamuprime As Complex
		Dim temp As Complex
		Dim temp2 As Complex
		
		For lamda As Integer = 0 To _NRA - 1
			mu = _RA(lamda,0)
			nu = _RA(lamda,1)
			
			eminusialphamu = Complex.CExp(0.0,-alpha*CDbl(mu))
			
			For lamdaprime As Integer = 0 To _NRA - 1
				muprime = _RA(lamdaprime,0)
				nuprime = _RA(lamdaprime,1)
				
				eminusigammamuprime = Complex.CExp(0.0,-gamma*CDbl(muprime))
				
				'// Clear the accumulator:
				temp = New Complex(0.0,0.0)
				
				'// Begin sum over l:
				For l As Integer = 0 To Lmax
					lgamma = RehrAlbers.LGamma(l,mu,nu,rho12)
					lgammatwiddle = RehrAlbers.LGammaTwid(l,muprime,nuprime,rho23)
					lrotm = functions.LRotm(l,mu,muprime,beta)
					temp += T(l) * lgamma * lrotm * lgammatwiddle
				Next
				temp2 = eminusialphamu * temp * eminusigammamuprime
				
				If Double.IsNaN(temp2.Real) Or Double.IsNaN(temp2.Imag) Then
					Console.WriteLine("NaN value in F! ==============================================================")
					Console.WriteLine("R1 = " & R1.ToString)
					Console.WriteLine("R2 = " & R2.ToString)
					Console.WriteLine("R3 = " & R3.ToString)
					Console.WriteLine("k = " & k.ToString)
					Console.WriteLine("Rho12 (rho, theta, phi) = (" & rho12.ToString & ", " & theta12.ToString & ", " & phi12.ToString & ")")
					Console.WriteLine("Rho23 (rho, theta, phi) = (" & rho23.ToString & ", " & theta23.ToString & ", " & phi23.ToString & ")")
					Console.WriteLine("(mu, nu)(muprime, nuprime) = (" & mu.ToString & "," & nu.ToString & ")(" & muprime.ToString & "," & nuprime.ToString & ")")
					Console.WriteLine("Tmatrix elements:")
					For ll As Integer = 0 To Lmax
						Console.WriteLine(ll.ToString & ", " & T(ll).ToString)
					Next
				End If
				
				Retval(lamda,lamdaprime) = temp2
				
			Next
		Next
		
		Return New ComplexMatrix(Retval)
				
	End Function

	
	Public Function H(R1 As Vector, R2 As Vector, R3 As Vector, k As Complex, T As Complex(), raorder as integer) As ComplexMatrix
		
		'// Compute the bond vectors:
		Dim R12 As Vector = R1 - R2
		Dim rho12 As Complex = k * R12.R
		
		Dim temp As ComplexMatrix = F(R1,R2,R3,k,T,raorder)
		
		temp *= (Complex.CExp(Complex.i * rho12) / rho12)
		
		Return temp
		
	End Function

	
	''' <summary>
	''' The Termination matrix Q.
	''' </summary>
	''' <param name="R1"></param>
	''' <param name="R2"></param>
	''' <param name="k"></param>
	''' <param name="khat"></param>
	''' <param name="T"></param>
	''' <param name="zv0"></param>
	''' <param name="raorder"></param>
	''' <returns></returns>
	Public Function Q(R1 As Vector, R2 As Vector, k As Complex, khat As Vector, T As Complex(), zv0 as Double, raorder As Integer) As ComplexMatrix
		'// Compute the bond vectors:
		Dim R12 As Vector = R1 - R2
		
		Dim theta12 As Double = R12.Theta
		Dim phi12 As Double = R12.Phi
		Dim rho12 As Complex = k * R12.R

		'// Cache these:
'		Dim RAvalues As Integer(,) = MuNu(raorder)
'		Dim LMvalues As Integer(,) = LM(T.Length - 1)

		Dim Retval(_NRA - 1,0) As Complex
		Dim Gamma1 As Complex
		Dim Ylm as Complex
		
		Dim l As Integer
		Dim m As Integer
		Dim mu As Integer
		Dim nu As Integer
		
		'// Compute the plane wave factor:
		Dim eikdotr As Complex = Complex.CExp(complex.i * Vector.ScalarProduct(khat,R2))
		
		'// Compute the attenuation factor:
		Dim attenuation as Double = Exp(-k.Imag*(zv0-R2.Z)/cos(Pi-khat.Theta))
		
		'// Perform the sums over L
		For lamda As Integer = 0 To _NRA - 1
			mu = _RA(lamda,0)
			nu = _RA(lamda,1)
			
			'// Clear the accumulator:
			Retval(lamda,0) = New Complex()
				
			'// Begin sum over L-values:
			For ilm As Integer = 0 To _NLM - 1
				l = _LM(ilm,0)
				m = _LM(ilm,1)
				Gamma1 = RehrAlbers.Gamma1(l,m,mu,nu,rho12,theta12,phi12)
				'// performance idea = cache these Ylm (tabulate) - why recalculate every path?
				Ylm = _YlmKhat(ilm)
				Retval(lamda,0) += Gamma1 * T(l) * Ylm
			Next
		Next
		
		Dim temp As New ComplexMatrix(Retval)
		
		temp *= (attenuation * eikdotr * Complex.CExp(Complex.i * rho12) / (k * rho12))
		
		Return temp

	End Function

	
	Public Function DirectTerm(R As Vector, k As Complex, khat As Vector, zv0 as Double, lmax As Integer) As ComplexMatrix
		'// Returns a ComplexMatrix with lmax + 1 rows and 1 column.
		
		'// Determine the number of distinct (l,m) values:
		'// Could declare these as static resources
'		Dim LMvalues As Integer(,) = LM(lmax)
		
		'// Dimension the array used to store M values:
		Dim DirectValues(_LM.Length\2-1,0) As Complex
		
		Dim eikdotr As Complex = Complex.CExp(complex.i * Vector.ScalarProduct(khat,R))
		Dim attenuation as Double = Exp(-k.Imag*(zv0-R.Z)/cos(Pi-khat.Theta))

		Dim l As Integer
		Dim m As Integer
		Dim Ylm as Complex
		For ilm As Integer = 0 To _NLM - 1
			l = _LM(ilm,0)
			m = _LM(ilm,1)
			Ylm = _YlmKhat(ilm) 'Functions.SphericalHarmonic(l,m,khat.Theta,khat.Phi).Conjugate
			DirectValues(ilm,0) = Ylm * eikdotr / k
		Next
		
		Return New ComplexMatrix(DirectValues)
		
	End Function
	
	Public Function MuNu(raorder As Integer) As Integer(,)
		
		Dim mu() As Integer = {0, -1, 1, 0, -2, 2, -1, 1, -3, 3, 0, -2, 2, -4, 4}
        Dim nu() As Integer = {0,  0, 0, 1,  0, 0,  1, 1,  0, 0, 2,  1, 1,  0, 0}

        Dim imax As Integer
        Select Case raorder
            Case 0
                imax = 0
            Case 1
                imax = 2
            Case 2
                imax = 5
            Case 3
                imax = 9
            Case 4
                imax = 14
            Case Else
                Throw New ArgumentOutOfRangeException("order", "must be positive and less than or equal to 4.")
        End Select
        
        Dim retval(imax,1) As Integer
        
        For i As Integer = 0 To imax
        	retval(i,0) = mu(i)
        	retval(i,1) = nu(i)
        Next
		
		Return retval
		
	End Function
	
	Public Function LM(lmax As Integer) As Integer(,)
		
		'// Determine the number of distinct (l,m) values:
		Dim NLvalues As Integer = lmax * lmax + 2 * lmax + 1

		Dim retval(NLValues-1,1) As Integer
		Dim count As Integer = -1
		
		For l As Integer = 0 To lmax
			For m As Integer = -l To l
				count += 1
				retval(count,0) = l
				retval(count,1) = m
			Next
		Next
		
		Return retval
		
	End Function


	Public Function Eikdotr(k As Complex, khat As Vector, R As Vector) As Complex
		
		Return Complex.CExp(complex.i * Vector.ScalarProduct(khat,R))
		
	End Function
	
	
End Class

