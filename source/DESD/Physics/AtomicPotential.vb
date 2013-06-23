


Public Class AtomicPotential

	Private _mesh As IRadialMesh
	Private _V As Double()
	Private _Rho as Double()
	
	Sub New(mesh As IRadialMesh, potential As Double(), chargedensity As Double())
		_mesh = mesh
		_V = potential
		_Rho = chargedensity
	End Sub
	
	Public ReadOnly Property V As Double()
		Get
			return _V
		End Get
	End Property
	
	Public ReadOnly Property Rho As Double()
		Get
			return _Rho
		End Get
	End Property
	
End Class
