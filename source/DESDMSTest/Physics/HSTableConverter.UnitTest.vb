Imports DESD


Namespace AtomicPhysics_Tests


    <TestClass()> _
    Public Class HSTableConverterTests


        <TestMethod()> _
        Public Sub ExpandU()

            Dim U() As Double = {1.0, 0.99609, 0.99205, 0.98787, 0.98357, 0.97916, 0.97464, 0.97002, 0.96531, 0.96053, _
                                  0.95566, 0.94574, 0.93558, 0.92524, 0.91477, 0.90418, 0.89352, 0.88282, 0.8721, 0.86139, _
                                  0.85071, 0.82949, 0.80858, 0.78807, 0.76804, 0.74854, 0.72961, 0.71127, 0.69354, 0.67642, _
                                        0.65991, 0.6287, 0.59981, 0.57311, 0.54844, 0.52566, 0.50461, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, _
                   0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, _
                   0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, _
                   0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, _
                   0.5, 0.5, 0.5}

            Dim EU() As Double = HSTableConverter.ExpandU(U, Element.Helium)
            Dim ExpectedU(U.Length - 1) As Double

            For i As Integer = 0 To U.Length - 1
                ExpectedU(i) = -U(i) * 4.0
            Next

            For i As Integer = 0 To 336 Step 4
                '// Assert that U() points are unchanged in EU()
                Assert.AreEqual(ExpectedU(i \ 4), EU(i))

                '// Assert that the intermediate points are in the range of the input points:
                Assert.IsTrue(ExpectedU(i \ 4) <= EU(i + 1))
                Assert.IsTrue(EU(i + 1) <= EU(i + 2))
                Assert.IsTrue(EU(i + 1) <= EU(i + 3))

                Assert.IsTrue(ExpectedU(i \ 4 + 1) >= EU(i + 1))
                Assert.IsTrue(EU(i + 3) >= EU(i + 2))
                Assert.IsTrue(EU(i + 2) >= EU(i + 1))

            Next

            Assert.AreEqual(ExpectedU(109), EU(440))


        End Sub



        <TestMethod()> _
        Public Sub ExpandP()

            Dim P() As Double = {0, 0.0336, 0.0663, 0.0981, 0.1289, 0.1589, 0.188, 0.2163, 0.2438, 0.2704, 0.2963, 0.3457, 0.3922, 0.4359, 0.4768, 0.5153, 0.5512, 0.5849, 0.6163, _
                 0.6456, 0.6729, 0.7218, 0.7638, 0.7995, 0.8296, 0.8545, 0.8749, 0.8911, 0.9036, 0.9128, 0.9191, 0.9239, 0.9203, 0.9101, 0.8949, 0.8759, 0.854, 0.8303, _
                 0.8051, 0.7788, 0.7519, 0.697, 0.6422, 0.5886, 0.537, 0.4881, 0.4422, 0.3995, 0.3599, 0.3235, 0.2902, 0.2322, 0.1846, 0.1461, 0.115, 0.0902, 0.0705, _
                 0.055, 0.0428, 0.0332, 0.0257, 0.0153, 0.0091, 0.0053, 0.0031, 0.0018, 0.0011, 0.0006, 0.0004, 0.0002, 0.0001, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, _
                 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

            Dim EP() As Double = HSTableConverter.ExpandP(P)

            Assert.IsTrue(P.Length = 110)
            Assert.IsTrue(EP.Length = 441)

            Dim Hi As Double
            Dim Lo As Double
            Dim Temp As Double
            For i As Integer = 0 To 336 Step 4
                '// Assert that U() points are unchanged in EU()
                Assert.AreEqual(P(i \ 4), EP(i))

                '// Assert that the intermediate points are in the range of the input points:
                Lo = P(i \ 4)
                Hi = P(i \ 4 + 1)

                If Lo > Hi Then
                    Temp = Lo
                    Lo = Hi
                    Hi = Temp
                End If

                Assert.IsTrue(Lo <= EP(i))
                Assert.IsTrue(Hi >= EP(i))

            Next

            Assert.AreEqual(P(109), EP(440))


        End Sub
    End Class

End Namespace
