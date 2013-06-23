
'<TestFixture> _
'Public Class PathEnumerationTest
'	<Test> _
'	Public Sub Test1
'		
'		Dim startTime As Long = now.Ticks
'		
'		Dim Natoms As Integer = 100
'		
'		Dim Paths As New ArrayList
'		
'		'// Add the origin (defined here to be zero)
'		Dim originID As Integer = 0
'		
'		Paths.Add(New Path(originID))
'		
'		
'		
'		Dim endTime as Long = now.ticks		
'		Console.WriteLine(endTime - startTime)
'		
'	End Sub
'	
'End Class

'Public Class Path
'	Private _Path As New ArrayList
'	Private _Parent As Path
'	Private _Amplitude As ComplexMatrix()
'	Private _RootAmplitude as ComplexMatrix()
'	
'	Sub New(rootID As Integer)
'		_Path.Add(rootID)
'	End Sub
'	
'	Sub New(parent As Path, appendID As Integer)
'		_Path.AddRange(parentpath)
'		_Parent = parent
'		_Path.Add(appendID)
'	End Sub
'
'	Public Function GetIDs As Integer()
'		Return _Path.ToArray(gettype(Integer))
'	End Function
'	
'	Public ReadOnly Property Order As Integer
'		Get
'			return _Path.Count-1
'		End Get
'	End Sub
'End Class