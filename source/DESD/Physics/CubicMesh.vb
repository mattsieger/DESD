
''' <summary>
''' Encapsulates a mesh where the distance between adjacent points increases with a cubic scale.
''' </summary>
Public Class CubicMesh

    Implements IRadialMesh

    Private mRMin As Double
    Private mRMax As Double
    Private mNPoints As Integer

    Private mXMesh() As Double
    Private mRMesh() As Double

#Region "Constructors"

	''' <summary>
	''' 
	''' </summary>
	''' <param name="rMin">The minimum value of the mesh.</param>
	''' <param name="rMax">The maximum value of the mesh.</param>
	''' <param name="Npoints">The number of points in the mesh.</param>
    Sub New(ByVal rMin As Double, ByVal rMax As Double, ByVal Npoints As Integer)
        mRMin = rMin
        mRMax = rMax
        mNPoints = Npoints

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
            Return MeshType.Cubic
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
    		Return mNPoints - 1
    	End Get
    End Property

#End Region


#Region "Private Methods"

    ''' <summary>
    ''' Does the work of actually creating the mesh.  
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateMesh()
        ReDim mXMesh(mNPoints - 1)
        ReDim mRMesh(mNPoints - 1)

        '// Calculate XMax from RMax and N
        Dim XMax As Double = System.Math.Pow(mRMax, 1.0 / 3.0)

        Dim DeltaX As Double = XMax / CDbl(mNPoints - 1)

        '// Build out the mesh:
        For i As Integer = 0 To mNPoints - 1
            mXMesh(i) = DeltaX * CDbl(i)
            mRMesh(i) = mRMin + mXMesh(i) ^ 3.0
        Next

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
