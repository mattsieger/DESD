Imports DESD

Namespace AtomicPhysics_Tests
	
    <TestClass()> _
    Public Class NeutralMuffinTin_UnitTest

        <TestMethod()> _
        Public Sub HeliumInfiniteRadius()

            Dim a As New NeutralMuffinTin(Element.Helium, Double.PositiveInfinity)

            Assert.AreEqual(-1.721, a.Orbitals(0).Energy, 0.001)
            Assert.IsTrue(Double.IsPositiveInfinity(a.Radius))

        End Sub


        <TestMethod()> _
        Public Sub Silicon()

            'Dim mt As New NeutralMuffinTin(Element.Silicon, double.PositiveInfinity)
            Dim mt As New NeutralMuffinTin(Element.Silicon, 2.22)

            Dim ps As PhaseShift

            Console.WriteLine("Silicon scatterer, Rmt = 2.22 Bohr Radii")
            Dim Imt As Integer = mt.Imt
            Console.WriteLine("Imt = " & Imt.ToString)
            Dim ip As Double = mt.InnerPotential
            Console.WriteLine("Inner potential = " & ip.ToString)
            Console.WriteLine()


            Console.WriteLine("Phase shifts:")

            Console.WriteLine("Energy = 0.01 Ry")
            Console.WriteLine("L     delta_L (Rad)")
            ps = New PhaseShift(0.01, 10, mt)
            For l As Integer = 0 To 10
                Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
            Next
            Console.WriteLine()

            Console.WriteLine("Energy = 0.5 Ry")
            Console.WriteLine("L     delta_L (Rad)")
            ps = New PhaseShift(0.5, 10, mt)
            For l As Integer = 0 To 10
                Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
            Next
            Console.WriteLine()
            Console.WriteLine("Energy = 1.0 Ry")
            Console.WriteLine("L     delta_L (Rad)")
            ps = New PhaseShift(1.0, 10, mt)
            For l As Integer = 0 To 10
                Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
            Next
            Console.WriteLine()
            Console.WriteLine("Energy = 2.0 Ry")
            Console.WriteLine("L     delta_L (Rad)")
            ps = New PhaseShift(2.0, 10, mt)
            For l As Integer = 0 To 10
                Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
            Next
            Console.WriteLine()
            Console.WriteLine("Energy = 3.0 Ry")
            Console.WriteLine("L     delta_L (Rad)")
            ps = New PhaseShift(3.0, 10, mt)
            For l As Integer = 0 To 10
                Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
            Next
            Console.WriteLine()
            Console.WriteLine("Energy = 4.0 Ry")
            Console.WriteLine("L     delta_L (Rad)")
            ps = New PhaseShift(4.0, 10, mt)
            For l As Integer = 0 To 10
                Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
            Next
            Console.WriteLine()
            Console.WriteLine("Energy = 5.0 Ry")
            Console.WriteLine("L     delta_L (Rad)")
            ps = New PhaseShift(5.0, 10, mt)
            For l As Integer = 0 To 10
                Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
            Next
            Console.WriteLine()
            Console.WriteLine("Energy = 6.0 Ry")
            Console.WriteLine("L     delta_L (Rad)")
            ps = New PhaseShift(6.0, 10, mt)
            For l As Integer = 0 To 10
                Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
            Next
            Console.WriteLine()
            Console.WriteLine("Energy = 7.0 Ry")
            Console.WriteLine("L     delta_L (Rad)")
            ps = New PhaseShift(7.0, 10, mt)
            For l As Integer = 0 To 10
                Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
            Next
            Console.WriteLine()


            Console.WriteLine("Muffin-tin potential")
            Dim ipot As Double() = mt.Potential
            For i As Integer = 0 To mt.Mesh.Count - 1
                Console.WriteLine(mt.Mesh.R(i).ToString & ", " & (-ipot(i)).ToString)
            Next
            Assert.IsTrue(True)


        End Sub


        <TestMethod()> _
        Public Sub SiliconPotential()

            'Dim mt As New NeutralMuffinTin(Element.Silicon, double.PositiveInfinity)
            'Dim RmtAng as Double = 1.046
            'Dim RmtAng as Double = 1.058
            Dim RmtAng As Double = 1.067
            Dim RmtBohrs As Double = RmtAng / 0.52917720859
            Dim mt As New NeutralMuffinTin(Element.Silicon, RmtBohrs)

            Dim Imt As Integer = mt.Imt
            Console.WriteLine("Rmt = " & RmtAng.ToString & " [Angstroms]")
            Console.WriteLine("Rmt = " & RmtBohrs.ToString & " [Bohrs]")
            Console.WriteLine("Imt = " & mt.Imt.ToString)
            Console.WriteLine("Inner potential = " & (13.6056923 * mt.InnerPotential).ToString & " [eV]")

            Console.WriteLine("Orbital occupancies:")
            For Each o As Orbital In mt.Orbitals
                Console.WriteLine("(" & o.N.ToString & ", " & o.L.ToString & ") = " & o.Occupancy.ToString & "   " & o.Energy.ToString)
                '				For j As Integer = 0 To o.PArray.Length - 1
                '					console.WriteLine(mt.Mesh.R(j).ToString & ", " & o.P(j).ToString)
                '				Next
            Next

            Console.WriteLine("Muffin-tin potential")
            Dim ipot As Double() = mt.Potential
            For i As Integer = 0 To mt.Mesh.Count - 1
                Console.WriteLine(mt.Mesh.R(i).ToString & ", " & (-ipot(i)).ToString)
            Next
            'assert.IsTrue(True) 

        End Sub


        <TestMethod()> _
        Public Sub SiliconPhaseShifts()

            Dim Rmt As Double = 2.22
            Dim mt As New NeutralMuffinTin(Element.Silicon, Rmt)

            Console.WriteLine("Silicon scatterer, Rmt = " & Rmt.ToString & " Bohr Radii")

            Dim ip As Double = mt.InnerPotential
            Console.WriteLine("Inner potential = " & ip.ToString)
            Console.WriteLine()

            Console.WriteLine("Phase shifts:")

            Dim Emin As Double = 0.0
            Dim Emax As Double = 10.0
            Dim E As Double
            Dim NE As Integer = 100
            Dim ps(NE) As PhaseShift
            For i As Integer = 0 To 100
                E = Emin + CDbl(i) * (Emax - Emin) / 100.0
                If E = 0.0 Then E = 0.000001
                ps(i) = New PhaseShift(E, 3, mt)
            Next

            For L As Integer = 0 To 3
                Console.WriteLine("L = " & L.ToString)
                For i As Integer = 0 To NE
                    E = Emin + CDbl(i) * (Emax - Emin) / 100.0

                    Console.WriteLine(E.ToString & "    " & ps(i).RealValue(L).ToString)

                Next
                Console.WriteLine()
            Next

            Console.WriteLine()
            Console.WriteLine("R-multiplied L = 0 Wave Function for E = 10.0")
            Console.WriteLine("Imt = " & ps(100).Imatch.ToString)
            Dim wf As Double() = ps(100).Wavefunction(0)
            For i As Integer = 0 To ps(100).Mesh.Count - 1
                Console.WriteLine(ps(100).Mesh.R(i).ToString & ", " & wf(i).ToString)
            Next


            '            Console.WriteLine()
            '            Console.WriteLine("R-divided L = 0 Wave Function for E = 10.0")
            '            Console.WriteLine("Imt = " & ps(100).Imatch.ToString)
            '            'Dim wf As Double() = ps(100).Wavefunction(0)
            '            For i As Integer = 0 To ps(100).Mesh.Count - 1
            '                Console.WriteLine(ps(100).Mesh.R(i).ToString & ", " & (wf(i) / ps(100).Mesh.R(i)).tostring)
            '            Next

            Console.WriteLine()
            Console.WriteLine("Potential")
            Dim pot() As Double = mt.Potential

            For i As Integer = 0 To mt.Mesh.Count - 1
                Console.WriteLine(mt.Mesh.R(i).ToString & ", " & pot(i).ToString)
            Next

            '// Spherical bessel function:
            '            console.WriteLine()
            '            console.WriteLine("Spherical bessel function j0(kr)")
            '            Dim sphb as Double()
            '            For i As Integer = 0 To ps(100).Mesh.Count - 1
            '            	sphb = sieger.Math.Functions.SphericalBessel(0,system.math.sqrt(10.0)*ps(100).Mesh.R(i))
            '                Console.WriteLine(ps(100).Mesh.R(i).ToString & ", " & sphb(0).tostring)
            '            Next
            '
            '           	console.WriteLine
            'dim pps as PhaseShift = New PhaseShift(10.0,0,mt,0.0,0.0)
        End Sub

        <TestMethod()> _
        Public Sub Aluminum()

            'Dim Rmt as Double = 2.6993
            Dim Rmt As Double = 2.6
            Dim mt As New NeutralMuffinTin(Element.Aluminum, Rmt)

            Console.WriteLine("Aluminum scatterer, Rmt = " & Rmt.ToString & " Bohr Radii")

            Dim ip As Double = mt.InnerPotential
            Console.WriteLine("Inner potential = " & ip.ToString)
            Console.WriteLine()

            Console.WriteLine("Phase shifts:")

            Dim Emin As Double = 0.0
            Dim Emax As Double = 10.0
            Dim E As Double
            Dim NE As Integer = 100
            Dim ps(NE) As PhaseShift
            For i As Integer = 0 To 100
                E = Emin + CDbl(i) * (Emax - Emin) / 100.0
                If E = 0.0 Then E = 0.000001
                ps(i) = New PhaseShift(E, 3, mt)
            Next

            For L As Integer = 0 To 3
                Console.WriteLine("L = " & L.ToString)
                For i As Integer = 0 To NE
                    E = Emin + CDbl(i) * (Emax - Emin) / 100.0

                    Console.WriteLine(E.ToString & "    " & ps(i).RealValue(L).ToString)

                Next
                Console.WriteLine()
            Next

            Console.WriteLine("Testing PhaseShiftLookup")
            Emin = 0.1
            Emax = 10.0
            Dim psl As New PhaseShiftLookup(mt, 3, 0.1, 10.0)
            For L As Integer = 0 To 3
                Console.WriteLine("L = " & L.ToString)
                For i As Integer = 0 To NE
                    E = Emin + CDbl(i) * (Emax - Emin) / 100.0

                    Console.WriteLine(E.ToString & "    " & psl.GetRealPhaseShift(E, L).ToString)

                Next
                Console.WriteLine()
            Next


        End Sub

        <TestMethod()> _
        Public Sub Nickel()

            'Dim Rmt as Double = 2.6993
            Dim Rmt As Double = 1.99
            Dim mt As New NeutralMuffinTin(Element.Nickel, Rmt)

            Console.WriteLine("Nickel scatterer, Rmt = " & Rmt.ToString & " Bohr Radii")

            Dim ip As Double = mt.InnerPotential
            Console.WriteLine("Inner potential = " & ip.ToString)
            Console.WriteLine()

            Console.WriteLine("Phase shifts:")

            Dim Emin As Double = 0.0
            Dim Emax As Double = 16.0
            Dim E As Double
            Dim NE As Integer = 160
            Dim ps(NE) As PhaseShift
            For i As Integer = 0 To 160
                E = Emin + CDbl(i) * (Emax - Emin) / 160.0
                If E = 0.0 Then E = 0.000001
                ps(i) = New PhaseShift(E, 4, mt)
            Next

            For L As Integer = 0 To 4
                Console.WriteLine("L = " & L.ToString)
                For i As Integer = 0 To NE
                    E = Emin + CDbl(i) * (Emax - Emin) / 160.0

                    Console.WriteLine(E.ToString & "    " & ps(i).RealValue(L).ToString)

                Next
                Console.WriteLine()
            Next

            '			console.WriteLine("Testing PhaseShiftLookup")
            '			Emin = 0.1
            '			Emax = 10.0
            '			Dim psl As New PhaseShiftLookup(mt,3,0.1,10.0)
            '			For L as Integer = 0 To 3
            '            	console.WriteLine("L = " & l.tostring)
            '                For i As Integer = 0 To NE
            '                    E = Emin + CDbl(i) * (Emax - Emin) / 100.0
            '
            '                    Console.WriteLine(E.ToString & "    " & psl.GetRealPhaseShift(E,L).ToString)
            '
            '                Next
            '				Console.WriteLine()
            '			Next
            '		

        End Sub


        <TestMethod()> _
        Public Sub NickelPotential()

            Dim RmtBohrs As Double = 1.99

            Dim mt As New NeutralMuffinTin(Element.Nickel, RmtBohrs)

            Dim Imt As Integer = mt.Imt
            Console.WriteLine("Z = " & Element.Nickel.ToString)
            Console.WriteLine("Rmt = " & RmtBohrs.ToString & " [Bohrs]")
            Console.WriteLine("Imt = " & mt.Imt.ToString)
            Console.WriteLine("Inner potential = " & (13.6056923 * mt.InnerPotential).ToString & " [eV]")

            Console.WriteLine("Orbital occupancies:")
            For Each o As Orbital In mt.Orbitals
                Console.WriteLine("(" & o.N.ToString & ", " & o.L.ToString & ") = " & o.Occupancy.ToString & "   " & o.Energy.ToString)
                '				For j As Integer = 0 To o.PArray.Length - 1
                '					console.WriteLine(mt.Mesh.R(j).ToString & ", " & o.P(j).ToString)
                '				Next
            Next

            Console.WriteLine("Muffin-tin potential")
            Dim ipot As Double() = mt.Potential
            For i As Integer = 0 To mt.Mesh.Count - 1
                Console.WriteLine(mt.Mesh.R(i).ToString & ", " & (-ipot(i)).ToString)
            Next
            'assert.IsTrue(True) 

        End Sub

        <TestMethod()> _
        Public Sub Xenon()

            Dim Rmt As Double = 2.9
            Dim mt As New NeutralMuffinTin(Element.Xenon, Rmt)

            Console.WriteLine("Nickel scatterer, Rmt = " & Rmt.ToString & " Bohr Radii")

            Dim ip As Double = mt.InnerPotential
            Console.WriteLine("Inner potential = " & ip.ToString)
            Console.WriteLine()

            Console.WriteLine("Phase shifts:")

            Dim Emin As Double = 0.0
            Dim Emax As Double = 16.0
            Dim E As Double
            Dim NE As Integer = 160
            Dim ps(NE) As PhaseShift
            For i As Integer = 0 To 160
                E = Emin + CDbl(i) * (Emax - Emin) / 160.0
                If E = 0.0 Then E = 0.000001
                ps(i) = New PhaseShift(E, 4, mt)
            Next

            For L As Integer = 0 To 4
                Console.WriteLine("L = " & L.ToString)
                For i As Integer = 0 To NE
                    E = Emin + CDbl(i) * (Emax - Emin) / 160.0

                    Console.WriteLine(E.ToString & "    " & ps(i).RealValue(L).ToString)

                Next
                Console.WriteLine()
            Next

            '			console.WriteLine("Testing PhaseShiftLookup")
            '			Emin = 0.1
            '			Emax = 10.0
            '			Dim psl As New PhaseShiftLookup(mt,3,0.1,10.0)
            '			For L as Integer = 0 To 3
            '            	console.WriteLine("L = " & l.tostring)
            '                For i As Integer = 0 To NE
            '                    E = Emin + CDbl(i) * (Emax - Emin) / 100.0
            '
            '                    Console.WriteLine(E.ToString & "    " & psl.GetRealPhaseShift(E,L).ToString)
            '
            '                Next
            '				Console.WriteLine()
            '			Next
            '		

        End Sub


        <TestMethod()> _
        Public Sub ChargeNeutrality01()

            Dim RmtBohrs As Double = 1.99

            Dim mt As New NeutralMuffinTin(Element.Nickel, RmtBohrs)
            Dim r As Double() = mt.Mesh.GetArray()
            Dim Imt As Integer = mt.Imt

            Dim TotalCharge As Double = 0.0

            '			For Each o As Orbital In mt.Orbitals
            '				TotalCharge += o.Occupancy
            '			Next

            For Each o As Orbital In mt.Orbitals
                TotalCharge += DESD.Math.Integration.TrapezoidalRuleIntegrator.Integrate(r, o.SigmaArray, 0, Imt)

            Next

            Assert.AreEqual(-28.0, TotalCharge, 0.01)

        End Sub



        <TestMethod()> _
        Public Sub Chlorine()

            Dim Rmt As Double = 2.22 '1.97
            Dim mt As New NeutralMuffinTin(Element.Chlorine, Rmt)

            Console.WriteLine("Chlorine scatterer, Rmt = " & Rmt.ToString & " Bohr Radii")

            Dim ip As Double = mt.InnerPotential
            Console.WriteLine("Inner potential = " & ip.ToString)
            Console.WriteLine()

            Console.WriteLine("Phase shifts:")

            Dim Emin As Double = 0.0
            Dim Emax As Double = 10.0
            Dim E As Double
            Dim NE As Integer = 100
            Dim ps(NE) As PhaseShift
            For i As Integer = 0 To 100
                E = Emin + CDbl(i) * (Emax - Emin) / 100.0
                If E = 0.0 Then E = 0.000001
                ps(i) = New PhaseShift(E, 3, mt)
            Next

            For L As Integer = 0 To 3
                Console.WriteLine("L = " & L.ToString)
                For i As Integer = 0 To NE
                    E = Emin + CDbl(i) * (Emax - Emin) / 100.0

                    Console.WriteLine(E.ToString & "    " & ps(i).RealValue(L).ToString)

                Next
                Console.WriteLine()
            Next


        End Sub


        '
        '        <TestMethod()> _
        '        Public Sub Aluminum()
        '
        '            'Dim mt As New NeutralMuffinTin(Element.Silicon, double.PositiveInfinity)
        '            Dim mt As New NeutralMuffinTin(Element.Aluminum, 2.6993)
        '            Console.WriteLine("Aluminum scatterer, Rmt = 2.6993 Bohr Radii")
        '            Dim Imt As integer = mt.Imt
        '            console.WriteLine("Imt = " & Imt.ToString)
        '            Dim ip As Double = mt.InnerPotential
        '            console.WriteLine("Inner potential = " & Ip.ToString)
        '            Console.WriteLine()
        '           	console.WriteLine("Phase shifts:")
        '           	console.WriteLine("Energy = 0.5 Ry")
        '           	console.WriteLine("L     delta_L (Rad)")
        '           	For l As Integer = 0 To 10
        '           		console.WriteLine(l.ToString & "     " & mt.PhaseShift(0.5,l).ToString)
        '           	Next
        '           	console.WriteLine()
        '           	console.WriteLine("Energy = 1.0 Ry")
        '           	console.WriteLine("L     delta_L (Rad)")
        '           	For l As Integer = 0 To 10
        '           		console.WriteLine(l.ToString & "     " & mt.PhaseShift(1.0,l).ToString)
        '           	Next
        '           	console.WriteLine()
        '           	console.WriteLine("Energy = 2.0 Ry")
        '           	console.WriteLine("L     delta_L (Rad)")
        '           	For l As Integer = 0 To 10
        '           		console.WriteLine(l.ToString & "     " & mt.PhaseShift(2.0,l).ToString)
        '           	Next
        '           	console.WriteLine()
        '           	console.WriteLine("Energy = 3.0 Ry")
        '           	console.WriteLine("L     delta_L (Rad)")
        '           	For l As Integer = 0 To 10
        '           		console.WriteLine(l.ToString & "     " & mt.PhaseShift(3.0,l).ToString)
        '           	Next
        '           	console.WriteLine()
        '           	console.WriteLine("Energy = 4.0 Ry")
        '           	console.WriteLine("L     delta_L (Rad)")
        '           	For l As Integer = 0 To 10
        '           		console.WriteLine(l.ToString & "     " & mt.PhaseShift(4.0,l).ToString)
        '           	Next
        '           	console.WriteLine()
        '           	console.WriteLine("Energy = 5.0 Ry")
        '           	console.WriteLine("L     delta_L (Rad)")
        '           	For l As Integer = 0 To 10
        '           		console.WriteLine(l.ToString & "     " & mt.PhaseShift(5.0,l).ToString)
        '           	Next
        '			Console.WriteLine()
        '           	console.WriteLine("Energy = 6.0 Ry")
        '           	console.WriteLine("L     delta_L (Rad)")
        '           	For l As Integer = 0 To 10
        '           		console.WriteLine(l.ToString & "     " & mt.PhaseShift(6.0,l).ToString)
        '           	Next
        '			Console.WriteLine()
        '           	console.WriteLine("Energy = 7.0 Ry")
        '           	console.WriteLine("L     delta_L (Rad)")
        '           	For l As Integer = 0 To 10
        '           		console.WriteLine(l.ToString & "     " & mt.PhaseShift(7.0,l).ToString)
        '           	Next
        '			Console.WriteLine()
        '
        '
        '			Console.WriteLine("Muffin-tin potential")
        '            Dim ipot as Double() =  mt.Potential
        '            For i As Integer = 0 To mt.Mesh.Count-1
        '            	console.WriteLine(mt.Mesh.R(i).ToString & ", " & (-ipot(i)).tostring)
        '            Next
        '            assert.IsTrue(True)
        '
        '        End Sub
        '
        '
        '
        '
        <TestMethod()> _
        Public Sub Aluminum_PhaseShiftWaveFunctions()

            Dim mt As New NeutralMuffinTin(Element.Aluminum, 2.6993)

            Console.WriteLine("Aluminum scatterer, Rmt = 2.6993 Bohr Radii")

            Dim Imt As Integer = mt.Imt
            Console.WriteLine("Imt = " & Imt.ToString)

            Dim ip As Double = mt.InnerPotential
            Console.WriteLine("Inner potential = " & ip.ToString)
            Console.WriteLine()


            Console.WriteLine("Muffin-tin potential")
            Dim ipot As Double() = mt.Potential
            For i As Integer = 0 To mt.Mesh.Count - 1
                Console.WriteLine(mt.Mesh.R(i).ToString & ", " & (-ipot(i)).ToString)
            Next
            Assert.IsTrue(True)

            Console.WriteLine("Interpolated potential")
            Dim ps As New PhaseShift(10.0, 0, mt)
            Dim npot As Double() = ps.Potential
            For i As Integer = 0 To ps.Mesh.Count - 1
                Console.WriteLine(ps.Mesh.R(i).ToString & ", " & (-npot(i)).ToString)

            Next
            Console.WriteLine()
            Console.WriteLine("Phase shift for L = 0:" & ps.RealValue(0).ToString)
            Console.WriteLine("L = 0 Wave Function for E = 10.0")
            Dim wf As Double() = ps.Wavefunction(0)
            For i As Integer = 0 To ps.Mesh.Count - 1
                Console.WriteLine(ps.Mesh.R(i).ToString & ", " & wf(i).ToString)
            Next

            '            Dim wf As Double() = {1.0}
            '
            '            Console.WriteLine("Wave function for E = 10 Ry, L = 0")
            '            Dim delta As Double = mt.PhaseShift(10.0,0,wf)
            '            console.WriteLine("Phase shift = " & delta.ToString)
            '            For i As Integer = 0 To wf.Length-1
            '            	console.WriteLine(mt.Mesh.R(i).ToString & "    " & wf(i).ToString)
            '            Next
            '
            '            console.WriteLine()
            '            console.WriteLine("Phase shifts:")
            '
            '           	Dim Emin As Double = 0.0
            '           	Dim Emax As Double = 10.0
            '           	Dim E As Double
            '           	For l As Integer = 0 To 3
            '            	console.WriteLine("L = " & l.tostring)
            '	           	For i As Integer = 0 To 100
            '	           		E = Emin + CDbl(i)*(Emax - Emin) / 100.0
            '	           		If E = 0.0 Then E = 1.0E-6
            '	            	console.WriteLine(E.ToString & "    " & mt.PhaseShift(E,l).ToString)
            '	           	Next
            '				Console.WriteLine()
            '            Next

        End Sub

        '        <TestMethod()> _
        '        Public Sub CopperIon()
        '
        '            Dim a As New Atom(Element.Copper, New ElectronicConfiguration("1s2 2s2 2p6 3s2 3p6 3d10"))
        '            Dim percentDiff As Double
        '            Dim percentTol As Double = 0.007
        '            Dim EHS As Double
        '
        '            Dim U As Double() = HSTableConverter.CompressU(a.Mesh, Element.Copper, a.Potential)
        '
        '            EHS = -650.4
        '            Dim o1s As Orbital = a.GetOrbital(1, 0)
        '            percentDiff = System.Math.Abs((o1s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -78.872
        '            Dim o2s As Orbital = a.GetOrbital(2, 0)
        '            percentDiff = System.Math.Abs((o2s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -69.74
        '            Dim o2p As Orbital = a.GetOrbital(2, 1)
        '            percentDiff = System.Math.Abs((o2p.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -9.3548
        '            Dim o3s As Orbital = a.GetOrbital(3, 0)
        '            percentDiff = System.Math.Abs((o3s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -6.4286
        '            Dim o3p As Orbital = a.GetOrbital(3, 1)
        '            percentDiff = System.Math.Abs((o3p.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -1.4592
        '            Dim o3d As Orbital = a.GetOrbital(3, 2)
        '            percentDiff = System.Math.Abs((o3d.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '        End Sub
        '
        '
        '        <TestMethod()> _
        '        Public Sub Copper()
        '
        '            Dim a As New Atom(Element.Copper)
        '            Dim percentDiff As Double
        '            Dim percentTol As Double = 0.005
        '            Dim EHS As Double
        '
        '            Dim U As Double() = HSTableConverter.CompressU(a.Mesh, Element.Copper, a.Potential)
        '
        '            EHS = -649.7
        '            Dim o1s As Orbital = a.GetOrbital(1, 0)
        '            percentDiff = System.Math.Abs((o1s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -78.15
        '            Dim o2s As Orbital = a.GetOrbital(2, 0)
        '            percentDiff = System.Math.Abs((o2s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -69.02
        '            Dim o2p As Orbital = a.GetOrbital(2, 1)
        '            percentDiff = System.Math.Abs((o2p.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -8.634
        '            Dim o3s As Orbital = a.GetOrbital(3, 0)
        '            percentDiff = System.Math.Abs((o3s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -5.709
        '            Dim o3p As Orbital = a.GetOrbital(3, 1)
        '            percentDiff = System.Math.Abs((o3p.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -0.7431
        '            Dim o3d As Orbital = a.GetOrbital(3, 2)
        '            percentDiff = System.Math.Abs((o3d.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -0.5091
        '            Dim o4s As Orbital = a.GetOrbital(4, 0)
        '            percentDiff = System.Math.Abs((o4s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '        End Sub
        '
        '
        '        <Test> _
        '        Public Sub Lawrencium
        '            Dim a As New Atom(Element.Lawrencium, New ElectronicConfiguration("[Rn] 5f14 6d1 7s2"))
        '            Dim percentDiff As Double
        '            Dim percentTol As Double = 0.05
        '            Dim EHS As Double
        '
        '            'Dim U As Double() = HSTableConverter.CompressU(a.Mesh, Element.Copper, a.Potential)
        '
        '            EHS = -9364.3
        '            Dim o1s As Orbital = a.GetOrbital(1, 0)
        '            percentDiff = System.Math.Abs((o1s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -1667.1
        '            Dim o2s As Orbital = a.GetOrbital(2, 0)
        '            percentDiff = System.Math.Abs((o2s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -1620.9
        '            Dim o2p As Orbital = a.GetOrbital(2, 1)
        '            percentDiff = System.Math.Abs((o2p.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -437.33
        '            Dim o3s As Orbital = a.GetOrbital(3, 0)
        '            percentDiff = System.Math.Abs((o3s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -413.78
        '            Dim o3p As Orbital = a.GetOrbital(3, 1)
        '            percentDiff = System.Math.Abs((o3p.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -369.41
        '            Dim o3d As Orbital = a.GetOrbital(3, 2)
        '            percentDiff = System.Math.Abs((o3d.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -117.31
        '            Dim o4s As Orbital = a.GetOrbital(4, 0)
        '            percentDiff = System.Math.Abs((o4s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -105.9
        '            Dim o4p As Orbital = a.GetOrbital(4, 1)
        '            percentDiff = System.Math.Abs((o4p.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -84.373
        '            Dim o4d As Orbital = a.GetOrbital(4, 2)
        '            percentDiff = System.Math.Abs((o4d.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -54.080
        '            Dim o4f As Orbital = a.GetOrbital(4, 3)
        '            percentDiff = System.Math.Abs((o4f.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -26.141
        '            Dim o5s As Orbital = a.GetOrbital(5, 0)
        '            percentDiff = System.Math.Abs((o5s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -21.260
        '            Dim o5p As Orbital = a.GetOrbital(5, 1)
        '            percentDiff = System.Math.Abs((o5p.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -12.765
        '            Dim o5d As Orbital = a.GetOrbital(5, 2)
        '            percentDiff = System.Math.Abs((o5d.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -2.3011
        '            Dim o5f As Orbital = a.GetOrbital(5, 3)
        '            percentDiff = System.Math.Abs((o5f.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -3.6731
        '            Dim o6s As Orbital = a.GetOrbital(6, 0)
        '            percentDiff = System.Math.Abs((o6s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -2.3183
        '            Dim o6p As Orbital = a.GetOrbital(6, 1)
        '            percentDiff = System.Math.Abs((o6p.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -0.4358
        '            Dim o6d As Orbital = a.GetOrbital(6, 2)
        '            percentDiff = System.Math.Abs((o6d.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '            EHS = -0.3859
        '            Dim o7s As Orbital = a.GetOrbital(7, 0)
        '            percentDiff = System.Math.Abs((o7s.Energy - EHS) / EHS)
        '            Assert.LessOrEqual(percentDiff, percentTol)
        '
        '        End Sub
        '
        '        <TestMethod()> _
        '        Public Sub TabulateLevels()
        '
        '            'Dim a As New Atom(Element.Mercury, New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(Element.Mercury) * 205.0, 400))
        '            'Dim a As New Atom(Element.Lawrencium, New ElectronicConfiguration("[Rn] 5f14 6d1 7s2"), New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(103) * 205.0, 600))
        '            Dim a As New Atom(81)
        '
        '            Console.WriteLine(a.Configuration.ToString)
        '
        '            Dim E As Double
        '            Dim l As Integer
        '            Dim n As Integer
        '
        '            For Each o As Orbital In a.Orbitals
        '                E = o.Energy
        '                n = o.N
        '                l = o.L
        '                Console.WriteLine("(" & n.ToString & ", " & l.ToString & ") = " & E.ToString)
        '
        '            Next
        '            Assert.IsTrue(True)
        '
        '        End Sub


    End Class

End Namespace
