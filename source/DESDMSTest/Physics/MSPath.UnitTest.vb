
Imports DESD

<TestClass> _
Public Class MSPath_UnitTest

    <TestMethod()> _
    Public Sub Constructor01()
        Dim c As Cluster = CreateTestCluster()
        Dim newpath As New MSPath(c, 0)

        Assert.AreEqual(1, newpath.Count)
        Assert.AreEqual(0, newpath.Order)
        Assert.IsNull(newpath.Amplitude)
        Assert.IsNull(newpath.Parent)
        Assert.IsNull(newpath.ParentRootAmplitude)

        Assert.AreEqual(0.0, newpath.TerminalAtom.Position.X)

    End Sub

    Private Function CreateTestCluster() As Cluster
        Dim c As New Cluster()

        Dim atomID As Integer
        atomID = c.AddAtom(0.0, 0.0, 0.0, 0)
        atomID = c.AddAtom(2.0, 0.0, 0.0, 0)
        atomID = c.AddAtom(2.0, 2.0, 0.0, 1)

        Dim speciesID As Integer
        speciesID = c.AddSpecies(Element.Silicon, 1.1747734030698, Element.Silicon.Configuration.ToString)
        speciesID = c.AddSpecies(Element.Nickel, 1.0530626450941)

        Return c
    End Function

    <TestMethod()> _
    Public Sub Constructor02()
        Dim c As Cluster = CreateTestCluster()
        Dim newpath As New MSPath(c.GetAtom(0))

        Assert.AreEqual(1, newpath.Count)
        Assert.AreEqual(0, newpath.Order)

    End Sub


End Class
