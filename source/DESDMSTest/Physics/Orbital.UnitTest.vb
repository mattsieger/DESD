Imports DESD

Namespace AtomicPhysics_Tests

    <TestClass()> _
    Public Class OrbitalTests

        <TestMethod()> _
        Public Sub Constructor01()
            Dim wf As Double() = {1.0, 2.0, 3.0}
            Dim mesh As New SimpleMesh(0.0, 2.0, 3)

            Dim o As New Orbital(2, 1, 1.0, -5.0, wf)

            Assert.AreEqual(2, o.N)
            Assert.AreEqual(1, o.L)
            Assert.AreEqual(1.0, o.Occupancy)
            Assert.AreEqual(-5.0, o.Energy)

            For i As Integer = 0 To wf.Length - 1
                Assert.AreEqual(wf(i), o.P(i))
            Next
        End Sub

    End Class

End Namespace

