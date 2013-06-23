Imports DESD.Math

<TestClass> _
Public Class Types_Matrix



    <TestMethod()> _
    Public Sub Constructor1()

        Dim m As New Matrix(2, 2)

        m(0, 0) = 1.0
        m(1, 0) = -1.0
        m(0, 1) = 2.0
        m(1, 1) = -2.0

        Assert.IsTrue(m.Rows = 2)
        Assert.IsTrue(m.Columns = 2)

        Assert.IsTrue(m(0, 0) = 1.0)
        Assert.IsTrue(m(1, 0) = -1.0)
        Assert.IsTrue(m(0, 1) = 2.0)
        Assert.IsTrue(m(1, 1) = -2.0)

    End Sub

    <TestMethod()> _
    Public Sub Constructor2()

        Dim d As Double(,) = {{1.0, 2.0}, {-1.0, -2.0}}
        Dim m As New Matrix(d)


        Assert.IsTrue(m.Rows = 2)
        Assert.IsTrue(m.Columns = 2)

        Assert.IsTrue(m(0, 0) = 1.0)
        Assert.IsTrue(m(1, 0) = -1.0)
        Assert.IsTrue(m(0, 1) = 2.0)
        Assert.IsTrue(m(1, 1) = -2.0)

    End Sub

    '    <Test()> _
    '    Public Sub Constructor3()
    '
    '		Dim m As New Matrix(of Complex)(2,2)
    '		
    '		assert.IsTrue(m.Rows = 2)
    '		assert.IsTrue(m.Columns = 2)
    '		
    '		m(0,0) = new Complex(1.0,1.0)
    '		m(1,0) = new Complex(1.0,-1.0)
    '		m(0,1) = new Complex(-1.0,1.0)
    '		m(1,1) = new Complex(-1.0,-1.0)
    '
    '		assert.IsTrue(m(0,0)= new Complex(1.0,1.0))
    '		assert.IsTrue(m(1,0)= new Complex(1.0,-1.0))
    '		assert.IsTrue(m(0,1)=new Complex(-1.0,1.0))
    '		assert.IsTrue(m(1,1)=new Complex(-1.0,-1.0))
    '	
    '    End Sub

    <TestMethod()> _
    Public Sub Multiply1()
        Dim d As Double(,) = {{1.0, 2.0}, {-1.0, -2.0}}
        Dim m As New Matrix(d)

        Dim m2 As Matrix = 2.0 * m

        Assert.IsTrue(m2.Rows = 2)
        Assert.IsTrue(m2.Columns = 2)

        Assert.IsTrue(m2(0, 0) = 2.0)
        Assert.IsTrue(m2(1, 0) = -2.0)
        Assert.IsTrue(m2(0, 1) = 4.0)
        Assert.IsTrue(m2(1, 1) = -4.0)

    End Sub

    <TestMethod()> _
    Public Sub Multiply2()
        Dim d As Double(,) = {{1.0, 2.0}, {-1.0, -2.0}}
        Dim m As New Matrix(d)

        Dim m2 As Matrix = m * 2.0

        Assert.IsTrue(m2.Rows = 2)
        Assert.IsTrue(m2.Columns = 2)

        Assert.IsTrue(m2(0, 0) = 2.0)
        Assert.IsTrue(m2(1, 0) = -2.0)
        Assert.IsTrue(m2(0, 1) = 4.0)
        Assert.IsTrue(m2(1, 1) = -4.0)

    End Sub

    <TestMethod()> _
    Public Sub Multiply3()
        Dim d1 As Double(,) = {{1.0, 0.0, 2.0}, {-1.0, 3.0, 1.0}}
        Dim m1 As New Matrix(d1)

        Dim d2 As Double(,) = {{3.0, 1.0}, {2.0, 1.0}, {1.0, 0.0}}
        Dim m2 As New Matrix(d2)

        Dim m3 As Matrix = m1 * m2

        Assert.IsTrue(m3.Rows = 2)
        Assert.IsTrue(m3.Columns = 2)

        Assert.IsTrue(m3(0, 0) = 5.0)
        Assert.IsTrue(m3(1, 0) = 4.0)
        Assert.IsTrue(m3(0, 1) = 1.0)
        Assert.IsTrue(m3(1, 1) = 2.0)

    End Sub

    <TestMethod()> _
    Public Sub Multiply4()
        '// Test non-commutivity of multiplication:
        Dim d1 As Double(,) = {{1.0, 2.0}, {3.0, 4.0}}
        Dim m1 As New Matrix(d1)

        Dim d2 As Double(,) = {{0.0, 1.0}, {0.0, 0.0}}
        Dim m2 As New Matrix(d2)

        Dim m3 As Matrix = m1 * m2

        Assert.IsTrue(m3.Rows = 2)
        Assert.IsTrue(m3.Columns = 2)

        Assert.IsTrue(m3(0, 0) = 0.0)
        Assert.IsTrue(m3(1, 0) = 0.0)
        Assert.IsTrue(m3(0, 1) = 1.0)
        Assert.IsTrue(m3(1, 1) = 3.0)

        Dim m4 As Matrix = m2 * m1

        Assert.IsTrue(m4.Rows = 2)
        Assert.IsTrue(m4.Columns = 2)

        Assert.IsTrue(m4(0, 0) = 3.0)
        Assert.IsTrue(m4(1, 0) = 0.0)
        Assert.IsTrue(m4(0, 1) = 4.0)
        Assert.IsTrue(m4(1, 1) = 0.0)

    End Sub

    <TestMethod(), ExpectedException(GetType(InvalidOperationException))> _
    Public Sub Multiply5()
        '// Test non-matching number of rows and columns for valid multiplication:
        Dim d1 As Double(,) = {{1.0, 2.0, 3.0}, {3.0, 4.0, 5.0}}
        Dim m1 As New Matrix(d1)

        Dim d2 As Double(,) = {{0.0, 1.0}, {0.0, 0.0}}
        Dim m2 As New Matrix(d2)

        Dim m3 As Matrix = m1 * m2

    End Sub

    <TestMethod()> _
    Public Sub Addition1()
        Dim d1 As Double(,) = {{1.0, 2.0}, {-1.0, 1.0}}
        Dim m1 As New Matrix(d1)

        Dim d2 As Double(,) = {{3.0, 1.0}, {2.0, 1.0}}
        Dim m2 As New Matrix(d2)

        Dim m3 As Matrix = m1 + m2

        Assert.IsTrue(m3.Rows = 2)
        Assert.IsTrue(m3.Columns = 2)

        Assert.IsTrue(m3(0, 0) = 4.0)
        Assert.IsTrue(m3(1, 0) = 1.0)
        Assert.IsTrue(m3(0, 1) = 3.0)
        Assert.IsTrue(m3(1, 1) = 2.0)

    End Sub

    <TestMethod()> _
    Public Sub Addition2()
        '// Test commutivity of addition:
        Dim d1 As Double(,) = {{1.0, 2.0}, {-1.0, 1.0}}
        Dim m1 As New Matrix(d1)

        Dim d2 As Double(,) = {{3.0, 1.0}, {2.0, 1.0}}
        Dim m2 As New Matrix(d2)

        Dim m3 As Matrix = m1 + m2
        Dim m4 As Matrix = m2 + m1

        Assert.IsTrue(m3.Rows = m4.rows)
        Assert.IsTrue(m3.Columns = m4.columns)

        Assert.IsTrue(m3(0, 0) = m4(0, 0))
        Assert.IsTrue(m3(1, 0) = m4(1, 0))
        Assert.IsTrue(m3(0, 1) = m4(0, 1))
        Assert.IsTrue(m3(1, 1) = m4(1, 1))

    End Sub

    <TestMethod, ExpectedException(GetType(InvalidOperationException))> _
    Public Sub Addition3()
        '// Test non-matching number of rows and columns for valid multiplication:
        Dim d1 As Double(,) = {{1.0, 2.0, 3.0}, {3.0, 4.0, 5.0}}
        Dim m1 As New Matrix(d1)

        Dim d2 As Double(,) = {{0.0, 1.0}, {0.0, 0.0}}
        Dim m2 As New Matrix(d2)

        Dim m3 As Matrix = m1 + m2

    End Sub

    <TestMethod()> _
    Public Sub Subtraction1()
        Dim d1 As Double(,) = {{1.0, 2.0}, {-1.0, 1.0}}
        Dim m1 As New Matrix(d1)

        Dim d2 As Double(,) = {{3.0, 1.0}, {2.0, 1.0}}
        Dim m2 As New Matrix(d2)

        Dim m3 As Matrix = m1 - m2

        Assert.IsTrue(m3.Rows = 2)
        Assert.IsTrue(m3.Columns = 2)

        Assert.IsTrue(m3(0, 0) = -2.0)
        Assert.IsTrue(m3(1, 0) = -3.0)
        Assert.IsTrue(m3(0, 1) = 1.0)
        Assert.IsTrue(m3(1, 1) = 0.0)

    End Sub

    <TestMethod(), ExpectedException(GetType(InvalidOperationException))> _
    Public Sub Subtraction2()
        '// Test non-matching number of rows and columns for valid multiplication:
        Dim d1 As Double(,) = {{1.0, 2.0, 3.0}, {3.0, 4.0, 5.0}}
        Dim m1 As New Matrix(d1)

        Dim d2 As Double(,) = {{0.0, 1.0}, {0.0, 0.0}}
        Dim m2 As New Matrix(d2)

        Dim m3 As Matrix = m1 - m2

    End Sub

    <TestMethod()> _
    Public Sub Equals1()
        Dim d1 As Double(,) = {{1.0, 2.0}, {-1.0, 1.0}}
        Dim m1 As New Matrix(d1)

        Dim d2 As Double(,) = {{3.0, 1.0}, {2.0, 1.0}}
        Dim m2 As New Matrix(d2)

        Assert.IsFalse(m1 = m2)

    End Sub

    <TestMethod()> _
    Public Sub Equals2()
        Dim d1 As Double(,) = {{1.0, 2.0}, {-1.0, 1.0}}
        Dim m1 As New Matrix(d1)

        Dim d2 As Double(,) = {{1.0, 2.0}, {-1.0, 1.0}}
        Dim m2 As New Matrix(d2)

        Assert.IsTrue(m1 = m2)

    End Sub

    <TestMethod()> _
    Public Sub Inequal1()
        Dim d1 As Double(,) = {{1.0, 2.0}, {-1.0, 1.0}}
        Dim m1 As New Matrix(d1)

        Dim d2 As Double(,) = {{3.0, 1.0}, {2.0, 1.0}}
        Dim m2 As New Matrix(d2)

        Assert.IsTrue(m1 <> m2)

    End Sub

    <TestMethod()> _
    Public Sub Inequal2()
        Dim d1 As Double(,) = {{1.0, 2.0}, {-1.0, 1.0}}
        Dim m1 As New Matrix(d1)

        Dim d2 As Double(,) = {{1.0, 2.0}, {-1.0, 1.0}}
        Dim m2 As New Matrix(d2)

        Assert.IsFalse(m1 <> m2)

    End Sub

    <TestMethod()> _
    Public Sub Negation1()
        Dim d1 As Double(,) = {{1.0, 2.0}, {-1.0, 1.0}}
        Dim m1 As New Matrix(d1)

        Dim m2 As Matrix = -m1

        Assert.IsTrue(m2(0, 0) = -1.0)
        Assert.IsTrue(m2(1, 0) = 1.0)
        Assert.IsTrue(m2(0, 1) = -2.0)
        Assert.IsTrue(m2(1, 1) = -1.0)

    End Sub

    <TestMethod()> _
    Public Sub Transpose1()
        Dim d1 As Double(,) = {{1.0, 2.0, 3.0}, {0.0, -6.0, 7.0}}
        Dim m1 As New Matrix(d1)

        Dim m2 As Matrix = m1.Transpose

        Assert.IsTrue(m2.Rows = 3)
        Assert.IsTrue(m2.Columns = 2)

        Assert.IsTrue(m2(0, 0) = 1.0)
        Assert.IsTrue(m2(0, 1) = 0.0)
        Assert.IsTrue(m2(1, 0) = 2.0)
        Assert.IsTrue(m2(1, 1) = -6.0)
        Assert.IsTrue(m2(2, 0) = 3.0)
        Assert.IsTrue(m2(2, 1) = 7.0)

    End Sub

End Class

