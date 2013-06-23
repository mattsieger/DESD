
''' <summary>
''' Solves the radial Schrodinger equation for a positive (unbound) energy state.
''' </summary>
Public Class UnboundRSESolver
	
	
    ''' <summary>
    ''' Solves the radial Schrodinger equation for r times the radial wave function of an unbound state with 
    ''' energy E and angular momentum L.
    ''' </summary>
    ''' <param name="mesh">An instance of IRadialMesh containing the radial mesh.</param>
    ''' <param name="E">The energy of the electron.</param>
    ''' <param name="L">The angular momentum quantum number.</param>
    ''' <param name="V0">The potential, minus the angular momentum term.</param>
    ''' <returns>r times the radial wave function, tabulated at the mesh points.  The wave function is NOT normalized.</returns>
    ''' <remarks></remarks>
    Public Shared Function Solve(ByVal mesh As IRadialMesh, byval E as Double, ByVal L As Integer, ByVal V0 As Double()) As Double()

        '// Do some basic bounds checking:
        if E < 0.0 then throw new ArgumentException("Energy must be positive.")
        If L < 0 Then Throw New ArgumentException("Angular momentum quantum number L must be positive or zero.")
        If mesh Is Nothing Then Throw New ArgumentException("Invalid mesh.")
        If V0 Is Nothing Then Throw New ArgumentException("V0 array is nothing.")


        '// Get the biggest index - a convenience.
        Dim iMax As Integer = mesh.Count - 1

        '// Compute the potential used for the numerov solve V(r) = VL(r) - E, and
        '// Add the angular momentum term to the potential:
        Dim V(iMax) As Double
        V(0) = double.NegativeInfinity
        'V(0) = 0.0
        For i As Integer = 1 To iMax
            V(i) = V0(i) - E + CDbl(L * (L + 1)) / (mesh.R(i) ^ 2)
            'V(i) = -E + CDbl(L * (L + 1)) / (mesh.R(i) ^ 2) + 1.08 * V0(i)
        Next


        '// Integrate outward from the origin.
        '// This *could* throw an exception.  What do we do in that case?
        Dim OutwardIntegral() As Double = NumerovRadialIntegrator.IntegrateOutward(mesh, V, iMax, 0.0, 0.1)

        Return OutwardIntegral

    End Function

End Class
