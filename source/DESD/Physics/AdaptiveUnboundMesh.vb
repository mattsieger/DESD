
Imports System.Math

''' <summary>
''' Represents a mesh that is cubic near r = 0, then transitions to a linear mesh
''' whose spacing depends on energy (in Rydbergs) and the desired fidelity.
''' </summary>
Public Class AdaptiveUnboundMesh
	
	Implements IRadialMesh

    Private mRMin As Double
    Private mRMax As Double
    Private mNCubicPoints As Integer
    Private mNPointsPerPeriod As Integer
    Private mEnergy As Double
    Private mNPoints as integer

    Private mXMesh() As Double
    Private mRMesh() As Double

#Region "Constructors"

	''' <summary>
	'''
	''' </summary>
	''' <param name="rMin"></param>
	''' <param name="rMax"></param>
	''' <param name="NCubicPoints"></param>
	''' <param name="E">Energy in Rydbergs</param>
	''' <param name="NPointsPerPeriod"></param>
    Sub New(ByVal rMin As Double, ByVal rMax As Double, ByVal NCubicPoints As Integer, E as Double, NPointsPerPeriod as Integer)
        mRMin = rMin
        mRMax = rMax
        mNCubicPoints = NCubicpoints
		mEnergy = E
		mNPointsPerPeriod = NPointsPerPeriod
        CreateMesh()
    End Sub

#End Region



#Region "IRadialMesh Implementation"

    ''' <summary>
    ''' Returns the value of the radial mesh at the index point.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function R(ByVal index As Integer) As Double Implements IRadialMesh.R
        If (index < 0) OrElse (index >= mNPoints) Then Throw New IndexOutOfRangeException()
        Return mRMesh(index)
    End Function

    ''' <summary>
    ''' The type of mesh.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Type() As MeshType Implements IRadialMesh.Type
        Get
            Return MeshType.CubicAdaptive
        End Get
    End Property

    ''' <summary>
    ''' The number of points in the mesh.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ReadOnly Property Count() As Integer Implements IRadialMesh.Count
        Get
            Return CInt(mNPoints)
        End Get
    End Property

    ''' <summary>
    ''' Returns the absolute value of the distance between two mesh points R(endIndex) - R(startIndex).
    ''' </summary>
    ''' <param name="startIndex"></param>
    ''' <param name="endIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DR(ByVal startIndex As Integer, ByVal endIndex As Integer) As Double Implements IRadialMesh.DR
        If (startIndex < 0) OrElse (startIndex >= mNPoints) Then Throw New IndexOutOfRangeException()
        If (endIndex < 0) OrElse (endIndex >= mNPoints) Then Throw New IndexOutOfRangeException()
        Return System.Math.Abs(mRMesh(endIndex) - mRMesh(startIndex))
    End Function

    ''' <summary>
    ''' Returns the total range of R spanned by the mesh.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Range() As Double Implements IRadialMesh.Range
        Return (mRMesh(mNPoints - 1) - mRMesh(0))
    End Function

    ''' <summary>
    ''' Returns the highest R value in the mesh.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Max() As Double Implements IRadialMesh.Max
        Return mRMesh(mNPoints - 1)
    End Function

    ''' <summary>
    ''' Returns the smallest R value in the mesh.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Min() As Double Implements IRadialMesh.Min
        Return mRMesh(0)
    End Function

    ''' <summary>
    ''' For meshes that use blocks, returns the number of blocks.  If no blocks are used, returns 1.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function BlockCount() As Integer Implements IRadialMesh.BlockCount
        Return 1
    End Function

    ''' <summary>
    ''' Returns the values of the mesh as a System.Array of type Double.
    ''' </summary>
    ''' <returns></returns>
    Function GetArray() As Double() Implements IRadialMesh.GetArray
        Dim retval(mNPoints - 1) As Double
        System.Array.Copy(mRMesh, retval, mNPoints)
        Return retval
    End Function
    
    Public ReadOnly Property Imax As Integer Implements IRadialMesh.Imax
    	Get
    		Return mNCubicPoints - 1
    	End Get
    End Property
#End Region


#Region "Private Methods"

    ''' <summary>
    ''' Does the work of actually creating the mesh.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateMesh()
    	
    	'// First, work on the cubic part of the mesh:
        ReDim mXMesh(mNCubicPoints - 1)
        Dim mRCubicMesh(mNCubicPoints - 1) as Double

		'// Calculate minimum interpoint spacing from given Energy
		Dim DeltaRMin As Double = 4.44288293815837R / Sqrt(mEnergy) / CDbl(mNPointsPerPeriod)
		
        '// Calculate XMax for the cubic part from RMax, and DeltaX from XMax and NCubicPoints
        Dim XMax As Double = System.Math.Pow(mRMax, 1.0 / 3.0)
        Dim DeltaX As Double = XMax / CDbl(mNCubicPoints - 1)

        '// Build out the mesh, starting with the cubic portion:
        Dim deltaR As Double
        Dim iSwitch As Integer = -1
        Dim RSwitch as Double
        For i As Integer = 0 To mNCubicPoints - 1
            mXMesh(i) = DeltaX * CDbl(i)
            mRCubicMesh(i) = mRMin + mXMesh(i) ^ 3.0
            '// Calculate deltaR in the cubic region and compare to deltaRMin:
            If i > 0 Then
            	deltaR = mRCubicMesh(i) - mRCubicMesh(i-1)
            	If deltaR > DeltaRMin Then
            		'// We need to terminate the cubic mesh at this point and switch to linear.
            		'// Discard the current point, since the deltaR is too big.
            		iSwitch = i-1
            		RSwitch = mRCubicMesh(i-1)
            		exit for
            	End If
            End If
       Next
       		
       		
       '// If no need to switch to linear, just return what we have:
       If iSwitch < 0 Then
       		mNPoints = mRCubicMesh.length
       		mRmesh = mRCubicMesh
       		Exit Sub
       end if
       	
       	'// We need to switch to a linear mesh:
       	'// First, compute the number of points in the linear region:
       	Dim NlinearPoints As Integer = CInt((mRmax - Rswitch) / DeltaRMin) + 1
       		
       	'// Now smooth it out - compute new deltaR corresponding to this number of points
       	'// (so that last point will exactly = Rmax)
       	Dim LDR As Double = (mRMax - Rswitch) / CDbl(NLinearPoints)
       		
       	'// Now we know how many total points will be in the mesh - we can dim the full return array
       	'// and copy over the cubic points:
       	Dim NTotalPoints as Integer = iSwitch + NLinearPoints
       	ReDim mRMesh(NTotalPoints)
       	For i As Integer = 0 To NTotalPoints
       		If i <= iSwitch Then
       			mRMesh(i) = mRCubicMesh(i)
       		Else
       			mRMesh(i) = mRMesh(i-1) + LDR
       		End If
       			
       	Next
       	
       	mNPoints = mRMesh.Length

    End Sub

#End Region


#Region "IEnumerable Implementation"

    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of Double) Implements System.Collections.Generic.IEnumerable(Of Double).GetEnumerator
        Return New RadialMeshEnumerator(Me)
    End Function

    Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return New RadialMeshEnumerator(Me)
    End Function

#End Region

End Class
