Imports DESD.Math

Namespace Integration_Tests
	
    <TestClass> _
Public Class SimpsonsRule_UnitTest

        <TestMethod> _
        Public Sub Integrate01()
            Dim x() As Double = {0.0, 1.0, 2.0, 3.0}
            Dim y() As Double = {0.0, 1.0, 2.0, 3.0}
            Dim Expected() As Double = {0.0, 0.5, 2.0, 4.5}
            Dim integral As Double() = Integration.SimpsonsRule.NonuniformArray(x, y)

            For i As Integer = 0 To 2
                Assert.AreEqual(Expected(i), integral(i))
            Next

        End Sub

        Private Function GetRandomMesh(nPoints As Integer, xMin As Double, xMax As Double) As Double()
            Dim mRMesh(nPoints - 1) As Double

            '// Build out the mesh:
            '// The start and endpoints are already defined by Rmin and Rmax
            '// We'll just fill the first 2 slots with those values, since
            '// we'll be sorting the array at the end.
            mRMesh(0) = xMin
            mRMesh(1) = xMax

            'Dim rng As New MersenneTwister
            Dim rng As New Random

            For i As Integer = 2 To nPoints - 1
                'mRMesh(i) = mRmin + me.Range * rng.GetDouble()
                mRMesh(i) = xMin + (xMax - xMin) * rng.NextDouble
            Next

            Array.Sort(mRMesh)

            Return mRMesh

        End Function

        <TestMethod> _
        Public Sub Integrate02()
            '// Define a simple mesh:



            Dim x As Double() = GetRandomMesh(1000, 0.0, 20.0)


            '// Define the y() array and the expectedvalues array
            Dim y(x.Length - 1) As Double
            Dim expectedy(x.Length - 1) As Double
            For i As Integer = 0 To x.Length - 1
                y(i) = x(i)
                expectedy(i) = x(i) ^ 2 / 2.0
            Next

            '// Do the integration:
            Dim integral As Double() = Integration.SimpsonsRule.NonuniformArray(x, y)

            Dim tol As Double = 0.0000001
            '// Assert the results:
            For i As Integer = 0 To x.Length - 1
                TestUtilities.AssertEqualWithinTolerance(expectedy(i), integral(i), tol)
            Next

        End Sub

        <TestMethod> _
        Public Sub Integrate03()
            '// Define a simple mesh:
            Dim x As Double() = GetRandomMesh(1000, 0.0, 20.0)

            '// Define the y() array and the expectedvalues array
            Dim y(x.Length - 1) As Double
            Dim expectedy(x.Length - 1) As Double
            For i As Integer = 0 To x.Length - 1
                y(i) = System.Math.Cos(x(i))
                expectedy(i) = System.Math.Sin(x(i))
            Next

            '// Do the integration:
            Dim integral As Double() = Integration.SimpsonsRule.NonuniformArray(x, y)

            Dim tol As Double = 0.0001
            '// Assert the results:
            For i As Integer = 0 To x.Length - 1
                TestUtilities.AssertEqualWithinTolerance(expectedy(i), integral(i), tol)
            Next

        End Sub

        '<Test()> _
        Public Sub CompareToTrapezoidal()

            '// The purpose of this test is to demonstrate that Simpson's Rule integration is more accurate than
            '// Trapezoidal Rule integration.

            '// Define a random mesh:
            Dim x As Double() = GetRandomMesh(1000, 0.0, 20.0)

            '// Define the y() array and the expectedvalues array
            Dim y(x.Length - 1) As Double
            Dim expectedy(x.Length - 1) As Double
            For i As Integer = 0 To x.Length - 1
                y(i) = System.Math.Cos(x(i))
                expectedy(i) = System.Math.Sin(x(i))
            Next

            '// Do the integration:
            Dim SimpsonIntegral As Double() = Integration.SimpsonsRule.NonuniformArray(x, y)
            Dim TrapezoidalIntegral As Double() = Integration.TrapezoidalRuleIntegrator.IntegrateArray(x, y)

            Dim SimpsonTol As Double = 0.0001

            '// Assert the results:
            For i As Integer = 0 To x.Length - 1
                TestUtilities.AssertEqualWithinTolerance(expectedy(i), SimpsonIntegral(i), SimpsonTol)
            Next

            Dim TrapTol As Double = 0.0004
            '// Assert the results:
            For i As Integer = 0 To x.Length - 1
                TestUtilities.AssertEqualWithinTolerance(expectedy(i), TrapezoidalIntegral(i), TrapTol)
            Next

        End Sub

    End Class

End Namespace