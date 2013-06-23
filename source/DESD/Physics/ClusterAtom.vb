Imports DESD.Math

Public Class ClusterAtom
	
	Private _ID As Integer
	Private _Position As Vector
	Private _SpeciesID As Integer
	Private _PhaseShifts As Complex() = {}
	Private _Tmatrix as Complex() = {}
		
#Region "Constructors"

	Public Sub New(ID As Integer, speciesID as integer, coordinates As Vector)
		_ID = ID
		_SpeciesID = speciesID
		_Position = new Vector(coordinates.X,coordinates.Y,coordinates.z)
	End Sub
	
	Public Sub New(ID As Integer, speciesID as integer, X as Double, Y as Double, Z as double)
		_ID = ID
		_SpeciesID = speciesID
		_Position = new Vector(X, Y, Z)
	End Sub
	
	Public Sub New(ID As Integer, speciesID as integer, coordinates As Vector, phaseshifts as Complex(), tmatrix as Complex())
		_ID = ID
		_SpeciesID = speciesID
		_Position = new Vector(coordinates.X,coordinates.Y,coordinates.Z)
		_PhaseShifts = phaseshifts
		_Tmatrix = tmatrix
	End Sub

#End Region


#Region "Public Properties"
	
	Public Readonly Property ID As Integer
		Get
			return _ID
		End Get
	End Property
	
	Public Readonly Property Species As Integer
		Get
			return _SpeciesID
		End Get
	End Property
	
	Public ReadOnly Property Position As Vector
		Get
			return _Position
		End Get
	End Property
	
	Public ReadOnly Property PhaseShifts As Complex()
		Get
			return _PhaseShifts
		End Get
	End Property
	
	Public ReadOnly Property Tmatrix As Complex()
		Get
			Return _Tmatrix
		End Get
	End Property
	
#End Region



End Class
