Imports DESD
Imports DESD.Math

'
' Created by SharpDevelop.
' User: Matt
' Date: 1/15/2012
' Time: 5:48 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

<TestClass> _
Public Class FEFFPhaseShiftFile_UnitTest

    '	<TestMethod()> _
    '	Public Sub Test01()
    '		
    '		Dim expectValsRe As Double() = {-6.927530E-01, 3.993719E-01, 1.343644E+00, 4.045049E-01, 7.214461E-02, 9.873045E-03, 1.042793E-03, 8.481022E-05, 5.398999E-06,2.744265E-07}  
    '   
    '   		Dim expectValsIm As Double() = {-6.927530E-01 -8.735734E-02  3.993719E-01 -7.292201E-02
    '   1.343644E+00 -3.784874E-02  4.045049E-01  5.224455E-02
    '   7.214461E-02  1.821587E-02  9.873045E-03  3.374244E-03
    '   1.042793E-03  4.518152E-04  8.481022E-05  4.503273E-05
    '   5.398999E-06  3.432972E-06  2.744265E-07  2.059731E-07
    '   
    '   
    '   
    '		Dim testobj As New FEFFPhaseShiftFile("C:\Users\Matt\Documents\FEFF\Feff6ldist\phase00.dat")
    '		Dim shifts As Complex() = testobj.PhaseShifts(0)
    '		
    '		For i As Integer = 0 To testobj.Lmax
    '			Console.WriteLine("L = " & i.ToString & " = " & shifts(i).ToString)
    '		Next
    '		
    '	End Sub
    '

    <TestMethod> _
    Public Sub Interpolation01()
        Dim testobj As New FEFFPhaseShiftFile("C:\Users\Matt\Documents\FEFF\Feff6ldist\phase00.dat")

        Dim emax As Double = testobj.Emax
        Dim emin As Double = testobj.Emin
        Dim erange As Double = emax - emin
        Dim e As Double
        Dim p As Complex
        For i As Integer = 1 To 99
            e = emin + erange * CDbl(i) / 100.0
            p = testobj.PhaseShift(e, 0)
            Console.WriteLine(e & ", " & p.Real.ToString & ", " & p.Imag.tostring)
        Next

        Console.WriteLine()

        Dim energies As Double() = testobj.Energies
        Dim phase As Complex
        For j As Integer = 0 To testobj.EnergyCount - 1
            phase = testobj.PhaseShift(j, 0)
            Console.WriteLine(energies(j).ToString & ", " & phase.Real.ToString & ", " & phase.Imag.ToString)
        Next
    End Sub

    <TestMethod()> _
    Public Sub Interpolation02()
        Dim testobj As New FEFFPhaseShiftFile("C:\Users\Matt\Documents\FEFF\Feff6ldist\phase00.dat")

        Dim emax As Double = testobj.Emax
        Dim emin As Double = testobj.Emin
        Dim erange As Double = emax - emin
        Dim e As Double
        Dim p As Complex
        For i As Integer = 1 To 99
            e = emin + erange * CDbl(i) / 100.0
            p = testobj.PhaseShift(e, 1)
            Console.WriteLine(e & ", " & p.Real.ToString & ", " & p.Imag.tostring)
        Next

        Console.WriteLine()

        Dim energies As Double() = testobj.Energies
        Dim phase As Complex
        For j As Integer = 0 To testobj.EnergyCount - 1
            phase = testobj.PhaseShift(j, 1)
            Console.WriteLine(energies(j).ToString & ", " & phase.Real.ToString & ", " & phase.Imag.ToString)
        Next
    End Sub

    <TestMethod()> _
    Public Sub Interpolation03()
        Dim testobj As New FEFFPhaseShiftFile("C:\Users\Matt\Documents\FEFF\Feff6ldist\phase00.dat")

        Dim emax As Double = testobj.Emax
        Dim emin As Double = testobj.Emin
        Dim erange As Double = emax - emin
        Dim e As Double
        Dim p As Complex
        For i As Integer = 1 To 99
            e = emin + erange * CDbl(i) / 100.0
            p = testobj.PhaseShift(e, 2)
            Console.WriteLine(e & ", " & p.Real.ToString & ", " & p.Imag.tostring)
        Next

        Console.WriteLine()

        Dim energies As Double() = testobj.Energies
        Dim phase As Complex
        For j As Integer = 0 To testobj.EnergyCount - 1
            phase = testobj.PhaseShift(j, 2)
            Console.WriteLine(energies(j).ToString & ", " & phase.Real.ToString & ", " & phase.Imag.ToString)
        Next
    End Sub

    <TestMethod()> _
    Public Sub Interpolation04()
        Dim testobj As New FEFFPhaseShiftFile("C:\Users\Matt\Documents\FEFF\Feff6ldist\phase00.dat")

        Dim emax As Double = testobj.Emax
        Dim emin As Double = testobj.Emin
        Dim erange As Double = emax - emin
        Dim e As Double
        Dim p As Complex
        For i As Integer = 1 To 999
            e = emin + erange * CDbl(i) / 1000.0
            p = testobj.PhaseShift(e, 2)
            Console.WriteLine(e & ", " & p.Real.ToString & ", " & p.Imag.tostring)
        Next

        Console.WriteLine()

        Dim energies As Double() = testobj.Energies
        Dim phase As Complex
        For j As Integer = 0 To testobj.EnergyCount - 1
            phase = testobj.PhaseShift(j, 2)
            Console.WriteLine(energies(j).ToString & ", " & phase.Real.ToString & ", " & phase.Imag.ToString)
        Next
    End Sub
End Class
