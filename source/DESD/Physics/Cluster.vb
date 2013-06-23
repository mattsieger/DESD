Imports System.Collections.Generic
Imports DESD.Math
'Imports Sieger.Math.Types

''' <summary>
''' Represents a cluster of atoms, approximating a solid or surface.
''' </summary>
Public Class Cluster
	
	Private _Data As New DataSet
	
	Private _Atoms As DataTable
	Private _Species As DataTable
	Private _PhaseShifts As New SortedList(Of Integer, Complex())
	Private _Tmatrices as New SortedList(Of Integer, Complex())
	
	Private Const COL_ATOMID as String = "AtomID"
	Private Const COL_SPECIESID As String = "SpeciesID"
	Private Const COL_POSITIONX As String = "PositionX"
	Private Const COL_POSITIONY As String = "PositionY"
	Private Const COL_POSITIONZ As String = "PositionZ"
	Private Const COL_ENABLED as String = "Enabled"
	Private Const COL_ATOMICNUMBER As String = "AtomicNumber"
	Private Const COL_CONFIGURATION As String = "Configuration"
	Private Const COL_RMT as String = "Rmt"
	
		
#Region "Constructors"

	''' <summary>
	''' Create an empty cluster.
	''' </summary>
	Sub New()
		CreateInternalDataTables()
	End Sub
		
	''' <summary>
	''' Create a cluster from an XML file
	''' </summary>
	''' <param name="clusterXML"></param>
	Sub New(fileName As String)
		CreateInternalDataTables()
		Call ReadXml(fileName)
	End Sub
	
#End Region


#Region "Private Methods"

	''' <summary>
	''' Creates the internal data tables for storing cluster atom and species information:
	''' </summary>
	Private Sub CreateInternalDataTables()
		_Data = New DataSet()
		
		_Atoms = New DataTable("Atoms")
		
		With _Atoms.Columns
			.Add(COL_ATOMID,GetType(Integer))
			.Add(COL_SPECIESID,GetType(Integer))
			.Add(COL_POSITIONX,GetType(Double))
			.Add(COL_POSITIONY,GetType(Double))
			.Add(COL_POSITIONZ,GetType(Double))
			.Add(COL_ENABLED,GetType(Boolean))
		End With
		
		Dim keys() as DataColumn = {_Atoms.Columns(COL_ATOMID)}
		_Atoms.PrimaryKey = keys
		
		With _Atoms.Columns(COL_ATOMID)
			.AutoIncrement = True
			.Unique = True
		End With
		
		With _Atoms
			.Columns(COL_SPECIESID).AllowDBNull = False
			.Columns(COL_POSITIONX).AllowDBNull = False
			.Columns(COL_POSITIONY).AllowDBNull = False
			.Columns(COL_POSITIONZ).AllowDBNull = False
			.Columns(COL_ENABLED).AllowDBNull = False
		End With
	
		_Data.Tables.Add(_Atoms)
		
		_Species = New DataTable("Species")
		With _Species.Columns
			.Add(COL_SPECIESID,GetType(Integer))
			.Add(COL_ATOMICNUMBER,GetType(Integer))
			.Add(COL_CONFIGURATION,GetType(String))
			.Add(COL_RMT,GetType(Double))
		End With
		
		Dim specieskeys() as DataColumn = {_Species.Columns(COL_SPECIESID)}
		_Species.PrimaryKey = specieskeys
		
		With _Species.Columns(COL_SPECIESID)
			.AutoIncrement = True
			.Unique = True
		End With
		
		With _Species
			.Columns(COL_ATOMICNUMBER).AllowDBNull = False
		End With

		_Data.Tables.Add(_Species)
		
	End Sub


#End Region
	
	
#Region "Public Methods"

	''' <summary>
	''' Returns the number of atoms in the cluster.
	''' </summary>
	Public ReadOnly Property Count As Integer
		Get
			Return _Atoms.Rows.Count
		End Get
	End Property
	
	''' <summary>
	''' Returns the number of individual atomic species defined in the cluster.
	''' </summary>
	Public ReadOnly Property SpeciesCount As Integer
		Get
			Return _Species.Rows.Count
		End Get
	End Property

	''' <summary>
	''' Adds an atom to the cluster.  Note that coordinates are in Angstrom units.
	''' </summary>
	''' <param name="x">The x coordinate of the atom [Angstroms]</param>
	''' <param name="y">The x coordinate of the atom [Angstroms]</param>
	''' <param name="z">The x coordinate of the atom [Angstroms]</param>
	''' <param name="species">The SpeciesID of the atom to add.</param>
	''' <returns>The AtomID of the new atom.</returns>
	Public Overloads Function AddAtom(x As Double, y As Double, z As Double, species As Integer) As Integer
		
		Dim newRow As DataRow = _Atoms.NewRow
		newRow(COL_SPECIESID) = species
		newRow(COL_POSITIONX) = x
		newRow(COL_POSITIONY) = y
		newRow(COL_POSITIONZ) = z
		newRow(COL_ENABLED) = TRUE

		_Atoms.Rows.Add(newRow)
		_Atoms.AcceptChanges
		
		Return newRow(COL_ATOMID)
		
	End Function
	
	''' <summary>
	''' Adds an atom to the cluster.  Note that coordinates are in Angstrom units.
	''' </summary>
	''' <param name="position">A Vector object containing the atom coordinates, in Angstroms.</param>
	''' <param name="species">The SpeciesID of the atom to add.</param>
	''' <returns></returns>
	Public Overloads Function AddAtom(position As Vector, species as Integer) As Integer
		Return AddAtom(position.X, position.Y, position.Z, species)
	End Function

	''' <summary>
	''' Gets or sets the Enabled property of the given atom.  Atoms that are disabled are effectively removed from the cluster
	''' for the purposes of scattering calculations.
	''' </summary>
	Public Property AtomEnabled(atomID As Integer) As Boolean
		Get
			If Not _Atoms.Rows.Contains(atomid) Then Throw New ArgumentException("Invalid atom ID")
			Dim row As DataRow = _Atoms.Rows.Find(atomID)
			Return row(COL_ENABLED)
		End Get
		Set (value As Boolean)
			If Not _Atoms.Rows.Contains(atomid) Then Throw New ArgumentException("Invalid atom ID")
			Dim row As DataRow = _Atoms.Rows.Find(atomID)
			row(COL_ENABLED) = Value
			_Atoms.AcceptChanges
		End Set
	End Property
	
	''' <summary>
	''' Permanently removes the specified atom from the cluster.
	''' </summary>
	''' <param name="atomid"></param>
	Public Sub RemoveAtom(atomid As Integer)
		'// Check to make sure this atomID is valid:
		If Not _Atoms.Rows.Contains(atomid) Then Throw New ArgumentException("Invalid atom ID")
		
		Dim row As DataRow = _Atoms.Rows.Find(atomID)
		row.Delete
		_Atoms.AcceptChanges
		
	End Sub
	
	
	''' <summary>
	''' Returns a reference to an (free) ATOM instance corresponding to the element of the given atomid.
	''' </summary>
	''' <param name="atomid"></param>
	''' <returns></returns>
	Public Default ReadOnly Property GetAtom(atomid As Integer) As ClusterAtom
		Get
			'// Check to make sure this atomID is valid:
			If Not _Atoms.Rows.Contains(atomid) Then Throw New ArgumentException("Invalid atom ID")
			
			'// Get the datarow for the atom:
			Dim row As DataRow = _Atoms.Rows.Find(atomID)
			
			'// Extract the position and speciesID data from the datarow:
			Dim position As New Vector(row(COL_POSITIONX),row(COL_POSITIONY),row(COL_POSITIONZ))
			Dim speciesID As Integer = row(COL_SPECIESID)
			
			'Dim speciesRow As DataRow
			Dim phaseshifts As Complex()
			Dim tmatrix As Complex()
			Dim ca As ClusterAtom
			
			If _PhaseShifts.ContainsKey(speciesID) Then
				phaseshifts = _PhaseShifts(speciesID)
			 	tmatrix= _Tmatrices(speciesID)
			 	ca = New ClusterAtom(atomid,speciesID,position,phaseshifts,tmatrix)
			Else
				ca = new ClusterAtom(atomid,speciesID,position)
			End If

			Return ca
			
		End Get
	End Property
	
	''' <summary>
	''' Gets/Sets the species ID of the specified atom.
	''' </summary>
	Public Property Species(atomID As Integer) As Integer
		Get
			If Not _Atoms.Rows.Contains(atomid) Then Throw New ArgumentException("Invalid atom ID")
			Dim row As DataRow = _Atoms.Rows.Find(atomID)
			Return row(COL_SPECIESID)
		End Get
		Set (value As Integer)
			If Not _Atoms.Rows.Contains(atomid) Then Throw New ArgumentException("Invalid atom ID")
			Dim row As DataRow = _Atoms.Rows.Find(atomID)
			row(COL_SPECIESID) = value
			_Atoms.AcceptChanges
		End Set
	End Property
	
	''' <summary>
	''' Adds a ClusterSpecies with the given properties to the cluster.
	''' </summary>
	''' <param name="Z">The element of the atomic species.</param>
	''' <param name="Rmt">The muffin-tin radius of the atomic species, in [Angstroms].</param>
	''' <param name="configuration">The electronic configuration of the species.</param>
	''' <returns>Returns the speciesID of the new species.</returns>
	Public Overloads Function AddSpecies(Z As Element, Rmt As Double, configuration As String) As Integer
		
		If (Rmt <= 0.0) Then Throw New ArgumentException("Rmt must be greater than zero.")
		
		'// Validate configuration - if invalid, an exception will be thrown here:
		if Not string.IsNullOrEmpty(configuration) then
			Dim config As New ElectronicConfiguration(configuration)
		end if
		
		Dim newRow As DataRow = _Species.NewRow
		newRow(COL_ATOMICNUMBER) = CInt(Z)
		newRow(COL_CONFIGURATION) = configuration
		newRow(COL_RMT) = Rmt
		
		_Species.Rows.Add(newRow)
		_Species.AcceptChanges

		Return newRow(COL_SPECIESID)
		
	End Function
	
	''' <summary>
	''' Adds a ClusterSpecies with the given properties to the cluster.  The electronic configuration is assumed
	''' to be the default configuration for the element.
	''' </summary>
	''' <param name="Z">The element of the atomic species.</param>
	''' <param name="Rmt">The muffin-tin radius of the atomic species, in [Angstroms].</param>
	''' <returns></returns>
	Public Overloads Function AddSpecies(Z As Element, Rmt As Double) As Integer
		Return AddSpecies(Z,Rmt,"")
	End Function
	
	''' <summary>
	''' Retrieves species data.
	''' </summary>
	''' <param name="speciesID"></param>
	''' <returns>A ClusterSpecies structure containing data regarding the species.</returns>
	Public Function GetSpecies(speciesID As Integer) As ClusterSpecies
		
		If Not _Species.Rows.Contains(speciesID) Then Throw New ArgumentException("Invalid species ID")

		Dim speciesRow As DataRow = _Species.Rows.Find(speciesID)
		Dim Z As Element = CType(speciesRow(COL_ATOMICNUMBER),Element)
		Dim config As String = speciesRow(COL_CONFIGURATION)
		Dim Rmt as Double = speciesRow(COL_RMT)
		Return New ClusterSpecies(Z, config, Rmt)
		
	End Function
	
	''' <summary>
	''' Removes the specified species from the cluster.
	''' </summary>
	''' <param name="speciesID"></param>
	Public Sub RemoveSpecies(speciesID As Integer)
		If Not _Species.Rows.Contains(speciesID) Then Throw New ArgumentException("Invalid species ID")

		'// Remove the species from the _Species table
		Dim speciesRow As DataRow = _Species.Rows.Find(speciesID)
		_Species.Rows.Remove(speciesRow)
		_Species.AcceptChanges
		
		'// Remove any phase shifts or tmatrices associated with this species:
		If _PhaseShifts.ContainsKey(speciesID) Then
			_PhaseShifts.Remove(speciesID)
			_Tmatrices.Remove(speciesID)
		End If
		
	End Sub
	
	''' <summary>
	'''
	''' </summary>
    Public Property PhaseShifts(speciesID As Integer) As Complex()
        'TODO:  This method needs to be abstracted from this class
        Get
            Dim Retval As Complex() = {}
            If _PhaseShifts.ContainsKey(speciesID) Then
                Retval = _PhaseShifts(speciesID)
            End If

            Return Retval
        End Get
        Set(value As Complex())
            If _PhaseShifts.ContainsKey(speciesID) Then
                _PhaseShifts.Remove(speciesID)
                _Tmatrices.Remove(speciesID)
            End If
            _PhaseShifts.Add(speciesID, value)
            '// Compute T-matrix for these phase shifts:
            Dim tmatrix(value.Length - 1) As Complex
            Dim im As New Complex(0.0, 1.0)
            For i As Integer = 0 To value.Length - 1
                tmatrix(i) = Complex.CExp(im * value(i)) * Complex.CSin(value(i))
            Next
            _Tmatrices.Add(speciesID, tmatrix)
        End Set
    End Property
	
	''' <summary>
	'''
	''' </summary>
	''' <param name="atomID"></param>
	''' <returns></returns>
	Public Function GetPhaseShifts(atomID As Integer) As Complex()
        'TODO:  This method needs to be abstracted from this class

		If Not _Atoms.Rows.Contains(atomid) Then Throw New ArgumentException("Invalid atom ID")

		Dim row As DataRow = _Atoms.Rows.Find(atomID)
		Dim speciesID As Integer = row(COL_SPECIESID)
		
		Dim Retval As Complex() = {}
		If _PhaseShifts.ContainsKey(speciesID) Then
			Retval = _PhaseShifts(speciesID)
		End If
		
		Return Retval
		
	End Function
	
	Public Readonly Property Tmatrix(speciesID As Integer) As Complex()
        'TODO:  This method needs to be abstracted from this class
        Get
            Dim Retval As Complex() = {}

            If _Tmatrices.ContainsKey(speciesID) Then
                Retval = _Tmatrices(speciesID)
            End If

            Return Retval

        End Get
	End Property


	''' <summary>
	'''
	''' </summary>
	''' <param name="atomid"></param>
	''' <returns></returns>
	Public Function GetTMatrix(atomid As Integer) As Complex()
        'TODO:  This method needs to be abstracted from this class

		If Not _Atoms.Rows.Contains(atomid) Then Throw New ArgumentException("Invalid atom ID")

		Dim row As DataRow = _Atoms.Rows.Find(atomID)
		Dim speciesID As Integer = row(COL_SPECIESID)
		
		Dim Retval As Complex() = {}
			
		If _Tmatrices.ContainsKey(speciesID) Then
			Retval = _Tmatrices(speciesID)
		End If
		
		Return Retval

	End Function

	''' <summary>
	''' Returns a SortedList containing the nearest neighbor atom IDs and the radial distance to the central atomID,
	''' for all atoms in the cluster with distance from the origin (set at the atom with ID = atomid) less than or
	''' equal to Rmax.
	''' </summary>
	''' <param name="atomid"></param>
	''' <param name="rmax"></param>
	''' <returns></returns>
	Public Function GetNearestNeighbors(atomid As Integer, rmax as double) As SortedList(Of Integer, Double)
		
		'// First, get a reference to the ClusterAtom at the origin:
		Dim originAtom As ClusterAtom = Me.GetAtom(atomid)
		Dim R0 As Vector = originAtom.Position
		
		'// Loop through all atoms in the cluster (skipping the origin) and compute the
		'// distance |R - R0|.  If d <= rmax, then add it to the output list:
		Dim retval As New SortedList(Of Integer, Double)
		Dim d As Double
		Dim ids As List(Of Integer) = Me.GetAtomIDs
		For Each id As Integer In ids
			If id <> atomid Then
				d = Vector.Abs(Me.GetPosition(id) - R0)
				If (d <= rmax) and (d > 0.0) Then
					retval.Add(id,d)
				End If
			End If
		Next
		
		Return retval
		
	End Function
	
	''' <summary>
	''' Returns the muffin-tin radius (one-half of the smallest nearest-neighbor distance) of the indicated atom in the cluster.
	''' </summary>
	''' <param name="atomid"></param>
	''' <returns></returns>
	Public Function HalfNeighborRadius(atomid As Integer) As Double
		'// First, get a reference to the ClusterAtom at the origin:
		Dim originAtom As ClusterAtom = Me.GetAtom(atomid)
		Dim R0 As Vector = originAtom.Position
		
		'// Loop through all atoms in the cluster (skipping the originAtom) and compute the
		'// distance |R - R0|.  If d <= dmax, then record it and move on:
		Dim dmin As Double = Double.MaxValue
		Dim d As Double
		Dim ids As List(Of Integer) = Me.GetAtomIDs
		For Each id As Integer In ids
			If id <> atomid Then
				'console.WriteLine("id = " & id.ToString)
				d = Vector.Abs(Me.GetPosition(id) - R0)
				If (d < dmin) and (d > 0.0) Then
					dmin = d
					'console.WriteLine("dmin = " & dmin.ToString)
				End If
			End If
		Next
		
		Return dmin / 2.0
		
	End Function
	
	''' <summary>
	'''
	''' </summary>
	''' <returns></returns>
	Public Function GetAtomIDs As List(Of Integer)
		Dim retval As New List(Of Integer)
		Dim row as DataRow
		For i As Integer = 0 To _Atoms.Rows.Count-1
			row = _Atoms.Rows(i)
			retval.Add(row(COL_ATOMID))
		Next
		Return retval
	End Function

	''' <summary>
	'''
	''' </summary>
	''' <returns></returns>
	Public Function GetSpeciesIDs As List(Of Integer)
		Dim retval As New List(Of Integer)
		Dim row as DataRow
		For i As Integer = 0 To _Species.Rows.Count-1
			row = _Species.Rows(i)
			retval.Add(row(COL_SPECIESID))
		Next
		Return retval
	End Function

	''' <summary>
	'''
	''' </summary>
	''' <param name="atomid"></param>
	''' <returns></returns>
	Public Function GetPosition(atomid As Integer) As Vector
		If Not _Atoms.Rows.Contains(atomid) Then Throw New ArgumentException("Invalid atom ID")

		Dim row As DataRow = _Atoms.Rows.Find(atomID)
		Dim position As New Vector(row(COL_POSITIONX),row(COL_POSITIONY),row(COL_POSITIONZ))
		Return position
	End Function
	
	''' <summary>
	''' Reads the cluster data from the specified file.
	''' </summary>
	''' <param name="filename"></param>
	Public Sub ReadXml(filename As String)
		_Data.ReadXml(filename)
		
		
	End Sub
	
	''' <summary>
	''' Writes the cluster data to the specified file.
	''' </summary>
	''' <param name="filename"></param>
	Public Sub WriteXml(filename As String)
		_Data.WriteXml(filename)
	End Sub
	
'	''' <summary>
'	'''
'	''' </summary>
'	''' <param name="path"></param>
'	''' <returns></returns>
'	Public Function GetPathLength(path As MSPath) As Double
'		Dim atomids As List(Of Integer) = path.GetAtomIDs
'		Dim first As Integer = atomids(0)
'		Dim second As Integer
'		Dim length As Double = 0.0
'		dim d as double
'		For i As Integer = 1 To atomids.Count-1
'			second = atomids(i)
'			d = Vector.Abs(Me.GetPosition(second) - Me.GetPosition(first))
'			length += d
'			first = second
'		Next
'		
'		Return length
'	
'	End Function
		
#End Region
	

	
End Class
