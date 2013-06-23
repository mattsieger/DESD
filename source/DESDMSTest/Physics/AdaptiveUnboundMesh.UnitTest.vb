Imports DESD

<TestClass> _
Public Class AdaptiveUnboundMesh_UnitTest

    ''' <summary>
    ''' Test the default mesh size.
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub Constructor1()
        '// Test for Aluminum
        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)
        Console.WriteLine("Length = " & myMesh.Count)
        '// Print out the mesh:
        For i As Integer = 0 To myMesh.Count - 1
            Console.WriteLine(myMesh.R(i).ToString)
        Next

    End Sub

    ''' <summary>
    ''' Tests proposition that all mesh values are unique and are sorted smallest to largest.
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()> _
    Public Sub MeshIsSorted()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Dim lastValue As Double = myMesh.R(0)

        For i As Integer = 1 To myMesh.Count - 1

            Assert.IsTrue(myMesh.R(i) > lastValue)
            lastValue = myMesh.R(i)

        Next

    End Sub

    ''' <summary>
    ''' Attempts to call for a mesh point with invalid index.
    ''' </summary>
    <TestMethod(), ExpectedException(GetType(IndexOutOfRangeException))> _
    Public Sub Exception1()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Dim X As Double = myMesh.R(-1)

    End Sub

    <TestMethod(), ExpectedException(GetType(IndexOutOfRangeException))> _
    Public Sub Exception2()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Dim X As Double = myMesh.R(myMesh.Count)

    End Sub

    <TestMethod()> _
    Public Sub GetEnumerator1()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Dim Enumerator As IEnumerator = myMesh.GetEnumerator

        Assert.IsNotNull(Enumerator)

    End Sub

    <TestMethod()> _
    Public Sub GetEnumerator2()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Dim Enumerator As IEnumerator = myMesh.GetEnumerator1

        Assert.IsNotNull(Enumerator)

    End Sub

    <TestMethod()> _
    Public Sub Type()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Assert.IsTrue(myMesh.Type = MeshType.CubicAdaptive)

    End Sub




    <TestMethod(), ExpectedException(GetType(IndexOutOfRangeException))> _
    Public Sub DX4()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Dim x As Double = myMesh.DR(-1, 0)

    End Sub

    <TestMethod(), ExpectedException(GetType(IndexOutOfRangeException))> _
    Public Sub DX5()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Dim x As Double = myMesh.DR(5, myMesh.Count)

    End Sub

    <TestMethod(), ExpectedException(GetType(IndexOutOfRangeException))> _
    Public Sub DX6()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Dim x As Double = myMesh.DR(-1, myMesh.Count)

    End Sub

    <TestMethod()> _
    Public Sub DX7()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Assert.AreEqual(0.0, myMesh.DR(1, 1))

    End Sub


    <TestMethod()> _
    Public Sub Range1()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Assert.AreEqual(myMesh.R(myMesh.Count - 1) - myMesh.R(0), myMesh.Range)

    End Sub

    <TestMethod()> _
    Public Sub Max1()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Assert.AreEqual(myMesh.R(myMesh.Count - 1), myMesh.Max)

    End Sub

    <TestMethod()> _
    Public Sub Max2()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, 100.0, 650, 10.0, 30)

        Assert.AreEqual(100.0, myMesh.Max, 0.0000000001)

    End Sub

    <TestMethod()> _
    Public Sub Min1()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Assert.AreEqual(myMesh.R(0), myMesh.Min)

    End Sub

    <TestMethod()> _
    Public Sub Min2()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Assert.AreEqual(0.0, myMesh.Min, 0.0000000001)

    End Sub


    <TestMethod()> _
    Public Sub BlockCount1()

        Dim myMesh As New AdaptiveUnboundMesh(0.0, HermanSkillmanMesh.GetCMU(13) * 205.0, 650, 10.0, 30)

        Assert.AreEqual(1, myMesh.BlockCount)

    End Sub


End Class
