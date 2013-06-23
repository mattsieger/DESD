

'Imports System.Math
'
'
'    ''' <summary>
'    ''' Encodes a 3-vector with real-valued components.
'    ''' </summary>
'    ''' <remarks></remarks>
'    <Serializable> _
'    Public Class ComplexVector
'		
'        Private mComponents(2) As Complex
'
'#Region "Constructors"
'
'        ''' <summary>
'        ''' Creates a new zero-length vector.
'        ''' </summary>
'        ''' <remarks></remarks>
'        Sub New()
'            mComponents(0) = New Complex()
'            mComponents(1) = New Complex()
'            mComponents(2) = New Complex()
'        End Sub
'
'        ''' <summary>
'        ''' Creates a new vector with the specified components.
'        ''' </summary>
'        ''' <param name="componentX"></param>
'        ''' <param name="componentY"></param>
'        ''' <param name="componentZ"></param>
'        ''' <remarks></remarks>
'        Sub New(ByVal componentX As Complex, ByVal componentY As Complex, ByVal componentZ As Complex)
'            mComponents(0) = componentX
'            mComponents(1) = componentY
'            mComponents(2) = componentZ
'        End Sub
'
'        Sub New(ByVal cartesianarray As Complex())
'            If cartesianarray.Length <> 3 Then Throw New ArgumentException("vectorarray", "must be of length 3.")
'            mComponents(0) = cartesianarray(0)
'            mComponents(1) = cartesianarray(1)
'            mComponents(2) = cartesianarray(2)
'        End Sub
'
'#End Region
'
'
'
'        ''' <summary>
'        ''' Returns the value of the vector formatted as a string (x,y,z).
'        ''' </summary>
'        ''' <returns></returns>
'        ''' <remarks></remarks>
'        Public Overrides Function ToString() As String
'
'            Return "(" & mComponents(0).ToString & "," & mComponents(1).ToString & "," & mComponents(2).ToString & ")"
'
'        End Function
'
'
'
'#Region "Properties"
'
'        Public Property X() As Complex
'            Get
'                Return mComponents(0)
'            End Get
'            Set(ByVal value As Complex)
'                mComponents(0) = value
'            End Set
'        End Property
'
'        Public Property Y() As Complex
'            Get
'                Return mComponents(1)
'            End Get
'            Set(ByVal value As Complex)
'                mComponents(1) = value
'            End Set
'        End Property
'
'        Public Property Z() As Complex
'            Get
'                Return mComponents(2)
'            End Get
'            Set(ByVal value As Complex)
'                mComponents(2) = value
'            End Set
'        End Property
'
'        Public Property R() As Complex
'            Get
'                Return Me.Magnitude
'            End Get
'            Set(ByVal value As Complex)
'                Me.SetValuesSpherical(value, Me.Theta, Me.Phi)
'            End Set
'        End Property
'
'        ''' <summary>
'        ''' The angle between the vector and the z axis. 
'        ''' </summary>
'        ''' <value></value>
'        ''' <returns></returns>
'        ''' <remarks></remarks>
'        Public Property Theta() As Complex
'            Get
'                Dim temp As Complex = Complex.Cpow(mComponents(0) * mComponents(0) + mComponents(1) * mComponents(1),0.5)
'                Return System.Math.Atan2(temp, mComponents(2))
'            End Get
'            Set(ByVal value As Double)
'                Me.SetValuesSpherical(Me.R, value, Me.Phi)
'            End Set
'        End Property
'
'        Public Property Phi() As Double
'            Get
'                Return System.Math.Atan2(mComponents(1), mComponents(0))
'            End Get
'            Set(ByVal value As Double)
'                Me.SetValuesSpherical(Me.R, Me.Theta, value)
'            End Set
'        End Property
'
'#End Region
'
'
'#Region "Methods"
'
'        Public Function Magnitude() As Double
'            Return Abs(Me)
'        End Function
'
'        Public Sub SetValuesCartesian(ByVal x As Double, ByVal y As Double, ByVal z As Double)
'            Me.X = x
'            Me.Y = y
'            Me.Z = z
'        End Sub
'
'        Public Sub SetValuesCartesian(ByVal varray As Double())
'            If varray.Length <> 3 Then Throw New ArgumentException("varray", "must be of length 3")
'            Me.X = varray(0)
'            Me.Y = varray(1)
'            Me.Z = varray(2)
'        End Sub
'
'        Public Function GetValuesCartesian() As Double()
'            Dim retval(2) As Double
'            System.Array.Copy(mComponents, retval, 3)
'            Return retval
'        End Function
'
'        Public Sub SetValuesSpherical(ByVal r As Double, ByVal theta As Double, ByVal phi As Double)
'            Dim sintheta As Double = System.Math.Sin(theta)
'            Me.X = r * sintheta * System.Math.Cos(phi)
'            Me.Y = r * sintheta * System.Math.Sin(phi)
'            Me.Z = r * System.Math.Cos(theta)
'        End Sub
'
'        Public Sub SetValuesSpherical(ByVal sarray As Double())
'            If sarray.Length <> 3 Then Throw New ArgumentException("sarray", "must be of length 3")
'            SetValuesSpherical(sarray(0), sarray(1), sarray(2))
'        End Sub
'
'        Public Function GetValuesSpherical() As Double()
'
'            Dim retval(2) As Double
'            retval(0) = Me.Magnitude
'            Dim temp As Double = System.Math.Sqrt(mComponents(0) * mComponents(0) + mComponents(1) * mComponents(1))
'            retval(1) = System.Math.Atan2(temp, mComponents(2))
'            retval(2) = System.Math.Atan2(mComponents(1), mComponents(0))
'
'            Return retval
'
'        End Function
'
'#End Region
'
'
'#Region "Shared Methods"
'
'        Shared Function Abs(ByVal v As Vector) As Double
'            Return System.Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z)
'        End Function
'
'        Shared Function Clone(ByVal v As Vector) As Vector
'            Return New Vector(v.X, v.Y, v.Z)
'        End Function
'
'        Shared Function NewFromSpherical(ByVal r As Double, ByVal theta As Double, ByVal phi As Double) As Vector
'            Dim retval As New Vector
'            retval.SetValuesSpherical(r, theta, phi)
'            Return retval
'        End Function
'
'        Shared Function ScalarProduct(ByVal v1 As Vector, ByVal v2 As Vector) As Double
'            Return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z
'        End Function
'
'        Shared Function CrossProduct(ByVal v1 As Vector, ByVal v2 As Vector) As Vector
'            Return New Vector(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X)
'        End Function
'
'        ''' <summary>
'        ''' Returns the included angle between two vectors v1 and v2.
'        ''' </summary>
'        ''' <param name="v1"></param>
'        ''' <param name="v2"></param>
'        ''' <returns></returns>
'        ''' <remarks></remarks>
'        Shared Function AngleBetween(ByVal v1 As Vector, ByVal v2 As Vector) As Double
'            Dim a As Double = v1.Magnitude
'            Dim b As Double = v2.Magnitude
'            If (a = 0.0) OrElse (b = 0.0) Then Return Double.NaN
'
'            Return System.Math.Acos(ScalarProduct(v1, v2) / (a * b))
'
'        End Function
'
'        ''' <summary>
'        ''' Returns the euler angles (alpha, beta, gamma) that rotate vector v1 onto the Z axis, and then onto vector v2.
'        ''' </summary>
'        ''' <param name="v1"></param>
'        ''' <param name="v2"></param>
'        ''' <returns></returns>
'        ''' <remarks></remarks>
'        Shared Function EulerAngles(ByVal v1 As Vector, ByVal v2 As Vector) As Double()
'
'            '// Calculate the euler angles:
'            Dim theta As Double = v1.Theta
'            Dim thetaprime As Double = v2.Theta
'            Dim phi As Double = v1.Phi
'            Dim phiprime As Double = v2.Phi
'
'            Dim calpha As New Complex(Sin(theta) * Cos(thetaprime) - Cos(theta) * Sin(thetaprime) * Cos(phiprime - phi), _
'                                            Sin(thetaprime) * Sin(phiprime - phi))
'
'            Dim alpha As Double = calpha.Argument
'
'            Dim beta As Double = Acos(Cos(theta) * Cos(thetaprime) + Sin(theta) * Sin(thetaprime) * Cos(phi - phiprime))
'
'            Dim cgamma As New Complex(Sin(theta) * Cos(thetaprime) * Cos(phiprime - phi) - Cos(theta) * Sin(thetaprime), _
'                                            Sin(theta) * Sin(phiprime - phi))
'
'            Dim gamma As Double = cgamma.Argument
'
'            Dim retval() As Double = {alpha, beta, gamma}
'
'            Return retval
'
'        End Function
'
'#End Region
'
'
'#Region "Operator Overloads"
'
'
'        Public Shared Operator *(ByVal x As Double, ByVal v As Vector) As Vector
'            Return New Vector(x * v.X, x * v.Y, x * v.Z)
'        End Operator
'
'        Public Shared Operator *(ByVal v As Vector, ByVal x As Double) As Vector
'            Return New Vector(x * v.X, x * v.Y, x * v.Z)
'        End Operator
'
'
'        Public Shared Operator +(ByVal v1 As Vector, ByVal v2 As Vector) As Vector
'            Return New Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z)
'        End Operator
'
'        Public Shared Operator -(ByVal v1 As Vector, ByVal v2 As Vector) As Vector
'            Return New Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z)
'        End Operator
'
'
'        Public Shared Operator =(ByVal v1 As Vector, ByVal v2 As Vector) As Boolean
'            Return (v1.X = v2.X) AndAlso (v1.Y = v2.Y) AndAlso (v1.Z = v2.Z)
'        End Operator
'
'
'        Public Shared Operator <>(ByVal v1 As Vector, ByVal v2 As Vector) As Boolean
'            If (v1.X <> v2.X) Then Return False
'            If (v1.Y <> v2.Y) Then Return False
'            If (v1.Z <> v2.Z) Then Return False
'            Return True
'        End Operator
'
'
'        Public Shared Operator -(ByVal v As Vector) As Vector
'            Return New Vector(-v.X, -v.Y, -v.Z)
'        End Operator
'
'
'#End Region
'
'
'    End Class
'
'
'End Namespace
