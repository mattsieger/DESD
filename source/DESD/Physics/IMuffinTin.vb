

Public Interface IMuffinTin
	ReadOnly Property AtomicNumber() As Integer
	ReadOnly Property Element() As Element
	Readonly Property Configuration as ElectronicConfiguration
	ReadOnly Property Mesh As IRadialMesh
	ReadOnly Property Potential As Double()
	Readonly Property Radius As Double
	Readonly Property InnerPotential As Double
	ReadOnly Property Imt As Integer
	'Function IsSameSpecies(mt as IMuffinTin) as Boolean
End Interface
