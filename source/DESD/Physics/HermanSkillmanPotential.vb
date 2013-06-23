


    ''' <summary>
    ''' Encapsulates the algorithms for computing the atomic potential
    ''' using the method of Herman and Skillman.
    ''' </summary>
    Public Class HermanSkillmanPotential

		Public Enum ExchangeMode
			NoExchange = 0
			NonStatisticalExchange = 1
			StatisticalExchange = 2
		End Enum


#Region "Shared Methods"

        Public Shared Function GetPotential(ByVal mesh As IRadialMesh, ByVal Z As Element, ByVal orbitals As IList(Of Orbital), ByVal exchange As ExchangeMode, byval useLatterTail as boolean) As Double()

            '// Dimension arrays
            Dim iMax As Integer = mesh.Count - 1
            Dim r As Double() = mesh.GetArray()     '// An array for radial r values

            '// Compute the total number of electrons and the total radial charge density Sigma(),

            '// Initialize the number of electrons as zero.  We'll accumulate the total
            '// occupancy as we enumerate through the electron orbitals.
            Dim nE As Double = 0.0

            '// Go through the list of electron orbitals and accumulate the total
            '// number of electrons and the the total radial charge density.
            '// The first point of Sigma is zero by definition,
            '// since Pnl(0) = 0.0
            Dim Sigma(iMax) As Double
            For Each orb As Orbital In orbitals
                nE += orb.Occupancy
                For i As Integer = 0 To iMax
                    'Sigma(i) += -orb.Occupancy * orb.P(i) ^ 2
                    Sigma(i) += orb.sigma(i)
                Next
            Next
			'Console.WriteLine("Number of electrons in HSP is " & nE.ToString)
			
            '// Compute the integral of Sigma from 0 to r -
            '// This will be used later to compute Term 2 of the potential.
            'Dim Term2 As Double() = Sieger.Math.Integration.SimpsonsRule.NonuniformArray(r, Sigma)
        Dim Term2 As Double() = DESD.Math.Integration.TrapezoidalRuleIntegrator.IntegrateArray(r, Sigma)


            '// Compute Rho, Term1 and the intermediate array for Term3, SigmaOverR():
            Dim Rho(iMax) As Double
            Dim Term1(iMax) As Double               '// Array for -2Z/r, the nuclear coulomb term.
            Dim SigmaOverR(iMax) As Double
            Dim VExchange(iMax) As Double           '// Array for the exchange potential.

            '// Get the statistical exchange parameter alpha, if called for
            Dim alpha As Double = 1.0
            If exchange = ExchangeMode.StatisticalExchange Then alpha = StatisticalAlpha(Z)

            '// Set the values at the origin:
            Rho(0) = 0.0
            Term1(0) = Double.NegativeInfinity  '// We won't use this value, so it's kind of academic.
            SigmaOverR(0) = 0.0

            '// Define convenience values
            Dim OneOver4Pi As Double = 1.0 / (4.0 * System.Math.PI)
            Dim ThreeOver8Pi As Double = 3.0 / (8.0 * System.Math.PI)
            Dim OneThird As Double = 1.0 / 3.0

            For i As Integer = 1 To iMax
                Term1(i) = -2.0 * Z / r(i)
                SigmaOverR(i) = Sigma(i) / r(i)
                Rho(i) = OneOver4Pi * Sigma(i) / r(i) ^ 2
                If exchange = exchangemode.NoExchange Then
                	VExchange(i) = 0.0
                Else
                	VExchange(i) = -6.0 * alpha * System.Math.Pow(System.Math.Abs(ThreeOver8Pi * System.Math.Abs(Rho(i))), OneThird)	
                End If
            Next

            '// Now compute the integral of SigmaOverR from 0 to r:
            'Dim Term3 As Double() = Sieger.Math.Integration.SimpsonsRule.NonuniformArray(r, SigmaOverR)
        Dim Term3 As Double() = DESD.Math.Integration.TrapezoidalRuleIntegrator.IntegrateArray(r, SigmaOverR)

            '// Now we're ready to put it all together and compute the potential:

            '// Define the potential array:
            Dim V(iMax) As Double

            '// We'll set the first value to Double.NegativeInfinity
            V(0) = Double.NegativeInfinity

            '// Compute V at the switching radius for Latter tail:
            '// if r * V is greater than this value, we should replace it with the latter tail.
            Dim RVLatter As Double
            Dim iLatter As Integer = 1
			If useLatterTail Then
             	RVLatter  = -2.0 * (CDbl(Z) - nE + 1.0)
			Else
             	RVLatter  = double.PositiveInfinity
			End If

            '// Now compute the potential:
            Dim Temp As Double
            For i As Integer = 1 To iMax
                Temp = Term1(i) - 2.0 * Term2(i) / r(i) - 2.0 * (Term3(iMax) - Term3(i)) + VExchange(i)
                If r(i) * Temp > RVLatter Then Exit For
                V(i) = Temp
                iLatter += 1
            Next

			'// Purely diagnostic
'			If Not(uselattertail) Then
'				Console.WriteLine("R, V, Term1, Term2, Term3, Vexchange")
'				For i As Integer = 1 To iMax
'					Console.WriteLine(r(i).ToString & ", " & V(i).ToString & ", " & Term1(i).ToString & ", " & Term2(i).ToString & ", " & Term3(i).ToString & ", " & VExchange(i).ToString)
'	            Next
'
'			End If
			'// OK, so the issue is instability in term2 and term3.  The bug comes from Term3(iMax) - Term3(i)
			
            '// Fill out the rest of the potential with the Latter tail:
            'Console.WriteLine("Ilatter = " & ilatter.ToString & " and iMax = " & iMax.ToString)
            For i As Integer = iLatter To iMax
                V(i) = (-2.0 * (CDbl(Z) - nE + 1.0)) / r(i)
            Next

            Return V

        End Function


        Public Shared Function GetPotentialDetails(ByVal mesh As IRadialMesh, ByVal Z As Element, ByVal orbitals As IList(Of Orbital), ByVal exchange As ExchangeMode, byval useLatterTail as boolean) As AtomicPotential

            '// Dimension arrays
            Dim iMax As Integer = mesh.Count - 1
            Dim r As Double() = mesh.GetArray()     '// An array for radial r values

            '// Compute the total number of electrons and the total radial charge density Sigma(),

            '// Initialize the number of electrons as zero.  We'll accumulate the total
            '// occupancy as we enumerate through the electron orbitals.
            Dim nE As Double = 0.0

            '// Go through the list of electron orbitals and accumulate the total
            '// number of electrons and the the total radial charge density.
            '// The first point of Sigma is zero by definition,
            '// since Pnl(0) = 0.0
            Dim Sigma(iMax) As Double
            For Each orb As Orbital In orbitals
                nE += orb.Occupancy
                For i As Integer = 0 To iMax
                    'Sigma(i) += -orb.Occupancy * orb.P(i) ^ 2
                    Sigma(i) += orb.sigma(i)
                Next
            Next

            '// Compute the integral of Sigma from 0 to r -
            '// This will be used later to compute Term 2 of the potential.
            'Dim Term2 As Double() = Sieger.Math.Integration.SimpsonsRule.NonuniformArray(r, Sigma)
        Dim Term2 As Double() = DESD.Math.Integration.TrapezoidalRuleIntegrator.IntegrateArray(r, Sigma)


            '// Compute Rho, Term1 and the intermediate array for Term3, SigmaOverR():
            Dim Rho(iMax) As Double
            Dim Term1(iMax) As Double               '// Array for -2Z/r, the nuclear coulomb term.
            Dim SigmaOverR(iMax) As Double
            Dim VExchange(iMax) As Double           '// Array for the exchange potential.

            '// Get the statistical exchange parameter alpha, if called for
            Dim alpha As Double = 1.0
            If exchange = ExchangeMode.StatisticalExchange Then alpha = StatisticalAlpha(Z)

            '// Set the values at the origin:
            Rho(0) = 0.0
            Term1(0) = Double.NegativeInfinity  '// We won't use this value, so it's kind of academic.
            SigmaOverR(0) = 0.0

            '// Define convenience values
            Dim OneOver4Pi As Double = 1.0 / (4.0 * System.Math.PI)
            Dim ThreeOver8Pi As Double = 3.0 / (8.0 * System.Math.PI)
            Dim OneThird As Double = 1.0 / 3.0

            For i As Integer = 1 To iMax
                Term1(i) = -2.0 * Z / r(i)
                SigmaOverR(i) = Sigma(i) / r(i)
                Rho(i) = OneOver4Pi * Sigma(i) / r(i) ^ 2
                If exchange = exchangemode.NoExchange Then
                	VExchange(i) = 0.0
                Else
                	VExchange(i) = -6.0 * alpha * System.Math.Pow(System.Math.Abs(ThreeOver8Pi * System.Math.Abs(Rho(i))), OneThird)	
                End If
            Next

            '// Now compute the integral of SigmaOverR from 0 to r:
            'Dim Term3 As Double() = Sieger.Math.Integration.SimpsonsRule.NonuniformArray(r, SigmaOverR)
        Dim Term3 As Double() = DESD.Math.Integration.TrapezoidalRuleIntegrator.IntegrateArray(r, SigmaOverR)

            '// Now we're ready to put it all together and compute the potential:

            '// Define the potential array:
            Dim V(iMax) As Double

            '// We'll set the first value to Double.NegativeInfinity
            V(0) = Double.NegativeInfinity

            '// Compute V at the switching radius for Latter tail:
            '// if r * V is greater than this value, we should replace it with the latter tail.
            Dim RVLatter As Double
            Dim iLatter As Integer = 1
			If useLatterTail Then
             	RVLatter  = -2.0 * (CDbl(Z) - nE + 1.0)
			Else
             	RVLatter  = double.PositiveInfinity
			End If

            '// Now compute the potential:
            Dim Temp As Double
            For i As Integer = 1 To iMax
                Temp = Term1(i) - 2.0 * Term2(i) / r(i) - 2.0 * (Term3(iMax) - Term3(i)) + VExchange(i)
                If r(i) * Temp > RVLatter Then Exit For
                V(i) = Temp
                iLatter += 1
            Next

            '// Fill out the rest of the potential with the Latter tail:
            For i As Integer = iLatter To iMax
                V(i) = (-2.0 * (CDbl(Z) - nE + 1.0)) / r(i)
            Next

            Return New AtomicPotential(mesh,V,Rho)

        End Function

'        Public Shared Function GetPotential(ByVal mesh As IRadialMesh, ByVal Z As Element, ByVal nE As Double) As Double()
'            '// For starters, let's just use V(r) = -2Z/r and the latter tail...
'
'            '// Compute V at the switching radius for Latter tail:
'            '// if r * V is greater than this value, we should replace it with the latter tail.
'            Dim RVLatter As Double = -2.0 * (CDbl(Z) - nE + 1.0)
'            Dim iLatter As Integer = 1
'            Dim iMax As Integer = mesh.Count - 1
'            Dim V(iMax) As Double
'            V(0) = Double.NegativeInfinity
'
'            '// Now compute the potential:
'            Dim Temp As Double
'            For i As Integer = 1 To iMax
'                Temp = -2.0 * CDbl(Z)
'                If Temp > RVLatter Then Exit For
'                V(i) = Temp / mesh.R(i)
'                iLatter += 1
'            Next
'
'            '// Fill out the rest of the potential with the Latter tail:
'            For i As Integer = iLatter To iMax
'                V(i) = (-2.0 * (CDbl(Z) - nE + 1.0)) / mesh.R(i)
'            Next
'
'            Return V
'
'        End Function

        ''' <summary>
        ''' Statistical exchange parameter Alpha, taken from
        ''' http://hermes.phys.uwm.edu/projects/elecstruct/Alpha.StatExchange.html
        ''' We are using the HF value.  Only values out to Z = 41 are tabulated.
        ''' We fill in the matrix with Z = 0 as zero (not used), and Z = 1 as 1.0.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function StatisticalAlpha(ByVal Z As Integer) As Double
            Dim mAlpha As Double() = {0.0, 1.0, 0.77298, 0.78147, 0.76823, 0.76531, 0.75928, 0.75197, 0.74447, 0.73732, 0.73081, _
                                          0.73115, 0.72913, 0.72853, 0.72751, 0.7262, 0.72475, 0.72325, 0.72177, 0.72117, 0.71984, _
                                          0.71841, 0.71695, 0.71556, 0.71352, 0.71279, 0.71151, 0.71018, 0.70896, 0.70697, 0.70673, _
                                          0.7069, 0.70684, 0.70665, 0.70638, 0.70606, 0.70574, 0.70553, 0.70504, 0.70465, 0.70424, _
            0.70383}
            If Z <= 41 Then
                Return mAlpha(Z)
            Else
                Return mAlpha(41)
            End If
        End Function
        
        
#End Region

    End Class

