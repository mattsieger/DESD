Imports DESD


Namespace AtomicPhysics_Tests

    <TestClass()> _
    Public Class HermanSkillmanMeshTests

        ''' <summary>
        ''' Test the default mesh size.
        ''' </summary>
        ''' <remarks></remarks>
        <TestMethod()> _
        Public Sub Constructor1()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Assert.IsTrue(myMesh.Size = 441)

            Assert.IsTrue(myMesh.R(0) = 0.0)
            Assert.IsTrue(myMesh.R(1) = myMesh.DeltaR)

        End Sub

        <TestMethod()> _
        Public Sub Constructor2()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum, HermanSkillmanMesh.HermanSkillmanMeshSize.Medium)

            Assert.IsTrue(myMesh.Size = 481)

            Assert.IsTrue(myMesh.R(0) = 0.0)
            Assert.IsTrue(myMesh.R(1) = myMesh.DeltaR)

        End Sub


        <TestMethod()> _
        Public Sub Constructor3()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum, HermanSkillmanMesh.HermanSkillmanMeshSize.Large)

            Assert.IsTrue(myMesh.Size = 521)

            Assert.IsTrue(myMesh.R(0) = 0.0)
            Assert.IsTrue(myMesh.R(1) = myMesh.DeltaR)

        End Sub

        <TestMethod()> _
        Public Sub Constructor4()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum, HermanSkillmanMesh.HermanSkillmanMeshSize.Normal, 0.002)

            Assert.IsTrue(myMesh.Size = 441)

            Assert.IsTrue(myMesh.R(0) = 0.0)
            Assert.IsTrue(myMesh.R(1) = 0.002 * myMesh.CMU)

        End Sub

        ''' <summary>
        ''' Tests proposition that all mesh values are unique and are sorted smallest to largest.
        ''' </summary>
        ''' <remarks></remarks>
        <TestMethod()> _
        Public Sub MeshIsSorted()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Dim lastValue As Double = myMesh.R(0)

            For i As Integer = 1 To myMesh.Count - 1

                Assert.IsTrue(myMesh.R(i) > lastValue)
                lastValue = myMesh.R(i)

            Next

        End Sub

        ''' <summary>
        ''' Attempts to call for a mesh point with invalid index.
        ''' </summary>
        <TestMethod(), ExpectedException(GetType(IndexOutOfRangeException))> _
        Public Sub Exception1()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Dim X As Double = myMesh.R(-1)

        End Sub

        <TestMethod(), ExpectedException(GetType(IndexOutOfRangeException))> _
        Public Sub Exception2()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Dim X As Double = myMesh.R(myMesh.Count)

        End Sub

        <TestMethod()> _
        Public Sub GetEnumerator1()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Dim Enumerator As IEnumerator = myMesh.GetEnumerator

            Assert.IsNotNull(Enumerator)

        End Sub

        <TestMethod()> _
        Public Sub GetEnumerator2()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Dim Enumerator As IEnumerator = myMesh.GetEnumerator1

            Assert.IsNotNull(Enumerator)

        End Sub

        <TestMethod()> _
        Public Sub Type()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Assert.IsTrue(myMesh.Type = MeshType.HermanSkillman)

        End Sub

        <TestMethod()> _
        Public Sub Count1()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Assert.AreEqual(441, myMesh.Count)

        End Sub

        <TestMethod()> _
        Public Sub Count2()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum, HermanSkillmanMesh.HermanSkillmanMeshSize.Medium)

            Assert.AreEqual(481, myMesh.Count)

        End Sub


        <TestMethod()> _
        Public Sub Count3()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum, HermanSkillmanMesh.HermanSkillmanMeshSize.Large)

            Assert.AreEqual(521, myMesh.Count)

        End Sub

        <TestMethod()> _
        Public Sub DX1()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum, HermanSkillmanMesh.HermanSkillmanMeshSize.Large)

            Assert.AreEqual(myMesh.DeltaR, myMesh.DR(0, 1))

        End Sub

        <TestMethod()> _
        Public Sub DX2()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum, HermanSkillmanMesh.HermanSkillmanMeshSize.Large)

            Assert.AreEqual(myMesh.DeltaR, myMesh.DR(1, 2))

        End Sub

        <TestMethod()> _
        Public Sub DX3()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum, HermanSkillmanMesh.HermanSkillmanMeshSize.Large)

            Assert.AreEqual(2.0 * myMesh.DeltaX * myMesh.CMU, myMesh.DR(0, 2))

        End Sub

        <TestMethod(), ExpectedException(GetType(IndexOutOfRangeException))> _
        Public Sub DX4()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum, HermanSkillmanMesh.HermanSkillmanMeshSize.Large)

            Dim x As Double = myMesh.DR(-1, 0)

        End Sub

        <TestMethod(), ExpectedException(GetType(IndexOutOfRangeException))> _
        Public Sub DX5()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum, HermanSkillmanMesh.HermanSkillmanMeshSize.Large)

            Dim x As Double = myMesh.DR(5, myMesh.Count)

        End Sub

        <TestMethod(), ExpectedException(GetType(IndexOutOfRangeException))> _
        Public Sub DX6()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Dim x As Double = myMesh.DR(-1, myMesh.Count)

        End Sub

        <TestMethod()> _
        Public Sub DX7()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Assert.AreEqual(0.0, myMesh.DR(1, 1))

        End Sub

        <TestMethod()> _
        Public Sub DX8()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Assert.AreEqual(myMesh.DeltaX * myMesh.CMU, myMesh.DR(1, 0))

        End Sub

        <TestMethod()> _
        Public Sub Range1()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Assert.AreEqual(myMesh.R(myMesh.Count - 1) - myMesh.R(0), myMesh.Range)

        End Sub

        <TestMethod()> _
        Public Sub Max1()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Assert.AreEqual(myMesh.R(myMesh.Count - 1), myMesh.Max)

        End Sub

        <TestMethod()> _
        Public Sub Min1()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum)

            Assert.AreEqual(myMesh.R(0), myMesh.Min)

        End Sub

        <TestMethod()> _
        Public Sub BlockCount1()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum, HermanSkillmanMesh.HermanSkillmanMeshSize.Normal)

            Assert.AreEqual(11, myMesh.BlockCount)

        End Sub

        <TestMethod()> _
        Public Sub BlockCount2()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum, HermanSkillmanMesh.HermanSkillmanMeshSize.Medium)

            Assert.AreEqual(12, myMesh.BlockCount)

        End Sub

        <TestMethod()> _
        Public Sub BlockCount3()

            Dim myMesh As New HermanSkillmanMesh(Element.Aluminum, HermanSkillmanMesh.HermanSkillmanMeshSize.Large)

            Assert.AreEqual(13, myMesh.BlockCount)

        End Sub

        <TestMethod()> _
        Public Sub HeliumMesh()

            '// This test compares the R values tabulated in Herman and Skillman for He on a 110 point mesh with those calculated by the 
            '// HermanSkillmanMesh class.
            Dim ExpectedR() As Double = {0, 0.00703, 0.01405, 0.02108, 0.02811, 0.03513, 0.04216, 0.04919, 0.05622, 0.06324, 0.07027, 0.08432, 0.09838, 0.11243, 0.12649, _
                   0.14054, 0.15459, 0.16865, 0.1827, 0.19675, 0.21081, 0.23892, 0.26702, 0.29513, 0.32324, 0.35135, 0.37946, 0.40756, 0.43567, 0.46378, _
                   0.49189, 0.5481, 0.60432, 0.66053, 0.71675, 0.77297, 0.82918, 0.8854, 0.94161, 0.99783, 1.05404, 1.16648, 1.27891, 1.39134, 1.50377, _
                   1.6162, 1.72863, 1.84106, 1.95349, 2.06593, 2.17836, 2.40322, 2.62808, 2.85295, 3.07781, 3.30267, 3.52753, 3.7524, 3.97726, 4.20212, _
                   4.42698, 4.87671, 5.32643, 5.77616, 6.22589, 6.67561, 7.12534, 7.57506, 8.02479, 8.47451, 8.92424, 9.82369, 10.7231, 11.6226, 12.522, _
                   13.4215, 14.3209, 15.2204, 16.1198, 17.0193, 17.9187, 19.7176, 21.5165, 23.3154, 25.1144, 26.9133, 28.7122, 30.5111, 32.31, 34.1089, _
                   35.9078, 39.5056, 43.1034, 46.7012, 50.299, 53.8968, 57.4946, 61.0924, 64.6902, 68.288, 71.8858, 79.0814, 86.277, 93.4726, 100.668, _
                   107.864, 115.059, 122.255, 129.451, 136.646}

            Assert.IsTrue(ExpectedR.Length = 110)

            Dim TestR As New HermanSkillmanMesh(Element.Helium, HermanSkillmanMesh.HermanSkillmanMeshSize.Normal)

            For i As Integer = 0 To 436 Step 4
                '				assert.Greater(ExpectedR(i),TestR.R(i))
                '				assert.Less(ExpectedR(i),TestR.R(i))
                Assert.AreEqual(ExpectedR(i \ 4), TestR.R(i), 0.0005)

            Next

        End Sub

    End Class

End Namespace


