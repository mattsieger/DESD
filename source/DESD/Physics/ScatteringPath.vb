
'Imports Sieger.Math
'
'Public Class ScatteringPath
'	
'	Private _Path as New List(of ClusterAtom)
'	
'	Sub New()
'		
'	End Sub
'	
'	Sub New(c as Cluster, atomids As Integer())
'		For i As Integer = 0 To atomids.Length-1
'			_Path.Add(c.GetMember(atomids(i)))
'		Next
'	End Sub
'	
'	Sub New(parentPath As ScatteringPath, atomtoappend As ClusterAtom)
'		For i As Integer = 0 To parentPath.Count-1
'			_Path.Add(parentPath(i))
'		Next
'		_Path.Add(atomtoappend)
'	End Sub
'	
'	Default ReadOnly Property Item(i As Integer) As ClusterAtom
'		Get
'			Return _Path(i)
'		End Get
'	End Property
'	
'	Public ReadOnly Property Length As Double
'		Get
'			Dim totalLength as Double = 0.0
'			For i As Integer = 1 To _Path.Count-1
'				totalLength += (_Path(i).Position-_Path(i-1).Position).Magnitude
'			Next
'			Return totalLength
'		End Get
'	End Property
'	
'	Public Function ComplexRho(k As complex) As Complex
'		Return k*Length()
'	End Function
'	
'	Public Function Rho(k As Double) As Double
'		Return k*Length()
'	End Function
'	
'	Public Function GetRhoVectors(k As Double, index As Integer) As Vector()
'		Dim retval As Double(1)
'		retval(0) =k*( _Path(index+1)-_Path(index))
'		retval(1) = k * (_Path(index)-_Path(index-1))
'	End Function
'	
'	Public Function GetComplexRhoVectors(k As complex, index As Integer) As Vector()
'		
'	End Function
'	
'	Public Function GetEulerAngles(index As Integer) As Double()
'		Return Sieger.Math.Types.Vector.EulerAngles(_Path(index+1).Position,_Path(index-1).Position)
'	End Function
'	
'	Public ReadOnly Property Count As Integer
'		Get
'			Return _Path.Count
'		End Get
'	End Property
'	
'End Class
