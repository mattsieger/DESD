'Imports Sieger.Math
'
'Public Class MSPathCalculator
'	
'	Private _k As Complex
'	'Private _Cluster As Cluster
'	Private _RAOrder As Integer
'	Private _Lmax as integer
'	
''	Sub New(mscluster As Cluster, k As Complex, lmax as Integer, raorder as integer)
''		_Cluster = mscluster
''		_k = k
''		_RAOrder = raorder
''		_Lmax = lmax
''	End Sub
'
'	Sub New(k As Complex, lmax as Integer, raorder as integer)
'		_k = k
'		_RAOrder = raorder
'		_Lmax = lmax
'	End Sub
'
'
'	Public Function CalculateAmplitude(path As MSPath, k as Complex, lmax as Integer, raorder as integer) as ComplexMatrix
'				
'		Dim R1 As Vector
'		Dim R2 As Vector
'		Dim R3 As Vector
'		Dim T As Complex()
'		Dim retval As ComplexMatrix
'		
'		Dim scatteringorder As Integer = path.Order
'		Select Case scatteringorder
'			Case 0	' Direct term
'				'// Not allowed here, since the direct term depends on k-vector
'				Throw new InvalidOperationException("There is no root amplitude for the direct term.")
'			Case 1	' Single scattering
'				R1 = path.GetPosition(1)
'				R2 = path.GetPosition(0)
'				retval = M(R1, R2, k, raorder)
'			Case Else	' Higher-order (multiple) scattering
'				R1 = path.GetPosition(2)
'				R2 = path.GetPosition(1)
'				R3 = path.GetPosition(0)
'				T = path.GetTmatrix(1)
'				
'				'// If the parent's RootAmplitude is undefined, recurse to compute it.
'				If path.Parent.RootAmplitude Is Nothing Then
'					'// recurse
'					path.Parent.RootAmplitude = Me.CalculateAmplitude(path.Parent)
'				End If
'
'				retval = path.Parent.RootAmplitude * H(R1, R2, R3, k, T, raorder)
'		End Select
'		
'		Return retval
'		
'	End Function
'	
'	Public Function M(ra As Vector, rn As Vector, k As Complex, raorder as integer) As ComplexMatrix
'		
'	End Function
'	
'	Public Function H(R1 As Vector, R2 As Vector, R3 As Vector, k As Complex, T As Complex(), raorder as integer) As ComplexMatrix
'		
'	End Function
'	
'End Class
