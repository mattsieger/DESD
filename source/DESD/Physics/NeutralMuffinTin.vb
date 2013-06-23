Imports DESD.Math

''' <summary>
''' Represents a muffin-tin atom, its electronic wave functions, and
''' its total potential.
''' The atom is solved self-consistently using the method of Herman and Skillman, modified to produce
''' a muffin-tin potential in the spirit of Pendry, who increases the outermost orbitals' occupancy
''' in an effort to maintain charge neutrality at the muffin-tin radius.
''' </summary>
Public Class NeutralMuffinTin

	Implements IMuffinTin
	'Implements IEquatable(of NeutralMuffinTin)
	
    Private mAtom as Atom
	Private mIncidentPotential As Double()
	Private mRadius As Double
	Private mImt As Integer
	Private mInnerPotential As Double
	Private mOrbitals As New List(Of Orbital)
		
#Region "Constructors"

    ''' <summary>
    ''' Create a muffin-tin with the tabulated ground state configuration and the given muffin-tin radius.
    ''' </summary>
    ''' <param name="atomicNumber">The enumerated elemental species of the atom.</param>
    ''' <param name="muffintinradius">The radius of the muffin-tin, in Bohr radii.</param>
    Sub New(ByVal atomicNumber As Element, muffintinradius as Double)
        mAtom = New Atom(atomicNumber)
        MRadius = muffintinradius
        Solve()
    End Sub
    
    ''' <summary>
    ''' Create a muffin-tin with the tabulated ground state configuration and the given muffin-tin radius.
    ''' </summary>
    ''' <param name="Z">The atomic number of the atom.</param>
    ''' <param name="muffintinradius">The radius of the muffin-tin, in Bohr radii.</param>
    Sub New(ByVal Z as integer, muffintinradius as Double)
        mAtom = new Atom(Z)
        MRadius = muffintinradius
        Solve()
    End Sub
    
    Sub New(ByVal atomicNumber As Element, muffintinradius as Double, ByVal configuration As ElectronicConfiguration)
        mAtom = New Atom(atomicNumber, configuration)
        MRadius = muffintinradius
        Solve()
    End Sub

    Sub New(ByVal Z as integer, muffintinradius as Double, configuration as string)
        mAtom = New Atom(Z, configuration)
        MRadius = muffintinradius
        Solve()
    End Sub
    
   
    Sub New(ByVal atomicNumber As Element, muffintinradius as Double, mesh as IRadialMesh)
        mAtom = new Atom(atomicNumber, mesh)
        MRadius = muffintinradius
        Solve()
    End Sub

 

    Sub New(ByVal atomicNumber As Element, muffintinradius as Double, ByVal configuration As ElectronicConfiguration, mesh as IRadialMesh)
        mAtom = new Atom(atomicNumber, configuration, mesh)
        MRadius = muffintinradius
        Solve()
    End Sub
    
    
    
    ''' <summary>
    ''' Create a muffin-tin with the tabulated ground state configuration and the given muffin-tin radius.
    ''' </summary>
    ''' <param name="atomicNumber">The enumerated elemental species of the atom.</param>
    ''' <param name="muffintinradius">The radius of the muffin-tin, in Bohr radii.</param>
    Sub New(ByVal atomicNumber As Element, muffintinradius as Double, uniformCharge As Boolean)
        mAtom = New Atom(atomicNumber)
        MRadius = muffintinradius
        If uniformCharge Then
        	SolveUniform()
        Else
        	Solve()
        End If
        
    End Sub
    
    Sub New(ByVal atomicNumber As Element, muffintinradius as Double, cubicmeshsize As Integer)
        mAtom = New Atom(CInt(atomicNumber),cubicmeshsize)
        MRadius = muffintinradius
        Solve()
    End Sub

#End Region



#Region "Properties"

        Public ReadOnly Property AtomicNumber() As Integer implements IMuffinTin.AtomicNumber
            Get
                Return mAtom.AtomicNumber
            End Get
        End Property

        Public ReadOnly Property Element() As Element Implements IMuffinTin.Element
            Get
                Return mAtom.Element
            End Get
        End Property
        
        Public ReadOnly Property Configuration As ElectronicConfiguration Implements IMuffinTin.Configuration
        	Get
        		Return mAtom.Configuration
        	End Get
        End Property

		Public ReadOnly Property Mesh As IRadialMesh  Implements IMuffinTin.Mesh
			Get
				Return mAtom.Mesh
			end Get
		End Property

		
		Public ReadOnly Property Orbitals As List(Of Orbital)
			Get
				'return mAtom.Orbitals
				return mOrbitals
			End Get
        End Property

		Public ReadOnly Property FreeAtomOrbitals As List(Of Orbital)
			Get
				Return mAtom.orbitals
			End Get
		End Property
		
        Public ReadOnly Property GetOrbital(ByVal n As Integer, ByVal l As Integer) As Orbital
            Get
            	return mAtom.GetOrbital(n,l)
'                Dim orb = From o In mOrbitals Where (o.N = n) And (o.L = l) Take 1
'                Return orb.Single
            End Get
        End Property
		
		Public ReadOnly Property AtomicPotential As Double()
			Get
				return mAtom.Potential
'				Dim retval(mPotential.Length-1) as Double
'				system.Array.Copy(mPotential,retval,mPotential.Length)
'				Return retval
			End Get
		End Property
		
		Public ReadOnly Property Potential As Double() Implements IMuffinTin.Potential
			Get
				Dim retval(mIncidentPotential.Length-1) as Double
				system.Array.Copy(mIncidentPotential,retval,mIncidentPotential.Length)
				Return retval
			End Get
		End Property
		
		''' <summary>
		''' The muffin-tin radius, in Bohr Radii.
		''' </summary>
		Public Readonly Property Radius As Double Implements IMuffinTin.Radius
			Get
				Return mRadius
			End Get
'			Set (value as Double)
'			    if value <= 0.0 then throw new ArgumentException("Invalid muffin-tin radius.")
'				mRadius = value
'				Solve()
'			End Set
		End Property
		
		
		''' <summary>
		''' The inner potential, in Rydbergs.
		''' </summary>
		Public Readonly Property InnerPotential As Double Implements IMuffinTin.InnerPotential
			Get
				return mInnerPotential
			End Get
		End Property
		
		Public ReadOnly Property Imt As Integer Implements IMuffinTin.Imt
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

        '// Strategy:  Adjust any orbitals that have electron density
        '// outside of Rmt to have higher occupancy, such that the total
        '// electronic charge inside of Rmt is neutral.

        Dim r As Double() = mAtom.Mesh.GetArray()

        '// First, find the index of the first point in the radial mesh
        '// bigger than Rmt:
        Dim Imt As Integer = System.Array.BinarySearch(r, mRadius) Xor -1

        '// Two possibilities - either Imt is < Imax or >= to Imax.
        '// In the first case, calculate the integrals.  In the other,
        '// there is nothing to do.
        mOrbitals.Clear()
        Dim occupancy As Double
        Dim SigmaIntegral As Double()
        'Dim SigmaIntegral as Double
        Dim P As Double()
        If Imt < mAtom.Mesh.Count - 1 Then

            For Each o As Orbital In mAtom.Orbitals
                '// First, set the wave function to zero outside of Rmt:
                P = o.PArray
                For i As Integer = Imt + 1 To r.Length - 1
                    P(i) = 0.0
                Next
                '// Now compute the integral of Sigma from 0 to Rmt:
                'SigmaIntegral = Sieger.Math.Integration.SimpsonsRule.NonuniformArray(r, o.SigmaArray)
                SigmaIntegral = Integration.TrapezoidalRuleIntegrator.IntegrateArray(r, o.SigmaArray)
                occupancy = (o.Occupancy) ^ 2 / -SigmaIntegral(Imt)
                'SigmaIntegral = Sieger.Math.Integration.TrapezoidalRuleIntegrator.Integrate(r, o.SigmaArray,0,Imt)
                'occupancy = (o.Occupancy) ^ 2 / -SigmaIntegral
                mOrbitals.Add(New Orbital(o.N, o.L, occupancy, o.Energy, P))
            Next
        Else
            mOrbitals = mAtom.Orbitals
        End If


'        '// Check the orbitals for total charge:
'        '// This stuff is diagnostic:
'        
'        Dim total as double = 0.0
'        For Each o As Orbital In mOrbitals
'        	total += sieger.Math.Integration.TrapezoidalRuleIntegrator.Integrate(r,o.SigmaArray,0, Imt)'r.Length-1)
'        				
'        Next
'		Console.WriteLine("Total charge in the muffin-tin is " & total.ToString)
'
'        '// Check the orbitals for total charge:
'        total = 0.0
'        For Each o As Orbital In mAtom.Orbitals
'        	total += sieger.Math.Integration.TrapezoidalRuleIntegrator.Integrate(r,o.SigmaArray,0,r.Length-1)
'        				
'        Next
'		Console.WriteLine("Total charge in the free atom is " & total.ToString)
'


			
        '// Now construct the potential that an incident electron might see (same as mPotential, but without
        '// the latter tail correction and using statistical exchange)
        'mIncidentPotential = HermanSkillmanPotential.GetPotential(mAtom.Mesh, mAtom.AtomicNumber, mOrbitals, True, False)
        mIncidentPotential = HermanSkillmanPotential.GetPotential(mAtom.Mesh, mAtom.AtomicNumber, mOrbitals, HermanSkillmanPotential.ExchangeMode.StatisticalExchange, False)

        '// Now clean up the potential - set the zero to the inner potential zero.
        mImt = Imt
        If Imt > mAtom.Mesh.Count - 2 Then mImt = mAtom.Mesh.Count - 2

        mInnerPotential = mIncidentPotential(mImt)
        '// Add the inner potential for r <= Rmt and set the potential to zero for r > Rmt
        For i As Integer = 0 To mAtom.Mesh.Count - 1

            If i > mImt Then
                mIncidentPotential(i) = 0.0
            Else
				'// Don't forget to put this back:
                mIncidentPotential(i) -= mInnerPotential
            End If
            'mIncidentPotential(i) = 0.0

        Next

    End Sub


    ''' <summary>
    ''' Solves for the atomic potential and electronic radial wavefunctions using the method of Herman and Skillman
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SolveUniform()

        '// Strategy:  Adjust any orbitals that have electron density
        '// outside of Rmt to have higher occupancy, such that the total
        '// electronic charge inside of Rmt is neutral.

        Dim r As Double() = mAtom.Mesh.GetArray()

        '// First, find the index of the first point in the radial mesh
        '// bigger than Rmt:
        Dim Imt As Integer = System.Array.BinarySearch(r, mRadius) Xor -1

        '// Two possibilities - either Imt is < Imax or >= to Imax.
        '// In the first case, calculate the integrals.  In the other,
        '// there is nothing to do.
        mOrbitals.Clear()
        Dim occupancy As Double
        Dim SigmaIntegral As Double()
        'Dim SigmaIntegral as Double
        Dim P As Double()
        If Imt < mAtom.Mesh.Count - 1 Then

            For Each o As Orbital In mAtom.Orbitals
                '// First, set the wave function to zero outside of Rmt:
                P = o.PArray
                For i As Integer = Imt + 1 To r.Length - 1
                    P(i) = 0.0
                Next
                '// Now compute the integral of Sigma from 0 to Rmt:
                'SigmaIntegral = Sieger.Math.Integration.SimpsonsRule.NonuniformArray(r, o.SigmaArray)
                SigmaIntegral = Integration.TrapezoidalRuleIntegrator.IntegrateArray(r, o.SigmaArray)
                occupancy = (o.Occupancy) ^ 2 / -SigmaIntegral(Imt)
                'SigmaIntegral = Sieger.Math.Integration.TrapezoidalRuleIntegrator.Integrate(r, o.SigmaArray,0,Imt)
                'occupancy = (o.Occupancy) ^ 2 / -SigmaIntegral
                mOrbitals.Add(New Orbital(o.N, o.L, occupancy, o.Energy, P))
            Next
            
            'mAtom.Rho
            
            For Each o As Orbital In mAtom.Orbitals
                '// First, set the wave function to zero outside of Rmt:
                P = o.PArray
                For i As Integer = Imt + 1 To r.Length - 1
                    P(i) = 0.0
                Next
                '// Now compute the integral of Sigma from 0 to Rmt:
                'SigmaIntegral = Sieger.Math.Integration.SimpsonsRule.NonuniformArray(r, o.SigmaArray)
                SigmaIntegral = Integration.TrapezoidalRuleIntegrator.IntegrateArray(r, o.SigmaArray)
                occupancy = (o.Occupancy) ^ 2 / -SigmaIntegral(Imt)
                'SigmaIntegral = Sieger.Math.Integration.TrapezoidalRuleIntegrator.Integrate(r, o.SigmaArray,0,Imt)
                'occupancy = (o.Occupancy) ^ 2 / -SigmaIntegral
                mOrbitals.Add(New Orbital(o.N, o.L, occupancy, o.Energy, P))
            Next

        Else
            mOrbitals = mAtom.Orbitals
        End If


'        '// Check the orbitals for total charge:
'        '// This stuff is diagnostic:
'        
'        Dim total as double = 0.0
'        For Each o As Orbital In mOrbitals
'        	total += sieger.Math.Integration.TrapezoidalRuleIntegrator.Integrate(r,o.SigmaArray,0, Imt)'r.Length-1)
'        				
'        Next
'		Console.WriteLine("Total charge in the muffin-tin is " & total.ToString)
'
'        '// Check the orbitals for total charge:
'        total = 0.0
'        For Each o As Orbital In mAtom.Orbitals
'        	total += sieger.Math.Integration.TrapezoidalRuleIntegrator.Integrate(r,o.SigmaArray,0,r.Length-1)
'        				
'        Next
'		Console.WriteLine("Total charge in the free atom is " & total.ToString)
'


			
        '// Now construct the potential that an incident electron might see (same as mPotential, but without
        '// the latter tail correction and using statistical exchange)
        'mIncidentPotential = HermanSkillmanPotential.GetPotential(mAtom.Mesh, mAtom.AtomicNumber, mOrbitals, True, False)
        mIncidentPotential = HermanSkillmanPotential.GetPotential(mAtom.Mesh, mAtom.AtomicNumber, mOrbitals, HermanSkillmanPotential.ExchangeMode.StatisticalExchange, False)

        '// Now clean up the potential - set the zero to the inner potential zero.
        mImt = Imt
        If Imt > mAtom.Mesh.Count - 2 Then mImt = mAtom.Mesh.Count - 2

        mInnerPotential = mIncidentPotential(mImt)
        '// Add the inner potential for r <= Rmt and set the potential to zero for r > Rmt
        For i As Integer = 0 To mAtom.Mesh.Count - 1

            If i > mImt Then
                mIncidentPotential(i) = 0.0
            Else
				'// Don't forget to put this back:
                mIncidentPotential(i) -= mInnerPotential
            End If
            'mIncidentPotential(i) = 0.0

        Next

    End Sub


#End Region


#Region "Public Methods"

	Public Function IsSameSpecies(mt As IMuffinTin) As Boolean 'Implements IMuffinTin.IsSameSpecies
		If (Me.AtomicNumber <> mt.AtomicNumber) Then Return False
		If (Me.Configuration.ToString <> mt.Configuration.ToString) Then Return False
		If (Me.Radius <> mt.Radius) Then Return False
		Return true
	End Function
	
'	Public ReadOnly Property Configuration As ElectronicConfiguration Implements IMuffinTin.Configuration
'		Get
'			return mAtom.Configuration
'		End Get
'	End Property
	
#End Region

End Class
