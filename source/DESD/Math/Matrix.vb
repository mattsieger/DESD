Namespace Math


    ''' <summary>
    ''' Implements a mathematical matrix of an arbitrary number of columns and rows.
    ''' </summary>
    Public Class Matrix

        Private _Values(,) As Double

        Public Sub New(values As Double(,))
            '// Copy the values to the private backing field:
            ReDim _Values(values.GetUpperBound(0), values.GetUpperBound(1))
            System.Array.Copy(values, _Values, values.Length)
        End Sub

        Public Sub New(rows As Integer, columns As Integer)
            If (rows < 0) Or (columns < 0) Then Throw New IndexOutOfRangeException()
            ReDim _Values(rows - 1, columns - 1)
        End Sub

        Default Public Property Value(row As Integer, column As Integer) As Double
            Get
                Return _Values(row, column)
            End Get
            Set(v As Double)
                _Values(row, column) = v
            End Set
        End Property

        ReadOnly Property Rows As Integer
            Get
                Return _Values.GetUpperBound(0) + 1
            End Get
        End Property

        ReadOnly Property Columns As Integer
            Get
                Return _Values.GetUpperBound(1) + 1
            End Get
        End Property

        ''' <summary>
        ''' Returns the transpose of the matrix.
        ''' </summary>
        ''' <returns></returns>
        Public Function Transpose() As Matrix
            Dim retval(Me.Columns - 1, Me.Rows - 1) As Double
            For row As Integer = 0 To Me.Rows - 1
                For column As Integer = 0 To Me.Columns - 1
                    retval(column, row) = Me.Value(row, column)
                Next
            Next
            Return New Matrix(retval)
        End Function

        Public Shared Operator *(ByVal x As Double, ByVal m As Matrix) As Matrix
            Dim retval(m.Rows - 1, m.Columns - 1) As Double
            For row As Integer = 0 To m.Rows - 1
                For column As Integer = 0 To m.Columns - 1
                    retval(row, column) = x * m(row, column)
                Next
            Next
            Return New Matrix(retval)
        End Operator

        Public Shared Operator *(ByVal m As Matrix, ByVal x As Double) As Matrix
            Dim retval(m.Rows - 1, m.Columns - 1) As Double
            For row As Integer = 0 To m.Rows - 1
                For column As Integer = 0 To m.Columns - 1
                    retval(row, column) = x * m(row, column)
                Next
            Next
            Return New Matrix(retval)
        End Operator

        Public Shared Operator *(ByVal m1 As Matrix, ByVal m2 As Matrix) As Matrix
            If m1.Columns <> m2.Rows Then Throw New InvalidOperationException("Number of columns of left matrix must equal the number of rows of right matrix for multiplication to be valid.")
            Dim retval(m1.Rows - 1, m2.Columns - 1) As Double
            For row As Integer = 0 To m1.Rows - 1
                For column As Integer = 0 To m2.Columns - 1
                    retval(row, column) = 0.0
                    For r As Integer = 0 To m1.Columns - 1
                        retval(row, column) += m1(row, r) * m2(r, column)
                    Next
                Next
            Next
            Return New Matrix(retval)
        End Operator

        Public Shared Operator +(ByVal m1 As Matrix, ByVal m2 As Matrix) As Matrix
            If (m1.Columns <> m2.Columns) Or (m1.Rows <> m2.Rows) Then Throw New InvalidOperationException("Matrices are of unequal dimension.")
            Dim retval(m1.Rows - 1, m1.Columns - 1) As Double
            For row As Integer = 0 To m1.Rows - 1
                For column As Integer = 0 To m1.Columns - 1
                    retval(row, column) = m1(row, column) + m2(row, column)
                Next
            Next
            Return New Matrix(retval)
        End Operator

        Public Shared Operator -(ByVal m1 As Matrix, ByVal m2 As Matrix) As Matrix
            If (m1.Columns <> m2.Columns) Or (m1.Rows <> m2.Rows) Then Throw New InvalidOperationException("Matrices are of unequal dimension.")
            Dim retval(m1.Rows - 1, m1.Columns - 1) As Double
            For row As Integer = 0 To m1.Rows - 1
                For column As Integer = 0 To m1.Columns - 1
                    retval(row, column) = m1(row, column) - m2(row, column)
                Next
            Next
            Return New Matrix(retval)
        End Operator

        Public Shared Operator =(ByVal m1 As Matrix, ByVal m2 As Matrix) As Boolean
            If (m1.Columns <> m2.Columns) Or (m1.Rows <> m2.Rows) Then Return False
            For row As Integer = 0 To m1.Rows - 1
                For column As Integer = 0 To m1.Columns - 1
                    If m1(row, column) <> m2(row, column) Then Return False
                Next
            Next
            Return True
        End Operator

        Public Shared Operator <>(ByVal m1 As Matrix, ByVal m2 As Matrix) As Boolean
            If (m1.Columns <> m2.Columns) Or (m1.Rows <> m2.Rows) Then Return True
            For row As Integer = 0 To m1.Rows - 1
                For column As Integer = 0 To m1.Columns - 1
                    If m1(row, column) <> m2(row, column) Then Return True
                Next
            Next
            Return False
        End Operator

        Public Shared Operator -(ByVal m As Matrix) As Matrix
            Dim retval(m.Rows - 1, m.Columns - 1) As Double
            For row As Integer = 0 To m.Rows - 1
                For column As Integer = 0 To m.Columns - 1
                    retval(row, column) = -m(row, column)
                Next
            Next
            Return New Matrix(retval)
        End Operator

    End Class
End Namespace