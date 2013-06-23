Imports DESD
Imports DESD.Math


'
' Created by SharpDevelop.
' User: Sieger Family
' Date: 6/16/2010
' Time: 8:38 PM
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'


''' <summary>
''' Test values were taken from Pendry's book "Low Energy Electron Diffraction",
''' zero-temperature phase shifts for Ni, page 372.
''' Note that energy in Pendry's book is 4 Hartrees, while my input is in Rydbergs.
''' </summary>
<TestClass> _
Public Class PhaseShift_UnitTest

    <TestMethod> _
    Public Sub Nickel01()

        '// The Rmt is not documented in Pendry's book, I'm not sure where I got this.
        '// From metallic Ni?
        Dim Rmt As Double = 1.99

        Dim mt As New NeutralMuffinTin(Element.Nickel, Rmt)

        Dim ps As PhaseShift

        '// Pendry's energy is 4 Hartrees, = 8 Rydbergs.
        ps = New PhaseShift(8.0, 10, mt)

        Dim delta As Double = 0.05

        '// Pendry's values are given to 2 decimal places.
        Assert.AreEqual(1.01, ps.RealValue(0), delta)
        Assert.AreEqual(-1.09, ps.RealValue(1), delta)
        Assert.AreEqual(-0.58, ps.RealValue(2), delta)
        Assert.AreEqual(0.42, ps.RealValue(3), delta)
        Assert.AreEqual(0.11, ps.RealValue(4), delta)
        Assert.AreEqual(0.03, ps.RealValue(5), delta)
        Assert.AreEqual(0.01, ps.RealValue(6), delta)
        Assert.AreEqual(0.0, ps.RealValue(7), delta)
        Assert.AreEqual(0.0, ps.RealValue(8), delta)
        Assert.AreEqual(0.0, ps.RealValue(9), delta)


    End Sub


    ''' <summary>
    ''' Test values taken from Pendry's book, page 372.
    ''' </summary>
    <TestMethod()> _
    Public Sub Nickel_TemperatureDependent01()
        Dim Rmt As Double = 1.99

        Dim mt As New NeutralMuffinTin(Element.Nickel, Rmt)

        Dim ps As PhaseShift

        '            Console.WriteLine("Nickel scatterer, Rmt = " & Rmt.tostring & " Bohr Radii")
        '            Dim Imt As integer = mt.Imt
        '            console.WriteLine("Imt = " & Imt.ToString)
        '            Dim ip As Double = mt.InnerPotential
        '            console.WriteLine("Inner potential = " & Ip.ToString)
        '            Console.WriteLine()
        '            
        '            
        '           	console.WriteLine("Phase shifts:")
        '           	
        '            console.WriteLine("Energy = 8.0 Ry")
        '           	console.WriteLine("L     delta_L (Rad)")
        ps = New PhaseShift(8.0, 10, mt)
        '           	For l As Integer = 0 To 10
        '           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
        '           	Next
        '           	console.WriteLine()


        '// Now compute temperature dependence:

        Dim cps As complex
        '            console.WriteLine("Temperature Dependent at 300K")
        '           	console.WriteLine("L     delta_L(Re)    delta_L(Im)")
        '           	For l As Integer = 0 To 10
        '           		cps = ps.ComplexValue(l,0.0,300.0,440.0)
        '           		console.WriteLine(l.ToString & "     " & cps.real.ToString & "    " & cps.Imag.tostring)
        '           	Next
        '           	console.WriteLine()
        '           	
        Dim delta As Double = 0.05

        cps = ps.ComplexValue(0, 0.0, 300.0, 440.0)
        Assert.AreEqual(1.0602, cps.Real, delta)
        Assert.AreEqual(0.080404, cps.Imag, delta)

        cps = ps.ComplexValue(1, 0.0, 300.0, 440.0)
        Assert.AreEqual(-1.075, cps.Real, delta)
        Assert.AreEqual(0.043623, cps.Imag, delta)

        cps = ps.ComplexValue(2, 0.0, 300.0, 440.0)
        Assert.AreEqual(-0.56782, cps.Real, delta)
        Assert.AreEqual(0.055128, cps.Imag, delta)

        cps = ps.ComplexValue(3, 0.0, 300.0, 440.0)
        Assert.AreEqual(0.38134, cps.Real, delta)
        Assert.AreEqual(0.035907, cps.Imag, delta)

        cps = ps.ComplexValue(4, 0.0, 300.0, 440.0)
        Assert.AreEqual(0.11771, cps.Real, delta)
        Assert.AreEqual(0.0047956, cps.Imag, delta)

        cps = ps.ComplexValue(5, 0.0, 300.0, 440.0)
        Assert.AreEqual(0.032799, cps.Real, delta)
        Assert.AreEqual(0.00046002, cps.Imag, delta)

        cps = ps.ComplexValue(6, 0.0, 300.0, 440.0)
        Assert.AreEqual(0.010478, cps.Real, delta)
        Assert.AreEqual(0.000037301, cps.Imag, delta)


    End Sub



    <TestMethod()> _
    Public Sub Aluminum01()

        Dim Rmt As Double = 2.6

        Dim mt As New NeutralMuffinTin(Element.Aluminum, Rmt)

        Dim ps As PhaseShift


        Dim delta As Double = 0.07

        '// Pendry's values are given to 2 decimal places.
        ps = New PhaseShift(2.0, 3, mt)
        Assert.AreEqual(-0.53, ps.RealValue(0), delta)
        Assert.AreEqual(0.21, ps.RealValue(1), delta)
        Assert.AreEqual(0.38, ps.RealValue(2), delta)
        Assert.AreEqual(0.02, ps.RealValue(3), delta)

        '        Console.WriteLine(ps.RealValue(0).tostring)
        '        Console.WriteLine(ps.RealValue(1).tostring)
        '        Console.WriteLine(ps.RealValue(2).tostring)
        '        Console.WriteLine(ps.RealValue(3).tostring)

        ps = New PhaseShift(4.0, 3, mt)
        Assert.AreEqual(-1.02, ps.RealValue(0), delta)
        Assert.AreEqual(0.0, ps.RealValue(1), delta)
        Assert.AreEqual(0.8, ps.RealValue(2), delta)
        Assert.AreEqual(0.19, ps.RealValue(3), delta)

        ps = New PhaseShift(6.0, 3, mt)
        Assert.AreEqual(-1.32, ps.RealValue(0), delta)
        Assert.AreEqual(-0.19, ps.RealValue(1), delta)
        Assert.AreEqual(0.98, ps.RealValue(2), delta)
        Assert.AreEqual(0.33, ps.RealValue(3), delta)

        ps = New PhaseShift(8.0, 3, mt)
        Assert.AreEqual(-1.53, ps.RealValue(0), delta)
        Assert.AreEqual(-0.3, ps.RealValue(1), delta)
        Assert.AreEqual(1.05, ps.RealValue(2), delta)
        Assert.AreEqual(0.4, ps.RealValue(3), delta)

        ps = New PhaseShift(10.0, 3, mt)
        Assert.AreEqual(1.46, ps.RealValue(0), delta)
        Assert.AreEqual(-0.4, ps.RealValue(1), delta)
        Assert.AreEqual(1.1, ps.RealValue(2), delta)
        Assert.AreEqual(0.47, ps.RealValue(3), delta)

        ps = New PhaseShift(12.0, 3, mt)
        Assert.AreEqual(-0.51, ps.RealValue(1), delta)


    End Sub

    ''' <summary>
    ''' Test values taken from Pendry page 56.
    ''' We do pretty well until we get to higher energy, then it starts to diverge.
    ''' </summary>
    <TestMethod()> _
    Public Sub Aluminum02()

        Dim Rmt As Double = 2.69

        Dim mt As New NeutralMuffinTin(Element.Aluminum, Rmt)

        Dim ps As PhaseShift


        Dim delta As Double = 0.09

        '// Pendry's values are given to 2 decimal places.
        ps = New PhaseShift(1.0, 2, mt)
        Assert.AreEqual(-0.21, ps.RealValue(0), delta)
        Assert.AreEqual(0.18, ps.RealValue(1), delta)
        Assert.AreEqual(0.09, ps.RealValue(2), delta)

        '        Console.WriteLine(ps.RealValue(0).tostring)
        '        Console.WriteLine(ps.RealValue(1).tostring)
        '        Console.WriteLine(ps.RealValue(2).tostring)
        '        Console.WriteLine(ps.RealValue(3).tostring)

        ps = New PhaseShift(1.5, 3, mt)
        Assert.AreEqual(-0.4, ps.RealValue(0), delta)
        Assert.AreEqual(0.21, ps.RealValue(1), delta)
        Assert.AreEqual(0.22, ps.RealValue(2), delta)
        Assert.AreEqual(0.02, ps.RealValue(3), delta)

        ps = New PhaseShift(2.0, 3, mt)
        Assert.AreEqual(-0.55, ps.RealValue(0), delta)
        Assert.AreEqual(0.18, ps.RealValue(1), delta)
        Assert.AreEqual(0.36, ps.RealValue(2), delta)
        Assert.AreEqual(0.04, ps.RealValue(3), delta)

        ps = New PhaseShift(3.0, 4, mt)
        Assert.AreEqual(-0.8, ps.RealValue(0), delta)
        Assert.AreEqual(0.09, ps.RealValue(1), delta)
        Assert.AreEqual(0.63, ps.RealValue(2), delta)
        Assert.AreEqual(0.1, ps.RealValue(3), delta)
        Assert.AreEqual(0.02, ps.RealValue(4), delta)

        ps = New PhaseShift(4.0, 4, mt)
        Assert.AreEqual(-1.0, ps.RealValue(0), delta)
        Assert.AreEqual(-0.01, ps.RealValue(1), delta)
        Assert.AreEqual(0.8, ps.RealValue(2), delta)
        Assert.AreEqual(0.17, ps.RealValue(3), delta)
        Assert.AreEqual(0.03, ps.RealValue(4), delta)

        ps = New PhaseShift(5.0, 5, mt)
        Assert.AreEqual(-1.17, ps.RealValue(0), delta)
        Assert.AreEqual(-0.11, ps.RealValue(1), delta)
        Assert.AreEqual(0.9, ps.RealValue(2), delta)
        Assert.AreEqual(0.25, ps.RealValue(3), delta)
        Assert.AreEqual(0.07, ps.RealValue(4), delta)
        Assert.AreEqual(0.01, ps.RealValue(5), delta)

        ps = New PhaseShift(6.0, 5, mt)
        Assert.AreEqual(-1.31, ps.RealValue(0), delta)
        Assert.AreEqual(-0.19, ps.RealValue(1), delta)
        Assert.AreEqual(0.97, ps.RealValue(2), delta)
        Assert.AreEqual(0.31, ps.RealValue(3), delta)
        Assert.AreEqual(0.1, ps.RealValue(4), delta)
        Assert.AreEqual(0.02, ps.RealValue(5), delta)

        ps = New PhaseShift(8.0, 6, mt)
        Assert.AreEqual(-1.52, ps.RealValue(0), delta)
        Assert.AreEqual(-0.32, ps.RealValue(1), delta)
        Assert.AreEqual(1.06, ps.RealValue(2), delta)
        Assert.AreEqual(0.4, ps.RealValue(3), delta)
        Assert.AreEqual(0.15, ps.RealValue(4), delta)
        Assert.AreEqual(0.06, ps.RealValue(5), delta)
        Assert.AreEqual(0.01, ps.RealValue(6), delta)

        ps = New PhaseShift(10.0, 6, mt)
        Assert.AreEqual(1.44, ps.RealValue(0), delta)
        Assert.AreEqual(-0.42, ps.RealValue(1), delta)
        Assert.AreEqual(1.12, ps.RealValue(2), delta)
        Assert.AreEqual(0.46, ps.RealValue(3), delta)
        Assert.AreEqual(0.22, ps.RealValue(4), delta)
        Assert.AreEqual(0.09, ps.RealValue(5), delta)
        Assert.AreEqual(0.04, ps.RealValue(6), delta)

        ps = New PhaseShift(12.0, 6, mt)
        Assert.AreEqual(1.3, ps.RealValue(0), delta)
        Assert.AreEqual(-0.51, ps.RealValue(1), delta)
        Assert.AreEqual(1.14, ps.RealValue(2), delta)
        Assert.AreEqual(0.51, ps.RealValue(3), delta)
        Assert.AreEqual(0.26, ps.RealValue(4), delta)
        Assert.AreEqual(0.13, ps.RealValue(5), delta)
        Assert.AreEqual(0.06, ps.RealValue(6), delta)

        ps = New PhaseShift(14.0, 6, mt)
        Assert.AreEqual(1.16, ps.RealValue(0), delta)
        Assert.AreEqual(-0.58, ps.RealValue(1), delta)
        Assert.AreEqual(1.16, ps.RealValue(2), delta)
        Assert.AreEqual(0.55, ps.RealValue(3), delta)
        Assert.AreEqual(0.29, ps.RealValue(4), delta)
        Assert.AreEqual(0.16, ps.RealValue(5), delta)
        Assert.AreEqual(0.08, ps.RealValue(6), delta)

        ps = New PhaseShift(16.0, 6, mt)
        Assert.AreEqual(1.02, ps.RealValue(0), delta)
        Assert.AreEqual(-0.65, ps.RealValue(1), delta)
        Assert.AreEqual(1.18, ps.RealValue(2), delta)
        Assert.AreEqual(0.58, ps.RealValue(3), delta)
        Assert.AreEqual(0.32, ps.RealValue(4), delta)
        Assert.AreEqual(0.17, ps.RealValue(5), delta)
        Assert.AreEqual(0.09, ps.RealValue(6), delta)


    End Sub



    ''' <summary>
    ''' Test values taken from Pendry page 56.
    ''' We do pretty well until we get to higher energy, then it starts to diverge.
    ''' </summary>
    <TestMethod()> _
    Public Sub Nickel02()

        Dim Rmt As Double = 2.0

        Dim mt As New NeutralMuffinTin(Element.Nickel, Rmt)

        Dim ps As PhaseShift


        Dim delta As Double = 0.1

        '// Pendry's values are given to 2 decimal places.
        ps = New PhaseShift(3.0, 3, mt)
        '        Console.WriteLine(ps.RealValue(0).tostring)
        '        Console.WriteLine(ps.RealValue(1).tostring)
        '        Console.WriteLine(ps.RealValue(2).tostring)
        '        

        Dim r As Double() = ps.Mesh.GetArray
        Dim w As Double() = ps.Wavefunction(2)
        Dim p As Double() = ps.Potential

        Console.WriteLine("Wave function for l = 2")
        For i As Integer = 0 To r.Length - 1
            'Console.WriteLine(r(i).ToString & ", " & p(i).ToString & ", " & w(i).ToString)
            Console.WriteLine(r(i).ToString & ", " & w(i).ToString)
            'Console.WriteLine(w(i).ToString)
        Next

        Assert.AreEqual(-0.7, ps.RealValue(0), delta)
        Assert.AreEqual(-0.12, ps.RealValue(1), delta)
        Assert.AreEqual(-0.36, ps.RealValue(2), delta)
        '        ps = new PhaseShift(1.5, 3, mt)
        '        Assert.AreEqual(-0.40, ps.RealValue(0), delta)
        '        Assert.AreEqual(0.21, ps.RealValue(1), delta)
        '        Assert.AreEqual(0.22, ps.RealValue(2), delta)
        '        Assert.AreEqual(0.02, ps.RealValue(3), delta)
        '        
        '        ps = new PhaseShift(2.0, 3, mt)
        '        Assert.AreEqual(-0.55, ps.RealValue(0), delta)
        '        Assert.AreEqual(0.18, ps.RealValue(1), delta)
        '        Assert.AreEqual(0.36, ps.RealValue(2), delta)
        '        Assert.AreEqual(0.04, ps.RealValue(3), delta)
        '        
        '        ps = new PhaseShift(3.0, 4, mt)
        '        Assert.AreEqual(-0.8, ps.RealValue(0), delta)
        '        Assert.AreEqual(0.09, ps.RealValue(1), delta)
        '        Assert.AreEqual(0.63, ps.RealValue(2), delta)
        '        Assert.AreEqual(0.10, ps.RealValue(3), delta)
        '        Assert.AreEqual(0.02, ps.RealValue(4), delta)
        '
        ps = New PhaseShift(4.0, 4, mt)
        Assert.AreEqual(1.55, System.Math.Abs(ps.RealValue(0)), delta)
        Assert.AreEqual(-0.66, ps.RealValue(1), delta)
        Assert.AreEqual(-0.53, ps.RealValue(2), delta)
        Assert.AreEqual(0.15, ps.RealValue(3), delta)
        Assert.AreEqual(0.03, ps.RealValue(4), delta)
        '
        '        ps = new PhaseShift(5.0, 5, mt)
        '        Assert.AreEqual(-1.17, ps.RealValue(0), delta)
        '        Assert.AreEqual(-0.11, ps.RealValue(1), delta)
        '        Assert.AreEqual(0.90, ps.RealValue(2), delta)
        '        Assert.AreEqual(0.25, ps.RealValue(3), delta)
        '        Assert.AreEqual(0.07, ps.RealValue(4), delta)
        '        Assert.AreEqual(0.01, ps.RealValue(5), delta)
        '
        ps = New PhaseShift(6.0, 5, mt)
        Assert.AreEqual(1.27, ps.RealValue(0), delta)
        Assert.AreEqual(-0.9, ps.RealValue(1), delta)
        Assert.AreEqual(-0.56, ps.RealValue(2), delta)
        Assert.AreEqual(0.28, ps.RealValue(3), delta)
        Assert.AreEqual(0.07, ps.RealValue(4), delta)
        Assert.AreEqual(0.02, ps.RealValue(5), delta)
        '
        ps = New PhaseShift(8.0, 6, mt)

        '        Console.WriteLine(ps.RealValue(0).tostring)
        '        Console.WriteLine(ps.RealValue(1).tostring)
        '        Console.WriteLine(ps.RealValue(2).tostring)
        '        Console.WriteLine(ps.RealValue(3).tostring)
        '        Console.WriteLine(ps.RealValue(4).tostring)
        '        Console.WriteLine(ps.RealValue(5).tostring)
        '        Console.WriteLine(ps.RealValue(6).tostring)

        Assert.AreEqual(1.01, ps.RealValue(0), delta)
        Assert.AreEqual(-1.09, ps.RealValue(1), delta)
        Assert.AreEqual(-0.58, ps.RealValue(2), delta)
        Assert.AreEqual(0.42, ps.RealValue(3), delta)
        Assert.AreEqual(0.11, ps.RealValue(4), delta)
        Assert.AreEqual(0.03, ps.RealValue(5), delta)
        Assert.AreEqual(0.01, ps.RealValue(6), delta)
        '
        '        ps = new PhaseShift(10.0, 6, mt)
        '        Assert.AreEqual(1.44, ps.RealValue(0), delta)
        '        Assert.AreEqual(-0.42, ps.RealValue(1), delta)
        '        Assert.AreEqual(1.12, ps.RealValue(2), delta)
        '        Assert.AreEqual(0.46, ps.RealValue(3), delta)
        '        Assert.AreEqual(0.22, ps.RealValue(4), delta)
        '        Assert.AreEqual(0.09, ps.RealValue(5), delta)
        '        Assert.AreEqual(0.04, ps.RealValue(6), delta)
        '
        '        ps = new PhaseShift(12.0, 6, mt)
        '        Assert.AreEqual(1.30, ps.RealValue(0), delta)
        '        Assert.AreEqual(-0.51, ps.RealValue(1), delta)
        '        Assert.AreEqual(1.14, ps.RealValue(2), delta)
        '        Assert.AreEqual(0.51, ps.RealValue(3), delta)
        '        Assert.AreEqual(0.26, ps.RealValue(4), delta)
        '        Assert.AreEqual(0.13, ps.RealValue(5), delta)
        '        Assert.AreEqual(0.06, ps.RealValue(6), delta)
        '
        '        ps = new PhaseShift(14.0, 6, mt)
        '        Assert.AreEqual(1.16, ps.RealValue(0), delta)
        '        Assert.AreEqual(-0.58, ps.RealValue(1), delta)
        '        Assert.AreEqual(1.16, ps.RealValue(2), delta)
        '        Assert.AreEqual(0.55, ps.RealValue(3), delta)
        '        Assert.AreEqual(0.29, ps.RealValue(4), delta)
        '        Assert.AreEqual(0.16, ps.RealValue(5), delta)
        '        Assert.AreEqual(0.08, ps.RealValue(6), delta)
        '
        '        ps = new PhaseShift(16.0, 6, mt)
        '        Assert.AreEqual(1.02, ps.RealValue(0), delta)
        '        Assert.AreEqual(-0.65, ps.RealValue(1), delta)
        '        Assert.AreEqual(1.18, ps.RealValue(2), delta)
        '        Assert.AreEqual(0.58, ps.RealValue(3), delta)
        '        Assert.AreEqual(0.32, ps.RealValue(4), delta)
        '        Assert.AreEqual(0.17, ps.RealValue(5), delta)
        '        Assert.AreEqual(0.09, ps.RealValue(6), delta)


    End Sub

    ''' <summary>
    ''' Test values taken from Pendry page 56.
    ''' We do pretty well until we get to higher energy, then it starts to diverge.
    ''' </summary>
    <TestMethod()> _
    Public Sub NickelForWaveFunction()

        Dim Rmt As Double = 2.0

        '		Dim cubicmeshsize As integer = CInt(InputBox("Cubic mesh size:"))
        '
        '        Dim mt As New NeutralMuffinTin(Element.Nickel, Rmt, cubicmeshsize)
        Dim mt As New NeutralMuffinTin(Element.Nickel, Rmt)

        Dim ps As PhaseShift = New PhaseShift(2.0, 3, mt)


        Dim r As Double() = ps.Mesh.GetArray
        Dim w As Double() = ps.Wavefunction(2)

        Console.WriteLine("Wave function for l = 2")
        For i As Integer = 0 To r.Length - 1
            Console.WriteLine(r(i).ToString & ", " & w(i).ToString)
        Next


    End Sub


    <TestMethod()> _
    Public Sub PendryBFunctionTest1()

        Dim result As Double = PhaseShift.PendryBFunction(1, 2, 3)

        Console.WriteLine(result.ToString)

    End Sub

    <TestMethod()> _
    Public Sub PendryBFunctionTest2()

        Dim result1 As Double = PhaseShift.PendryBFunction(1, 2, 3)
        Dim result2 As Double = PhaseShift.PendryBFunction(3, 1, 2)
        Dim result3 As Double = PhaseShift.PendryBFunction(2, 3, 1)

        Assert.AreEqual(result1, result2, 0.000000000000001)
        Assert.AreEqual(result1, result3, 0.000000000000001)

    End Sub

    <TestMethod()> _
    Public Sub PendryBFunctionTest3()

        Dim result1 As Double = PhaseShift.PendryBFunction(1, 2, 3)
        Dim result2 As Double = PhaseShift.PendryBFunction(1, 3, 2)
        Dim result3 As Double = PhaseShift.PendryBFunction(2, 1, 3)
        Dim result4 As Double = PhaseShift.PendryBFunction(2, 3, 1)
        Dim result5 As Double = PhaseShift.PendryBFunction(3, 1, 2)
        Dim result6 As Double = PhaseShift.PendryBFunction(3, 2, 1)

        Console.WriteLine(result1.ToString)
        Console.WriteLine(result2.ToString)
        Console.WriteLine(result3.ToString)
        Console.WriteLine(result4.ToString)
        Console.WriteLine(result5.ToString)
        Console.WriteLine(result6.ToString)

    End Sub

    <TestMethod()> _
    Public Sub PendryBFunctionTest4()

        Dim a As Integer = 5
        Dim result1 As Double = PhaseShift.PendryBFunction(a, a, 0)
        Dim result2 As Double = PhaseShift.PendryBFunction(a, a, 1)
        Dim result3 As Double = PhaseShift.PendryBFunction(a, a, 2)
        Dim result4 As Double = PhaseShift.PendryBFunction(a, a, 3)
        Dim result5 As Double = PhaseShift.PendryBFunction(a, a, 4)
        Dim result6 As Double = PhaseShift.PendryBFunction(a, a, 5)

        Console.WriteLine(result1.ToString)
        Console.WriteLine(result2.ToString)
        Console.WriteLine(result3.ToString)
        Console.WriteLine(result4.ToString)
        Console.WriteLine(result5.ToString)
        Console.WriteLine(result6.ToString)

    End Sub

    <TestMethod()> _
    Public Sub PendryBFunctionTest5()

        For i As Integer = 0 To 10
            Console.WriteLine(PhaseShift.PendryBFunction(i, i, i).ToString)
        Next


    End Sub

    <TestMethod()> _
    Public Sub PendryBFunctionTest6()
        '// Test the condition that unless l1 + l2 >= l3 >= l1 - l2 then it is zero.
        Dim zerotol As Double = 0.0001
        Dim value As Double
        Dim lmax As Integer = 5
        For l1 As Integer = 0 To lmax
            For l2 As Integer = 0 To lmax
                For l3 As Integer = 0 To lmax
                    value = PhaseShift.PendryBFunction(l1, l2, l3)

                    If ((l1 + l2) >= l3) And ((l1 - l2) <= l3) Then
                        '// Should be non-zero:
                        Assert.IsTrue(System.Math.Abs(value) > 0.0)
                    Else
                        '// should be zero
                        Assert.IsTrue(System.Math.Abs(value) <= zerotol)
                    End If

                Next
            Next
        Next


    End Sub

    <TestMethod()> _
    Public Sub Normalization01()

        Dim Rmt As Double = 1.99
        Dim mt As New NeutralMuffinTin(Element.Nickel, Rmt)
        Dim Lmax As Integer = 10
        Dim ps As PhaseShift = New PhaseShift(8.0, Lmax, mt)

        '// Write out the normalizations and total charges:
        For ll As Integer = 0 To Lmax
            Console.WriteLine(ll.ToString & "     " & ps.Normalization(ll).ToString & "     " & ps.TotalCharge(ll).ToString)
        Next
        Console.WriteLine()

        '// Write out the l = 0 wave function
        Dim mesh As IRadialMesh = ps.Mesh
        Dim L As Integer = 4
        Dim wf As Double() = ps.Wavefunction(L)
        Dim kR As Double = 5.687644
        Dim sphb As Double()
        Dim delta As Double = ps.RealValue(L)
        Dim e2idl As Complex = Complex.CExp(0.0, 2.0 * delta)

        Dim hn As Complex

        For i As Integer = 0 To mesh.Count - 1
            sphb = Functions.SphericalBessel(L, kR)
            hn = (e2idl + 1.0) * sphb(0) + complex.i * (e2idl - 1.0) * sphb(1)
            Console.WriteLine(mesh.R(i).ToString & "     " & wf(i).ToString) ' & "     " & hn.tostring & "     " & hn.magnitude)
            'Console.WriteLine(mesh.R(i).ToString & "     " & wf(i).ToString & "     " & hn.tostring & "     " & hn.magnitude)
        Next


    End Sub


    <TestMethod()> _
    Public Sub InnerPotentialTest()

        Dim RmtMin As Double = 0.5
        Dim RmtMax As Double = 2.0
        Dim NRmt As Integer = 1000

        Dim mt As NeutralMuffinTin
        Dim ps As PhaseShift
        Dim EnergyInRydbergs As Double = 30.0 / 13.6056923

        Dim VopticalInRydbergs As Double = 0.0

        Dim MuffinTinRadiusInBohrRadii As Double
        Dim Rmt As Double
        Dim Vinner As Double

        Console.WriteLine("Rmt, Vinner, Delta(0), Delta(1)")
        For i As Integer = 0 To NRmt
            Rmt = RmtMin + CDbl(i) * (RmtMax - RmtMin) / CDbl(NRmt)
            MuffinTinRadiusInBohrRadii = Rmt / 0.52917720859
            mt = New NeutralMuffinTin(14, MuffinTinRadiusInBohrRadii)
            Vinner = mt.InnerPotential * 13.6056923

            '// Calculate phase shifts for this muffin-tin:
            'ps = New PhaseShift(EnergyInRydbergs, 5, mt)

            Console.WriteLine(Rmt.ToString & ", " & Vinner.ToString & ", " & mt.Imt.ToString) '& ", " & ps.RealValue(0).ToString & ", " & ps.RealValue(1).ToString)
        Next

    End Sub

    '<<<<<<< .mine
    <TestMethod()> _
    Public Sub Silicon1()
        Dim Rmt As Double = Double.MaxValue
        Dim mt As New NeutralMuffinTin(Element.Silicon, Rmt)

        Dim ps As PhaseShift

        Dim ip As Double = mt.InnerPotential
        Console.WriteLine("Inner potential = " & ip.ToString)

        Console.WriteLine("Phase shifts:")

        Dim e As Double = CDbl(InputBox("Energy (eV)"))
        Dim EnergyInRydbergs As Double = e / 13.6056923

        Console.WriteLine("Energy = " & e.ToString)
        Console.WriteLine("L     delta_L (Rad)")
        ps = New PhaseShift(EnergyInRydbergs, 10, mt)
        For l As Integer = 0 To 10
            Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
        Next
        Console.WriteLine()
    End Sub


    '=======
    <TestMethod()> _
    Public Sub SiliconAtom()
        Dim Rmt As Double = Double.MaxValue
        Dim mt As New NeutralMuffinTin(Element.Silicon, Rmt)

        Dim ps As PhaseShift

        Console.WriteLine("Si scatterer, Rmt = " & Rmt.ToString & " Bohr Radii")
        Dim Imt As Integer = mt.Imt
        Console.WriteLine("Imt = " & Imt.ToString)
        Dim ip As Double = mt.InnerPotential
        Console.WriteLine("Inner potential = " & ip.ToString)
        Console.WriteLine()


        Console.WriteLine("Phase shifts:")

        Console.WriteLine("Energy = 1.0 Ry")
        Console.WriteLine("L     delta_L (Rad)")
        ps = New PhaseShift(1.0, 10, mt)
        For l As Integer = 0 To 9
            Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
        Next
        Console.WriteLine()

        Console.WriteLine("Energy = 2.0 Ry")
        Console.WriteLine("L     delta_L (Rad)")
        ps = New PhaseShift(2.0, 10, mt)
        For l As Integer = 0 To 9
            Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
        Next
        Console.WriteLine()

        Console.WriteLine("Energy = 3.0 Ry")
        Console.WriteLine("L     delta_L (Rad)")
        ps = New PhaseShift(3.0, 10, mt)
        For l As Integer = 0 To 9
            Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
        Next
        Console.WriteLine()

        Console.WriteLine("Energy = 4.0 Ry")
        Console.WriteLine("L     delta_L (Rad)")
        ps = New PhaseShift(4.0, 10, mt)
        For l As Integer = 0 To 9
            Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
        Next
        Console.WriteLine()

        Console.WriteLine("Energy = 5.0 Ry")
        Console.WriteLine("L     delta_L (Rad)")
        ps = New PhaseShift(5.0, 10, mt)
        For l As Integer = 0 To 9
            Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
        Next
        Console.WriteLine()

        Console.WriteLine("Energy = 6.0 Ry")
        Console.WriteLine("L     delta_L (Rad)")
        ps = New PhaseShift(6.0, 10, mt)
        For l As Integer = 0 To 9
            Console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
        Next
        Console.WriteLine()

    End Sub

    <TestMethod()> _
    Public Sub SiliconForFeff()

        Dim mt As New NeutralMuffinTin(Element.Silicon, 1.027)

        Dim ps As PhaseShift

        Dim ip As Double = mt.InnerPotential
        Console.WriteLine("Inner potential = " & ip.ToString)

        Console.WriteLine("Phase shifts:")

        Dim e As Double = CDbl(InputBox("Energy (eV)"))
        Dim EnergyInRydbergs As Double = e / 13.6056923

        Dim vopt As Double = CDbl(InputBox("Optical Potential (Ry)"))

        Console.WriteLine("Energy = " & e.ToString)
        Console.WriteLine("L     delta_L (Rad)")
        ps = New PhaseShift(EnergyInRydbergs, 5, mt)
        Dim cps As Complex
        For l As Integer = 0 To 5
            cps = ps.ComplexValue(l, vopt, 300.0, 645.0)
            Console.WriteLine(l.ToString & "    " & cps.tostring)
        Next
        Console.WriteLine(ps.ComplexValue(0, vopt, 300.0, 645.0).Real.ToString & "  " & ps.ComplexValue(0, vopt, 300.0, 645.0).Imag.ToString & "  " & _
            ps.ComplexValue(1, vopt, 300.0, 645.0).Real.ToString & "  " & ps.ComplexValue(1, vopt, 300.0, 645.0).Imag.ToString & "  " & _
            ps.ComplexValue(2, vopt, 300.0, 645.0).Real.ToString & "  " & ps.ComplexValue(2, vopt, 300.0, 645.0).Imag.ToString & "  " & _
            ps.ComplexValue(3, vopt, 300.0, 645.0).Real.ToString & "  " & ps.ComplexValue(3, vopt, 300.0, 645.0).Imag.ToString & "  " & _
            ps.ComplexValue(4, vopt, 300.0, 645.0).Real.ToString & "  " & ps.ComplexValue(4, vopt, 300.0, 645.0).Imag.ToString & "  " & _
            ps.ComplexValue(5, vopt, 300.0, 645.0).Real.ToString & "  " & ps.ComplexValue(5, vopt, 300.0, 645.0).Imag.ToString)
        Console.WriteLine()
    End Sub

End Class
