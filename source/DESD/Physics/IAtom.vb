


Public Interface IAtom
	
		''' <summary>
		''' Returns the atomic number (number of protons) of the atom.
		''' </summary>
        ReadOnly Property AtomicNumber() As Integer

		''' <summary>
		''' Returns the value of the Element enumeration corresponding to the chemical element of the atom.
		''' </summary>
        ReadOnly Property Element() As Element

		''' <summary>
		''' Returns a reference to the radial mesh.
		''' </summary>
		ReadOnly Property Mesh As IRadialMesh
		
		''' <summary>
		''' Returns an ElectronicConfiguration object containing the electronic configuration information of the atom.
		''' </summary>
		ReadOnly Property Configuration As ElectronicConfiguration
		
		
'		Public ReadOnly Property Orbitals As List(Of Orbital)
'		
'		
'        Public ReadOnly Property GetOrbital(ByVal n As Integer, ByVal l As Integer) As Orbital
        
        ''' <summary>
        ''' The radial potential, tabulated on the mesh.
        ''' </summary>
		ReadOnly Property Potential As Double()
		
		''' <summary>
		''' The spherically averaged total electronic radial charge density, tabulated on the mesh.
		''' </summary>
		ReadOnly Property Rho As Double()
		
		
End Interface
