
'Namespace AtomicPhysics_Tests
'	
'    <TestFixture()> _
'    Public Class NeutralSCMuffinTin_UnitTest
'
'        <Test()> _
'        Public Sub HeliumInfiniteRadius()
'
'            Dim a As New NeutralSCMuffinTin(Element.Helium, double.PositiveInfinity)
'
'            Assert.AreEqual(-1.721, a.Orbitals(0).Energy, 0.001)
'			Assert.IsTrue(Double.IsPositiveInfinity(a.Radius))
'			
'        End Sub
'
''        <Test()> _
''        Public Sub Carbon()
''
''            Dim a As New Atom(Element.Carbon)
''
''            Dim o1s As Orbital = a.GetOrbital(1, 0)
''
''            Assert.AreEqual(-21.378, o1s.Energy, 0.01)
''
''            Dim o2s As Orbital = a.GetOrbital(2, 0)
''
''            Assert.AreEqual(-1.2895, o2s.Energy, 0.005)
''
''            Dim o2p As Orbital = a.GetOrbital(2, 1)
''
''            Assert.AreEqual(-0.6603, o2p.Energy, 0.0005)
''
''        End Sub
'
'        <Test()> _
'        Public Sub Silicon()
'
'            Dim mt As New NeutralSCMuffinTin(Element.Silicon, 2.1)
'            
'            Dim ip As Double = mt.InnerPotential
'            
'            Dim Imt As integer = mt.Imt
'            
'            assert.IsTrue(True)
'            
'        End Sub
'
''        <Test()> _
''        Public Sub CopperIon()
''
''            Dim a As New Atom(Element.Copper, New ElectronicConfiguration("1s2 2s2 2p6 3s2 3p6 3d10"))
''            Dim percentDiff As Double
''            Dim percentTol As Double = 0.007
''            Dim EHS As Double
''
''            Dim U As Double() = HSTableConverter.CompressU(a.Mesh, Element.Copper, a.Potential)
''
''            EHS = -650.4
''            Dim o1s As Orbital = a.GetOrbital(1, 0)
''            percentDiff = System.Math.Abs((o1s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -78.872
''            Dim o2s As Orbital = a.GetOrbital(2, 0)
''            percentDiff = System.Math.Abs((o2s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -69.74
''            Dim o2p As Orbital = a.GetOrbital(2, 1)
''            percentDiff = System.Math.Abs((o2p.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -9.3548
''            Dim o3s As Orbital = a.GetOrbital(3, 0)
''            percentDiff = System.Math.Abs((o3s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -6.4286
''            Dim o3p As Orbital = a.GetOrbital(3, 1)
''            percentDiff = System.Math.Abs((o3p.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -1.4592
''            Dim o3d As Orbital = a.GetOrbital(3, 2)
''            percentDiff = System.Math.Abs((o3d.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''        End Sub
''
''
''        <Test()> _
''        Public Sub Copper()
''
''            Dim a As New Atom(Element.Copper)
''            Dim percentDiff As Double
''            Dim percentTol As Double = 0.005
''            Dim EHS As Double
''
''            Dim U As Double() = HSTableConverter.CompressU(a.Mesh, Element.Copper, a.Potential)
''
''            EHS = -649.7
''            Dim o1s As Orbital = a.GetOrbital(1, 0)
''            percentDiff = System.Math.Abs((o1s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -78.15
''            Dim o2s As Orbital = a.GetOrbital(2, 0)
''            percentDiff = System.Math.Abs((o2s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -69.02
''            Dim o2p As Orbital = a.GetOrbital(2, 1)
''            percentDiff = System.Math.Abs((o2p.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -8.634
''            Dim o3s As Orbital = a.GetOrbital(3, 0)
''            percentDiff = System.Math.Abs((o3s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -5.709
''            Dim o3p As Orbital = a.GetOrbital(3, 1)
''            percentDiff = System.Math.Abs((o3p.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -0.7431
''            Dim o3d As Orbital = a.GetOrbital(3, 2)
''            percentDiff = System.Math.Abs((o3d.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -0.5091
''            Dim o4s As Orbital = a.GetOrbital(4, 0)
''            percentDiff = System.Math.Abs((o4s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''        End Sub
''        
''        
''        <Test> _
''        Public Sub Lawrencium
''            Dim a As New Atom(Element.Lawrencium, New ElectronicConfiguration("[Rn] 5f14 6d1 7s2"))
''            Dim percentDiff As Double
''            Dim percentTol As Double = 0.05
''            Dim EHS As Double
''
''            'Dim U As Double() = HSTableConverter.CompressU(a.Mesh, Element.Copper, a.Potential)
''
''            EHS = -9364.3
''            Dim o1s As Orbital = a.GetOrbital(1, 0)
''            percentDiff = System.Math.Abs((o1s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -1667.1
''            Dim o2s As Orbital = a.GetOrbital(2, 0)
''            percentDiff = System.Math.Abs((o2s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -1620.9
''            Dim o2p As Orbital = a.GetOrbital(2, 1)
''            percentDiff = System.Math.Abs((o2p.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -437.33
''            Dim o3s As Orbital = a.GetOrbital(3, 0)
''            percentDiff = System.Math.Abs((o3s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -413.78
''            Dim o3p As Orbital = a.GetOrbital(3, 1)
''            percentDiff = System.Math.Abs((o3p.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -369.41
''            Dim o3d As Orbital = a.GetOrbital(3, 2)
''            percentDiff = System.Math.Abs((o3d.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -117.31
''            Dim o4s As Orbital = a.GetOrbital(4, 0)
''            percentDiff = System.Math.Abs((o4s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -105.9
''            Dim o4p As Orbital = a.GetOrbital(4, 1)
''            percentDiff = System.Math.Abs((o4p.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''            
''            EHS = -84.373
''            Dim o4d As Orbital = a.GetOrbital(4, 2)
''            percentDiff = System.Math.Abs((o4d.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -54.080
''            Dim o4f As Orbital = a.GetOrbital(4, 3)
''            percentDiff = System.Math.Abs((o4f.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -26.141
''            Dim o5s As Orbital = a.GetOrbital(5, 0)
''            percentDiff = System.Math.Abs((o5s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -21.260
''            Dim o5p As Orbital = a.GetOrbital(5, 1)
''            percentDiff = System.Math.Abs((o5p.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -12.765
''            Dim o5d As Orbital = a.GetOrbital(5, 2)
''            percentDiff = System.Math.Abs((o5d.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -2.3011
''            Dim o5f As Orbital = a.GetOrbital(5, 3)
''            percentDiff = System.Math.Abs((o5f.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -3.6731
''            Dim o6s As Orbital = a.GetOrbital(6, 0)
''            percentDiff = System.Math.Abs((o6s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -2.3183
''            Dim o6p As Orbital = a.GetOrbital(6, 1)
''            percentDiff = System.Math.Abs((o6p.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -0.4358
''            Dim o6d As Orbital = a.GetOrbital(6, 2)
''            percentDiff = System.Math.Abs((o6d.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''            EHS = -0.3859
''            Dim o7s As Orbital = a.GetOrbital(7, 0)
''            percentDiff = System.Math.Abs((o7s.Energy - EHS) / EHS)
''            Assert.LessOrEqual(percentDiff, percentTol)
''
''        End Sub
''
''        <Test()> _
''        Public Sub TabulateLevels()
''
''            'Dim a As New Atom(Element.Mercury, New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(Element.Mercury) * 205.0, 400))
''            'Dim a As New Atom(Element.Lawrencium, New ElectronicConfiguration("[Rn] 5f14 6d1 7s2"), New CubicMesh(0.0, HermanSkillmanMesh.GetCMU(103) * 205.0, 600))
''            Dim a As New Atom(81)
''
''            Console.WriteLine(a.Configuration.ToString)
''
''            Dim E As Double
''            Dim l As Integer
''            Dim n As Integer
''
''            For Each o As Orbital In a.Orbitals
''                E = o.Energy
''                n = o.N
''                l = o.L
''                Console.WriteLine("(" & n.ToString & ", " & l.ToString & ") = " & E.ToString)
''
''            Next
''            Assert.IsTrue(True)
''
''        End Sub
'
'
'    End Class
'
'End Namespace
