Imports DESD


Namespace AtomicPhysics_Tests

    <TestClass()> _
    Public Class BoundRSESolver_UnitTest

        <TestMethod()> _
        Public Sub Helium1s()

            Dim HSPnl() As Double = {0, 0.0336, 0.0663, 0.0981, 0.1289, 0.1589, 0.188, 0.2163, 0.2438, 0.2704, 0.2963, 0.3457, 0.3922, 0.4359, 0.4768, 0.5153, 0.5512, 0.5849, 0.6163, _
           0.6456, 0.6729, 0.7218, 0.7638, 0.7995, 0.8296, 0.8545, 0.8749, 0.8911, 0.9036, 0.9128, 0.9191, 0.9239, 0.9203, 0.9101, 0.8949, 0.8759, 0.854, 0.8303, _
           0.8051, 0.7788, 0.7519, 0.697, 0.6422, 0.5886, 0.537, 0.4881, 0.4422, 0.3995, 0.3599, 0.3235, 0.2902, 0.2322, 0.1846, 0.1461, 0.115, 0.0902, 0.0705, _
           0.055, 0.0428, 0.0332, 0.0257, 0.0153, 0.0091, 0.0053, 0.0031, 0.0018, 0.0011, 0.0006, 0.0004, 0.0002, 0.0001, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, _
           0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

            Dim U() As Double = {1.0, 0.99609, 0.99205, 0.98787, 0.98357, 0.97916, 0.97464, 0.97002, 0.96531, 0.96053, _
                            0.95566, 0.94574, 0.93558, 0.92524, 0.91477, 0.90418, 0.89352, 0.88282, 0.8721, 0.86139, _
                            0.85071, 0.82949, 0.80858, 0.78807, 0.76804, 0.74854, 0.72961, 0.71127, 0.69354, 0.67642, _
                                  0.65991, 0.6287, 0.59981, 0.57311, 0.54844, 0.52566, 0.50461, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, _
             0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, _
             0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, _
             0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, _
             0.5, 0.5, 0.5}

            Dim ExpectedPnl() As Double = HSTableConverter.ExpandP(HSPnl)

            Dim EU() As Double = HSTableConverter.ExpandU(U, Element.Helium)

            Dim mesh As New HermanSkillmanMesh(Element.Helium)

            Dim iMax As Integer = mesh.Count - 1
            Dim V(iMax) As Double
            For i As Integer = 1 To iMax
                V(i) = EU(i) / mesh.R(i)
            Next
            Dim solution As Orbital = BoundRSESolver.Solve(mesh, 1, 0, 2.0, V)

            '// Check normalization
            Dim expectednorm As Double = DESD.Math.Integration.TrapezoidalRuleIntegrator.Integrate(mesh.GetArray, ExpectedPnl, 0, mesh.Count - 1)
            Dim norm As Double = DESD.Math.Integration.TrapezoidalRuleIntegrator.Integrate(mesh.GetArray, solution.PArray, 0, mesh.Count - 1)

            '// Assert normalizations are same
            Assert.AreEqual(expectednorm, norm, 0.001)

            '// Assert on energy:
            Assert.AreEqual(-1.721, solution.Energy, 0.001)

            '// assert wave function matches
            For i As Integer = 0 To iMax

                Assert.AreEqual(ExpectedPnl(i), solution.P(i), 0.0003)

            Next

        End Sub


        <TestMethod()> _
        Public Sub Carbon1s()

            ' Dim HSPnl() As Double = {0, 0.0336, 0.0663, 0.0981, 0.1289, 0.1589, 0.188, 0.2163, 0.2438, 0.2704, 0.2963, 0.3457, 0.3922, 0.4359, 0.4768, 0.5153, 0.5512, 0.5849, 0.6163, _
            '0.6456, 0.6729, 0.7218, 0.7638, 0.7995, 0.8296, 0.8545, 0.8749, 0.8911, 0.9036, 0.9128, 0.9191, 0.9239, 0.9203, 0.9101, 0.8949, 0.8759, 0.854, 0.8303, _
            '0.8051, 0.7788, 0.7519, 0.697, 0.6422, 0.5886, 0.537, 0.4881, 0.4422, 0.3995, 0.3599, 0.3235, 0.2902, 0.2322, 0.1846, 0.1461, 0.115, 0.0902, 0.0705, _
            '0.055, 0.0428, 0.0332, 0.0257, 0.0153, 0.0091, 0.0053, 0.0031, 0.0018, 0.0011, 0.0006, 0.0004, 0.0002, 0.0001, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, _
            '0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

            '// Carbon potential from Herman and Skillman, http://hermes.phys.uwm.edu/projects/elecstruct/hermsk/inputs/in06.C.0N.html

            Dim U() As Double = {1.0, 0.99384, 0.98748, 0.98095, 0.97428, 0.9675, 0.96064, 0.95371, 0.94674, 0.93974, _
                                0.93273, 0.91873, 0.90483, 0.89111, 0.8776, 0.86436, 0.85141, 0.83876, 0.82642, 0.8144, _
                                0.80269, 0.7802, 0.75887, 0.7386, 0.71929, 0.70084, 0.68315, 0.66616, 0.64979, 0.634, _
                                0.61874, 0.58976, 0.56278, 0.53786, 0.51509, 0.49448, 0.47596, 0.4593, 0.4442, 0.43038, _
                                0.41755, 0.3941, 0.37273, 0.35292, 0.33441, 0.31711, 0.30095, 0.28586, 0.2718, 0.25871, _
                                0.24652, 0.22462, 0.20558, 0.18896, 0.17441, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667}

            'Dim ExpectedPnl() As Double = HSTableConverter.ExpandP(HSPnl)

            Dim EU() As Double = HSTableConverter.ExpandU(U, Element.Carbon)

            Dim mesh As New HermanSkillmanMesh(Element.Carbon)

            Dim iMax As Integer = mesh.Count - 1
            Dim V(iMax) As Double
            For i As Integer = 1 To iMax
                V(i) = EU(i) / mesh.R(i)
            Next
            Dim solution As Orbital = BoundRSESolver.Solve(mesh, 1, 0, 2.0, V)

            '// Check normalization
            'Dim expectednorm As Double = DESD.Math.Integration.TrapezoidalRuleIntegrator.Integrate(mesh.GetArray, ExpectedPnl, 0, mesh.Count - 1)
            'Dim norm As Double = DESD.Math.Integration.TrapezoidalRuleIntegrator.Integrate(mesh.GetArray, solution.PArray, 0, mesh.Count - 1)

            '// Assert normalizations are same
            'Assert.AreEqual(expectednorm, norm, 0.001)

            '// Assert on energy:
            Assert.AreEqual(-21.378, solution.Energy, 0.01)

            '// assert wave function matches
            'For i As Integer = 0 To iMax

            '    Assert.AreEqual(ExpectedPnl(i), solution.P(i), 0.0003)

            'Next

        End Sub

        <TestMethod()> _
        Public Sub Carbon2s()

            ' Dim HSPnl() As Double = {0, 0.0336, 0.0663, 0.0981, 0.1289, 0.1589, 0.188, 0.2163, 0.2438, 0.2704, 0.2963, 0.3457, 0.3922, 0.4359, 0.4768, 0.5153, 0.5512, 0.5849, 0.6163, _
            '0.6456, 0.6729, 0.7218, 0.7638, 0.7995, 0.8296, 0.8545, 0.8749, 0.8911, 0.9036, 0.9128, 0.9191, 0.9239, 0.9203, 0.9101, 0.8949, 0.8759, 0.854, 0.8303, _
            '0.8051, 0.7788, 0.7519, 0.697, 0.6422, 0.5886, 0.537, 0.4881, 0.4422, 0.3995, 0.3599, 0.3235, 0.2902, 0.2322, 0.1846, 0.1461, 0.115, 0.0902, 0.0705, _
            '0.055, 0.0428, 0.0332, 0.0257, 0.0153, 0.0091, 0.0053, 0.0031, 0.0018, 0.0011, 0.0006, 0.0004, 0.0002, 0.0001, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, _
            '0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

            '// Carbon potential from Herman and Skillman, http://hermes.phys.uwm.edu/projects/elecstruct/hermsk/inputs/in06.C.0N.html

            Dim U() As Double = {1.0, 0.99384, 0.98748, 0.98095, 0.97428, 0.9675, 0.96064, 0.95371, 0.94674, 0.93974, _
                                0.93273, 0.91873, 0.90483, 0.89111, 0.8776, 0.86436, 0.85141, 0.83876, 0.82642, 0.8144, _
                                0.80269, 0.7802, 0.75887, 0.7386, 0.71929, 0.70084, 0.68315, 0.66616, 0.64979, 0.634, _
                                0.61874, 0.58976, 0.56278, 0.53786, 0.51509, 0.49448, 0.47596, 0.4593, 0.4442, 0.43038, _
                                0.41755, 0.3941, 0.37273, 0.35292, 0.33441, 0.31711, 0.30095, 0.28586, 0.2718, 0.25871, _
                                0.24652, 0.22462, 0.20558, 0.18896, 0.17441, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667}

            'Dim ExpectedPnl() As Double = HSTableConverter.ExpandP(HSPnl)

            Dim EU() As Double = HSTableConverter.ExpandU(U, Element.Carbon)

            Dim mesh As New HermanSkillmanMesh(Element.Carbon)

            Dim iMax As Integer = mesh.Count - 1
            Dim V(iMax) As Double
            For i As Integer = 1 To iMax
                V(i) = EU(i) / mesh.R(i)
            Next
            Dim solution As Orbital = BoundRSESolver.Solve(mesh, 2, 0, 2.0, V)

            '// Check normalization
            'Dim expectednorm As Double = DESD.Math.Integration.TrapezoidalRuleIntegrator.Integrate(mesh.GetArray, ExpectedPnl, 0, mesh.Count - 1)
            'Dim norm As Double = DESD.Math.Integration.TrapezoidalRuleIntegrator.Integrate(mesh.GetArray, solution.PArray, 0, mesh.Count - 1)

            '// Assert normalizations are same
            'Assert.AreEqual(expectednorm, norm, 0.001)

            '// Assert on energy:
            Assert.AreEqual(-1.2895, solution.Energy, 0.0005)

            '// assert wave function matches
            'For i As Integer = 0 To iMax

            '    Assert.AreEqual(ExpectedPnl(i), solution.P(i), 0.0003)

            'Next

        End Sub

        <TestMethod()> _
        Public Sub Carbon2p()

            ' Dim HSPnl() As Double = {0, 0.0336, 0.0663, 0.0981, 0.1289, 0.1589, 0.188, 0.2163, 0.2438, 0.2704, 0.2963, 0.3457, 0.3922, 0.4359, 0.4768, 0.5153, 0.5512, 0.5849, 0.6163, _
            '0.6456, 0.6729, 0.7218, 0.7638, 0.7995, 0.8296, 0.8545, 0.8749, 0.8911, 0.9036, 0.9128, 0.9191, 0.9239, 0.9203, 0.9101, 0.8949, 0.8759, 0.854, 0.8303, _
            '0.8051, 0.7788, 0.7519, 0.697, 0.6422, 0.5886, 0.537, 0.4881, 0.4422, 0.3995, 0.3599, 0.3235, 0.2902, 0.2322, 0.1846, 0.1461, 0.115, 0.0902, 0.0705, _
            '0.055, 0.0428, 0.0332, 0.0257, 0.0153, 0.0091, 0.0053, 0.0031, 0.0018, 0.0011, 0.0006, 0.0004, 0.0002, 0.0001, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, _
            '0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

            '// Carbon potential from Herman and Skillman, http://hermes.phys.uwm.edu/projects/elecstruct/hermsk/inputs/in06.C.0N.html

            Dim U() As Double = {1.0, 0.99384, 0.98748, 0.98095, 0.97428, 0.9675, 0.96064, 0.95371, 0.94674, 0.93974, _
                                0.93273, 0.91873, 0.90483, 0.89111, 0.8776, 0.86436, 0.85141, 0.83876, 0.82642, 0.8144, _
                                0.80269, 0.7802, 0.75887, 0.7386, 0.71929, 0.70084, 0.68315, 0.66616, 0.64979, 0.634, _
                                0.61874, 0.58976, 0.56278, 0.53786, 0.51509, 0.49448, 0.47596, 0.4593, 0.4442, 0.43038, _
                                0.41755, 0.3941, 0.37273, 0.35292, 0.33441, 0.31711, 0.30095, 0.28586, 0.2718, 0.25871, _
                                0.24652, 0.22462, 0.20558, 0.18896, 0.17441, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, _
                                0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667, 0.16667}

            'Dim ExpectedPnl() As Double = HSTableConverter.ExpandP(HSPnl)

            Dim EU() As Double = HSTableConverter.ExpandU(U, Element.Carbon)

            Dim mesh As New HermanSkillmanMesh(Element.Carbon)

            Dim iMax As Integer = mesh.Count - 1
            Dim V(iMax) As Double
            For i As Integer = 1 To iMax
                V(i) = EU(i) / mesh.R(i)
            Next
            Dim solution As Orbital = BoundRSESolver.Solve(mesh, 2, 1, 2.0, V)

            '// Check normalization
            'Dim expectednorm As Double = DESD.Math.Integration.TrapezoidalRuleIntegrator.Integrate(mesh.GetArray, ExpectedPnl, 0, mesh.Count - 1)
            'Dim norm As Double = DESD.Math.Integration.TrapezoidalRuleIntegrator.Integrate(mesh.GetArray, solution.PArray, 0, mesh.Count - 1)

            '// Assert normalizations are same
            'Assert.AreEqual(expectednorm, norm, 0.001)

            '// Assert on energy:
            Assert.AreEqual(-0.6603, solution.Energy, 0.0005)

            '// assert wave function matches
            'For i As Integer = 0 To iMax

            '    Assert.AreEqual(ExpectedPnl(i), solution.P(i), 0.0003)

            'Next

        End Sub

        <TestMethod()> _
        Public Sub Silicon1s()


            '//  potential from Herman and Skillman, http://hermes.phys.uwm.edu/projects/elecstruct/hermsk/inputs/in06.C.0N.html

            Dim U() As Double = {1.0, 0.99144, 0.98265, 0.97369, 0.96463, 0.95552, 0.94641, 0.93731, 0.92827, 0.91929, _
 0.91041, 0.89294, 0.87593, 0.8594, 0.84335, 0.82778, 0.81266, 0.79798, 0.7837, 0.76982, _
 0.75631, 0.73038, 0.70584, 0.68267, 0.66083, 0.64028, 0.62096, 0.60274, 0.58551, 0.56917, _
 0.55361, 0.52452, 0.49779, 0.47316, 0.45043, 0.42947, 0.41014, 0.39232, 0.37587, 0.36066, _
 0.34657, 0.32127, 0.29915, 0.27955, 0.26199, 0.24617, 0.23189, 0.21905, 0.20758, 0.19743, _
 0.18849, 0.17369, 0.16187, 0.15193, 0.14314, 0.13516, 0.12779, 0.12094, 0.11457, 0.10864, _
 0.10311, 0.09317, 0.08454, 0.07704, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143}

            Dim EU() As Double = HSTableConverter.ExpandU(U, Element.Silicon)

            Dim mesh As New HermanSkillmanMesh(Element.Silicon)

            Dim iMax As Integer = mesh.Count - 1
            Dim V(iMax) As Double
            For i As Integer = 1 To iMax
                V(i) = EU(i) / mesh.R(i)
            Next
            Dim solution As Orbital = BoundRSESolver.Solve(mesh, 1, 0, 2.0, V)

            '// Assert on energy:
            Assert.AreEqual(-134.04, solution.Energy, 0.1)

        End Sub

        <TestMethod()> _
        Public Sub Silicon2s()


            '//  potential from Herman and Skillman, http://hermes.phys.uwm.edu/projects/elecstruct/hermsk/inputs/in06.C.0N.html

            Dim U() As Double = {1.0, 0.99144, 0.98265, 0.97369, 0.96463, 0.95552, 0.94641, 0.93731, 0.92827, 0.91929, _
 0.91041, 0.89294, 0.87593, 0.8594, 0.84335, 0.82778, 0.81266, 0.79798, 0.7837, 0.76982, _
 0.75631, 0.73038, 0.70584, 0.68267, 0.66083, 0.64028, 0.62096, 0.60274, 0.58551, 0.56917, _
 0.55361, 0.52452, 0.49779, 0.47316, 0.45043, 0.42947, 0.41014, 0.39232, 0.37587, 0.36066, _
 0.34657, 0.32127, 0.29915, 0.27955, 0.26199, 0.24617, 0.23189, 0.21905, 0.20758, 0.19743, _
 0.18849, 0.17369, 0.16187, 0.15193, 0.14314, 0.13516, 0.12779, 0.12094, 0.11457, 0.10864, _
 0.10311, 0.09317, 0.08454, 0.07704, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143}

            Dim EU() As Double = HSTableConverter.ExpandU(U, Element.Silicon)

            Dim mesh As New HermanSkillmanMesh(Element.Silicon)

            Dim iMax As Integer = mesh.Count - 1
            Dim V(iMax) As Double
            For i As Integer = 1 To iMax
                V(i) = EU(i) / mesh.R(i)
            Next
            Dim solution As Orbital = BoundRSESolver.Solve(mesh, 2, 0, 2.0, V)

            '// Assert on energy:
            Assert.AreEqual(-11.087, solution.Energy, 0.005)

        End Sub

        <TestMethod()> _
        Public Sub Silicon2p()


            '//  potential from Herman and Skillman, http://hermes.phys.uwm.edu/projects/elecstruct/hermsk/inputs/in06.C.0N.html

            Dim U() As Double = {1.0, 0.99144, 0.98265, 0.97369, 0.96463, 0.95552, 0.94641, 0.93731, 0.92827, 0.91929, _
 0.91041, 0.89294, 0.87593, 0.8594, 0.84335, 0.82778, 0.81266, 0.79798, 0.7837, 0.76982, _
 0.75631, 0.73038, 0.70584, 0.68267, 0.66083, 0.64028, 0.62096, 0.60274, 0.58551, 0.56917, _
 0.55361, 0.52452, 0.49779, 0.47316, 0.45043, 0.42947, 0.41014, 0.39232, 0.37587, 0.36066, _
 0.34657, 0.32127, 0.29915, 0.27955, 0.26199, 0.24617, 0.23189, 0.21905, 0.20758, 0.19743, _
 0.18849, 0.17369, 0.16187, 0.15193, 0.14314, 0.13516, 0.12779, 0.12094, 0.11457, 0.10864, _
 0.10311, 0.09317, 0.08454, 0.07704, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143}

            Dim EU() As Double = HSTableConverter.ExpandU(U, Element.Silicon)

            Dim mesh As New HermanSkillmanMesh(Element.Silicon)

            Dim iMax As Integer = mesh.Count - 1
            Dim V(iMax) As Double
            For i As Integer = 1 To iMax
                V(i) = EU(i) / mesh.R(i)
            Next
            Dim solution As Orbital = BoundRSESolver.Solve(mesh, 2, 1, 2.0, V)

            '// Assert on energy:
            Assert.AreEqual(-7.954, solution.Energy, 0.002)

        End Sub

        <TestMethod()> _
        Public Sub Silicon3s()


            '//  potential from Herman and Skillman, http://hermes.phys.uwm.edu/projects/elecstruct/hermsk/inputs/in06.C.0N.html

            Dim U() As Double = {1.0, 0.99144, 0.98265, 0.97369, 0.96463, 0.95552, 0.94641, 0.93731, 0.92827, 0.91929, _
 0.91041, 0.89294, 0.87593, 0.8594, 0.84335, 0.82778, 0.81266, 0.79798, 0.7837, 0.76982, _
 0.75631, 0.73038, 0.70584, 0.68267, 0.66083, 0.64028, 0.62096, 0.60274, 0.58551, 0.56917, _
 0.55361, 0.52452, 0.49779, 0.47316, 0.45043, 0.42947, 0.41014, 0.39232, 0.37587, 0.36066, _
 0.34657, 0.32127, 0.29915, 0.27955, 0.26199, 0.24617, 0.23189, 0.21905, 0.20758, 0.19743, _
 0.18849, 0.17369, 0.16187, 0.15193, 0.14314, 0.13516, 0.12779, 0.12094, 0.11457, 0.10864, _
 0.10311, 0.09317, 0.08454, 0.07704, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143}

            Dim EU() As Double = HSTableConverter.ExpandU(U, Element.Silicon)

            Dim mesh As New HermanSkillmanMesh(Element.Silicon)

            Dim iMax As Integer = mesh.Count - 1
            Dim V(iMax) As Double
            For i As Integer = 1 To iMax
                V(i) = EU(i) / mesh.R(i)
            Next
            Dim solution As Orbital = BoundRSESolver.Solve(mesh, 3, 0, 2.0, V)

            '// Assert on energy:
            Assert.AreEqual(-0.9975, solution.Energy, 0.0005)

        End Sub


        <TestMethod()> _
        Public Sub Silicon3p()


            '//  potential from Herman and Skillman, http://hermes.phys.uwm.edu/projects/elecstruct/hermsk/inputs/in06.C.0N.html

            Dim U() As Double = {1.0, 0.99144, 0.98265, 0.97369, 0.96463, 0.95552, 0.94641, 0.93731, 0.92827, 0.91929, _
 0.91041, 0.89294, 0.87593, 0.8594, 0.84335, 0.82778, 0.81266, 0.79798, 0.7837, 0.76982, _
 0.75631, 0.73038, 0.70584, 0.68267, 0.66083, 0.64028, 0.62096, 0.60274, 0.58551, 0.56917, _
 0.55361, 0.52452, 0.49779, 0.47316, 0.45043, 0.42947, 0.41014, 0.39232, 0.37587, 0.36066, _
 0.34657, 0.32127, 0.29915, 0.27955, 0.26199, 0.24617, 0.23189, 0.21905, 0.20758, 0.19743, _
 0.18849, 0.17369, 0.16187, 0.15193, 0.14314, 0.13516, 0.12779, 0.12094, 0.11457, 0.10864, _
 0.10311, 0.09317, 0.08454, 0.07704, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, _
 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143, 0.07143}

            Dim EU() As Double = HSTableConverter.ExpandU(U, Element.Silicon)

            Dim mesh As New HermanSkillmanMesh(Element.Silicon)

            Dim iMax As Integer = mesh.Count - 1
            Dim V(iMax) As Double
            For i As Integer = 1 To iMax
                V(i) = EU(i) / mesh.R(i)
            Next
            Dim solution As Orbital = BoundRSESolver.Solve(mesh, 3, 1, 2.0, V)

            '// Assert on energy:
            Assert.AreEqual(-0.4802, solution.Energy, 0.0003)

        End Sub


        <TestMethod()> _
        Public Sub CopperIon()


            '//  potential from Herman and Skillman, http://hermes.phys.uwm.edu/projects/elecstruct/hermsk/inputs/in06.C.0N.html

            Dim U() As Double = {1.0, 0.98997, 0.97969, 0.96931, 0.95892, 0.94859, 0.93837, 0.92828, 0.91835, 0.90859, _
   0.89901, 0.88036, 0.86238, 0.84504, 0.82832, 0.81218, 0.79663, 0.78165, 0.76722, 0.75333, _
   0.73994, 0.71456, 0.69077, 0.66835, 0.64711, 0.62693, 0.60771, 0.58939, 0.57189, 0.55517, _
   0.53915, 0.50905, 0.4812, 0.45534, 0.43128, 0.40892, 0.38816, 0.36888, 0.35093, 0.33416, _
   0.31841, 0.28955, 0.26368, 0.24044, 0.21962, 0.20102, 0.18445, 0.16972, 0.15664, 0.14501, _
   0.13467, 0.11722, 0.10322, 0.09185, 0.08251, 0.07475, 0.06825, 0.06276, 0.05807, 0.05406, _
   0.05058, 0.04492, 0.04052, 0.03703, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, _
   0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, _
   0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, _
   0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, _
   0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448}

            Dim EU() As Double = HSTableConverter.ExpandU(U, Element.Copper)

            Dim mesh As New HermanSkillmanMesh(Element.Copper)

            Dim iMax As Integer = mesh.Count - 1
            Dim V(iMax) As Double
            For i As Integer = 1 To iMax
                V(i) = EU(i) / mesh.R(i)
            Next


            Dim solution As Orbital

            Dim startTime As Long = My.Computer.Clock.TickCount


            '// Do all of the orbitals in this test:
            solution = BoundRSESolver.Solve(mesh, 1, 0, 2.0, V)

            Assert.AreEqual(-649.7, solution.Energy, 0.3)

            solution = BoundRSESolver.Solve(mesh, 2, 0, 2.0, V)

            Assert.AreEqual(-78.15, solution.Energy, 0.05)

            solution = BoundRSESolver.Solve(mesh, 2, 1, 6.0, V)

            Assert.AreEqual(-69.02, solution.Energy, 0.003)

            solution = BoundRSESolver.Solve(mesh, 3, 0, 2.0, V)

            Assert.AreEqual(-8.634, solution.Energy, 0.004)

            solution = BoundRSESolver.Solve(mesh, 3, 1, 6.0, V)

            Assert.AreEqual(-5.709, solution.Energy, 0.003)

            solution = BoundRSESolver.Solve(mesh, 3, 2, 10.0, V)

            Dim endTime As Long = My.Computer.Clock.TickCount
            Dim deltaTime As Long = endTime - startTime

            Assert.AreEqual(-0.7431, solution.Energy, 0.002)



        End Sub


        <TestMethod()> _
        Public Sub CopperIonThreaded()


            '//  potential from Herman and Skillman, http://hermes.phys.uwm.edu/projects/elecstruct/hermsk/inputs/in06.C.0N.html

            Dim U() As Double = {1.0, 0.98997, 0.97969, 0.96931, 0.95892, 0.94859, 0.93837, 0.92828, 0.91835, 0.90859, _
   0.89901, 0.88036, 0.86238, 0.84504, 0.82832, 0.81218, 0.79663, 0.78165, 0.76722, 0.75333, _
   0.73994, 0.71456, 0.69077, 0.66835, 0.64711, 0.62693, 0.60771, 0.58939, 0.57189, 0.55517, _
   0.53915, 0.50905, 0.4812, 0.45534, 0.43128, 0.40892, 0.38816, 0.36888, 0.35093, 0.33416, _
   0.31841, 0.28955, 0.26368, 0.24044, 0.21962, 0.20102, 0.18445, 0.16972, 0.15664, 0.14501, _
   0.13467, 0.11722, 0.10322, 0.09185, 0.08251, 0.07475, 0.06825, 0.06276, 0.05807, 0.05406, _
   0.05058, 0.04492, 0.04052, 0.03703, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, _
   0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, _
   0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, _
   0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, _
   0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448, 0.03448}

            Dim EU() As Double = HSTableConverter.ExpandU(U, Element.Copper)

            Dim mesh As New HermanSkillmanMesh(Element.Copper)

            Dim iMax As Integer = mesh.Count - 1
            Dim V(iMax) As Double
            For i As Integer = 1 To iMax
                V(i) = EU(i) / mesh.R(i)
            Next




            Dim delSolve As New SolveDelegate(AddressOf BoundRSESolver.Solve)
            Dim AsyncResults As New List(Of IAsyncResult)

            Dim startTime As Long = My.Computer.Clock.TickCount

            '// Invoke with 1s parameters:
            AsyncResults.Add(delSolve.BeginInvoke(mesh, 1, 0, 2.0, V, New AsyncCallback(AddressOf SolverCallback), Nothing))

            '// Invoke with 2s parameters:
            AsyncResults.Add(delSolve.BeginInvoke(mesh, 2, 0, 2.0, V, New AsyncCallback(AddressOf SolverCallback), Nothing))

            '// Invoke with 2p parameters:
            AsyncResults.Add(delSolve.BeginInvoke(mesh, 2, 1, 6.0, V, New AsyncCallback(AddressOf SolverCallback), Nothing))

            '// Invoke with 3s parameters:
            AsyncResults.Add(delSolve.BeginInvoke(mesh, 3, 0, 2.0, V, New AsyncCallback(AddressOf SolverCallback), Nothing))

            '// Invoke with 3p parameters:
            AsyncResults.Add(delSolve.BeginInvoke(mesh, 3, 1, 6.0, V, New AsyncCallback(AddressOf SolverCallback), Nothing))

            '// Invoke with 3d parameters:
            AsyncResults.Add(delSolve.BeginInvoke(mesh, 3, 2, 10.0, V, New AsyncCallback(AddressOf SolverCallback), Nothing))

            Dim isDone As Boolean = False

            Do While Not (isDone)
                isDone = True
                For Each AR As IAsyncResult In AsyncResults
                    isDone = isDone And AR.IsCompleted
                Next
            Loop

            Dim endTime As Long = My.Computer.Clock.TickCount
            Dim deltaTime As Long = endTime - startTime

            '// We should be all completed
            Assert.IsTrue(mOrbitals.Count = 6)


        End Sub

        Private Delegate Function SolveDelegate(ByVal m As IRadialMesh, ByVal N As Integer, ByVal L As Integer, ByVal occ As Double, ByVal Pot As Double()) As Orbital
        Private mOrbitals As New List(Of Orbital)

        Private Sub SolverCallback(ByVal ar As IAsyncResult)

            '// First, cast ar as an AsyncResult:
            Dim result As System.Runtime.Remoting.Messaging.AsyncResult = CType(ar, System.Runtime.Remoting.Messaging.AsyncResult)

            '// Grab the delegate
            Dim del As SolveDelegate = CType(result.AsyncDelegate, SolveDelegate)

            '// Now that we have the delegate, we have to call EndInvoke on it
            '// so we can get our return object:
            Dim solution As Orbital = del.EndInvoke(ar)

            '// We've got the soution!!  Now add it to the (global) list,
            '// Note we need to get a synclock first, since the list is 
            '// not thread safe.
            SyncLock mOrbitals
                mOrbitals.Add(solution)
            End SyncLock


            Console.WriteLine("Finished for N = " & solution.N.ToString & ", L = " & solution.L.ToString)
            Console.WriteLine("on thread " & System.Threading.Thread.CurrentThread.GetHashCode.ToString)
        End Sub

    End Class


End Namespace
