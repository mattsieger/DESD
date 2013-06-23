Imports DESD
Imports DESD.Math

<TestClass> _
Public Class DESDCalculation_UnitTest

    <TestMethod()> _
    Public Sub CalculateClusterPhaseShifts01()
        Dim desd As New DESDCalculation

        Dim c As New Cluster("C:\temp\clustertest.cst")

        desd.CalculateClusterPhaseShifts(c, 13.6056923, 6, 0.0, 0.0, 0.0)

        Assert.AreEqual(7, c.PhaseShifts(0).Length)
        Assert.AreEqual(7, c.Tmatrix(0).Length)

        '// Check silicon (speciesID = 0) phase shifts:
        Dim SiPhases As Complex() = c.PhaseShifts(0)

        '		Dim ps as Complex() = c.PhaseShifts(0)
        '		For i As Integer = 0 To 6
        '			console.WriteLine(ps(i).ToString)
        '		Next

        Assert.AreEqual(-0.0239582880414027, SiPhases(0).Real, 0.000000000000001)
        Assert.AreEqual(0.270423838156701, SiPhases(1).Real, 0.000000000000001)
        Assert.AreEqual(0.0620324086460263, SiPhases(2).Real, 0.000000000000001)
        Assert.AreEqual(0.00241537770598892, SiPhases(3).Real, 0.000000000000001)



    End Sub

    <TestMethod()> _
    Public Sub CalculateClusterPhaseShifts02()
        Dim desd As New DESDCalculation

        Dim c As New Cluster("C:\temp\clustertest.cst")

        desd.CalculateClusterPhaseShifts(c, 8.0 * 13.6056923, 6, 0.0, 300.0, 440.0)

        '// Check Nickel (speciesID = 1) phase shifts:
        Dim NiPhases As Complex() = c.PhaseShifts(1)

        '		For i As Integer = 0 To 6
        '			console.WriteLine(NiPhases(i).ToString)
        '		Next

        Assert.AreEqual(1.18462461019072, NiPhases(0).Real, 0.000001)
        Assert.AreEqual(0.0831946369154793, NiPhases(0).Imag, 0.000001)

        Assert.AreEqual(-0.927719438504006, NiPhases(1).Real, 0.000001)
        Assert.AreEqual(0.0472191061531443, NiPhases(1).Imag, 0.000001)

        Assert.AreEqual(-0.376577863560398, NiPhases(2).Real, 0.000001)
        Assert.AreEqual(0.0469060583750613, NiPhases(2).Imag, 0.000001)

        Assert.AreEqual(0.409465738620424, NiPhases(3).Real, 0.000001)
        Assert.AreEqual(0.0311583771667739, NiPhases(3).Imag, 0.000001)


    End Sub


    <TestMethod()> _
    Public Sub CalculateClusterPhaseShifts03()
        Dim desd As New DESDCalculation

        Dim c As New Cluster("C:\temp\clustertest.cst")

        desd.CalculateClusterPhaseShifts(c, 5.0, 6, 1.0, 0.0, 0.0)

        Assert.AreEqual(7, c.PhaseShifts(0).Length)
        Assert.AreEqual(7, c.Tmatrix(0).Length)

        '		Dim ps as Complex() = c.PhaseShifts(0)
        '		For i As Integer = 0 To 6
        '			console.WriteLine(ps(i).ToString)
        '		Next

    End Sub

    <TestMethod()> _
    Public Sub CalculateClusterPhaseShifts04()
        Dim desd As New DESDCalculation

        Dim c As New Cluster("C:\temp\clustertest.cst")

        desd.CalculateClusterPhaseShifts(c, 5.0, 6, 0.0, 300.0, 650.0)

        Assert.AreEqual(7, c.PhaseShifts(0).Length)
        Assert.AreEqual(7, c.Tmatrix(0).Length)

        '		Dim ps as Complex() = c.PhaseShifts(0)
        '		For i As Integer = 0 To 6
        '			console.WriteLine(ps(i).ToString)
        '		Next

    End Sub


    <TestMethod()> _
    Public Sub CalculateClusterPhaseShifts05()
        Dim desd As New DESDCalculation

        Dim c As New Cluster("C:\temp\clustertest.cst")

        desd.CalculateClusterPhaseShifts(c, 5.0, 6, 1.0, 300.0, 650.0)

        Assert.AreEqual(7, c.PhaseShifts(0).Length)
        Assert.AreEqual(7, c.Tmatrix(0).Length)


        '		Dim ps as Complex() = c.PhaseShifts(0)
        '		For i As Integer = 0 To 6
        '			console.WriteLine(ps(i).ToString)
        '		Next
        '		
        '		Console.WriteLine()
        '		
        '		Dim tm as Complex() = c.Tmatrix(0)
        '
        '		For i As Integer = 0 To 6
        '			console.WriteLine(tm(i).ToString)
        '		Next

    End Sub


    <TestMethod()> _
    Public Sub GetSinglePaths01()
        Dim desd As New DESDCalculation

        Dim c As New Cluster("C:\temp\clustertest.cst")

        Dim SortedSinglePaths As List(Of MSPath) = desd.GetSinglePaths(c, 0, 100.0)

        Assert.AreEqual(2, SortedSinglePaths.Count)

        Assert.AreEqual(2.0, SortedSinglePaths(0).PathLength)
        Assert.AreEqual(System.Math.Sqrt(8.0), SortedSinglePaths(1).PathLength)

    End Sub

    <TestMethod()> _
    Public Sub GetSinglePaths02()
        Dim desd As New DESDCalculation

        Dim c As New Cluster("C:\temp\clustertest.cst")

        Dim SortedSinglePaths As List(Of MSPath) = desd.GetSinglePaths(c, 0, 2.5)

        Assert.AreEqual(1, SortedSinglePaths.Count)

        Assert.AreEqual(2.0, SortedSinglePaths(0).PathLength)

    End Sub

    <TestMethod()> _
    Public Sub GetSinglePaths03()
        Dim desd As New DESDCalculation

        Dim c As New Cluster("C:\temp\clustertest.cst")

        Dim SortedSinglePaths As List(Of MSPath) = desd.GetSinglePaths(c, 1, 2.5)

        Assert.AreEqual(2, SortedSinglePaths.Count)

        Assert.AreEqual(2.0, SortedSinglePaths(0).PathLength)
        Assert.AreEqual(2.0, SortedSinglePaths(1).PathLength)

    End Sub

    '<TestMethod()> _
    'Public Sub TestCalculation01()

    '    Dim desd As New DESDCalculation
    '    desd.PhiN = 100
    '    'desd.Temperature = 0.0
    '    'desd.InnerPotential = 0.0
    '    'desd.MaxScatteringOrder = 3
    '    'desd.RAOrder = 3
    '    'desd.OpticalPotential = 2.0
    '    'desd.ZV0 = 3.0

    '    desd.ClusterFileName = "C:\temp\clustertest.cst"
    '    Console.WriteLine("Starting DESD Calculation at " & Now.ToString)
    '    desd.Calculate()
    '    Console.WriteLine("DESD Calculation finished at " & Now.ToString)

    'End Sub

    '<TestMethod()> _
    'Public Sub SiClCalculation01()

    '    Dim desd As New DESDCalculation

    '    With desd
    '        .PhiStart = System.Math.PI / 2.0
    '        .PhiEnd = 5.0 * System.Math.PI / 6.0
    '        .PhiN = 60
    '        .Energy = 20.0
    '        .ZV0 = 3.0
    '        .NumberOfThreads = 4
    '        .AbsorberAtomID = 0
    '        .MaxScatteringOrder = 3 '// By the time we get to triple, we're pretty much done (for short mean free path).
    '        .RAOrder = 2
    '        .OpticalPotential = 0.038
    '        .Temperature = 300.0
    '        .MaxPathLength = 15.0
    '        .ClusterFileName = "C:\temp\sicl_version1.cst"
    '        .InnerPotential = -11.0  '// Inner potential for bulk atom muffintin is 1.22 Ry = 16.6 eV
    '        .Lmax = 5   '// Empirically, Lmax = 3 is OK for single scattering.  Lmax = 5 is fine for multiple.
    '        .UseL0 = False
    '    End With
    '    Console.WriteLine("Starting DESD Calculation at " & Now.ToString)
    '    desd.Calculate()
    '    Console.WriteLine("DESD Calculation finished at " & Now.ToString)

    'End Sub

    '<TestMethod()> _
    'Public Sub PerformanceBenchmark01()

    '    Dim desd As New DESDCalculation

    '    With desd
    '        .PhiStart = System.Math.PI / 2.0
    '        .PhiEnd = 7.0 * System.Math.PI / 6.0
    '        .PhiN = 66
    '        .Energy = 20.0
    '        .ZV0 = 3.0
    '        .NumberOfThreads = 4
    '        .AbsorberAtomID = 0
    '        .MaxScatteringOrder = 3 '// By the time we get to triple, we're pretty much done (for short mean free path).
    '        .RAOrder = 2
    '        .OpticalPotential = 0.038
    '        .Temperature = 300.0
    '        .MaxPathLength = 10.0
    '        .ClusterFileName = "C:\temp\sicl_version1.cst"
    '        .InnerPotential = -16.6  '// Inner potential for bulk atom muffintin is 1.22 Ry = 16.6 eV
    '        .Lmax = 5   '// Empirically, Lmax = 3 is OK for single scattering.  Lmax = 5 is fine for multiple.
    '    End With
    '    Console.WriteLine("Starting DESD Calculation at " & Now.ToString)
    '    desd.Calculate()
    '    Console.WriteLine("DESD Calculation finished at " & Now.ToString)

    'End Sub

    '<TestMethod()> _
    'Public Sub PerformanceBenchmark02()

    '    Dim desd As New DESDCalculation

    '    With desd
    '        .PhiStart = System.Math.PI / 2.0
    '        .PhiEnd = 7.0 * System.Math.PI / 6.0
    '        .PhiN = 66
    '        .Energy = 20.0
    '        .ZV0 = 3.0
    '        .NumberOfThreads = 4
    '        .AbsorberAtomID = 0
    '        .MaxScatteringOrder = 3 '// By the time we get to triple, we're pretty much done (for short mean free path).
    '        .RAOrder = 2
    '        .OpticalPotential = 0.038
    '        .Temperature = 300.0
    '        .MaxPathLength = 20.0
    '        .ClusterFileName = "C:\temp\sicl_version1.cst"
    '        .InnerPotential = -16.6  '// Inner potential for bulk atom muffintin is 1.22 Ry = 16.6 eV
    '        .Lmax = 5   '// Empirically, Lmax = 3 is OK for single scattering.  Lmax = 5 is fine for multiple.
    '    End With
    '    Console.WriteLine("Starting DESD Calculation at " & Now.ToString)
    '    desd.Calculate()
    '    Console.WriteLine("DESD Calculation finished at " & Now.ToString)

    'End Sub

    '<TestMethod()> _
    'Public Sub SingleScattering01()

    '    Dim desd As New DESDCalculation

    '    With desd
    '        .PhiStart = System.Math.PI / 2.0
    '        .PhiEnd = 7.0 * System.Math.PI / 6.0
    '        .PhiN = 66
    '        .Energy = 20.0
    '        .ZV0 = 3.0
    '        .NumberOfThreads = 4
    '        .AbsorberAtomID = 0
    '        .MaxScatteringOrder = 1 '// By the time we get to triple, we're pretty much done (for short mean free path).
    '        .RAOrder = 2
    '        .OpticalPotential = 0.038
    '        .Temperature = 300.0
    '        .MaxPathLength = 10000.0
    '        .ClusterFileName = "C:\temp\sicl_version1.cst"
    '        .InnerPotential = -16.6  '// Inner potential for bulk atom muffintin is 1.22 Ry = 16.6 eV
    '        .Lmax = 5   '// Empirically, Lmax = 3 is OK for single scattering.  Lmax = 5 is fine for multiple.
    '    End With
    '    Console.WriteLine("Starting DESD Calculation at " & Now.ToString)
    '    desd.Calculate()
    '    Console.WriteLine("DESD Calculation finished at " & Now.ToString)

    'End Sub

    '<TestMethod()> _
    'Public Sub SingleScattering02()


    '    Dim desd As New DESDCalculation
    '    With desd
    '        .PhiStart = 0.0
    '        .PhiEnd = 2.0 * System.Math.PI
    '        .PhiN = 360
    '        .Energy = 20.0
    '        .ZV0 = 3.0
    '        .NumberOfThreads = 4
    '        .AbsorberAtomID = 0
    '        .MaxScatteringOrder = 1 '// By the time we get to triple, we're pretty much done (for short mean free path).
    '        .RAOrder = 2
    '        .OpticalPotential = 0.038
    '        .Temperature = 300.0
    '        .MaxPathLength = 10000.0
    '        .ClusterFileName = "C:\temp\sicl_version1.cst"
    '        .InnerPotential = -16.6  '// Inner potential for bulk atom muffintin is 1.22 Ry = 16.6 eV
    '        .Lmax = 5   '// Empirically, Lmax = 3 is OK for single scattering.  Lmax = 5 is fine for multiple.
    '    End With
    '    Console.WriteLine("Starting DESD Calculation at " & Now.ToString)
    '    desd.Calculate()
    '    Console.WriteLine("DESD Calculation finished at " & Now.ToString)

    'End Sub

    '<TestMethod()> _
    'Public Sub MathCadComparison()

    '    Dim desd As New DESDCalculation

    '    Dim e As Double = CDbl(InputBox("Enter electron energy (eV)"))

    '    With desd
    '        '.ThetaStart = 0.75 * System.Math.PI
    '        .PhiStart = System.Math.PI / 2.0
    '        .PhiEnd = 5.0 * System.Math.PI / 6.0
    '        .PhiN = 60
    '        '			.PhiStart = 0.0
    '        '			.PhiEnd = 2.0*System.Math.PI
    '        '			.PhiN = 360
    '        .Energy = e
    '        .ZV0 = 3.0
    '        .NumberOfThreads = 4
    '        .AbsorberAtomID = 0
    '        .MaxScatteringOrder = 1 '// By the time we get to triple, we're pretty much done (for short mean free path).
    '        .RAOrder = 2
    '        .OpticalPotential = 0.0 '0.038
    '        .Temperature = 0.0
    '        .MaxPathLength = 10.1
    '        .ClusterFileName = "C:\temp\sicl_version1ion.cst"
    '        .InnerPotential = 0.0  '// Inner potential for bulk atom muffintin is 1.22 Ry = 16.6 eV
    '        .Lmax = 5   '// Empirically, Lmax = 3 is OK for single scattering.  Lmax = 5 is fine for multiple.
    '        .UseL0 = True
    '    End With

    '    Console.WriteLine("Starting DESD Calculation at " & Now.ToString)
    '    desd.Calculate()
    '    Console.WriteLine("DESD Calculation finished at " & Now.ToString)

    'End Sub

    '<TestMethod()> _
    'Public Sub DoubleScattering01()

    '    Dim desd As New DESDCalculation

    '    With desd
    '        .PhiStart = System.Math.PI / 2.0
    '        .PhiEnd = 7.0 * System.Math.PI / 6.0
    '        .PhiN = 66
    '        .Energy = 20.0
    '        .ZV0 = 3.0
    '        .NumberOfThreads = 4
    '        .AbsorberAtomID = 0
    '        .MaxScatteringOrder = 2 '// By the time we get to triple, we're pretty much done (for short mean free path).
    '        .RAOrder = 2
    '        .OpticalPotential = 0.038
    '        .Temperature = 300.0
    '        .MaxPathLength = 15.0
    '        .ClusterFileName = "C:\temp\sicl_version1.cst"
    '        .InnerPotential = -16.6  '// Inner potential for bulk atom muffintin is 1.22 Ry = 16.6 eV
    '        .Lmax = 5   '// Empirically, Lmax = 3 is OK for single scattering.  Lmax = 5 is fine for multiple.
    '    End With
    '    Console.WriteLine("Starting DESD Calculation at " & Now.ToString)
    '    desd.Calculate()
    '    Console.WriteLine("DESD Calculation finished at " & Now.ToString)

    'End Sub

    '<TestMethod()> _
    'Public Sub DoubleScattering02()

    '    Dim desd As New DESDCalculation

    '    With desd
    '        .PhiStart = System.Math.PI / 2.0
    '        .PhiEnd = 7.0 * System.Math.PI / 6.0
    '        .PhiN = 66
    '        .Energy = 20.0
    '        .ZV0 = 3.0
    '        .NumberOfThreads = 4
    '        .AbsorberAtomID = 0
    '        .MaxScatteringOrder = 2 '// By the time we get to triple, we're pretty much done (for short mean free path).
    '        .RAOrder = 2
    '        .OpticalPotential = 0.038
    '        .Temperature = 300.0
    '        .MaxPathLength = 25.0
    '        .ClusterFileName = "C:\temp\sicl_version1.cst"
    '        .InnerPotential = -16.6  '// Inner potential for bulk atom muffintin is 1.22 Ry = 16.6 eV
    '        .Lmax = 5   '// Empirically, Lmax = 3 is OK for single scattering.  Lmax = 5 is fine for multiple.
    '    End With
    '    Console.WriteLine("Starting DESD Calculation at " & Now.ToString)
    '    desd.Calculate()
    '    Console.WriteLine("DESD Calculation finished at " & Now.ToString)

    'End Sub

    '<TestMethod()> _
    'Public Sub ScatteringOrderStudy01()

    '    Dim desd As New DESDCalculation
    '    With desd
    '        .ClusterFileName = "C:\temp\sicl_version2.cst"
    '        .PhiStart = 0.0
    '        .PhiEnd = 2.0 * System.Math.PI
    '        .PhiN = 360
    '        '			.PhiStart = System.Math.PI/2.0
    '        '			.PhiEnd = 7.0 * System.Math.PI/6.0
    '        '			.PhiN = 66
    '        .Energy = 20.0
    '        .ZV0 = 3.0
    '        .NumberOfThreads = 4
    '        .AbsorberAtomID = 0
    '        .MaxScatteringOrder = 5 '// By the time we get to triple, we're pretty much done (for short mean free path).
    '        .RAOrder = 2
    '        .OpticalPotential = 0.038
    '        .Temperature = 0.0
    '        .MaxPathLength = 100000.0
    '        .InnerPotential = -16.6  '// Inner potential for bulk atom muffintin is 1.22 Ry = 16.6 eV
    '        .Lmax = 5   '// Empirically, Lmax = 3 is OK for single scattering.  Lmax = 5 is fine for multiple.
    '    End With
    '    Console.WriteLine("Starting DESD Calculation at " & Now.ToString)
    '    desd.Calculate()
    '    Console.WriteLine("DESD Calculation finished at " & Now.ToString)

    'End Sub


    '<TestMethod()> _
    'Public Sub TabulateCnlz()

    '    Dim rhomin As Double = 0.5
    '    Dim rhomax As Double = 100.0
    '    Dim Nrho As Integer = 100
    '    Dim rho As Double
    '    Dim cnlz As Complex

    '    For l As Integer = 0 To 10
    '        For mu As Integer = 0 To 4

    '            For irho As Integer = 0 To Nrho
    '                rho = rhomin + (CDbl(irho) / CDbl(Nrho)) * rhomax
    '                cnlz = RehrAlbers.Cnlz(mu, l, New Complex(0.0, -1.0 / rho))
    '                Console.WriteLine(mu.ToString & ", " & l.ToString & ", " & rho.ToString & ", " & cnlz.real.tostring & ", " & cnlz.Imag.tostring)
    '            Next
    '        Next
    '    Next

    'End Sub


    '<TestMethod()> _
    'Public Sub TwoSiAtomComparison()

    '    Dim desd As New DESDCalculation

    '    Dim e As Double = CDbl(InputBox("Enter energy in eV"))

    '    With desd
    '        '.ThetaStart = 0.75 * System.Math.PI
    '        .PhiStart = 0.0
    '        .PhiEnd = 2.0 * System.Math.PI
    '        .PhiN = 360
    '        '			.PhiStart = 0.0
    '        '			.PhiEnd = 2.0*System.Math.PI
    '        '			.PhiN = 360
    '        .Energy = e
    '        .ZV0 = 3.0
    '        .NumberOfThreads = 4
    '        .AbsorberAtomID = 0
    '        .MaxScatteringOrder = 1 '// By the time we get to triple, we're pretty much done (for short mean free path).
    '        .RAOrder = 2
    '        .OpticalPotential = 0.0 '0.038
    '        .Temperature = 0.0
    '        .MaxPathLength = 2.36
    '        .ClusterFileName = "C:\temp\TwoSiAtomCluster.cst"
    '        .InnerPotential = 0.0  '// Inner potential for bulk atom muffintin is 1.22 Ry = 16.6 eV
    '        .Lmax = 1   '// Empirically, Lmax = 3 is OK for single scattering.  Lmax = 5 is fine for multiple.
    '        .UseL0 = True
    '    End With

    '    Console.WriteLine("Starting DESD Calculation at " & Now.ToString)
    '    desd.Calculate()
    '    Console.WriteLine("DESD Calculation finished at " & Now.ToString)

    'End Sub

    '<TestMethod()> _
    'Public Sub TwoSiAtomComparison5Phase()

    '    Dim desd As New DESDCalculation

    '    Dim e As Double = CDbl(InputBox("Enter energy in eV"))

    '    With desd
    '        '.ThetaStart = 0.75 * System.Math.PI
    '        .PhiStart = 0.0
    '        .PhiEnd = 2.0 * System.Math.PI
    '        .PhiN = 360
    '        '			.PhiStart = 0.0
    '        '			.PhiEnd = 2.0*System.Math.PI
    '        '			.PhiN = 360
    '        .Energy = e
    '        .ZV0 = 3.0
    '        .NumberOfThreads = 4
    '        .AbsorberAtomID = 0
    '        .MaxScatteringOrder = 1 '// By the time we get to triple, we're pretty much done (for short mean free path).
    '        .RAOrder = 2
    '        .OpticalPotential = 0.0 '0.038
    '        .Temperature = 0.0
    '        .MaxPathLength = 2.36
    '        .ClusterFileName = "C:\temp\TwoSiAtomCluster.cst"
    '        .InnerPotential = -16.0  '// Inner potential for bulk atom muffintin is 1.22 Ry = 16.6 eV
    '        .Lmax = 4   '// Empirically, Lmax = 3 is OK for single scattering.  Lmax = 5 is fine for multiple.
    '        .UseL0 = True
    '    End With

    '    Console.WriteLine("Starting DESD Calculation at " & Now.ToString)
    '    desd.Calculate()
    '    Console.WriteLine("DESD Calculation finished at " & Now.ToString)

    'End Sub

    '<TestMethod()> _
    'Public Sub SiClCalculation02062011()

    '    Dim desd As New DESDCalculation
    '    Dim e As Double = CDbl(InputBox("Enter energy in eV"))

    '    With desd
    '        .PhiStart = System.Math.PI / 2.0
    '        .PhiEnd = 5.0 * System.Math.PI / 6.0
    '        .PhiN = 60
    '        .Energy = e
    '        .ZV0 = 3.0
    '        .NumberOfThreads = 4
    '        .AbsorberAtomID = 0
    '        .MaxScatteringOrder = 3 '// By the time we get to triple, we're pretty much done (for short mean free path).
    '        .RAOrder = 2
    '        .OpticalPotential = 0.038
    '        .Temperature = 300.0
    '        .MaxPathLength = 14.0
    '        .ClusterFileName = "C:\temp\sicl_version1.cst"
    '        .InnerPotential = -12.0
    '        .Lmax = 5   '// Empirically, Lmax = 3 is OK for single scattering.  Lmax = 5 is fine for multiple.
    '        .UseL0 = False
    '    End With
    '    Console.WriteLine("Starting DESD Calculation at " & Now.ToString)
    '    desd.Calculate()
    '    Console.WriteLine("DESD Calculation finished at " & Now.ToString)

    'End Sub

End Class
