Imports DESD.Math

''' <summary>
'''
''' </summary>
Public Class MSPath
	
	Implements IComparable(Of MSPath)
	
	Private _TerminalAtom as ClusterAtom
	Private _ParentPath as MSPath
	Private _PathLength as Double
	Private _RootAmplitude As ComplexMatrix
	Private _Amplitude As ComplexMatrix
	
#Region "Constructors"
	
	''' <summary>
	''' Create a new path containing one atom.
	''' </summary>
	''' <param name="c">A reference to the cluster the atomid belongs to.</param>
	''' <param name="atomid">The ID of the atom to add to the cluster.</param>
	Sub New(c As Cluster, atomid As Integer)
		Me.New(c.GetAtom(atomid))
	End Sub
	
	
	''' <summary>
	''' Creates a new path containing one atom.
	''' </summary>
	''' <param name="atom"></param>
	Sub New(atom as ClusterAtom)
		_TerminalAtom = atom
		_ParentPath = Nothing
		_PathLength = 0.0
	End Sub


	''' <summary>
	''' Creates a new single-scattering (order = 1) path with atomid1 as the absorber.
	''' </summary>
	''' <param name="c"></param>
	''' <param name="atomid1"></param>
	''' <param name="atomid2"></param>
	Sub New(c As Cluster, atomid1 As Integer, atomid2 As Integer)
		_TerminalAtom = c.GetAtom(atomid2)
		_ParentPath = New MSPath(c,atomid1)
		_PathLength = Vector.Abs(_TerminalAtom.Position - _ParentPath.TerminalAtom.Position)
	End Sub

	''' <summary>
	''' Append the given atom to the given path to create a new extended path.
	''' </summary>
	''' <param name="parentPath"></param>
	''' <param name="atomtoappend"></param>
	Sub New(parentPath As MSPath, atomtoappend As ClusterAtom)
		_TerminalAtom = atomtoappend
		_ParentPath = parentpath
		_PathLength = _ParentPath.PathLength + Vector.Abs(_TerminalAtom.Position - _ParentPath.TerminalAtom.Position)
	End Sub

	''' <summary>
	'''
	''' </summary>
	''' <param name="parentPath"></param>
	''' <param name="c"></param>
	''' <param name="atomIDtoappend"></param>
	Sub New(parentPath As MSPath, c as Cluster, atomIDtoappend As integer)
		Me.New(parentPath, c.GetAtom(atomIDtoappend))
	End Sub
	

'	Sub New(c as Cluster, atomids() As Integer)
'		For i As Integer = 0 To atomids.Length-1
'			_Path.Add(atomids(i))
'		Next
'	End Sub
'
'	Sub New(c as Cluster, atomids As List(Of Integer))
'		For Each i As Integer In atomids
'			_Path.Add(i)
'		Next
'	End Sub
	
#End Region
	
#Region "Public Properties"

	''' <summary>
	''' Returns a reference to the last atom in the path (the terminal atom).
	''' </summary>
	Public ReadOnly Property TerminalAtom As ClusterAtom
		Get
			Return _TerminalAtom
		End Get
	End Property
	
	
	''' <summary>
	''' Returns the total path length, in Angstroms.
	''' </summary>
	Public ReadOnly Property PathLength As Double
		Get
			Return _PathLength
		End Get
	End Property
			

	''' <summary>
	''' Returns the number of atoms in the path.
	''' </summary>
	Public ReadOnly Property Count As Integer
		Get
			If _ParentPath Is Nothing Then
				Return 1
			Else
				Return _ParentPath.Count + 1
			End If
		End Get
	End Property
	
	
	''' <summary>
	''' Returns the scattering order of the path, equal to the number of atoms minus 1.
	''' </summary>
	Public ReadOnly Property Order As Integer
		Get
			Return Me.Count - 1
		End Get
	End Property


	''' <summary>
	''' Gets or Sets the root amplitude (without the terminating matrix Q).
	''' </summary>
	Public Property RootAmplitude As ComplexMatrix
		Get
			Return _RootAmplitude
		End Get
		Set (value as ComplexMatrix)
			_RootAmplitude = value
		End Set
	End Property
	
	
	''' <summary>
	''' Gets or sets the full amplitude.  Elements of the complex array correspond to angular momenta L.
	''' </summary>
	Public Property Amplitude As ComplexMatrix
		Get
			Return _Amplitude
		End Get
		Set (value As ComplexMatrix)
			_Amplitude = value
		End Set
	End Property
	
	
	''' <summary>
	''' Gets the root amplitude of the parent path.
	''' </summary>
	Public ReadOnly Property ParentRootAmplitude As ComplexMatrix
		Get
			If _ParentPath Is Nothing Then
				Return Nothing
			Else
				Return _ParentPath.RootAmplitude	
			End If
		End Get
	End Property
	
	
	''' <summary>
	''' Gets a reference to the parent path.
	''' </summary>
	Public ReadOnly Property Parent As MSPath
		Get
			Return _ParentPath
		End Get
	End Property
	

#End Region

#Region "Public Methods"

'	Public Function GetAtomIDs As List(Of Integer)
'		Dim retval As New List(Of Integer)
'		For Each i As Integer In _Path
'			retval.Add(i)
'		Next
'		Return retval
'	End Function
	
	
	''' <summary>
	''' Returns a list of ClusterAtom references to all atoms in the path.
	''' </summary>
	''' <returns></returns>
	Public Function GetAtoms As List(Of ClusterAtom)
		Dim atoms As New List(Of ClusterAtom)
		atoms.Add(_TerminalAtom)
		
		Dim p As MSPath = Me.Parent
		
		Do Until (p Is Nothing)
			atoms.Add(p.TerminalAtom)
			p = p.parent
		Loop
		
		'// Now reverse the order of the list, so that the absorber is at index 0 and the terminal atom is last:
		Dim retval As New List(Of ClusterAtom)
		For i As Integer = atoms.Count-1 To 0 Step -1
			retval.Add(atoms(i))
		Next
		
		Return retval
		
	End Function
	
	
	Public Function GetPosition(distancefromterminus As Integer) As Vector
		
		If distancefromterminus = 0 Then Return _TerminalAtom.Position
		
		Return _ParentPath.GetPosition(distancefromterminus-1)
		
	End Function
	
	
	Public Function GetTMatrix(distancefromterminus As Integer) As Complex()
		
		If distancefromterminus = 0 Then Return _TerminalAtom.Tmatrix
		
		Return _ParentPath.GetTMatrix(distancefromterminus-1)

	End Function
	
	Public Overrides Function ToString() As String
		Dim pathString as String = ""
		For Each ca As ClusterAtom In Me.GetAtoms
			pathString &= ca.ID.ToString & "-"
		Next
		Return pathString.Substring(0,pathString.Length-1)
	End Function
	
#End Region
	
#Region "IComparable Implementation"
	
	Public Function CompareTo(other As MSPath) As Integer Implements IComparable(Of MSPath).CompareTo
		Dim melength As Double = Me.PathLength
		Dim otherlength As Double = other.PathLength
		
		If meLength > otherlength Then Return 1
		If meLength = otherlength Then Return 0
		If meLength < otherlength Then Return -1
		
	End Function
	
#End Region


End Class
