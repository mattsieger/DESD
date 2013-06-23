
Imports DESD.Math

''' <summary>
''' Encapsulates the routines for computing the partial wave phase shifts for a muffin-tin potential.
''' </summary>
Public Class PhaseShift
	
	Implements IPhaseShiftProvider
	
	Const NPOINTSPERPERIOD As Integer = 60	'TODO - do a study on NPOINTSPERPERIOD to reduce size.  This is related to the adaptive mesh.
	Const NCUBICPOINTS As Integer = 1000 '650
	
	Private mMesh As IRadialMesh
	Private mPotential As Double()
	Private mWaveFunctions as Double(,)
	Private mRealDeltas As Double()
	Private mEnergy as double
	Private mKR as double
	Private mImatch As Integer
	Private mLMax As Integer
	Private mAtomicMass As Double
	Private mDeltaDerivatives As Double()
	Private mNormalizations as Double()
	Private mTotalCharges As Double()
	Private mMuffinTin As IMuffinTin
	
#Region "Constructors"
	
	''' <summary>
	''' Instantiates the class and computes the phase shifts for L = 0 to Lmax at 
	''' the given energy, for the given muffin tin potential.
	''' </summary>
	''' <param name="energy">The incident electron energy, in Rydbergs.</param>
	''' <param name="Lmax"></param>
	''' <param name="muffintin"></param>
	''' <remarks>I really should generalize this to work with any potential and specify Rmatch.</remarks>
	Sub New(energy As Double, Lmax As Integer, muffintin As IMuffinTin)
		Me.New(energy, Lmax, muffintin, True)
	End Sub
	
	''' <summary>
	''' 
	''' </summary>
	''' <param name="energy">The incident electron energy, in Rydbergs.</param>
	''' <param name="Lmax"></param>
	''' <param name="muffintin"></param>
	''' <param name="calculateDerivatives"></param>
	Protected Sub New(energy As Double, Lmax As Integer, muffintin As IMuffinTin, calculateDerivatives as boolean)
		mEnergy = energy
		mLMax = Lmax
		mMuffinTin = muffintin
		
		'// Create a new (adaptive) mesh for the solver, which is energy dependent:
		mMesh = CreateMesh(muffinTin.Mesh, mEnergy)
		'// Find the corresponding matching radius on the new mesh:
		mImatch = FindMatchingRadius(mMesh,muffintin.Radius)
		'// Interpolate the muffin tin potential onto the new mesh:
		mPotential = InterpolatePotential(muffintin, mMesh)
		'// Solve for the wavefunctions (for L = 0 to Lmax):
		mWaveFunctions = SolveWaveFunctions(mEnergy, Lmax, mMesh, mPotential)
		
		mKR = mmesh.R(Imatch) * system.Math.sqrt(mEnergy)  '// This is correct in Rydberg units.
		
		'// Compute the (real) phase shifts and their derivatives wrt energy:
		mRealDeltas = SolveRealPhaseShifts(mMesh,mWaveFunctions,mKR,mImatch, mNormalizations)
		
		'// Normalize the wave functions and compute the total charges:
		mTotalCharges = NormalizeWaveFunctions(mMesh, mImatch, mWaveFunctions, mNormalizations)
		
		If calculatederivatives Then
			mDeltaDerivatives = SolvePhaseShiftDerivatives(energy, Lmax, muffintin)
		End If
		
		'// Remember the atomic mass for future use in temperature dependent phase shifts:
		mAtomicMass = muffintin.Element.ElectronMasses

	End Sub


#End Region

#Region "Public Properties"

	''' <summary>
	''' Returns the maximum value of angular momentum L that phase shifts are calculated for.
	''' </summary>
	Public ReadOnly Property LMax As Integer
		Get
			return mLmax
		End Get
	End Property
	
	''' <summary>
	''' Returns a reference to the potential used to compute the phase shifts, tabulated on the 
	''' adaptive mesh.
	''' </summary>
	Public ReadOnly Property Potential As Double()
		Get
			return mPotential
		End Get
	End Property
	
	
	''' <summary>
	''' Returns the (unbound) wave function for energy E and angular momentum L, tabulated on the
	''' adaptive phase shift mesh.
	''' </summary>
	Public ReadOnly Property Wavefunction(L As Integer) As Double()
		Get	
			Dim retval(mMesh.count-1) As Double
			For i As Integer = 0 To mMesh.count-1
				retval(i) = mwavefunctions(L,i)
			Next
			return retval
		End Get
	End Property
	
	''' <summary>
	''' Returns a reference to the adaptive mesh used to calculate the unbound wave functions.
	''' </summary>
	Public ReadOnly Property Mesh As IRadialMesh
		Get
			return mMesh
			
		End Get
	End Property
	
	''' <summary>
	''' Returns the index of the adaptive mesh element corresponding to the muffin tin radius (matching radius).
	''' </summary>
	Public ReadOnly Property Imatch As Integer
		Get
			return mImatch
		End Get
	End Property
	
	Public ReadOnly Property Normalization(L As Integer) As Double
		Get
			Return mNormalizations(L)
		End Get
	End Property

	Public ReadOnly Property TotalCharge(L As Integer) As Double
		Get
			Return mTotalCharges(L)
		End Get
	End Property
	
	Public ReadOnly Property MuffinTin As IMuffinTin
		Get
			Return mMuffinTin
		End Get
	End Property
	
	Public ReadOnly Property Energy As Double
		Get
			Return mEnergy
		End Get
	End Property
	
#End Region
	
#Region "Public Methods"

	Public Function RealValue(L As Integer) As Double
		if L > mLmax then throw new ArgumentException("Invalid L")
		return mRealDeltas(L)
	End Function
	
	Public Function GetTmatrix() As Complex()
		Dim retval(mLmax) As Complex
		Dim deltal as double
		For l = 0 To mLmax
			deltal = Me.RealValue(l)
			retval(l) = system.math.Sin(deltal) * complex.CExp(new Complex(0.0,deltal))
		Next
		return retval
	End Function
	
	''' <summary>
	''' 
	''' </summary>
	''' <param name="Voptical">The optical potential, in Rydbergs.</param>
	''' <param name="temperature"></param>
	''' <param name="debyetemperature"></param>
	''' <returns></returns>
	Public Function GetTmatrix(Voptical As Double, temperature As Double, debyetemperature as double) As Complex()
		Dim retval(mLmax) As Complex
		Dim deltal As Complex
		Dim ci as complex = new Complex(0.0,1.0)
		For l = 0 To mLmax
			deltal = Me.ComplexValue(l,voptical,temperature,debyetemperature)
			retval(l) = complex.cSin(deltal) * complex.CExp(ci * deltal)
		Next
		return retval
	End Function
	
	Public Function GetPhaseShifts(Voptical As Double, temperature As Double, debyeTemperature As Double) As Complex()
		Dim retval(mLmax) As Complex
		Dim deltal As Complex
		Dim ci as complex = new Complex(0.0,1.0)
		For l = 0 To mLmax
			deltal = Me.ComplexValue(l,voptical,temperature,debyetemperature)
			retval(l) = deltal
		Next
		return retval
		
	End Function
	
	''' <summary>
	''' Returns the complex phase shift including the effect of the optical potential and
	''' thermal vibrations.
	''' </summary>
	''' <param name="L">The angular momentum quantum numbe.</param>
	''' <param name="Voptical">The optical potential, in Rydbergs.  A lossy Voptical is expressed as a positive number.</param>
	''' <param name="temperature">The temperature, in Kelvins.</param>
	''' <param name="debyetemperature">The debye temperature, in Kelvins.</param>
	''' <returns></returns>
	Public Function ComplexValue(L As Integer, Voptical As Double, temperature As Double, debyetemperature as double) As Complex
		
		If L > mLmax Then Throw New ArgumentException
		
		Dim retval As Complex = New Complex(mRealDeltas(L),0.0)
		Dim opticalTerm as Complex = new Complex(0.0,-Voptical * mDeltaDerivatives(L))
		
		'// Add in the modifications for temperature:
		
		If temperature > 0.0 Then

			Dim alpha as Double = 3.0 * temperature / (6.33363E-6 * matomicmass * debyetemperature * debyetemperature)
			Dim k As Double = system.Math.Sqrt(mEnergy)
			Dim alphak2 as Double = -2.0 * alpha * k * k
			Dim e2alphak2 As Double = system.Math.exp(alphak2)
			Dim sphbessarg As complex = New Complex(0.0,alphak2)
			Dim ilprime as Complex
			Dim jlprime As Complex
			Dim bterm As Double
			Dim sum as new complex()
			Dim tmatrix As Complex() = Me.GetTmatrix()
			Dim norm As Double
			
			'// set up the double sum over Lprime and Ldoubleprime
			'// Need to find out what the summation limits are.
			'// l1 = lprime
			'// l2 = ldoubleprime
			For l1 As Integer = 0 To mLmax
				ilprime = new Complex(0,1.0)^l1
                jlprime = Functions.CSphericalBesselJ(l1, sphbessarg)
				For l2 As Integer = 0 To mLmax
					bterm = PendryBFunction(l2,l1,L)
					norm = system.math.Sqrt(4.0*system.math.PI*(2*l1+1)*(2*l2+1)/(2*L+1))
					sum += ilprime * e2alphak2 * jlprime * tmatrix(l2) * norm * bterm
				Next
			Next
			Dim cdelta As Complex = New Complex(0.0,-0.5)* complex.CLn(1.0 + sum * New Complex(0.0,2.0))
			retval = cdelta
		End If
		
		Return retval + opticalTerm
		
	End Function
	
#End Region
	
#Region "Private Methods"

	Private Function CreateMesh(inputMesh As IRadialMesh, energy as double) As IRadialMesh
		
		Return New AdaptiveUnboundMesh(inputMesh.Min,inputMesh.Max,NCUBICPOINTS,energy,NPOINTSPERPERIOD)
		
	End Function
	
	Private Function FindMatchingRadius(mesh As IRadialMesh, radius As Double) As Integer
		Dim retval As Integer =  system.Array.BinarySearch(mMesh.GetArray,Radius) Xor -1
		If retval > mesh.Count-2 Then retval = mesh.Count-2
		return retval
	End Function
	
	
	Private Function InterpolatePotential(muffintin As IMuffinTin, newMesh as IRadialMesh) As Double()
		'// First, convert the muffin-tin potential to its normalized form:
		Dim TwoZ as Double = 2.0*cdbl(muffintin.AtomicNumber)
		Dim MTR as Double() = muffintin.Mesh.GetArray
		Dim NormPot(MTR.length-1) As Double
		Dim MTPot as Double() = muffintin.Potential
		NormPot(0) = 1.0
		For i As Integer = 1 To MTR.length-1
			NormPot(i) = -MTPot(i) * MTR(i) / TwoZ
		Next
		
		'// Now create a cubic spline of the (normalized) potential:
		Dim cspline As New CubicSpline(MTR,NormPot,0.0,0.0)
		
		'// Now create a return value array on the new mesh:
		Dim retvalnorm(newMesh.Count-1) As Double
		retvalnorm(0) = 1.0
		For i As Integer = 1 To newmesh.Count-1
			retvalnorm(i) = cspline.Y(newMesh.R(i))
		Next
		
		'// Now reverse the normalization to return the full mesh:
		Dim retval(newMesh.Count-1) As Double
		retval(0) = double.NegativeInfinity
		For i As Integer = 1 To newmesh.Count-1
			retval(i) = -TwoZ*retvalnorm(i) / newMesh.R(i)
		Next
		
		Return retval
		
	End Function
	
	''' <summary>
	''' Solves the (unbound) radial Schrodinger equation for L = 0 to Lmax.
	''' </summary>
	''' <param name="E"></param>
	''' <param name="Lmax"></param>
	''' <param name="mesh"></param>
	''' <param name="pot"></param>
	''' <returns>A Double(,) array containing the wave function solutions.</returns>
	Private Function SolveWaveFunctions(E As Double, Lmax As Integer, mesh As IRadialMesh, pot As Double()) As Double(,)
		
		Dim retval(Lmax,mesh.Count-1) As Double
		Dim Pl As Double()
		
		For L As Integer = 0 To Lmax
			Pl = UnboundRSESolver.Solve(mesh, E, L, pot)
			For i As Integer = 0 To Pl.Length-1
				retval(L,i) = Pl(i)
			Next
		Next
		
		Return retval
		
	End Function
	

	
	'// Note that E is in Rydbergs.
	''' <summary>
	''' Computes the phase shift given the unbound wave functions and the matching radius.
	''' </summary>
	''' <param name="mesh"></param>
	''' <param name="wavefunctions"></param>
	''' <param name="kR"></param>
	''' <param name="Imatch"></param>
	''' <returns></returns>
	Private Function SolveRealPhaseShifts(mesh as IRadialMesh, wavefunctions as double(,), kR As Double, Imatch as integer, byref normalizations as Double()) As Double()
				
		Dim r As Double() = mesh.GetArray
		Dim rmt As Double = r(imatch)
		Dim rplus As Double = r(imatch+1)
		Dim rminus As Double = r(imatch-1)
		Dim k As Double = kR / rmt
		
		Dim NL As Integer = wavefunctions.GetUpperBound(0)
		Dim retval(NL) As Double
		
		Dim DPDR As Double
		Dim LDR as Double
		Dim sphb as Double()
		'Dim oneover2i as Complex = new Complex(0.0, -0.5)
		Dim delta as double
        Dim pminus, p0, pplus As Double

		Dim e2idl As Complex
		Dim nrm as Complex
		ReDim normalizations(NL)
		For L As Integer = 0 To NL
			
			'// Compute derivative at Imatch using the centered approximation:
            pminus = wavefunctions(L, Imatch - 1) / rminus
            p0 = wavefunctions(L, Imatch) / rmt
            pplus = wavefunctions(L, Imatch + 1) / rplus

            DPDR = 0.5 * ((pplus - p0) / (rplus - rmt) + (p0 - pminus) / (rmt - rminus))

            LDR = (DPDR / p0)

            sphb = Functions.SphericalBessel(L, kR)
			delta = system.Math.Atan((LDR*sphb(0)-sphb(2)*k)/(LDR*sphb(1) - sphb(3)*k))
			retval(L) = delta
			
			'// Need to add in here a calculation of the p0 normalization norm * p0 = [ exp(2idelta_l) h_l(1) + h_l(2) ]
			e2idl = Complex.CExp(0.0,2.0*delta)
			nrm = ((e2idl + 1.0) * sphb(0) + complex.i * (e2idl - 1.0) * sphb(1)) / p0  'wavefunctions(L, Imatch)
			normalizations(L) = nrm.Magnitude
		Next
			
		return retval
			
	End Function
	
	''' <summary>
	''' Computes the derivatives of the phase shifts with respect to energy.
	''' </summary>
	''' <param name="energy"></param>
	''' <param name="Lmax"></param>
	''' <param name="muffintin"></param>
	''' <returns></returns>
	Private Function SolvePhaseShiftDerivatives(energy As Double, Lmax As Integer, muffintin As IMuffinTin) as double()
		'// This routine actually solves for the derivatives of the phase shifts:
		Dim deltaE as Double = 0.01
		Dim psPlus As PhaseShift = New PhaseShift(energy  + deltaE, Lmax, muffintin, false)
		'Dim psMinus As PhaseShift = New PhaseShift(energy - 0.01, Lmax, muffintin)
		'TODO Need to account for wraparound of the phase shift when computing derivative.
		Dim retval(Lmax) As Double
		Dim adder As Double
		
		For L As Integer = 0 To Lmax
			adder = 0.0
			If (system.math.Sign(psPlus.RealValue(L)) = -system.math.Sign(mRealDeltas(L))) Then
				'// The plus deltaE solution has changed sign.
				'// Either we are near zero and crossing, or we're wrapping around.
				'// If the difference in values is big, we must be wrapping around:
				If system.math.Abs(mRealDeltas(L)) > system.math.PI/3.0 Then
					'// last point was large, likely we are wrapping around:
					'// Determine the direction - rising or falling:
					If mRealDeltas(L) < 0 Then
						'// falling - we should add -pi to future values
						adder += -system.math.PI
					Else
						'// rising, we should add +pi to future values
						adder += system.math.PI
					End If
				End If
			End If

			'retval(L) = (0.5/0.01)*(psPlus.RealValue(L)-psMinus.RealValue(L))
			retval(L) = ((psPlus.RealValue(L)+ adder)-mRealDeltas(L))/0.01
		Next
		
		Return retval
	End Function
	
	
	Private Function NormalizeWaveFunctions(mesh As IRadialMesh, imatch as Integer, byref waveFunctions As Double(,), normalizations As Double()) As Double()
		
		Dim r As Double() = mesh.GetArray
		Dim rmt As Double = r(imatch)
		
		Dim NL As Integer = wavefunctions.GetUpperBound(0)
		Dim retval(NL) As Double
		
		Dim temp1 As Double
		Dim temp2 as Double
		For L As Integer = 0 To NL
			
			'// Normalize the wave function, accumulating total charge integral as we go:
			retval(L) = 0.0
			For i As Integer = 1 To imatch
				wavefunctions(L, i) = wavefunctions(L, i) * normalizations(L)
				
				'// Accumulate integral via the trapezoidal rule
				temp1 = wavefunctions(L,i) ^2
				temp2 = wavefunctions(L,i-1) ^2
				retval(L) += 0.5*(temp1 + temp2)*(r(i)-r(i-1))
			Next
			
			'// Complete the rest of the normalization:
			For i As Integer = imatch + 1 To mesh.Count-1
				wavefunctions(L, i) = wavefunctions(L, i) * normalizations(L)
			Next
			
		Next
			
		return retval

	End Function
	
#End Region
	
#Region "Public Shared Methods"

	''' <summary>
	''' Returns the Pendry B_L1(L2,L3) function.
	''' </summary>
	''' <param name="L1"></param>
	''' <param name="L2"></param>
	''' <param name="L3"></param>
	''' <returns></returns>
	Public Shared Function PendryBFunction(L1 As Integer, L2 As Integer, L3 As Integer) As Double
		'TODO Make this private and cache the results for speed improvement, or look up another algorithm
		
		
		'// Tabulate the integrand on a mesh over the interval [0,pi]
		Dim integMesh As New SimpleMesh(0.0,system.Math.PI,100)
		
		Dim integrand(integMesh.Count-1) As Double
		Dim temp As Double
		Dim cosTheta as double
		For i As Integer = 0 To integMesh.Count-1
			cosTheta = system.Math.Cos(integmesh.R(i))
            temp = Functions.Legendre(L1, 0, cosTheta) * Functions.Legendre(L2, 0, cosTheta) * Functions.Legendre(L3, 0, cosTheta) * System.Math.Sin(integMesh.R(i))
			integrand(i) = temp
		Next
		
		Dim result As Double = Integration.TrapezoidalRuleIntegrator.Integrate(integMesh.GetArray,integrand,0,integMesh.Count-1)
		'Dim result As Double = Integration.SimpsonsRule.Integrate(integMesh.GetArray,integrand)
        result *= 0.25 * System.Math.Sqrt((2 * L1 + 1) * (2 * L2 + 1) * (2 * L3 + 1)) * DESD.Math.Constants.InvSqrtPi
		
		Return result
		
	End Function
	
	#End Region
	
#Region "IPhaseShiftProvider Implementation"

	Public Function GetPhaseShift(E As Double, L As Integer, Voptical As Double, Temperature As Double, debyeTemperature As Double) As Complex	Implements IPhaseShiftProvider.GetPhaseShift
		Return ComplexValue(L, Voptical, Temperature, debyeTemperature)
	End Function
	
	Function GetPhaseShifts1(E As Double, Voptical As Double, Temperature As Double, debyeTemperature As Double) As Complex() Implements IPhaseShiftProvider.GetPhaseShifts
		Return GetPhaseShifts(Voptical, Temperature, debyeTemperature)
	End Function
		
	Function GetTMatrix(E As Double, Voptical As Double, Temperature As Double, debyeTemperature As Double) As Complex() Implements IPhaseShiftProvider.GetTmatrix
		Return GetTmatrix(Voptical, Temperature, debyeTemperature)
	End Function
	
#End Region

End Class
