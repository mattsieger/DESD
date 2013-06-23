




''' <summary>
''' Provides a base class for implementation of solvers for free (non muffin-tin) atoms.
''' </summary>
'Public MustInherit Class AtomBase
'	
'	Private mZ As Element
'	Private mConfiguration As ElectronicConfiguration 
'	
'	Private mMesh As IRadialMesh
'	
'    Private mPotential as Double()
'    Private mRho As Double()
'            
'    		
'#Region "Constructors"
'
'    ''' <summary>
'    ''' Create an atom with the default ground state configuration.
'    ''' </summary>
'    ''' <param name="atomicNumber"></param>
'    ''' <remarks></remarks>
'    Sub New(ByVal atomicNumber As Element)
'        mZ = atomicNumber
'        mConfiguration = New ElectronicConfiguration(atomicNumber)
'    End Sub  
'    
'    
'    Sub New(ByVal atomicNumber As Element, ByVal configuration As ElectronicConfiguration)
'        mZ = atomicNumber
'        mConfiguration = configuration
'    End Sub
'
'
'#End Region
'
'
'#Region "Properties"
'
'		''' <summary>
'		''' Returns the atomic number (number of protons) of the atom.
'		''' </summary>
'        Public ReadOnly Property AtomicNumber() As Integer
'            Get
'                Return CInt(mZ)
'            End Get
'        End Property
'
'		''' <summary>
'		''' Returns the value of the Element enumeration corresponding to the chemical element of the atom.
'		''' </summary>
'        Public ReadOnly Property Element() As Element
'            Get
'                Return mZ
'            End Get
'        End Property
'
'
'		Public ReadOnly Property Mesh As IRadialMesh
'			Get
'				Return mMesh
'			end Get
'		End Property
'
'		Public ReadOnly Property Configuration As ElectronicConfiguration
'			Get
'				Return mConfiguration
'			End Get
'		End Property
'		
'		Public ReadOnly Property Orbitals As List(Of Orbital)
'			Get
'				return mOrbitals
'			End Get
'        End Property
'
'        Public ReadOnly Property GetOrbital(ByVal n As Integer, ByVal l As Integer) As Orbital
'            Get
'                Dim orb = From o In mOrbitals Where (o.N = n) And (o.L = l) Take 1
'                Return orb.Single
'            End Get
'        End Property
'        
'        ''' <summary>
'        ''' The radial potential, tabulated on the mesh.
'        ''' </summary>
'		Public ReadOnly Property Potential As Double()
'			Get
'				Dim retval(mPotential.Length-1) as Double
'				system.Array.Copy(mPotential,retval,mPotential.Length)
'				Return retval
'			End Get
'		End Property
'		
'		''' <summary>
'		''' The spherically averaged total electronic radial charge density, tabulated on the mesh.
'		''' </summary>
'		Public ReadOnly Property Rho As Double()
'			Get
'				Dim retval(mRho.Length-1) as Double
'				system.Array.Copy(mRho,retval,mRho.Length)
'				Return retval
'			End Get
'		End Property
'		
'#End Region
'
'
'
'End Class
