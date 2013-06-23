
Imports DESD

Namespace AtomicPhysics_Tests

    <TestClass()> _
    Public Class Electronic_Configuration


        <TestMethod()> _
        Public Sub Constructor1()

            Dim c As New ElectronicConfiguration(Element.Hydrogen)

            Assert.IsTrue(c.Occupancy(1, 0) = 1)
            Assert.IsTrue(c.Occupancy = 1)
            Assert.IsTrue(c.NMax = 1)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 1)

        End Sub

        ''' <summary>
        ''' Parse a fully qualified string with one 1s1 orbital:
        ''' </summary>
        <TestMethod()> _
        Public Sub Constructor2()

            Dim c As New ElectronicConfiguration("1s1")

            Assert.IsTrue(c.Occupancy(1, 0) = 1)
            Assert.IsTrue(c.Occupancy = 1)
            Assert.IsTrue(c.NMax = 1)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 1)

        End Sub

        ''' <summary>
        ''' Parse a fully qualified string with one 1s2 orbital:
        ''' </summary>
        <TestMethod()> _
        Public Sub Constructor3()

            Dim c As New ElectronicConfiguration("1s2")

            Assert.IsTrue(c.Occupancy(1, 0) = 2)
            Assert.IsTrue(c.Occupancy = 2)
            Assert.IsTrue(c.NMax = 1)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 1)

        End Sub

        ''' <summary>
        ''' Parse a fully qualified string with one 1s3 orbital:
        ''' </summary>
        <TestMethod()> _
        Public Sub Constructor4()

            Dim c As New ElectronicConfiguration("1s3")

            Assert.IsTrue(c.Occupancy(1, 0) = 3)
            Assert.IsTrue(c.Occupancy = 3)
            Assert.IsTrue(c.NMax = 1)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 1)

        End Sub

        ''' <summary>
        ''' Parse a fully qualified string with one 1s1 orbital, omitting the occupancy:
        ''' </summary>
        <TestMethod()> _
        Public Sub Constructor5()

            Dim c As New ElectronicConfiguration("1s")

            Assert.IsTrue(c.Occupancy(1, 0) = 1)
            Assert.IsTrue(c.Occupancy = 1)
            Assert.IsTrue(c.NMax = 1)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 1)

        End Sub

        ''' <summary>
        ''' Parse a symbol string:
        ''' </summary>
        <TestMethod()> _
        Public Sub Constructor6()

            Dim c As New ElectronicConfiguration("[H]")

            Assert.IsTrue(c.Occupancy(1, 0) = 1)
            Assert.IsTrue(c.Occupancy = 1)
            Assert.IsTrue(c.NMax = 1)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 1)

        End Sub

        <TestMethod()> _
        Public Sub Constructor7()

            Dim c As New ElectronicConfiguration("1s1 2s1")

            Assert.IsTrue(c.Occupancy(1, 0) = 1)
            Assert.IsTrue(c.Occupancy(2, 0) = 1)
            Assert.IsTrue(c.Occupancy = 2)
            Assert.IsTrue(c.NMax = 2)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 2)

        End Sub

        <TestMethod()> _
        Public Sub Constructor8()

            Dim c As New ElectronicConfiguration("1s2 2s1")

            Assert.IsTrue(c.Occupancy(1, 0) = 2)
            Assert.IsTrue(c.Occupancy(2, 0) = 1)
            Assert.IsTrue(c.Occupancy = 3)
            Assert.IsTrue(c.NMax = 2)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 2)

        End Sub

        <TestMethod()> _
        Public Sub Constructor9()

            Dim c As New ElectronicConfiguration("1s2.1")

            Assert.IsTrue(c.Occupancy(1, 0) = 2.1)
            Assert.IsTrue(c.Occupancy = 2.1)
            Assert.IsTrue(c.NMax = 1)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 1)

        End Sub

        <TestMethod()> _
        Public Sub Constructor9a()

            Dim c As New ElectronicConfiguration("1s2 2s1.1")

            Assert.IsTrue(c.Occupancy(1, 0) = 2)
            Assert.IsTrue(c.Occupancy(2, 0) = 1.1)
            Assert.IsTrue(c.Occupancy = 3.1)
            Assert.IsTrue(c.NMax = 2)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 2)

        End Sub

        <TestMethod()> _
        Public Sub Constructor10()

            Dim c As New ElectronicConfiguration("1s 2s1.1")

            Assert.IsTrue(c.Occupancy(1, 0) = 1)
            Assert.IsTrue(c.Occupancy(2, 0) = 1.1)
            Assert.IsTrue(c.Occupancy = 2.1)
            Assert.IsTrue(c.NMax = 2)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 2)

        End Sub

        ''' <summary>
        ''' Test the addition of redundant orbitals in the notation.
        ''' </summary>
        <TestMethod()> _
        Public Sub Constructor11()

            Dim c As New ElectronicConfiguration("1s 1s1.1")

            Assert.IsTrue(c.Occupancy(1, 0) = 2.1)
            Assert.IsTrue(c.Occupancy = 2.1)
            Assert.IsTrue(c.NMax = 1)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 1)

        End Sub

        ''' <summary>
        ''' Test the addition of redundant orbitals in the notation.
        ''' </summary>
        <TestMethod()> _
        Public Sub Constructor12()

            Dim c As New ElectronicConfiguration("1s 1s")

            Assert.IsTrue(c.Occupancy(1, 0) = 2)
            Assert.IsTrue(c.Occupancy = 2)
            Assert.IsTrue(c.NMax = 1)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 1)

        End Sub

        ''' <summary>
        ''' Test sensitivity to leading spaces in the string.
        ''' </summary>
        <TestMethod()> _
        Public Sub Constructor13()

            Dim c As New ElectronicConfiguration(" 1s2")

            Assert.IsTrue(c.Occupancy(1, 0) = 2)
            Assert.IsTrue(c.Occupancy = 2)
            Assert.IsTrue(c.NMax = 1)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 1)

        End Sub

        ''' <summary>
        ''' Test sensitivity to trailing spaces in the string.
        ''' </summary>
        <TestMethod()> _
        Public Sub Constructor14()

            Dim c As New ElectronicConfiguration("1s2 ")

            Assert.IsTrue(c.Occupancy(1, 0) = 2)
            Assert.IsTrue(c.Occupancy = 2)
            Assert.IsTrue(c.NMax = 1)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 1)

        End Sub

        ''' <summary>
        ''' Test sensitivity to leading and trailing spaces in the string.
        ''' </summary>
        <TestMethod()> _
        Public Sub Constructor15()

            Dim c As New ElectronicConfiguration(" 1s2 ")

            Assert.IsTrue(c.Occupancy(1, 0) = 2)
            Assert.IsTrue(c.Occupancy = 2)
            Assert.IsTrue(c.NMax = 1)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 1)

        End Sub


        ''' <summary>
        ''' Test construction with symbol.
        ''' </summary>
        <TestMethod()> _
        Public Sub Constructor16()

            Dim c As New ElectronicConfiguration("[He]")

            Assert.IsTrue(c.Occupancy(1, 0) = 2)
            Assert.IsTrue(c.Occupancy = 2)
            Assert.IsTrue(c.NMax = 1)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 1)

        End Sub

        ''' <summary>
        ''' Test construction with mixed base symbol and orbital.
        ''' </summary>
        <TestMethod()> _
        Public Sub Constructor17()

            Dim c As New ElectronicConfiguration("[He] 2s1")

            Assert.IsTrue(c.Occupancy(1, 0) = 2)
            Assert.IsTrue(c.Occupancy(2, 0) = 1)
            Assert.IsTrue(c.Occupancy = 3)
            Assert.IsTrue(c.NMax = 2)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 2)

        End Sub

        ''' <summary>
        ''' Test the empty configuration:
        ''' </summary>
        <TestMethod()> _
        Public Sub Constructor18()

            Dim c As New ElectronicConfiguration("")

            Assert.IsTrue(c.Occupancy(1, 0) = 0)
            Assert.IsTrue(c.Occupancy(2, 0) = 0)
            Assert.IsTrue(c.Occupancy = 0)
            Assert.IsTrue(c.NMax = 0)
            Assert.IsTrue(c.LMax = 0)
            Assert.IsTrue(c.OrbitalCount = 0)

        End Sub

        <TestMethod()> _
        Public Sub ToString1()

            Dim c As New ElectronicConfiguration("1s1")

            Assert.IsTrue(c.ToString = "1s1")
            Assert.IsTrue(c.LMax = 0)

        End Sub

        <TestMethod()> _
        Public Sub ToString2()

            Dim c As New ElectronicConfiguration("1s1 2s1")

            Assert.IsTrue(c.ToString = "1s1 2s1")
            Assert.IsTrue(c.LMax = 0)

        End Sub


        <TestMethod()> _
        Public Sub ToString3()

            Dim c As New ElectronicConfiguration("1s2 2s2 2p6 3d1")
            Dim s As String = c.ToString
            Assert.IsTrue(c.ToString = "1s2 2s2 2p6 3d1")
            Assert.IsTrue(c.LMax = 2)

        End Sub

        <TestMethod()> _
        Public Sub GetQuantumNumbers1()

            Dim c As New ElectronicConfiguration("1s2 2s2 2p6 3d1")

            Dim QNs(,) As Integer = c.GetQuantumNumbers

            Assert.AreEqual(8, QNs.Length)
        End Sub


        <TestMethod()> _
        Public Sub GetQuantumNumbers2()

            Dim c As New ElectronicConfiguration(Element.Beryllium)

            Dim QNs(,) As Integer = c.GetQuantumNumbers

            Assert.AreEqual(4, QNs.Length)
            Assert.AreEqual(1, QNs(0, 0))
            Assert.AreEqual(0, QNs(0, 1))
            Assert.AreEqual(2, QNs(1, 0))
            Assert.AreEqual(0, QNs(1, 1))

        End Sub

        <TestMethod()> _
        Public Sub GetQuantumNumbers3()

            Dim c As New ElectronicConfiguration(Element.Actinium)

            Dim QNs(,) As Integer = c.GetQuantumNumbers

            Assert.AreEqual(c.OrbitalCount * 2, QNs.Length)

        End Sub


    End Class

End Namespace
