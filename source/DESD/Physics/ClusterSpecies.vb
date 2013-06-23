

''' <summary>
'''
''' </summary>
Public Structure ClusterSpecies
	
	Private _atomicNumber As Element
	Private _configuration As ElectronicConfiguration
	
	''' <summary>
	''' The radius of the muffin-tin, in Angstroms.
	''' </summary>
	Private _muffintinradius As Double
	
	#Region "Constructors"
	
	''' <summary>
	''' 
	''' </summary>
	''' <param name="atomicNumber"></param>
	''' <param name="configuration"></param>
	''' <param name="muffintinradius">The radius of the muffin-tin, in Angstroms.</param>
	Sub New(atomicNumber As Element, configuration As ElectronicConfiguration, muffintinradius As Double)
		_atomicNumber = atomicnumber
		_configuration = configuration
		_muffintinradius = muffintinradius
	End Sub
	
	''' <summary>
	''' 
	''' </summary>
	''' <param name="atomicNumber"></param>
	''' <param name="configuration"></param>
	''' <param name="muffintinradius">The radius of the muffin-tin, in Angstroms.</param>
	Sub New(atomicNumber As Element, configuration As string, muffintinradius As Double)
		_atomicNumber = atomicnumber
		If configuration = "" Then
			_configuration = atomicnumber.configuration
		Else
			_configuration = New ElectronicConfiguration(configuration)	
		End If
		_muffintinradius = muffintinradius
	End Sub
	
	''' <summary>
	''' 
	''' </summary>
	''' <param name="atomicNumber"></param>
	''' <param name="muffintinradius">The radius of the muffin-tin, in Angstroms.</param>
	Sub New(atomicNumber As Element, muffintinradius As Double)
		_atomicNumber = atomicnumber
		_configuration = atomicnumber.Configuration
		_muffintinradius = muffintinradius
	End Sub

	#End Region

	Public ReadOnly Property AtomicNumber As Element
		Get
			Return _atomicNumber
		End Get
	End Property
	
	Public ReadOnly Property Configuration As ElectronicConfiguration
		Get
			Return _configuration
		End Get
	End Property
	
	''' <summary>
	''' The radius of the muffin-tin, in Angstroms.
	''' </summary>
	Public ReadOnly Property MuffinTinRadius As Double
		Get
			Return _MuffinTinRadius
		End Get
	End Property
	
	Public Shared Operator =(ByVal s1 As ClusterSpecies, ByVal s2 As ClusterSpecies) As Boolean
        Return (s1.AtomicNumber = s2.AtomicNumber) AndAlso (s1.Configuration = s2.Configuration) AndAlso (s1.MuffinTinRadius = s2.MuffinTinRadius)
    End Operator

    Public Shared Operator <>(ByVal s1 As ClusterSpecies, ByVal s2 As ClusterSpecies) As Boolean
        Return Not(s1 = s2)
    End Operator

End Structure
