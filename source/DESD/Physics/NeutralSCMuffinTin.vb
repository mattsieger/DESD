
Imports DESD.Math

''' <summary>
''' Represents a muffin-tin atom, its electronic wave functions, and
''' its total potential.
''' The atom is solved self-consistently using the method of Herman and Skillman, modified to produce
''' a muffin-tin potential in the spirit of Pendry, who increases the outermost orbitals' occupancy
''' in an effort to maintain charge neutrality at the muffin-tin radius.
''' </summary>
Public Class NeutralSCMuffinTin

    Private mZ As Element
    Private mMesh As IRadialMesh
    Private mConfiguration As ElectronicConfiguration
	Private mOrbitals As New List(Of Orbital)
	Private mPotential As Double()
	Private mIncidentPotential As Double()
	Private mRadius As Double
	Private mImt As Integer
	Private mInnerPotential as Double
		
#Region "Constructors"

    ''' <summary>
    ''' Create a muffin-tin with the tabulated ground state configuration and the given muffin-tin radius.
    ''' </summary>
    ''' <param name="atomicNumber">The enumerated elemental species of the atom.</param>
    ''' <param name="muffintinradius">The radius of the muffin-tin, in Bohr radii.</param>
    Sub New(ByVal atomicNumber As Element, muffintinradius as Double)
        mZ = atomicNumber
        mConfiguration = New ElectronicConfiguration(atomicNumber)
        mMesh = New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(mZ) * 205.0, 650)
        Me.Radius = muffintinradius
        Solve()
    End Sub
    
    ''' <summary>
    ''' Create a muffin-tin with the tabulated ground state configuration and the given muffin-tin radius.
    ''' </summary>
    ''' <param name="Z">The atomic number of the atom.</param>
    ''' <param name="muffintinradius">The radius of the muffin-tin, in Bohr radii.</param>
    Sub New(ByVal Z as integer, muffintinradius as Double)
        mZ = ctype(Z,element)
        mConfiguration = New ElectronicConfiguration(mZ)
        mMesh = New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(mZ) * 205.0, 650)
        if muffintinradius <= 0.0 then throw new ArgumentException("Invalid muffin-tin radius.")
        Me.Radius = muffintinradius
        Solve()
    End Sub
    
    Sub New(ByVal atomicNumber As Element, muffintinradius as Double, ByVal configuration As ElectronicConfiguration)
        mZ = atomicNumber
        mConfiguration = configuration
        mMesh = New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(mZ) * 205.0, 650)
        Me.Radius = muffintinradius
        Solve()
    End Sub

    Sub New(ByVal Z as integer, muffintinradius as Double, configuration as string)
        mZ = ctype(Z,element)
        mConfiguration = New ElectronicConfiguration(configuration)
        mMesh = New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(mZ) * 205.0, 650)
        Me.Radius = muffintinradius
        Solve()
    End Sub
    
   
    Sub New(ByVal atomicNumber As Element, muffintinradius as Double, mesh as IRadialMesh)
        mZ = atomicNumber
        mConfiguration = New ElectronicConfiguration(atomicNumber)
        mMesh = mesh
        Me.Radius = muffintinradius
        Solve()
    End Sub

    Sub New(ByVal Z as integer, muffintinradius as Double, cubicmeshsize as integer)
        mZ = CType(Z,Element)
        mConfiguration = New ElectronicConfiguration(mZ)
        mMesh = New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(mZ) * 205.0, cubicmeshsize)
        Me.Radius = muffintinradius
        Solve()
    End Sub
    

    Sub New(ByVal atomicNumber As Element, muffintinradius as Double, ByVal configuration As ElectronicConfiguration, mesh as IRadialMesh)
        mZ = atomicNumber
        mConfiguration = configuration
        mMesh = mesh
        Me.Radius = muffintinradius
        Solve()
    End Sub


#End Region



#Region "Properties"

        Public ReadOnly Property AtomicNumber() As Integer
            Get
                Return CInt(mZ)
            End Get
        End Property

        Public ReadOnly Property Element() As Element
            Get
                Return mZ
            End Get
        End Property

		Public ReadOnly Property Mesh As IRadialMesh
			Get
				Return mMesh
			end Get
		End Property

		Public ReadOnly Property Configuration As ElectronicConfiguration
			Get
				Return mConfiguration
			End Get
		End Property
		
		Public ReadOnly Property Orbitals As List(Of Orbital)
			Get
				return mOrbitals
			End Get
        End Property

        Public ReadOnly Property GetOrbital(ByVal n As Integer, ByVal l As Integer) As Orbital
            Get
                Dim orb = From o In mOrbitals Where (o.N = n) And (o.L = l) Take 1
                Return orb.Single
            End Get
        End Property
		
		Public ReadOnly Property Potential As Double()
			Get
				Dim retval(mPotential.Length-1) as Double
				system.Array.Copy(mPotential,retval,mPotential.Length)
				Return retval
			End Get
		End Property
		
		Public ReadOnly Property IncidentPotential As Double()
			Get
				Dim retval(mIncidentPotential.Length-1) as Double
				system.Array.Copy(mIncidentPotential,retval,mIncidentPotential.Length)
				Return retval
			End Get
		End Property
		
		Public Property Radius As Double
			Get
				Return mRadius
			End Get
			Set (value as Double)
			    if value <= 0.0 then throw new ArgumentException("Invalid muffin-tin radius.")
				mRadius = value
				Solve()
			End Set
		End Property
		
		Public Readonly Property InnerPotential As Double
			Get
				return mInnerPotential
			End Get
		End Property
		
		Public ReadOnly Property Imt As Integer
			Get
				Return mImt
			End Get
		End Property
		
#End Region


#Region "Private Methods"

        ''' <summary>
        ''' Solves for the atomic potential and electronic radial wavefunctions using the method of Herman and Skillman
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Solve()
        	
        	'// First, get a list of the (n,l) values and occupancies from the Configuration:
        	Dim NOrbitals As Integer = mConfiguration.OrbitalCount
        	Dim OrbitalQNs(,) As Integer = mConfiguration.GetQuantumNumbers
        	        	
			Dim iMax As Integer = mMesh.Count-1
			
			Dim Vtrial() As Double
			Dim Vnew(iMax) As Double
			Dim Solution As Orbital
			Dim N As Integer
			Dim L As Integer
            Dim Converged As Boolean = False
            Dim DeltaV as Double
            Dim DeltaVMax as Double
            Dim DeltaVtolerance As Double = 0.0001
			Dim nIterations As Integer = 0
			         
            Dim SigmaIntegral As Double()
            
            '// Find the index of r corresponding to the muffin-tin radius Rmt = Imt:
            Dim Imt As Integer = system.Array.BinarySearch(mMesh.GetArray,mRadius) Xor -1
        	Dim Occupancies(, ) as Double = mconfiguration.GetOccupancyArray()
            Dim r As Double() = mMesh.GetArray()
            
            '// Before we start, pre-fill the occpancies table with data from Configuration
            Do Until Converged
            
            	'// 1) Create the trial potential
                If (Vtrial Is Nothing) Then
                    '// This is the first loop, create an initial trial potential
                    Vtrial = HermanSkillmanTable.GetPotential(mZ, mMesh)
                Else
                    '// Construct the new trial potential from the average of the previous trial potential
                    '// and the new potential.
                    For i As Integer = 0 To iMax
                        Vtrial(i) = (Vtrial(i) + Vnew(i)) / 2.0
                    Next
                End If

            	'// 2) Solve for electronic wave functions and energies for all orbitals in the configuration

            	'// First, clear out any existing orbitals:
            	mOrbitals.clear
            	
            	'//Non-threaded version:
            	For i As Integer = 0 To NOrbitals - 1
					N = OrbitalQNs(i,0)
					L = OrbitalQNs(i,1)
					Solution = BoundRSESOlver.Solve(mMesh,N,L,Occupancies(N,L),Vtrial)
					mOrbitals.Add(Solution)
				Next
				
				
					'// 2a) Compute new occupancy numbers for outermost orbitals based on Rmt and
					'//     orbital solutions.  Compute the amount of charge density outside of Rmt
					'//     and increase occupancy to suit.
				If Imt < mMesh.Count-1 Then
					'//  Only need to do this if Rmt < Rmax
					For Each o As Orbital In mOrbitals
						'// Now compute the integral of Sigma from 0 to Rmt:
                    SigmaIntegral = Integration.SimpsonsRule.NonuniformArray(r, o.SigmaArray)
	        			Occupancies(o.N,o.L) = Occupancies(o.N,o.L) * mConfiguration.Occupancy(o.N,o.L) / -SigmaIntegral(Imt)
					Next
				
				End If
				
				
            	'// 3) Compute new potential from the solution orbitals
                Vnew = HermanSkillmanPotential.GetPotential(mMesh, mZ, mOrbitals, false, true)
				
            	'// 4) Check for convergence
            	DeltaVMax = 0.0
            	For i As Integer = 0 To iMax
            		DeltaV = system.Math.Abs(Vnew(i) - Vtrial(i)) / System.Math.Abs(Vnew(i) + Vtrial(i))
            		if DeltaV > DeltaVMax then DeltaVMax = DeltaV
            	Next
            	If DeltaVMax < DeltaVTolerance Then Converged = True
            	
            	nIterations += 1
            	
            	If nIterations > 200 Then
            		'throw new Exception("Failed to converge in Atom.Solve for N = " & mZ.tostring)
            		exit do
            	End If
            	
            Loop
            
            '// We've converged on a self-consistent potential.
            mPotential = Vnew

			'// Now construct the potential that an incident electron might see (same as mPotential, but without
			'// the latter tail correction and using statistical exchange)
			mIncidentPotential = HermanSkillmanPotential.GetPotential(mMesh, mZ, mOrbitals, true, false)
			mImt = Imt
			If Imt > mMesh.Count-1 Then mImt = mMesh.Count-1
			mInnerPotential = mIncidentPotential(mImt)
			
        End Sub




#End Region


    End Class
