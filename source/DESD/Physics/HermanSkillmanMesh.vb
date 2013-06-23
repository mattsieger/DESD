Imports System.Math
Imports Sieger.Math



''' <summary>
''' Encapsulates a mesh as defined by Herman and Skillman.
''' </summary>
''' <remarks></remarks>
Public Class HermanSkillmanMesh

    Implements IRadialMesh

    Private mZ As Element
    Private mCMU As Double
    Private mDeltaX As Double = 0.0025
    Private mSize As HermanSkillmanMeshSize = HermanSkillmanMeshSize.Normal


    Private mXMesh() As Double
    Private mRMesh() As Double
    
    
    Public Enum HermanSkillmanMeshSize
	
	Normal = 441
    Medium = 481
    Large = 521
          
End Enum


#Region "Constructors"

    Sub New(ByVal z As Element, ByVal size As HermanSkillmanMeshSize, ByVal deltaX As Double)
        mZ = z
        mCMU = GetCMU(z)
        mDeltaX = deltaX
        mSize = size
        CreateMesh()
    End Sub

    Sub New(ByVal z As Element, ByVal size As HermanSkillmanMeshSize)
        mZ = z
        mCMU = GetCMU(z)
        mSize = size
        CreateMesh()
    End Sub

    Sub New(ByVal z As Element)
        mZ = z
        mCMU = GetCMU(z)
        CreateMesh()
    End Sub

#End Region


#Region "Public Properties"

    ''' <summary>
    ''' The z-dependent mesh scaling factor defined in Herman and Skillman, page 4-2.
    ''' CMU = 0.88534138 * Z ^ (-1/3).  R = CMU * X
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CMU() As Double
        Get
            Return mCMU
        End Get
    End Property

    ''' <summary>
    ''' The atomic number of the atom the mesh is intended for.  Used to calculate CMU.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Z() As Integer
        Get
            Return CInt(mZ)
        End Get
    End Property

    ''' <summary>
    ''' The initial spacing between points on the X-mesh (NOT the scaled R-mesh).  At every new block
    ''' in the mesh, the spacing between points doubles.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DeltaX() As Double
        Get
            Return mDeltaX
        End Get
    End Property

    ''' <summary>
    ''' The spacing between R points in the first (initial) block.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DeltaR() As Double
        Get
            Return mDeltaX * mCMU
        End Get
    End Property

    ''' <summary>
    ''' The number of points in the mesh.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Size() As HermanSkillmanMeshSize
        Get
            Return mSize
        End Get
    End Property

#End Region


#Region "IRadialMesh Implementation"

    ''' <summary>
    ''' Returns the value of the radial mesh at the index point.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function R(ByVal index As Integer) As Double Implements IRadialMesh.R
        If (index < 0) OrElse (index >= mSize) Then Throw New IndexOutOfRangeException()
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
            Return MeshType.HermanSkillman
        End Get
    End Property

    ''' <summary>
    ''' The number of points in the mesh.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ReadOnly Property Count() As Integer Implements IRadialMesh.Count
        Get
            Return CInt(mSize)
        End Get
    End Property
    
    ''' <summary>
    ''' The largest index of the mesh array.
    ''' </summary>
    ReadOnly Property IMax() As Integer Implements IRadialMesh.Imax
    	Get
    		Return CInt(mSize) - 1
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
        If (startIndex < 0) OrElse (startIndex >= mSize) Then Throw New IndexOutOfRangeException()
        If (endIndex < 0) OrElse (endIndex >= mSize) Then Throw New IndexOutOfRangeException()
        Return Abs(mRMesh(endIndex) - mRMesh(startIndex))
    End Function

    ''' <summary>
    ''' Returns the total range of R spanned by the mesh.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Range() As Double Implements IRadialMesh.Range
        Return (mRMesh(mSize - 1) - mRMesh(0))
    End Function

    ''' <summary>
    ''' Returns the highest R value in the mesh.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Max() As Double Implements IRadialMesh.Max
        Return mRMesh(mSize - 1)
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
        Return (CInt(mSize) - 1) \ 40
    End Function

    ''' <summary>
    ''' Returns the values of the mesh as a System.Array of type Double.
    ''' </summary>
    ''' <returns></returns>
    Function GetArray() As Double() Implements IRadialMesh.GetArray
        Dim retval(mSize - 1) As Double
        System.Array.Copy(mRMesh, retval, mSize)
        Return retval
    End Function

#End Region


#Region "Private Methods"

    ''' <summary>
    ''' Returns the value of CMU(z).
    ''' </summary>
    ''' <param name="atomicNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCMU(ByVal atomicNumber As Integer) As Double

        Return 0.5 * Pow(3.0 * System.Math.Pi / 4.0, 2.0 / 3.0) * Pow(atomicNumber, -1.0 / 3.0)

    End Function

    ''' <summary>
    ''' Does the work of actually creating the mesh.   Code adapted from the FORTRAN.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateMesh()

        '// Initialize the radial meshes:
        '// Note that Xmesh is the direct radial mesh, Rmesh is scaled by Z
        '// Units of X are AU.

        'C ----------------------------------------------------------------------
        'C Construct X Mesh and R Mesh
        'C   I = 1..441; R = CMU * X
        'C 
        'C d CMU = 0.5 * (3pi/4)^2/3 * Z^-1/3
        'C         also appears in energy eigenvalue perturbation and SCHEQ call
        'C i NBLOCK = # of blocks of potential mesh.  Each block has a new DELTAX
        'C d DELTAX = step size of X mesh grid
        'C ----------------------------------------------------------------------
        Dim N As Integer = Me.Size

        '// Dimension the internal arrays:
        ReDim mXMesh(N - 1)
        ReDim mRMesh(N - 1)

        '// The number of blocks is determined by the total mesh size
        '// and the (fixed) block length of 40.
        Dim NBLOCKS As Integer = (mSize - 1) \ 40

        '// By definition, the first point in both meshes is zero:
        mXMesh(0) = 0.0
        mRMesh(0) = 0.0

        '// XStep starts as the minimum value = DeltaX:
        Dim XStep As Double = mDeltaX

        '// Build out the meshes:
        Dim index As Integer = 0
        For J As Integer = 1 To NBLOCKS
            For K As Integer = 1 To 40
                index += 1
                mXMesh(index) = mXMesh(index - 1) + XStep
                mRMesh(index) = mCMU * mXMesh(index)
            Next
            '// At the end of the block the step size doubles:
            XStep *= 2.0
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


