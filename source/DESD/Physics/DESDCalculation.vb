Imports DESD.Math
Imports System.Collections.Generic
Imports System.Threading
Imports System.Math

Public Class DESDCalculation
	
	'// the scattering cluster:
	Private _ClusterFileName As String
	Private _Cluster As Cluster
	
	'// the absorber atom ID (ref. to the cluster)
	Private _AbsorberAtomID as Integer = 0
	
	'// Incident Energy, in Electron Volts (eV), relative to the vacuum level:
	Private _Energy As Double = 30.0
	
	'// Maximum angular momentum for phase shifts:
	Private _Lmax As Integer = 8
	
	'// Derived value:
	Private _k As Complex  ' In inverse Angstroms
	
	'// Temperature, in Kelvins:
	Private _Temperature As Double = 300.0
	Private _DebyeTemperature as Double = 645.0	'// Debye temperature of Si, in Kelvin
	
	'// Inner and Optical potentials:
	Private _InnerPotential As Double = -10.0
	Private _OpticalPotential As Double = 0.0
	Private _zv0 as Double = 1.0
	
	'// By default, theta is constant at 135 degrees incident polar angle:
    Private _startTheta As Double = 3.0 * DESD.Math.Constants.Pi / 4.0
    Private _endTheta As Double = 3.0 * DESD.Math.Constants.Pi / 4.0
	Private _NTheta As Integer = 1
	
	'// By default, the azimuthal angle runs around the full circle, at 100 points in that interval:
	Private _startPhi As Double = 0.0
    Private _endPhi As Double = 2.0 * DESD.Math.Constants.Pi
	Private _NPhi as Integer = 5
	
	'// The Rehr-Albers order of the calculation (matrix dimension)
	Private _RAOrder As Integer = 2
	Private _MaxScatteringOrder As Integer = 3
	Private _MaxPathLength as Double = 40.0 ' Angstroms
	
	'// Muffin Tins and phase shifts are keyed by species:
	Private _PhaseShifts As New SortedList(Of Integer, PhaseShift)
    Private _PhaseShifts2 As New SortedList(Of Integer, IPhaseShiftProvider2)
	
	Private _MuffinTins As New SortedList(Of Integer, NeutralMuffinTin)
	
	Private _NumberOfThreads As Integer = 1
	'Private _FixedL as Integer = 0
	Private _UseL0 As Boolean = False

	Private _FEFFDirectory As String = "C:\Users\Matt\Documents\FEFF\Feff6ldist"

	Private _UseAbInitioV0 As Boolean = True
	Private _UseAbInitioVoptical As Boolean = True

	
#Region "Constructors"
	
	Sub New
		
	End Sub
	
#End Region
	
	
#Region "Public Properties"

	''' <summary>
	''' A reference to a scattering cluster.
	''' </summary>
	Public Readonly Property ScatteringCluster As Cluster
		Get
			return _Cluster
		End Get
	End Property
	
	
	''' <summary>
	''' The file name of a scattering cluster to use in the calculation.
	''' </summary>
	Public Property ClusterFileName As String
		Get
			Return _ClusterFileName
		End Get
		Set (value As String)
			_ClusterFileName = value
		End Set
	End Property
	
	
	''' <summary>
	''' The atomID of the target (absorber) atom in the cluster.
	''' </summary>
	Public Property AbsorberAtomID As Integer
		Get
			Return _AbsorberAtomID
		End Get
		Set (value As Integer)
			_AbsorberAtomID = value
		End Set
	End Property
	
	
	''' <summary>
	''' The energy of the incident electron, in eV.
	''' </summary>
	Public Property Energy As Double
		Get
			return _Energy
		End Get
		Set (value As Double)
			_Energy = value
		End Set
	End Property
	
	
	''' <summary>
	''' The maximum partial wave angular momentum.
	''' </summary>
	Public Property Lmax As Integer
		Get
			Return _Lmax
		End Get
		Set (value As Integer)
			If value < 0 then throw new ArgumentException("Lmax must be greater than or equal to zero.")
			_Lmax = value
		End Set
	End Property
	
	
	''' <summary>
	''' The wave number of the incident electron, corrected for the inner potential, in inverse Angstroms.
	''' </summary>
	Public ReadOnly Property K As Complex
		Get
			Return _k
		End Get
	End Property
	
	
	''' <summary>
	''' The temperature of the scattering cluster, in Kelvins.
	''' </summary>
	Public Property Temperature As Double
		Get
			Return _Temperature
		End Get
		Set (value As Double)
			_Temperature = value
		End Set
	End Property
	
	
	''' <summary>
	''' The Debye temperature of the scattering cluster, in Kelvins.
	''' </summary>
	Public Property DebyeTemperature As Double
		Get
			Return _DebyeTemperature
		End Get
		Set (Value As Double)
			_DebyeTemperature = value
		End Set
	End Property


	''' <summary>
	''' The inner potential of the solid, in eV.
	''' </summary>
	Public Property InnerPotential As Double
		Get
			Return _InnerPotential
		End Get
		Set (value As Double)
			_InnerPotential = value
		End Set
	End Property
	
	
	''' <summary>
	''' The optical potential of the solid, in eV.  The optical potential governs inelastic losses (mean free path).
	''' </summary>
	Public Property OpticalPotential As Double
		Get
			Return _OpticalPotential
		End Get
		Set (value As Double)
			_OpticalPotential = value
		End Set
	End Property
	
	
	''' <summary>
	'''
	''' </summary>
	Public Property ThetaStart As Double
		Get
			Return _StartTheta
		End Get
		Set (value As Double)
			_StartTheta = value
		End Set
	End Property
	
	
	''' <summary>
	'''
	''' </summary>
	Public Property ThetaEnd As Double
		Get
			Return _EndTheta
		End Get
		Set (value As Double)
			_EndTheta = value
		End Set
	End Property
	
	
	''' <summary>
	'''
	''' </summary>
	Public Property ThetaN As Integer
		Get
			Return _NTheta
		End Get
		Set (value As Integer)
			_NTheta = value
		End Set
	End Property
	
	
		''' <summary>
	'''
	''' </summary>
	Public Property PhiStart As Double
		Get
			Return _StartPhi
		End Get
		Set (value As Double)
			_StartPhi = value
		End Set
	End Property
	
	
	''' <summary>
	'''
	''' </summary>
	Public Property PhiEnd As Double
		Get
			Return _EndPhi
		End Get
		Set (value As Double)
			_EndPhi = value
		End Set
	End Property
	
	
	''' <summary>
	'''
	''' </summary>
	Public Property PhiN As Integer
		Get
			Return _NPhi
		End Get
		Set (value As Integer)
			_NPhi = value
		End Set
	End Property

	
	
	''' <summary>
	''' The order of the Rehr-Albers matrices used to calculate the multiple scattering.  Possible values are:
	''' 0 - scattering matrices are 1x1 (plane wave approximation)
	''' 1 - scattering matrices are 3x3
	''' 2 - scattering matrices are 6x6
	''' 3 - scattering matrices are 10x10
	''' 4 - scattering matrices are 15x15
	''' </summary>
	Public Property RAOrder As Integer
		Get
			return _RAOrder
		End Get
		Set (value As Integer)
			if (value < 0) or (value > 4) then throw new ArgumentException("RAOrder must be between 0 and 4")
			_RAOrder = value
		End Set
	End Property

	
	
	Public Property MaxScatteringOrder As Integer
		Get
			Return _MaxScatteringOrder
		End Get
		Set (value As Integer)
			_MaxScatteringOrder = value
		End Set
	End Property
	
	Public Property MaxPathLength As Double
		Get
			Return _MaxPathLength
		End Get
		Set (value As Double)
			_MaxPathLength = value
		End Set
	End Property
	
	Public Property NumberOfThreads As Integer
		Get
			Return _NumberOfThreads
		End Get
		Set (value As Integer)
			if value <= 0 then throw new ArgumentException("Number of threads must be greater than zero.")
			_NumberOfThreads = value
		End Set
	End Property
	
	Public Property ZV0 As Double
		Get
			Return _zv0
		End Get
		Set (value As Double)
			_zv0 = value
		End Set
	End Property
	
'	Public Property FixedL As Integer
'		Get
'			Return _FixedL
'		End Get
'		Set (value As Integer)
'			_FixedL = value
'		End Set
'	End Property
'
	Public Property UseL0 As Boolean
		Get
			Return _UseL0
		End Get
		Set (value As Boolean)
			_UseL0 = value
		End Set
	End Property
	
	Public Property FEFFDirectory As String
		Get
			Return _FEFFDirectory
		End Get
		Set (value As String)
			_FEFFDirectory = value
		End Set
	End Property
	
	Public Property UseAbInitioV0 As Boolean
		Get
			Return _UseAbInitioV0
		End Get
		Set (value As Boolean)
			_UseAbInitioV0 = value
		End Set
	End Property
	
	Public Property UseAbInitioVoptical As Boolean
		Get
			Return _UseAbInitioVoptical
		End Get
		Set (value As Boolean)
			_UseAbInitioVoptical = value
		End Set
	End Property	
	
#End Region

	
	Public Sub Calculate()
		
		Console.WriteLine("DESD Calculation =========================")
		Console.WriteLine("E = " & _Energy.ToString & " eV")
		Console.WriteLine("V0 = " & _InnerPotential.ToString & " eV")
		Console.WriteLine("Etotal (Ryd) = " & ((_Energy-_InnerPotential)/13.6056923).ToString & " Ry")
		Console.WriteLine("Lmax = " & _Lmax.ToString)
		Console.WriteLine("Max Scattering Order = " & _MaxScatteringOrder.ToString)
		Console.WriteLine("Max Path Length = " & _MaxPathLength.ToString)
		
		'// Load the cluster:
		_Cluster = new Cluster(_ClusterFileName)
		Console.WriteLine("Cluster file name = " & _ClusterFileName)
		Console.WriteLine("Number of atoms in cluster = " & _Cluster.Count.ToString)
		
		
		
		'// Calculate the complex magnitude of k:
		_k = GetKValue(_Energy, _InnerPotential, _OpticalPotential)
		
		Console.WriteLine("k = " & _k.ToString)
		
		
		'// Calculate cluster phase shifts:
		Console.WriteLine()
		Console.WriteLine("Calculating phase shifts...")
		CalculateClusterPhaseShifts(_Cluster, _Energy - _InnerPotential, _Lmax, _OpticalPotential, _Temperature, _DebyeTemperature)
		
		Console.WriteLine("Phase shifts calculated.")
		Dim delta As Complex
		Dim tm As Complex
		For ips As Integer = 0 To _PhaseShifts.Count-1
			Console.WriteLine("Inner Potential for species " & ips.tostring & " = " & _PhaseShifts(ips).MuffinTin.InnerPotential.ToString & " [Ry]")
			Console.WriteLine("Inner Potential for species " & ips.tostring & " = " & (13.6056923 *_PhaseShifts(ips).MuffinTin.InnerPotential).ToString & " [eV]")
			Console.WriteLine("Muffin-tin radius for species " & ips.tostring & " (Ry) = " & _PhaseShifts(ips).MuffinTin.Radius.ToString)
			Console.WriteLine("Muffin-tin radius for species " & ips.tostring & " (Angstroms) = " & (0.52917720859*_PhaseShifts(ips).MuffinTin.Radius).ToString)
			Console.WriteLine("Energy for phase shifts = " & _PhaseShifts(ips).Energy.tostring & " [Ry]")
			Console.WriteLine("Energy for phase shifts = " & (13.6056923 * _PhaseShifts(ips).Energy).tostring & " [eV]")
			
			Console.WriteLine("L     Delta_L                    Tmatrix_L")
			Console.WriteLine("----  -------------------------- ---------")
			For il As Integer = 0 To _PhaseShifts(ips).Lmax
				delta = _PhaseShifts(ips).ComplexValue(il, _OpticalPotential, _Temperature, _DebyeTemperature)
				tm = complex.CExp(complex.i*delta)*complex.CSin(delta)
				Console.WriteLine(il.ToString & "  " & delta.Tostring & "  " & tm.tostring)
			Next
			Console.WriteLine()
			Console.WriteLine("Scattering Factor:")
			PrintScatteringFactor(_PhaseShifts(ips).GetPhaseShifts(_OpticalPotential, _Temperature, _DebyeTemperature))
			Console.WriteLine()
		Next
			
		'// Create the path processors:\
		'// Note that the direct term gets added to the first PathProcessor in the list.
		Console.WriteLine("Creating path processors...")
		
		Dim PathProcessors As List(Of IMSPathProcessor) = _
			CreatePathProcessors(_NumberOfThreads, _Cluster, _AbsorberAtomID, _MaxPathLength, _MaxScatteringOrder, _k, _zv0, _RAOrder, _LMax)
		
		Console.WriteLine("Number of path processors = " & PathProcessors.Count.ToString)
		Console.WriteLine("Starting path calculations...")
		
		'// Start the PathProcessors going on multiple threads and wait for them to finish:
		Dim threads As New List(Of Thread)
		Dim thisThread As Thread
		Dim thisProcessor as IMSPathProcessor
		If _NumberOfThreads = 1 Then
			thisProcessor = PathProcessors(0)
			thisProcessor.Calculate
		Else
			For i As Integer = 0 To _NumberOfThreads - 1
				thisProcessor = PathProcessors(i)
				thisThread = New Thread(AddressOf thisProcessor.Calculate)
				thisThread.Start()
				threads.Add(thisThread)
			Next
		
			'// Wait for the threads to finish:
			For Each t As Thread In threads
				t.Join()
			Next

		End If
		
		'// Now list out amplitudes:
		Dim pathCounter As Integer = 0
		Dim pathAmp as Complex
		For i As Integer = 0 To _NumberOfThreads - 1
			thisProcessor = PathProcessors(i)
			Console.WriteLine("Path calculations complete.  Processor " & i.ToString & " calculated " & thisProcessor.NPathsCalculated.ToString & " paths.")
			For Each path As MSPath In thisProcessor.GetPaths
				pathCounter += 1
				If Not(path.Amplitude) Is Nothing Then
					pathAmp = path.Amplitude.Value(0,0)
				Else
					pathAmp = New Complex(0.0,0.0)
				End If
				Console.WriteLine(pathcounter.tostring & ",    " & path.ToString & ",    " & path.PathLength.ToString & ",   " & pathAmp.tostring)
			Next
		Next
		
		
		
		Console.WriteLine("Path calculations complete.  Starting terminations...")
		
		'// Now we'll construct the k-vector dependent results.  Set up a loop over theta and phi
		'// Future - enable a 2D mesh class for full hemisphere calculations.
		Dim thetamesh As New SimpleMesh(_startTheta,_endTheta,_NTheta)
		Dim phimesh As New SimpleMesh(_startPhi,_endPhi,_NPhi)
		Dim refractedTheta as Double
		Dim khat As Vector
		Dim allPaths As New List(Of MSPath)
		Dim alpha as ComplexMatrix
		Dim LM As Integer(,) = MSPathProcessor.LM(_Lmax)
		Dim AbsorberPS as PhaseShift = _PhaseShifts(_Cluster.Species(_absorberAtomID))
		Dim totalIntensity As Double
		
		Console.WriteLine("Number of theta points = " & thetamesh.Count.ToString)
		Console.WriteLine("Number of phi points = " & phimesh.Count.ToString)
		
		For Each theta As Double In thetamesh
			refractedTheta = Refract(theta,_Energy,_InnerPotential)
			Console.WriteLine("Theta = " & theta.ToString)
			Console.WriteLine("Refracted Theta = " & refractedTheta.ToString)
			For Each phi As Double In phimesh
				'Console.WriteLine()
				'Console.WriteLine("Phi = " & phi.ToString)
				
				'// Set the k vector direction:
				khat = Vector.NewFromSpherical(1.0,refractedTheta,phi)
				For Each pp As IMSPathProcessor In PathProcessors
					pp.Khat = khat
				Next
				
				'// Start the PathProcessors going on multiple threads and wait for them to finish:
				'// Clear the threads list:
				If _NumberOfThreads = 1 Then
					thisProcessor = PathProcessors(0)
					thisProcessor.CalculateTermination
				Else
					
					threads.clear
					For i As Integer = 0 To _NumberOfThreads - 1
						thisProcessor = PathProcessors(i)
						thisThread = New Thread(AddressOf thisProcessor.CalculateTermination)
						thisThread.Start()
						threads.Add(thisThread)
					Next
			
					'// Wait for the threads to finish:
					For Each t As Thread In threads
						t.Join()
					Next
				
				End If
				
				'// We should now have Alpha(L) for all paths, including the direct term.
				'// Sum all paths to arrive at the total amplitude.
				alpha = nothing
				For Each pp As IMSPathProcessor In PathProcessors
					For Each path As MSPath In pp.Getpaths
						If Not(path.Amplitude) Is Nothing Then
							pathAmp = path.Amplitude.Value(0,0)
						Else
							pathAmp = New Complex(0.0,0.0)
						End If
						if phi = 0.0 then Console.WriteLine(path.ToString & ",    " & pathAmp.tostring)

							If alpha Is Nothing Then
								alpha = path.Amplitude
							Else
								alpha += path.Amplitude
							End If

					Next
				Next
				
				'// Need from the absorber muffin-tin the integral of Pl*(r) Pl(r) from 0 to Rmt:
				'// at high values of l, this integral will tend towards zero.  How best to handle the
				'// normalization??  Answer - don't.  Let's calculate the betas first and later figure
				'// out how to handle the core wave functions.
		
				'// Now weight each amplitude by its contribution to the absorber atom total charge:
				totalIntensity = 0.0
				
				If _useL0 Then
					'totalIntensity = (alpha(0,0).Magnitude)^2
					totalIntensity = (alpha(0,0).Real)^2 + (alpha(0,0).Imag)^2
					'totalIntensity = AbsorberPS.TotalCharge(LM(0,0)) * (alpha(0,0).Magnitude)^2
					
				Else
					For iL As Integer = 0 To LM.Length\2 - 1
						totalIntensity += AbsorberPS.TotalCharge(LM(iL,0)) * (alpha(iL,0).Magnitude)^2
					Next
					
				End If
				
			
				'// Report out the amplitudes:
				Console.WriteLine(phi.ToString & "     " & totalIntensity.tostring)

				
			Next
		Next
			
	End Sub
	
	Public Sub Calculate2()
		
		Console.WriteLine("DESD Calculation =========================")
		Console.WriteLine("E = " & _Energy.ToString & " eV")
		Console.WriteLine("Lmax = " & _Lmax.ToString)
		Console.WriteLine("Max Scattering Order = " & _MaxScatteringOrder.ToString)
		Console.WriteLine("Max Path Length = " & _MaxPathLength.ToString)
		Console.WriteLine()
		
		'// Load the cluster:
		Console.WriteLine("Loading cluster...")
		_Cluster = new Cluster(_ClusterFileName)
		Console.WriteLine("Cluster file name = " & _ClusterFileName)
		Console.WriteLine("Number of atoms in cluster = " & _Cluster.Count.ToString)
		Console.WriteLine("Number of species = " & _Cluster.SpeciesCount.ToString)
		Console.WriteLine()
		
		'// Calculate phase shifts:
		Console.WriteLine("Calculating phase shifts...")
		CalculateClusterPhaseShifts2(_Cluster, _Energy, _Lmax, _Temperature, _DebyeTemperature)
		
		Console.WriteLine("Phase shifts successfully calculated.")
		
		Dim delta As Complex
		Dim tm As Complex
		For ips As Integer = 0 To _PhaseShifts.Count-1
			Console.WriteLine("Inner Potential for species " & ips.tostring & " = " & _PhaseShifts(ips).MuffinTin.InnerPotential.ToString & " [Ry]")
			Console.WriteLine("Inner Potential for species " & ips.tostring & " = " & (13.6056923 *_PhaseShifts(ips).MuffinTin.InnerPotential).ToString & " [eV]")
			Console.WriteLine("Muffin-tin radius for species " & ips.tostring & " (Ry) = " & _PhaseShifts(ips).MuffinTin.Radius.ToString)
			Console.WriteLine("Muffin-tin radius for species " & ips.tostring & " (Angstroms) = " & (0.52917720859*_PhaseShifts(ips).MuffinTin.Radius).ToString)
			Console.WriteLine("Energy for phase shifts = " & _PhaseShifts(ips).Energy.tostring & " [Ry]")
			Console.WriteLine("Energy for phase shifts = " & (13.6056923 * _PhaseShifts(ips).Energy).tostring & " [eV]")
			
			Console.WriteLine("L     Delta_L                    Tmatrix_L")
			Console.WriteLine("----  -------------------------- ---------")
			For il As Integer = 0 To _PhaseShifts(ips).Lmax
				delta = _PhaseShifts(ips).ComplexValue(il, _OpticalPotential, _Temperature, _DebyeTemperature)
				tm = complex.CExp(complex.i*delta)*complex.CSin(delta)
				Console.WriteLine(il.ToString & "  " & delta.Tostring & "  " & tm.tostring)
			Next
			Console.WriteLine()
			Console.WriteLine("Scattering Factor:")
			PrintScatteringFactor(_PhaseShifts(ips).GetPhaseShifts(_OpticalPotential, _Temperature, _DebyeTemperature))
			Console.WriteLine()
		Next
		
		Dim _V0 As Double = _InnerPotential
		Dim _Voptical As Double = _OpticalPotential
		
        'If _UseAbInitioV0 Then _V0 = 
		
		Console.WriteLine("V0 = " & _InnerPotential.ToString & " eV")
		Console.WriteLine("Etotal (Ryd) = " & ((_Energy-_InnerPotential)/13.6056923).ToString & " Ry")
		
		
		'// Calculate the complex magnitude of k:
		_k = GetKValue(_Energy, _InnerPotential, _OpticalPotential)
		
		Console.WriteLine("k = " & _k.ToString)
		
		
		
		
		
		'// Create the path processors:\
		'// Note that the direct term gets added to the first PathProcessor in the list.
		Console.WriteLine("Creating path processors...")
		
		Dim PathProcessors As List(Of IMSPathProcessor) = _
			CreatePathProcessors(_NumberOfThreads, _Cluster, _AbsorberAtomID, _MaxPathLength, _MaxScatteringOrder, _k, _zv0, _RAOrder, _LMax)
		
		Console.WriteLine("Number of path processors = " & PathProcessors.Count.ToString)
		Console.WriteLine("Starting path calculations...")
		
		'// Start the PathProcessors going on multiple threads and wait for them to finish:
		Dim threads As New List(Of Thread)
		Dim thisThread As Thread
		Dim thisProcessor as IMSPathProcessor
		If _NumberOfThreads = 1 Then
			thisProcessor = PathProcessors(0)
			thisProcessor.Calculate
		Else
			For i As Integer = 0 To _NumberOfThreads - 1
				thisProcessor = PathProcessors(i)
				thisThread = New Thread(AddressOf thisProcessor.Calculate)
				thisThread.Start()
				threads.Add(thisThread)
			Next
		
			'// Wait for the threads to finish:
			For Each t As Thread In threads
				t.Join()
			Next

		End If
		
		'// Now list out amplitudes:
		Dim pathCounter As Integer = 0
		Dim pathAmp as Complex
		For i As Integer = 0 To _NumberOfThreads - 1
			thisProcessor = PathProcessors(i)
			Console.WriteLine("Path calculations complete.  Processor " & i.ToString & " calculated " & thisProcessor.NPathsCalculated.ToString & " paths.")
			For Each path As MSPath In thisProcessor.GetPaths
				pathCounter += 1
				If Not(path.Amplitude) Is Nothing Then
					pathAmp = path.Amplitude.Value(0,0)
				Else
					pathAmp = New Complex(0.0,0.0)
				End If
				Console.WriteLine(pathcounter.tostring & ",    " & path.ToString & ",    " & path.PathLength.ToString & ",   " & pathAmp.tostring)
			Next
		Next
		
		
		
		Console.WriteLine("Path calculations complete.  Starting terminations...")
		
		'// Now we'll construct the k-vector dependent results.  Set up a loop over theta and phi
		'// Future - enable a 2D mesh class for full hemisphere calculations.
		Dim thetamesh As New SimpleMesh(_startTheta,_endTheta,_NTheta)
		Dim phimesh As New SimpleMesh(_startPhi,_endPhi,_NPhi)
		Dim refractedTheta as Double
		Dim khat As Vector
		Dim allPaths As New List(Of MSPath)
		Dim alpha as ComplexMatrix
		Dim LM As Integer(,) = MSPathProcessor.LM(_Lmax)
		Dim AbsorberPS as PhaseShift = _PhaseShifts(_Cluster.Species(_absorberAtomID))
		Dim totalIntensity As Double
		
		Console.WriteLine("Number of theta points = " & thetamesh.Count.ToString)
		Console.WriteLine("Number of phi points = " & phimesh.Count.ToString)
		
		For Each theta As Double In thetamesh
			refractedTheta = Refract(theta,_Energy,_InnerPotential)
			Console.WriteLine("Theta = " & theta.ToString)
			Console.WriteLine("Refracted Theta = " & refractedTheta.ToString)
			For Each phi As Double In phimesh
				'Console.WriteLine()
				'Console.WriteLine("Phi = " & phi.ToString)
				
				'// Set the k vector direction:
				khat = Vector.NewFromSpherical(1.0,refractedTheta,phi)
				For Each pp As IMSPathProcessor In PathProcessors
					pp.Khat = khat
				Next
				
				'// Start the PathProcessors going on multiple threads and wait for them to finish:
				'// Clear the threads list:
				If _NumberOfThreads = 1 Then
					thisProcessor = PathProcessors(0)
					thisProcessor.CalculateTermination
				Else
					
					threads.clear
					For i As Integer = 0 To _NumberOfThreads - 1
						thisProcessor = PathProcessors(i)
						thisThread = New Thread(AddressOf thisProcessor.CalculateTermination)
						thisThread.Start()
						threads.Add(thisThread)
					Next
			
					'// Wait for the threads to finish:
					For Each t As Thread In threads
						t.Join()
					Next
				
				End If
				
				'// We should now have Alpha(L) for all paths, including the direct term.
				'// Sum all paths to arrive at the total amplitude.
				alpha = nothing
				For Each pp As IMSPathProcessor In PathProcessors
					For Each path As MSPath In pp.Getpaths
						If Not(path.Amplitude) Is Nothing Then
							pathAmp = path.Amplitude.Value(0,0)
						Else
							pathAmp = New Complex(0.0,0.0)
						End If
						if phi = 0.0 then Console.WriteLine(path.ToString & ",    " & pathAmp.tostring)

							If alpha Is Nothing Then
								alpha = path.Amplitude
							Else
								alpha += path.Amplitude
							End If

					Next
				Next
				
				'// Need from the absorber muffin-tin the integral of Pl*(r) Pl(r) from 0 to Rmt:
				'// at high values of l, this integral will tend towards zero.  How best to handle the
				'// normalization??  Answer - don't.  Let's calculate the betas first and later figure
				'// out how to handle the core wave functions.
		
				'// Now weight each amplitude by its contribution to the absorber atom total charge:
				totalIntensity = 0.0
				
				If _useL0 Then
					'totalIntensity = (alpha(0,0).Magnitude)^2
					totalIntensity = (alpha(0,0).Real)^2 + (alpha(0,0).Imag)^2
					'totalIntensity = AbsorberPS.TotalCharge(LM(0,0)) * (alpha(0,0).Magnitude)^2
					
				Else
					For iL As Integer = 0 To LM.Length\2 - 1
						totalIntensity += AbsorberPS.TotalCharge(LM(iL,0)) * (alpha(iL,0).Magnitude)^2
					Next
					
				End If
				
			
				'// Report out the amplitudes:
				Console.WriteLine(phi.ToString & "     " & totalIntensity.tostring)

				
			Next
		Next
			
	End Sub
	
	''' <summary>
	''' Calculates the phase shifts for each species in the cluster, and stores internally the muffin-tin potential and phaseshift object.
	''' </summary>
	''' <param name="c"></param>
	''' <param name="energy">Incident electron energy, in eV.</param>
	''' <param name="lmax"></param>
	''' <param name="voptical">The optical potential (expressed as a positive number), in eV.</param></param>
	''' <param name="temperature">The cluster temperature, in Kelvins.</param>
	''' <param name="debyetemperature">The debye temperature of the cluster, in Kelvins.</param>
	Public Sub CalculateClusterPhaseShifts(c as Cluster, energy as Double, lmax as integer, voptical as Double, temperature as Double, debyetemperature as double)
		'// 1) Calculate muffin tins and phase shifts for inequivalent atoms in the cluster:
		Dim mt As NeutralMuffinTin
		Dim species As ClusterSpecies
		Dim ps As PhaseShift
		Dim EnergyInRydbergs As Double = energy / 13.6056923
		Dim VopticalInRydbergs As Double = voptical / 13.6056923
		Dim MuffinTinRadiusInBohrRadii as Double
		For Each speciesID As Integer In c.GetSpeciesIDs
			'// Get species data:
			species = c.GetSpecies(speciesID)
			
			MuffinTinRadiusInBohrRadii = species.MuffinTinRadius / 0.52917720859
			
			'// Calculate Pendry muffin-tin:
			mt = New NeutralMuffinTin(species.AtomicNumber, MuffinTinRadiusInBohrRadii, species.Configuration)
			
			'// Save the muffin tin for future reference:
			_MuffinTins.Add(speciesID, mt)
			
			'// Calculate phase shifts for this muffin-tin:
			ps = New PhaseShift(EnergyInRydbergs, lmax, mt)
			
			'// Save the phase shifts for future reference:
			_PhaseShifts.Add(speciesID,ps)
			
			'// Cache arrays of the phase shifts and the t-matrices into the cluster object:
			c.PhaseShifts(speciesID) = ps.GetPhaseShifts(VopticalInRydbergs,temperature,debyetemperature)
			
		Next
	End Sub
	
	''' <summary>
    ''' Calculates phase shifts using FEFF
	''' </summary>
	''' <param name="c"></param>
	''' <param name="energy">Incident electron energy, in eV</param>
	''' <param name="lmax">Maximum angular momentum</param>
 	''' <param name="temperature">Cluster temperature, in Kelvins</param>
	''' <param name="debyetemperature">Debye temperature of the cluster, in Kelvins</param>
	Public Sub CalculateClusterPhaseShifts2(c as Cluster, energy as Double, lmax as integer, temperature as Double, debyetemperature as double)
		'// 1) Calculate phase shifts for inequivalent atoms in the cluster:
		Dim species As ClusterSpecies
		Dim EnergyInRydbergs As Double = energy / 13.6056923
		Dim ps As IPhaseShiftProvider
		
		'// Run a FEFF calculation on the cluster:
		Dim FEFF As New FEFFCalculation(_FEFFDirectory,c,temperature,debyetemperature)
					
		For Each speciesID As Integer In c.GetSpeciesIDs
			
			'// Cache arrays of the phase shifts into the cluster object (it calculates t-matrices internally):
            c.PhaseShifts(speciesID) = FEFF.GetPhaseShifts(speciesID, lmax)
			
		Next
	End Sub	
	
	''' <summary>
	''' Returns a List sorted by path length, containing MSPath objects for each single-scattering path in the cluster.
	''' Also includes the direct path (containing only absorber atom).
	''' </summary>
	''' <param name="c"></param>
	''' <param name="absorberid"></param>
	''' <param name="maxlength"></param>
	''' <returns></returns>
	Public Function GetSinglePaths(c as Cluster, absorberid as Integer, maxlength As Double) As List(Of MSPath)
			
		Dim Retval As New List(Of MSPath)
		
		'// Add the direct term to the list:
		Dim directpath As New MSPath(c,absorberid)
		
		Retval.Add(directpath)
		
		Dim AtomIDs As List(Of Integer) = c.GetAtomIDs
		
		
		Dim path As MSPath
		Dim length as double
		For Each id As Integer In AtomIDs
			If ((id <> absorberid) andalso c.AtomEnabled(id)) Then
				path = New MSPath(directpath, c, id)
				length = path.PathLength
				If length <= maxlength Then
					Retval.Add(path)
				End If
			End If
		Next
		
		Retval.Sort
		
		Return Retval
		
	End Function

	Public Function CreatePathProcessors(nthreads as integer, c as Cluster, absorberid as Integer, maxlength As Double, maxorder as Integer, k as Complex, zv0 as Double, raorder as Integer, lmax as integer) As List(Of IMSPathProcessor)
		
		Console.WriteLine("Creating Path Processors...")
		
		'// Create a list of all single-scattering paths, sorted by path length
		Dim SinglePaths As List(Of MSPath) = GetSinglePaths(c, absorberid, maxlength)
		
		Console.WriteLine("List of all single-scattering paths:")
		Dim count as Integer = 0
		For Each path As MSPath In SinglePaths
			count += 1
			Console.WriteLine(count.ToString & ",    " & path.ToString & ",    " & path.PathLength.tostring)
		Next
		Console.WriteLine("End of single-scattering path list in CreatePathProcessors")
		Console.WriteLine()
		
		'// Partition the single-scattering paths between the MSPathProcessors
		Dim PathLists(nthreads-1) As List(Of MSPath)
		Dim ThisPathList as List(of MSPath)
		For i As Integer = 0 To nthreads - 1
			'// Create a new list for paths:
			ThisPathList = New List(Of MSPath)
			'// Add paths to the list:
			For j As Integer = i To SinglePaths.Count - 1 Step nthreads
				ThisPathList.Add(SinglePaths.Item(j))
			Next
			'// Add the list of paths to the collection:
			PathLists(i) = ThisPathList
		Next
				
		'// Create the path processors:
		Dim PathProcessors As New List(Of IMSPathProcessor)
		Dim thisProcessor as IMSPathProcessor
		For i As Integer = 0 To nthreads - 1
			thisProcessor = New MSPathProcessor2(c, PathLists(i), maxlength, k, zv0, raorder, maxorder, lmax)
			PathProcessors.Add(thisProcessor)
		Next

		Return PathProcessors
		
	End Function
	
	
	Public Function GetKValue(energy As Double, V0r As Double, V0i As Double) As Complex
		
		Dim E as Complex = New Complex(energy - V0r,V0i)
		'Dim k as Complex = Complex.CSqrt(E)
		Dim k as Complex = New Complex(Sqrt(energy - V0r),sqrt(V0i))
		
		'// Convert to inverse Angstom units (since bond distances in the cluster are in Angstroms)
		Return 0.5123 * k
		
	End Function
	
	''' <summary>
	''' Applies snell's law to compute the refracted angle theta:
	''' </summary>
	''' <param name="thetaLab"></param>
	''' <param name="E"></param>
	''' <param name="V0"></param>
	''' <returns></returns>
	Public Function Refract(thetaLab As Double, E As Double, V0 As Double) As Double
		Return System.Math.PI - Asin(Sin(thetalab)*Sqrt(E/(E-V0)))
	End Function
	
	
	Public Sub PrintScatteringFactor(phaseshifts As Complex())
		
		Dim theta As Double
		Dim Ntheta As Integer = 100
		Dim f as Complex
		For i As Integer = 0 To Ntheta
			theta = (CDbl(i)/CDbl(Ntheta))*system.Math.pi
			f = new Complex(0.0,0.0)
			For l As Integer = 0 To phaseshifts.Length-1
                f += CDbl(2 * l + 1) * Complex.CExp(Complex.i * phaseshifts(l)) * Complex.CSin(phaseshifts(l)) * DESD.Math.Functions.Legendre(l, 0, System.Math.Cos(theta))
			Next
			Console.WriteLine(theta.ToString & "   " & f.Magnitude.ToString)
		Next
	End Sub

End Class
