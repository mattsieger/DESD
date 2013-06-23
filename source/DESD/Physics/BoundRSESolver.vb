
	

''' <summary>
''' This class encapsulates the (shared) methods that solve the radial Schrodinger equation for a central 
''' potential, for a bound state (E less than 0).
''' </summary>
''' <remarks></remarks>
Public Class BoundRSESolver

    Private Const LogDerivativeTolerance As Double = 0.001

    ''' <summary>
    ''' Solves the radial Schrodinger equation for the bound state energy E and radial wave function.
    ''' </summary>
    ''' <param name="mesh"></param>
    ''' <param name="N">The primary quantum number.</param>
    ''' <param name="L">The angular momentum quantum number.</param>
    ''' <param name="occupancy">The occupancy of the state.  This value is not used
    ''' during the calculation, but is passed through to the Orbital object that is
    ''' returned.</param>
    ''' <param name="V0">The potential, minus the angular momentum term.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Solve(ByVal mesh As IRadialMesh, ByVal N As Integer, ByVal L As Integer, ByVal occupancy As Double, ByVal V0 As Double()) As Orbital

        '// Do some basic bounds checking:
        If N < 1 Then Throw New ArgumentException("Invalid primary quantum number N - must be greater than zero.")
        If L >= N Then Throw New ArgumentException("Angular Momentum quantum number L must be less than N.")
        If L < 0 Then Throw New ArgumentException("Angular Momentum quantum number L must be positive or zero.")
        If mesh Is Nothing Then Throw New ArgumentException("Invalid mesh.")
        If V0 Is Nothing Then Throw New ArgumentException("V0 array is nothing.")



        '// Get the biggest index - a convenience.
        Dim iMax As Integer = mesh.Count - 1

        '// Add the angular momentum term to the potential:
        Dim VL(iMax) As Double
        VL(0) = 0.0
        For i As Integer = 1 To iMax
            VL(i) = V0(i) + CDbl(L * (L + 1)) / mesh.R(i) ^ 2
        Next



        '// Set up the binary search on energy E
        '// The initial bounds are:
        Dim Emin As Double = VL.min
        Dim Emax As Double = System.Math.Min(VL(iMax), 0.0)
        Dim E As Double
        Dim Converged As Integer = +1
        Dim Pnl(iMax) As Double

        '// Set up the main binary search loop:
        Dim NIterations As Integer = 0
        Do Until (Converged = 0)

            '// Set the trial energy E to be the midpoint of the min and max bounds.
            E = (Emin + Emax) / 2.0

            Converged = SingleEStep(mesh, VL, E, N, L, Pnl)

            If Converged > 0 Then
                '// We need to increase E
                Emin = E
            ElseIf Converged < 0 Then
                '// We need to decrease E
                Emax = E
            End If

            NIterations += 1

            If NIterations > 1000 Then
                Throw New Exception("Failed to converge in BoundRSESolver.Solve")
            ElseIf NIterations = 100 Then
                Dim blah As Integer = 1
            End If
        Loop

        '// If we got here, then we've converged on E or reached the maximum number of iterations allowed.

        '// Wave function solved - construct output structure
        Return New Orbital(N, L, occupancy, E, Pnl)


    End Function


    ''' <summary>
    ''' Returns +1 if energy needs to be increased, -1 if energy needs to be decreased, 0 if trial energy
    ''' satisfies the convergence criteria.
    ''' </summary>
    ''' <param name="mesh"></param>
    ''' <param name="VL"></param>
    ''' <param name="E"></param>
    ''' <param name="ExpectedNCross"></param>
    ''' <param name="Pnl"></param>
    ''' <returns></returns>
    Private Shared Function SingleEStep(ByVal mesh As IRadialMesh, ByVal VL As Double(), ByVal E As Double, ByVal N As Integer, ByVal L As Integer, ByRef Pnl As Double()) As Integer

        Dim iMax As Integer = mesh.Count - 1

        '// Compute the potential used for the numerov solve V(r) = VL(r) - E, and
        '// find iMatch, the index for the matching radius rMatch where V(r) becomes greater than E
        '// Note that we move from iMax to 1, going inward.  This is because we are interested in 
        '// the *largest* radius at which the potential crosses zero, there could be other crossings.
        Dim iMatch As Integer = -1
        Dim V(iMax) As Double
        For i As Integer = iMax To 1 Step -1
            V(i) = VL(i) - E
            If (iMatch < 0) And (V(i) < 0.0) Then
                iMatch = i
            End If
        Next

        '// If V(i) is less than zero everywhere, then binding E is too small and we need to increase it.
        If iMatch < 4 Then Return +1
        '// If V(i) is everywhere greater than zero, the binding E is too big and we need to decrease it.
        If iMatch > iMax - 4 Then Return -1

        '// Do the outward integration to just beyond iMatch (iMatch + 1)
        '// This *could* throw an exception.  What do we do in that case?
        Dim OutwardIntegral() As Double = NumerovRadialIntegrator.IntegrateOutward(mesh, V, iMatch + 1, 0.0, 0.1)

        '// Count the number of zero crossings in the outward integral.
        Dim ExpectedNCross As Integer = N - L - 1
        Dim NCross As Integer = ZeroCrossings(OutwardIntegral, 1, iMatch)

        '// If the number of crossings is too low, we need to increase the binding energy:
        If NCross < ExpectedNCross Then Return +1

        '// If the number of crossings is too high, we need to decrease the binding energy:
        If NCross > ExpectedNCross Then Return -1

        '// The outward integral should either be positive with decreasing derivative,
        '// or negative with increasing derivative.
        '// SO, the logarithmic derivative should always be negative to be valid.
        '// If it's not, that means that the function is either positive and increasing
        '// (meaning that the energy should be decreased), or negative and
        '// decreasing (meaning the same thing).
        '// Using a centered approximation for the first derivative.
        Dim OutwardLD As Double = 0.5 * ((OutwardIntegral(iMatch + 1) - OutwardIntegral(iMatch)) / mesh.DR(iMatch, iMatch + 1) + (OutwardIntegral(iMatch) - OutwardIntegral(iMatch - 1)) / mesh.DR(iMatch - 1, iMatch)) / OutwardIntegral(iMatch)
        If OutwardLD >= 0.0 Then Return +1

        '// If we get here, we have the expected number of crossings and should do the inward integral.
        '// Here we have an issue.  In many cases, if the Rmax is much larger than the matching radius, 
        '// the inward integral exceeds double.maxvalue on the way in.  
        '// Final solution was to dynamically renormalize the inward integral if the value exceeds some maximum limit.
        'Dim InwardIntegral() As Double = NumerovRadialIntegrator.IntegrateInward(mesh, V, iMatch - 1, 0.0, 1.0E-50)
        Dim InwardIntegral() As Double = NumerovRadialIntegrator.IntegrateInward(mesh, V, iMatch - 1, 0.0, 1.0E-300)

        '// Compute the logarithmic derivatives at the matching radius:
        '// Using a centered approximation for the first derivative.
        Dim InwardLD As Double = 0.5 * ((InwardIntegral(iMatch + 1) - InwardIntegral(iMatch)) / mesh.DR(iMatch, iMatch + 1) + (InwardIntegral(iMatch) - InwardIntegral(iMatch - 1)) / mesh.DR(iMatch - 1, iMatch)) / InwardIntegral(iMatch)
        Dim LDDiff As Double = OutwardLD - InwardLD


        '// Check to see if Outward and Inward logarithmic derivatives are close enough to call "passing"
        Dim LDTol As Double = System.Math.Abs(OutwardLD - InwardLD) / System.Math.Abs(OutwardLD + InwardLD)
        If LDTol < LogDerivativeTolerance Then
            '// We've converged on a solution.  Stitch the outward and inward
            '// integrals together and normalize the full solution.
            ReDim Pnl(iMax)
            Dim OutwardNorm As Double = OutwardIntegral(iMatch)
            Dim Pnl2(iMax) As Double
            '// Copy over the outward solution:
            For i As Integer = 0 To iMatch
                Pnl(i) = OutwardIntegral(i) / OutwardNorm
                Pnl2(i) = Pnl(i) * Pnl(i)
            Next
            Dim InwardNorm As Double = InwardIntegral(iMatch)
            For i As Integer = iMatch + 1 To iMax
                Pnl(i) = InwardIntegral(i) / InwardNorm
                Pnl2(i) = Pnl(i) * Pnl(i)
            Next

            '// Now normalize the full solution - first compute the integral
            '// of Pnl^2
            'Dim IntegPnl2 As Double = Sieger.Math.Integration.TrapezoidalRuleIntegrator.Integrate(mesh.GetArray, Pnl2, 0, iMax)
            Dim IntegPnl2 As Double() = DESD.Math.Integration.SimpsonsRule.NonuniformArray(mesh.GetArray, Pnl2)
            'Dim NormFactor As Double = System.Math.Sqrt(IntegPnl2)
            Dim NormFactor As Double = System.Math.Sqrt(IntegPnl2(iMax))

            For i As Integer = 0 To iMax
                Pnl(i) = Pnl(i) / NormFactor
            Next

            Return 0

        End If

        '// Not in tolerance.  Figure out if we need to increase or decrease and move on.

        '// Outward LD should be less than zero.  If it isn't, we need to go deeper.
        'If OutwardLD > 1 Then Return +1


        If System.Math.Abs(OutwardLD / InwardLD) > 1 Then Return -1

        Return +1


    End Function


    ''' <summary>
    ''' Counts the number of times the given array A() crosses zero (changes sign) on the interval istart to iend.
    ''' </summary>
    ''' <param name="A"></param>
    ''' <param name="istart"></param>
    ''' <param name="iend"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ZeroCrossings(ByVal A As Double(), ByVal istart As Integer, ByVal iend As Integer) As Integer

        Dim NCross As Integer = 0
        Dim lastsign As Double = System.Math.Sign(A(istart))
        For i As Integer = istart + 1 To iend
            If System.Math.Sign(A(i)) <> lastsign Then
                NCross += 1
                lastsign = System.Math.Sign(A(i))
            End If
        Next

        Return NCross

    End Function


    Public Delegate Function SolveDelegate(ByVal mesh As IRadialMesh, ByVal N As Integer, ByVal L As Integer, ByVal occupancy As Double, ByVal V As Double()) As Orbital

End Class


