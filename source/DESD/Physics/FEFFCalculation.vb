Imports System
Imports System.Diagnostics
Imports System.ComponentModel
Imports DESD.Math

'
' Created by SharpDevelop.
' User: Matt
' Date: 9/29/2011
' Time: 11:10 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Class FEFFCalculation
	
    'Implements IPhaseShiftsFactory
	
	Private mExecutableDir As String 
	Private mCluster As Cluster
	Private mTemperature As Double
	Private mDebyeTemperature As Double
	Private mPhaseShifts As New SortedList(Of Integer, FEFFPhaseShiftFile)
	
	Sub New(FEFFDir As String, calcCluster As Cluster, temperature As Double, debyeTemperature As double)
		mCluster = calcCluster
		mExecutableDir = FEFFDir
		mTemperature = temperature
		mDebyeTemperature = debyeTemperature
	End Sub
	
	
	Public Property ExecutableDirectory As String
		Get
			Return mExecutableDir
		End Get
		Set (value As String)
			mExecutableDir = value
		End Set
	End Property
	

	Public Sub Execute()
		
		'// Write the input file:
		Call WriteInpFile(mExecutableDir,mCluster, mTemperature, mDebyeTemperature)
		
		'// Run FEFF:
		Call RunProcess(System.IO.Path.Combine(mExecutableDir , "feff6l.exe"))
		
		
		Call ReadPhaseShifts()
		
	End Sub
	
	''' <summary>
	''' Writes the FEFF input file to the given directory
	''' </summary>
	Public Shared Sub WriteInpFile(dirPath As String, clusterobj As Cluster, temperature As Double, debyeTemperature As Double)
		
		'// Create the output as a string"
		Dim outString As String = "* DESD Calculation Phase Shifts" & vbCrLf
		outString &= "TITLE DESD CALCULATION" & vbCrLf
		outString &= "PRINT 5 0 0 0" & vbCrLf
		outString &= "HOLE  0  1" & vbCrLf
		outString &= "DEBYE  " & temperature.ToString & "  " & debyeTemperature.ToString & vbCrLf
		outString &= "POTENTIALS" & vbCrLf
		
		'// Get all of the species (potentials)
		Dim mSpecies As ClusterSpecies
		Dim speciesIDs As List(Of Integer) = clusterobj.GetSpeciesIDs
		For each speciesID As Integer In speciesIDs
			mSpecies = clusterobj.GetSpecies(speciesID)
			outString &= speciesID.ToString & "  " & CInt(mSpecies.AtomicNumber).Tostring & "  " & mSpecies.AtomicNumber.Symbol & vbCrLf
		Next
		
		'// A blank line, for aesthetics
		outString &= vbCrLf
		
		'// Now list out the atoms in the cluster
		outString &= "ATOMS" & vbCrLf
		Dim atomIDs As List(Of Integer) = clusterobj.GetAtomIDs
		Dim cAtom As ClusterAtom
		Dim atomCount As integer = 0
		For each atomid As Integer In atomIDs
			cAtom = clusterobj.GetAtom(atomid)
			outString &= cAtom.Position.X.ToString & "  " & cAtom.Position.Y.ToString & "  " & cAtom.Position.Z.ToString & "  " & cAtom.Species.ToString & vbCrLf
			atomCount += 1
			'// FEFF6l has a restriction - no more than 250 atoms.  Cut off after that.
			If atomCount = 250 Then Exit for
		Next
		
		'// Construct the file path:
		Dim fileName As String = "feff.inp"
		Dim filePath As String = System.IO.Path.Combine(dirPath,fileName)
		
		'1) Create a file for the INP
		'2) Write the file
		'3) Copy the existing FEFF.INP to FEFF.INP.BAK - We'll do this later... not implemented now
		'4) Copy the new file into FEFF.INP
		Dim oWrite as System.IO.StreamWriter = System.IO.File.CreateText(filePath)
		oWrite.Write(outString)
		oWrite.Close
		
	End Sub
	
	
	Public Shared Sub RunProcess(executablePath As String)
		Dim myProcess As New Process()

        Try               
			myProcess.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(executablePath)
            myProcess.StartInfo.FileName = executablePath
            myProcess.Start()
            
            '// wait for the process to finish
            myProcess.WaitForExit()
            
        Catch e As Exception
           msgbox(e.Message)
        End Try
	End Sub
	
	
	Public Sub ReadPhaseShifts()
		
		Dim speciesIDs As List(Of Integer) = mCluster.GetSpeciesIDs
		Dim fileName As String
		
		For Each speciesID As Integer In speciesIDs
			If speciesID <= 9 Then
				fileName = "phase0" & speciesID.ToString & ".dat"	
			Else
				fileName = "phase" & speciesID.ToString & ".dat"
			End If
			
			mPhaseShifts.Add(speciesID, New FEFFPhaseShiftFile(fileName))
			
		Next
		
	End Sub
	
	
	Public Function GetPhaseShifts(speciesID As Integer, lmax As Integer) As Complex()
		
		Dim retval(lmax) As Complex
        Dim dblE As Double
        'TODO:  Energy, l arguments on PhaseShift are bogus, and temporary to get this to build.
		For l = 0 To lmax
            retval(l) = mPhaseShifts(speciesID).PhaseShift(dblE, l)
		Next
		
	End Function
	
#Region "IPhaseShiftsFactory Implementation"
	
    'Public ReadOnly Property V0() As Double Implements IPhaseShiftsFactory.V0
    '	Get
    '		Return mPhaseShifts(0).Vint
    '	End Get
    'End Property

    'Public ReadOnly Property Voptical(energy As Double) As Complex Implements IPhaseShiftsFactory.Voptical
    '	Get
    '		Return mPhaseShifts(0).GetVoptical(energy)
    '	End Get
    'End Property

    'Public ReadOnly Property PhaseShifts(speciesID As Integer) As IPhaseShiftProvider2 Implements IPhaseShiftsFactory.PhaseShifts
    '	Get
    '		Return mPhaseShifts(speciesID)
    '	End Get
    'End Property
	
	Public Function k(energy As Double) As Complex
		Dim eref as Complex = mPhaseShifts(0).V0(energy)
        Dim ke As Complex = Complex.CSqrt(energy - eref)
		
		'// Convert to inverse Angstom units (since bond distances in the cluster are in Angstroms)
        Return 0.5123 * ke

	End Function
		
#End Region
	
	
End Class
