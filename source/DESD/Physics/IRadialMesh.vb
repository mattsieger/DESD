

Public Interface IRadialMesh
	
	Inherits IEnumerable(Of Double)

    ''' <summary>
    ''' Returns the value of the radial mesh at the index point.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function R(ByVal index As Integer) As Double

    ''' <summary>
    ''' The type of mesh.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
	ReadOnly Property Type() As MeshType

    ''' <summary>
    ''' The number of points in the mesh.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Count() As Integer
    
    ''' <summary>
    ''' The largest index of the mesh array.
    ''' </summary>
    ReadOnly Property IMax() As Integer

    ''' <summary>
    ''' Returns the distance between two mesh points R(endIndex) - R(startIndex).
    ''' </summary>
    ''' <param name="startIndex"></param>
    ''' <param name="endIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DR(ByVal startIndex As Integer, ByVal endIndex As Integer) As Double

    ''' <summary>
    ''' Returns the total range of R spanned by the mesh.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
	Function Range() As Double

    ''' <summary>
    ''' Returns the highest R value in the mesh.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
	Function Max() As Double

    ''' <summary>
    ''' Returns the smallest R value in the mesh.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
	Function Min() As Double

    ''' <summary>
    ''' For meshes that use blocks, returns the number of blocks.  If no blocks are used, returns 1.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
	Function BlockCount() As Integer
	
	''' <summary>
	''' Returns the values of the mesh as a System.Array of type Double.
	''' </summary>
	''' <returns></returns>
	Function GetArray() as Double()
	
End Interface
