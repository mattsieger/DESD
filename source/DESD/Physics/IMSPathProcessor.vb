
Imports DESD.Math


Public Interface IMSPathProcessor
	'Sub New(mscluster As Cluster, startingPaths As List(Of MSPath), maxpathlength As Double, k As complex, zv0 As Double, raorder As Integer, maxmsorder As Integer, lmax As Integer)
	Property Khat As Vector
	ReadOnly Property NPathsCalculated As Integer
	Sub Calculate()
	Sub CalculateTermination()
	Function GetPaths() As List(Of MSPath)


End Interface
