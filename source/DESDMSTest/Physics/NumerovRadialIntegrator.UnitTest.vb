Imports DESD

Namespace AtomicPhysics_Tests

    <TestClass()> _
    Public Class NumerovRadialIntegratorTests


        ''' <summary>
        ''' Tests the solution of d2y/dr2 = -1 * y
        ''' Solution is sin(r)
        ''' Uses a simple (uniform) mesh
        ''' </summary>
        <TestMethod()> _
        Public Sub IntegrateOutward01()
            '// Define a simple mesh:
            Dim mesh As New SimpleMesh(0.0, 20.0, 1000)

            '// Define the v() array and the expectedvalues array
            Dim v(mesh.Count - 1) As Double
            Dim expectedy(mesh.Count - 1) As Double
            For i As Integer = 0 To mesh.Count - 1
                v(i) = -1.0
                expectedy(i) = System.Math.Sin(mesh.R(i))
            Next

            '// Do the integration:
            Dim y As Double()
            y = NumerovRadialIntegrator.IntegrateOutward(mesh, v, 999, 0.0, 1.0)

            Dim tol As Double = 0.00000001
            Dim expectedtol As Double = mesh.DeltaR ^ 6

            '// Assert the results:
            For i As Integer = 0 To mesh.Count - 1
                TestUtilities.AssertEqualWithinTolerance(expectedy(i), y(i), tol)
            Next

        End Sub


        ''' <summary>
        ''' Tests the solution of d2y/dr2 = -1 * y
        ''' Solution is sin(r)
        ''' Uses a Random mesh
        ''' </summary>
        <TestMethod()> _
        Public Sub IntegrateOutward02()
            '// Define a simple mesh:
            Dim mesh As New RandomMesh(0.0, 20.0, 1000)

            '// Define the v() array and the expectedvalues array
            Dim v(mesh.Count - 1) As Double
            Dim expectedy(mesh.Count - 1) As Double
            For i As Integer = 0 To mesh.Count - 1
                v(i) = -1.0
                expectedy(i) = System.Math.Sin(mesh.R(i))
            Next

            '// Do the integration:
            Dim y As Double()
            y = NumerovRadialIntegrator.IntegrateOutward(mesh, v, 999, 0.0, 1.0)

            Dim tol As Double = 0.00001

            '// Assert the results:
            For i As Integer = 0 To mesh.Count - 1
                TestUtilities.AssertEqualWithinTolerance(expectedy(i), y(i), tol)
            Next

        End Sub


        ''' <summary>
        ''' Tests the solution of d2y/dr2 = +1 * y
        ''' Solution is exp(-x)
        ''' Uses a Random mesh
        ''' </summary>
        <TestMethod()> _
        Public Sub IntegrateOutward03()
            '// Define a simple mesh:
            Dim mesh As New RandomMesh(0.0, 5.0, 1000)

            '// Define the v() array and the expectedvalues array
            Dim v(mesh.Count - 1) As Double
            Dim expectedy(mesh.Count - 1) As Double
            For i As Integer = 0 To mesh.Count - 1
                v(i) = 1.0
                expectedy(i) = System.Math.Exp(-mesh.R(i))
            Next

            '// Do the integration:
            Dim y As Double()
            y = NumerovRadialIntegrator.IntegrateOutward(mesh, v, 999, 1.0, -1.0)

            Dim tol As Double = 0.0001

            '// Assert the results:
            For i As Integer = 0 To mesh.Count - 1
                TestUtilities.AssertEqualWithinTolerance(expectedy(i), y(i), tol)
            Next

        End Sub

        ''' <summary>
        ''' Tests the solution of d2y/dr2 = +1 * y
        ''' Solution is exp(-x)
        ''' Uses a Random mesh
        ''' </summary>
        <TestMethod()> _
        Public Sub IntegrateOutward04()
            '// Define a simple mesh:
            Dim mesh As New SimpleMesh(0.0, 5.0, 1000)

            '// Define the v() array and the expectedvalues array
            Dim v(mesh.Count - 1) As Double
            Dim expectedy(mesh.Count - 1) As Double
            For i As Integer = 0 To mesh.Count - 1
                v(i) = 1.0
                expectedy(i) = System.Math.Exp(-mesh.R(i))
            Next

            '// Do the integration:
            Dim y As Double()
            y = NumerovRadialIntegrator.IntegrateOutward(mesh, v, 999, 1.0, -1.0)

            Dim tol As Double = 0.000001

            '// Assert the results:
            For i As Integer = 0 To mesh.Count - 1
                TestUtilities.AssertEqualWithinTolerance(expectedy(i), y(i), tol)
            Next

        End Sub

        <TestMethod()> _
        Public Sub IntegrateInward01()
            '// Define a simple mesh:
            Dim mesh As New SimpleMesh(0.0, 20.0, 1000)

            '// Define the v() array and the expectedvalues array
            Dim v(mesh.Count - 1) As Double
            Dim expectedy(mesh.Count - 1) As Double
            For i As Integer = 0 To mesh.Count - 1
                v(i) = -1.0
                expectedy(i) = System.Math.Sin(mesh.R(i) - 20.0)
            Next

            '// Do the integration:
            Dim y As Double()
            y = NumerovRadialIntegrator.IntegrateInward(mesh, v, 0, expectedy(999), expectedy(998))

            Dim tol As Double = 0.00000001
            Dim expectedtol As Double = mesh.DeltaR ^ 6

            '// Assert the results:
            For i As Integer = 0 To mesh.Count - 1
                TestUtilities.AssertEqualWithinTolerance(expectedy(i), y(i), tol)
            Next

        End Sub

        <TestMethod()> _
        Public Sub IntegrateInward02()
            '// Define a simple mesh:
            Dim mesh As New RandomMesh(0.0, 20.0, 1000)

            '// Define the v() array and the expectedvalues array
            Dim v(mesh.Count - 1) As Double
            Dim expectedy(mesh.Count - 1) As Double
            For i As Integer = 0 To mesh.Count - 1
                v(i) = -1.0
                expectedy(i) = System.Math.Sin(mesh.R(i))
            Next

            '// Do the integration:
            Dim y As Double()
            y = NumerovRadialIntegrator.IntegrateInward(mesh, v, 0, expectedy(999), expectedy(998))

            Dim tol As Double = 0.00001

            '// Assert the results:
            For i As Integer = 0 To mesh.Count - 1
                TestUtilities.AssertEqualWithinTolerance(expectedy(i), y(i), tol)
            Next

        End Sub

        <TestMethod()> _
        Public Sub IntegrateInward03()
            '// Define a simple mesh:
            Dim mesh As New RandomMesh(0.0, 5.0, 1000)

            '// Define the v() array and the expectedvalues array
            Dim v(mesh.Count - 1) As Double
            Dim expectedy(mesh.Count - 1) As Double
            For i As Integer = 0 To mesh.Count - 1
                v(i) = 1.0
                expectedy(i) = System.Math.Exp(-mesh.R(i))
            Next

            '// Do the integration:
            Dim y As Double()
            y = NumerovRadialIntegrator.IntegrateInward(mesh, v, 0, expectedy(999), expectedy(998))

            Dim tol As Double = 0.0001

            '// Assert the results:
            For i As Integer = 0 To mesh.Count - 1
                TestUtilities.AssertEqualWithinTolerance(expectedy(i), y(i), tol)
            Next

        End Sub

        ''' <summary>
        ''' Tests the solution of d2y/dr2 = +1 * y
        ''' Solution is exp(-x)
        ''' Uses a Random mesh
        ''' </summary>
        <TestMethod()> _
        Public Sub IntegrateInward04()
            '// Define a simple mesh:
            Dim mesh As New SimpleMesh(0.0, 5.0, 1000)

            '// Define the v() array and the expectedvalues array
            Dim v(mesh.Count - 1) As Double
            Dim expectedy(mesh.Count - 1) As Double
            For i As Integer = 0 To mesh.Count - 1
                v(i) = 1.0
                expectedy(i) = System.Math.Exp(-mesh.R(i))
            Next

            '// Do the integration:
            Dim y As Double()
            y = NumerovRadialIntegrator.IntegrateInward(mesh, v, 0, expectedy(999), expectedy(998))

            Dim tol As Double = 0.000001

            '// Assert the results:
            For i As Integer = 0 To mesh.Count - 1
                TestUtilities.AssertEqualWithinTolerance(expectedy(i), y(i), tol)
            Next

        End Sub


    End Class

End Namespace
