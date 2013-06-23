Imports DESD.Math


''' <summary>
''' Represents an PHASEnn.DAT output file from the FEFF POTPH routine.
''' </summary>
Public Class FEFFPhaseShiftFile
	
    'Implements IPhaseShiftProvider2
		
	Private _FileName As String
	Private _SpeciesID As Integer
	Private _FileText As String
	Private _Lmax As Integer
	Private _NE As Integer
	Private _Z As Integer
	Private _Rmt As Double			'// Muffin-tin radius
	Private _Rnm As Double
	Private _Mu As Double
	Private _Kf As Double
	Private _Vint As Double			'// Vint is the interstitial potential, in eV
	Private _Rs_int As Double
	Private _Phases(,) As Complex
	Private _Energy() As Double		'// Energies are in Ry, relative to the vacuum level
	Private _V0() As Complex		'// V0 are in Ry, and include the optical potential.
	
	Sub New(filename As String)
		_FileName = filename
		_FileText = System.IO.File.ReadAllText(_FileName)
		ParseFileText()
	End Sub
	
	
	
#Region "Public Properties"
	
	
	Public ReadOnly Property FileName As String
		Get
			Return _FileName
		End Get
	End Property
	
	Public ReadOnly Property SpeciesID As Integer
		Get
			Return _SpeciesID
		End Get
	End Property
	
	Public ReadOnly Property FileText As String
		Get
			Return _FileText
		End Get
	End Property
	
	Public ReadOnly Property Lmax As Integer
		Get
			Return _Lmax
		End Get
	End Property
	
	Public ReadOnly Property Energies As Double()
		Get
			Return _Energy.Clone
		End Get
	End Property
	
	Public ReadOnly Property Emax As Double
		Get
			Return _Energy(_Energy.Length - 1)
		End Get
	End Property
	
	Public ReadOnly Property Emin As Double
		Get
			Return _Energy(0)
		End Get
	End Property
	
	Public ReadOnly Property EnergyCount As Integer
		Get
			Return _NE
		End Get
	End Property
	
	Public ReadOnly Property Z As Integer
		Get
			Return _Z
		End Get
	End Property
	
	Public ReadOnly Property Rmt As Double
		Get
			Return _Rmt
		End Get
	End Property
	
	Public ReadOnly Property Rnm As Double
		Get
			Return _Rnm
		End Get
	End Property
	
	Public ReadOnly Property Mu As Double
		Get
			Return _Mu
		End Get
	End Property
	
	Public ReadOnly Property Kf As Double
		Get
			Return _Kf
		End Get
	End Property
	
	Public ReadOnly Property Vint As Double
		Get
			Return _Vint
		End Get
	End Property
	
	Public ReadOnly Property Rs_int As Double
		Get
			Return _Rs_int
		End Get
	End Property
	
	Public ReadOnly Property Phases As Complex(,)
		Get
			Return _Phases
		End Get
	End Property
	

#End Region

	
#Region "Public Methods"
	
	
	Public Function Energy(ie As Integer) As Double
		Return _Energy(ie)
	End Function
	
	Public Function PhaseShift(ie As Integer, l As Integer) As Complex
		Return _Phases(ie,l)
	End Function
	
	''' <summary>
	''' Returns an interpolated value for the phase shift, based upon the energy mesh output by FEFF.
	''' </summary>
	''' <param name="e"></param>
	''' <param name="l"></param>
	''' <returns></returns>
	Public Function PhaseShift(e As Double, l As Integer) As Complex
		
		Return InterpolatePhaseShift(e,l)
		
	End Function
	
	''' <summary>
	''' Returns a Lmax-dimensioned array containing the phase shifts interpolated at the specified energy.
	''' </summary>
	''' <param name="e"></param>
	''' <returns></returns>
	Public Function PhaseShifts(e As Double) As Complex()
				
		Dim retval(_Lmax) As Complex
		
		For i As Integer = 0 To _Lmax
			retval(i) = PhaseShift(e,i)
		Next
		
		Return retval
	End Function
	
	Public Function PhaseShifts(ie As Integer) As Complex()
		
		Dim retval(_Lmax) As Complex
		
		For i As Integer = 0 To _Lmax
			retval(i) = _Phases(ie,i)
		Next
		
		Return retval
	End Function
	
	''' <summary>
	''' Returns the phase shifts for a given angular momentum tabulated on the FEFF energy mesh.
	''' </summary>
	''' <param name="l"></param>
	''' <returns></returns>
	Public Function EnergyDependentPhaseShifts(l As Integer) As Complex()
		
		Dim retval(_NE) As Complex
		
		For i As Integer = 0 To _NE
			retval(i) = _Phases(i,l)
		Next
		
		Return retval
	End Function
	
	''' <summary>
	''' Returns an interpolated value for V0, based upon the energy mesh output by FEFF.
	''' </summary>
	''' <param name="e"></param>
	''' <returns></returns>
	Public Function V0(e As Double) As Complex
		Return InterpolateV0(e)
	End Function
	
	
#End Region


#Region "Private Methods"

	
	Private Sub ParseFileText()
		
		'// Split the lines on the vbCrLf
		Dim crlfDelimiter As Char() = {vbCr}
		Dim spaceDelimiter As Char() = {" "c}
				
		Dim TextLines As String() = _FileText.Split(crlfDelimiter,StringSplitOptions.RemoveEmptyEntries)
		
		'// Clean up by trimming each line:
		For i As Integer = 0 To TextLines.Length-1
			TextLines(i) = TextLines(i).Trim()
			'Console.WriteLine(i.ToString & "   " & TextLines(i))  '// For test/debug purposes only
		Next
		
		'// Deduce the number of species:
		Dim NSpecies As Integer = 1
		For i As Integer = 2 To TextLines.Length-1
			If TextLines(i).StartsWith("Pot") Then 
				NSpecies += 1
			Else
				Exit for
			End If
		Next
		
'		Console.WriteLine("Nspecies = " & NSpecies.ToString)
		
		'// Extract the Species ID, Lmax, and number of energy points (line 6 zero-based):
		Dim lineElements As String() = TextLines(NSpecies + 3).Split(spaceDelimiter,StringSplitOptions.RemoveEmptyEntries)
		
		_SpeciesID = CInt(lineElements(0))
		_Lmax = CInt(lineElements(1))
		_NE = CInt(lineElements(2))
		
		'// For test/debug:
'		Console.WriteLine("Species ID = " & _SpeciesID.tostring)
'		Console.WriteLine("Lmax = " & _Lmax.tostring)
'		Console.WriteLine("NE = " & _NE.tostring)

		'// Extract the Z and the muffin-tin radius - this information is found in the header line 		
		'// corresponding to the species ID (since all of them print for every species)
		lineElements = TextLines(_SpeciesID + 1).Split(spaceDelimiter,StringSplitOptions.RemoveEmptyEntries)
		
		Dim txtZ As String
		Dim txtRmt As String
		Dim txtRnm As String
		If _SpeciesID = 0 Then
			txtZ = lineElements(1)
			txtRmt = lineElements(3)
			txtRnm = lineElements(5)
		Else
			txtZ = lineElements(2)
			txtRmt = lineElements(4)
			txtRnm = lineElements(6)
		End If
		_Z = CInt(txtZ.Substring(2))
		_Rmt = CDbl(txtRmt)
		_Rnm = CDbl(txtRnm)
		
'		Console.WriteLine("Z = " & _Z.tostring)
'		Console.WriteLine("Rmt = " & _Rmt.ToString)
'		Console.WriteLine("Rnm = " & _Rnm.ToString)
		
		'// Get mu, kf, Vint, and Rs_int
		lineElements = TextLines(Nspecies + 2).Split(spaceDelimiter,StringSplitOptions.RemoveEmptyEntries)
		_Mu = CDbl(lineElements(0).Substring(3))
		_Kf = CDbl(lineElements(1).Substring(3))
		_Vint = CDbl(lineElements(2).Substring(5))
		_Rs_int = CDbl(lineElements(4))
		
'		Console.WriteLine("Mu= " & _Mu.tostring)
'		Console.WriteLine("Kf = " & _Kf.ToString)
'		Console.WriteLine("Vint = " & _Vint.ToString)
'		Console.WriteLine("Rs_int = " & _Rs_int.ToString)
		
		'// Now loop over each ie and read phase data
		
		'// Dimension the Phases, Energy, and V0 arrays:
		Dim PhasesTemp(_NE - 1, _Lmax) As Complex
		ReDim _Energy(_NE - 1)
		ReDim _V0(_NE - 1)
		
		'// We will fill these arrays using a line-by-line parse approach:
		Dim l As Integer
		Dim ie As Integer = -1
		For i As Integer = NSpecies + 4 To TextLines.Length - 1
			If TextLines(i).StartsWith("ie") Then
				'// This is a new block of phase shifts.  Reset the counters
				l = 0
				ie = -1	
			ElseIf ie = -1 Then
				'// This is the line that has the energy and V0.  Parse them out.
				lineElements = TextLines(i).Split(spaceDelimiter,StringSplitOptions.RemoveEmptyEntries)
				ie = CInt(lineElements(0))
				_Energy(ie - 1) = CDbl(lineElements(1))
				_V0(ie - 1) = New Complex(CDbl(lineElements(2)),CDbl(lineElements(3)))
				'Console.WriteLine("Energy = " & _Energy(ie-1).ToString & ", V0 = " & _V0(ie-1).tostring)
			Else
				'// This line has phase shift data.  Parse it out.
				lineElements = TextLines(i).Split(spaceDelimiter,StringSplitOptions.RemoveEmptyEntries)
				If lineElements.Length > 0 Then PhasesTemp(ie - 1,l) = New Complex(CDbl(lineElements(0)),CDbl(lineElements(1)))
				If lineElements.Length > 2 Then PhasesTemp(ie - 1,l+1) = New Complex(CDbl(lineElements(2)),CDbl(lineElements(3)))
				'Console.WriteLine("L = " & l.ToString & " = " & PhasesTemp(ie-1,l).tostring)
				
				'// Increment the l counter
				l += 2
			End If
			
		Next
		
		'// Write out to console for debug:
'		For i As Integer = 0 To _NE-1
'			Console.WriteLine("Energy = " & _Energy(i).ToString & ", V0 = " & _V0(i).tostring)
'		Next

'// Now I need to fix the phase shifts for discontinuities:
'// Here's how I did it earlier:
	'// One spline for each L value:
'		Dim retval(phases(0).LMax) as Sieger.Math.CubicSpline
'		Dim ps(emesh.Length-1) As Double
'		Dim cs As Sieger.Math.CubicSpline
'		Dim temp As Double
'		Dim lasttemp As Double
'		Dim adder as Double = 0.0
'		For L As Integer = 0 To phases(0).LMax
'			'// Copy over the phase shift values, cleaning up (adding factors of pi as needed) as we go
'			adder = 0.0
'			For i As Integer = 0 To emesh.Length-1
'				temp = phases(i).RealValue(L)
'				If i > 0 Then
'					If (system.math.Sign(temp) = -system.math.Sign(lasttemp)) Then
'						'// We've changed sign.  Either we are near zero and crossing, or we're wrapping around.
'						'// If the difference in values is big, we must be wrapping around:
'						If system.math.Abs(lasttemp) > system.math.PI/3.0 Then
'							'// last point was large, likely we are wrapping around:
'							'// Determine the direction - rising or falling:
'							If lasttemp < 0 Then
'								'// falling - we should add -pi to future values
'								adder += -system.math.PI
'							Else
'								'// rising, we should add +pi to future values
'								adder += system.math.PI
'							End If
'						End If
'					End If
'				End If
'				ps(i)= temp + adder
'				'// Remember last value
'				lasttemp = temp
'			Next
'			'// Create the cubic spline:
'			cs = New Sieger.Math.CubicSpline(emesh,ps,(ps(1)-ps(0))/DELTAE,(ps(emesh.Length-1)-ps(emesh.Length-2))/DELTAE)
'			retval(L) = cs
'		Next

		ReDim _Phases(_NE - 1, _Lmax)
		Dim adderr As Double = 0.0
		Dim adderi As Double = 0.0
		Dim temp As Complex
		Dim tempr As Double
		Dim tempi As Double
		Dim lasttempr As Double
		Dim lasttempi As Double
		For ll As Integer = 0 To _Lmax
			adderr = 0.0
			adderi = 0.0
			For i As Integer = 0 To _Energy.Length - 1
				temp = PhasesTemp(i,ll)
				tempr = temp.Real
				tempi = temp.Imag
				If i > 0 Then
					'// WE'RE HERE RIGHT NOW
					If (system.math.Sign(tempr) = -system.math.Sign(lasttempr)) Then
						'// We've changed sign.  Either we are near zero and crossing, or we're wrapping around.
						'// If the difference in values is big, we must be wrapping around:
						If system.math.Abs(lasttempr) > system.math.PI/3.0 Then
							'// last point was large, likely we are wrapping around:
							'// Determine the direction - rising or falling:
							If lasttempr < 0 Then
								'// falling - we should add -pi to future values
								adderr += -system.math.PI
							Else
								'// rising, we should add +pi to future values
								adderr += system.math.PI
							End If
						End If
						
					End If
					
					If (System.Math.Sign(tempi) = -System.Math.Sign(lasttempi)) Then
						'// We've changed sign.  Either we are near zero and crossing, or we're wrapping around.
						'// If the difference in values is big, we must be wrapping around:
						If system.math.Abs(lasttempi) > system.math.PI/3.0 Then
							'// last point was large, likely we are wrapping around:
							'// Determine the direction - rising or falling:
							If lasttempi < 0 Then
								'// falling - we should add -pi to future values
								adderi += -system.math.PI
							Else
								'// rising, we should add +pi to future values
								adderi += system.math.PI
							End If
						End If
					End If
				End If
				_Phases(i,ll) = New Complex(tempr + adderr, tempi + adderi)
				'// Remember last value
				lasttempr = tempr
				lasttempi = tempi
			Next
		Next
	End Sub
	
	Private Function InterpolatePhaseShift(e As Double, l As Integer) As Complex
		
		' Error condition: e < emin
		' Error condition: e > emax
		' Error condition: l < 0
		' Error condition: l > lmax
		
		Dim imax As Integer = _Energy.Length - 1
		Dim ihi As Integer
		'// Find the index of the energy mesh point just larger than e:
		For i As Integer = 0 To imax
			If _Energy(i) > e Then 
				ihi = i
				Exit For
			End If
		Next
		
		
		'// Determine the max order of the polynomial interpolation allowed by the position of e:
		Dim MAXORDER As Integer = 3
		Dim xa(MAXORDER) As Double
		Dim yar(MAXORDER) As Double
		Dim yai(MAXORDER) As Double
		
		Dim ilow As Integer
		If ihi = 1 Then
			ilow = 0
		ElseIf ihi = imax Then
			ilow = imax - 3 
		Else
			ilow = ihi - 2
		End If
		
		Dim temp As complex
		For i As Integer = 0 To MAXORDER
			xa(i) = _Energy(ilow + i)
			
			temp = _Phases(ilow + i, l)
			yar(i) = temp.Real
			yai(i) = temp.Imag
		Next
		
		Dim yr As Double = Functions.PolynomialInterpolation(xa,yar,e)(0)
		Dim yi As Double = Functions.PolynomialInterpolation(xa,yai,e)(0)
		
		Return New Complex(yr,yi)

	End Function
	
	Private Function InterpolateV0(e As Double) As Complex
		
		' Error condition: e < emin
		' Error condition: e > emax

		
		Dim imax As Integer = _Energy.Length - 1
		Dim ihi As Integer
		'// Find the index of the energy mesh point just larger than e:
		For i As Integer = 0 To imax
			If _Energy(i) > e Then 
				ihi = i
				Exit For
			End If
		Next
		
		
		'// Determine the max order of the polynomial interpolation allowed by the position of e:
		Dim MAXORDER As Integer = 3
		Dim xa(MAXORDER) As Double
		Dim yar(MAXORDER) As Double
		Dim yai(MAXORDER) As Double
		
		Dim ilow As Integer
		If ihi = 1 Then
			ilow = 0
		ElseIf ihi = imax Then
			ilow = imax - 3 
		Else
			ilow = ihi - 2
		End If
		
		Dim temp As complex
		For i As Integer = 0 To MAXORDER
			xa(i) = _Energy(ilow + i)
			temp = _V0(ilow + i)
			yar(i) = temp.Real
			yai(i) = temp.Imag
		Next
		
		Dim yr As Double = Functions.PolynomialInterpolation(xa,yar,e)(0)
		Dim yi As Double = Functions.PolynomialInterpolation(xa,yai,e)(0)
		
		Return New Complex(yr,yi)

	End Function	
	
#End Region
	
	
#Region "IPhaseShiftProvider2 Implementation"
	
	Function GetPhaseShift(E As Double, L As Integer) As Complex
		Return PhaseShift(E,L)
	End Function
	
	Function GetVoptical(E As Double) As Complex
		
	End Function
	
	Function GetTMatrix(E As Double) As Complex()
		
	End Function

#End Region
	
	
	
End Class
