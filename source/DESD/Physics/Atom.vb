
''' <summary>
''' Represents an atom, its electronic wave functions, and
''' its total potential.
''' </summary>
Public Class Atom
	
	Implements IAtom
	
        Private mZ As Element
        Private mMesh As IRadialMesh
        Private mConfiguration As ElectronicConfiguration
		Private mOrbitals As New List(Of Orbital)
		Private mPotential as Double()
        Private Delegate Function SolveDelegate(ByVal m As IRadialMesh, ByVal N As Integer, ByVal L As Integer, ByVal occ As Double, ByVal Pot As Double()) As Orbital
		Private mPotentialSansExchange As Double()
		Private mRho As Double()
		Private mNMeshPoints as Integer = 650
		
#Region "Constructors"

        ''' <summary>
        ''' Create an atom with the ground state configuration.
        ''' </summary>
        ''' <param name="atomicNumber"></param>
        ''' <remarks></remarks>
    Sub New(ByVal atomicNumber As Element)
        mZ = atomicNumber
        mConfiguration = New ElectronicConfiguration(atomicNumber)
        'mMesh = New HermanSkillmanMesh(mZ)
        mMesh = New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(mZ) * 205.0, mNMeshPoints)
        Solve(false)
    End Sub

    Sub New(ByVal Z as integer)
        mZ = ctype(Z,element)
        mConfiguration = New ElectronicConfiguration(mZ)
        'mMesh = New HermanSkillmanMesh(mZ)
        mMesh = New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(mZ) * 205.0, mNMeshPoints)
        Solve(false)
    End Sub
    
    
    Sub New(ByVal Z as integer, configuration as string)
        mZ = ctype(Z,element)
        mConfiguration = New ElectronicConfiguration(configuration)
        'mMesh = New HermanSkillmanMesh(mZ)
        mMesh = New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(mZ) * 205.0, mNMeshPoints)
        Solve(false)
    End Sub
    
    Sub New(ByVal Z as integer, multithreaded as boolean)
        mZ = ctype(Z,element)
        mConfiguration = New ElectronicConfiguration(mZ)
        'mMesh = New HermanSkillmanMesh(mZ)
        mMesh = New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(mZ) * 205.0, mNMeshPoints)
        Solve(multithreaded)
    End Sub
    
    Sub New(ByVal atomicNumber As Element, mesh as IRadialMesh)
        mZ = atomicNumber
        mConfiguration = New ElectronicConfiguration(atomicNumber)
        mMesh = mesh
        Solve(false)
    End Sub

    Sub New(ByVal Z as integer, cubicmeshsize as integer)
        mZ = CType(Z,Element)
        mConfiguration = New ElectronicConfiguration(mZ)
        mMesh = New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(mZ) * 205.0, cubicmeshsize)
        Console.WriteLine("Mesh size is " & cubicmeshsize.ToString)
        Solve(false)
    End Sub
    
    Sub New(ByVal atomicNumber As Element, ByVal configuration As ElectronicConfiguration)
        mZ = atomicNumber
        mConfiguration = configuration
        'mMesh = New HermanSkillmanMesh(mZ)
        mMesh = New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(mZ) * 205.0, mNMeshPoints)
        Solve(false)
    End Sub

    Sub New(ByVal atomicNumber As Element, ByVal configuration As ElectronicConfiguration, mesh as IRadialMesh)
        mZ = atomicNumber
        mConfiguration = configuration
        mMesh = mesh
        Solve(false)
    End Sub


#End Region


#Region "Properties"

		''' <summary>
		''' Returns the atomic number (number of protons) of the atom.
		''' </summary>
        Public ReadOnly Property AtomicNumber() As Integer Implements IAtom.AtomicNumber
            Get
                Return CInt(mZ)
            End Get
        End Property

		''' <summary>
		''' Returns the value of the Element enumeration corresponding to the chemical element of the atom.
		''' </summary>
        Public ReadOnly Property Element() As Element Implements IAtom.Element
            Get
                Return mZ
            End Get
        End Property


		Public ReadOnly Property Mesh As IRadialMesh Implements IAtom.Mesh
			Get
				Return mMesh
			end Get
		End Property

		Public ReadOnly Property Configuration As ElectronicConfiguration Implements IAtom.Configuration
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
		
		Public ReadOnly Property Potential As Double() Implements IAtom.Potential
			Get
				Dim retval(mPotential.Length-1) as Double
				system.Array.Copy(mPotential,retval,mPotential.Length)
				Return retval
			End Get
		End Property
		
		Public ReadOnly Property PotentialSansExchange As Double()
			Get
				Dim retval(mPotentialSansExchange.Length-1) as Double
				system.Array.Copy(mPotentialSansExchange,retval,mPotentialSansExchange.Length)
				Return retval
			End Get
		End Property
		
		''' <summary>
		''' The spherically averaged total electronic charge density, tabulated on the radial mesh.
		''' </summary>
		Public ReadOnly Property Rho As Double() Implements IAtom.Rho
			Get
				Dim retval(mRho.Length-1) as Double
				system.Array.Copy(mRho,retval,mRho.Length)
				Return retval
			End Get
		End Property
		
#End Region


#Region "Private Methods"

        ''' <summary>
        ''' Solves for the atomic potential and electronic radial wavefunctions using the method of Herman and Skillman
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Solve(usethreading as boolean)
        	
        	'// First, get a list of the (n,l) values and occupancies from the Configuration:
        	Dim NOrbitals As Integer = mConfiguration.OrbitalCount
        	Dim OrbitalQNs(,) As Integer = mConfiguration.GetQuantumNumbers
        	        	
			Dim iMax As Integer = mMesh.Count-1
			
			Dim Vtrial() As Double
			Dim Vnew(iMax) As Double
			Dim Solution As Orbital
			Dim N As Integer
			Dim L As Integer
			Dim Occupancy as Double
            Dim Converged As Boolean = False
            Dim DeltaV as Double
            Dim DeltaVMax as Double
            Dim DeltaVtolerance As Double = 0.0001
			Dim nIterations As Integer = 0
			
            Dim delSolve As New SolveDelegate(AddressOf BoundRSESolver.Solve)
            Dim AsyncResults As New List(Of IAsyncResult)
            Dim result As System.Runtime.Remoting.Messaging.AsyncResult
        	Dim del As SolveDelegate    
            Dim isDone As Boolean()
            Dim NDone as integer
            Dim AR As IAsyncResult
            
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
            	
            	If usethreading Then
	            	AsyncResults.clear
					For i As Integer = 0 To NOrbitals - 1
						N = OrbitalQNs(i,0)
						L = OrbitalQNs(i,1)
						Occupancy = mConfiguration.Occupancy(N,L)
						AsyncResults.Add(delSolve.BeginInvoke(mMesh,  N, L, Occupancy, Vtrial, nothing, Nothing))					
					Next
					
					'// Wait for solvers to finish and gather results:
					ReDim IsDone(AsyncResults.Count-1)
					NDone = 0
					Do until NDone = AsyncResults.count
						For i As Integer = 0 To AsyncResults.Count-1
							AR = AsyncResults.Item(i)
							If (AR.IsCompleted and not IsDone(i)) Then
		             			'// First, cast ar as an AsyncResult:
		           				result = CType(AR, System.Runtime.Remoting.Messaging.AsyncResult)
					            '// Grab the delegate
					            del = CType(result.AsyncDelegate, SolveDelegate)
					            '// Now that we have the delegate, we have to call EndInvoke on it
					            '// so we can get our return object:
					            Solution = del.EndInvoke(ar)
	                			mOrbitals.Add(solution)
	                			IsDone(i) = True
	                			NDone += 1
			                End If
						Next
					Loop
					
            	Else
            		For i As Integer = 0 To NOrbitals - 1
						N = OrbitalQNs(i,0)
						L = OrbitalQNs(i,1)
						Occupancy = mConfiguration.Occupancy(N,L)
						Solution = BoundRSESOlver.Solve(mMesh,N,L,Occupancy,Vtrial)
						mOrbitals.Add(Solution)
					Next
            	End If
				

				
            	'// 3) Compute new potential from the solution orbitals
                Vnew = HermanSkillmanPotential.GetPotential(mMesh, mZ, mOrbitals, HermanSkillmanPotential.ExchangeMode.NonStatisticalExchange,true)
				
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

			'// Do one more calculation to get the potential sans exchange and the charge density:
			Dim details as AtomicPotential = HermanSkillmanPotential.GetPotentialDetails(mMesh, mZ, mOrbitals, hermanskillmanpotential.ExchangeMode.NoExchange, True)

			mPotentialSansExchange = details.V
			mRho = details.Rho
		
        End Sub




#End Region


    End Class
