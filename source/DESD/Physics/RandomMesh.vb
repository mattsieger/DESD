
''' <summary>
''' Represents a mesh of points randomly scattered in a uniform distribution throughout a range.
''' </summary>
Public Class RandomMesh
	
        Implements IRadialMesh

		Private mRmin As Double
		Private mRmax as Double
        Private mNPoints As Integer

        Private mRMesh() As Double

#Region "Constructors"

        Sub New(rMin As Double, rMax As Double, Npoints As Integer)
        	mRmin = rMin
        	mRmax = rMax
        	mNPoints = NPoints
            CreateMesh()
        End Sub

#End Region


#Region "Public Properties"



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
                Return MeshType.Random
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
            Return system.Math.Abs(mRMesh(endIndex) - mRMesh(startIndex))
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
        	Dim retval(mNPoints-1) As Double
        	system.Array.Copy(mRMesh,retval,mNPoints)
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

            ReDim mRMesh(mNpoints - 1)

            '// Build out the mesh:
            '// The start and endpoints are already defined by Rmin and Rmax
            '// We'll just fill the first 2 slots with those values, since
            '// we'll be sorting the array at the end.
            mRMesh(0) = mRmin
            mRMesh(1) = mRmax
            
            'Dim rng As New MersenneTwister
            Dim rng As New Random
           
            For i As Integer = 2 To mNPoints-1
            	'mRMesh(i) = mRmin + me.Range * rng.GetDouble()
            	mRMesh(i) = mRmin + (mRmax - mRmin) * rng.NextDouble
            Next

			array.Sort(mRMesh)

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

