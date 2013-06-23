

''' <summary>
''' Ground state configurations taken from:
''' http://hermes.phys.uwm.edu/projects/elecstruct/hermsk/PeriodicTable.html and 
''' Wikipedia "Periodic Table" entry + individual element pages.
''' Atomic weight data taken from the same Wikipedia source.
''' </summary>
    Public Enum Element
        <Symbol("H"), GroundStateConfiguration("1s1"), AtomicWeight(1.00794)> Hydrogen = 1
        <Symbol("He"), GroundStateConfiguration("1s2"), AtomicWeight(4.002602)> Helium = 2
        <Symbol("Li"), GroundStateConfiguration("[He] 2s1"), AtomicWeight(6.941)> Lithium = 3
        <Symbol("Be"), GroundStateConfiguration("[He] 2s2"), AtomicWeight(9.102182)> Beryllium = 4
        <Symbol("B"), GroundStateConfiguration("[He] 2s2 2p1"), AtomicWeight(10.811)> Boron = 5
        <Symbol("C"), GroundStateConfiguration("[He] 2s2 2p2"), AtomicWeight(12.0107)> Carbon = 6
        <Symbol("N"), GroundStateConfiguration("[He] 2s2 2p3"), AtomicWeight(14.0067)> Nitrogen = 7
        <Symbol("O"), GroundStateConfiguration("[He] 2s2 2p4"), AtomicWeight(15.9994)> Oxygen = 8
        <Symbol("F"), GroundStateConfiguration("[He] 2s2 2p5"), AtomicWeight(18.9984032)> Flourine = 9
        <Symbol("Ne"), GroundStateConfiguration("[He] 2s2 2p6"), AtomicWeight(20.1797)> Neon = 10
        <Symbol("Na"), GroundStateConfiguration("[Ne] 3s1"), AtomicWeight(22.98976928)> Sodium = 11
        <Symbol("Mg"), GroundStateConfiguration("[Ne] 3s2"), AtomicWeight(24.3050)> Magnesium = 12
        <Symbol("Al"), GroundStateConfiguration("[Ne] 3s2 3p1"), AtomicWeight(26.9815386)> Aluminum = 13
        <Symbol("Si"), GroundStateConfiguration("[Ne] 3s2 3p2"), AtomicWeight(28.0855)> Silicon = 14
        <Symbol("P"), GroundStateConfiguration("[Ne] 3s2 3p3"), AtomicWeight(30.973762)> Phosphorous = 15
        <Symbol("S"), GroundStateConfiguration("[Ne] 3s2 3p4"), AtomicWeight(32.065)> Sulfur = 16
        <Symbol("Cl"), GroundStateConfiguration("[Ne] 3s2 3p5"), AtomicWeight(35.453)> Chlorine = 17
        <Symbol("Ar"), GroundStateConfiguration("[Ne] 3s2 3p6"), AtomicWeight(39.948)> Argon = 18
        <Symbol("K"), GroundStateConfiguration("[Ar] 4s1"), AtomicWeight(39.0983)> Potassium = 19
        <Symbol("Ca"), GroundStateConfiguration("[Ar] 4s2"), AtomicWeight(40.078)> Calcium = 20
        <Symbol("Sc"), GroundStateConfiguration("[Ar] 3d1 4s2"), AtomicWeight(44.955912)> Scanadium = 21
        <Symbol("Ti"), GroundStateConfiguration("[Ar] 3d2 4s2"), AtomicWeight(47.867)> Titanium = 22
        <Symbol("V"), GroundStateConfiguration("[Ar] 3d3 4s2"), AtomicWeight(50.9415)> Vanadium = 23
        <Symbol("Cr"), GroundStateConfiguration("[Ar] 3d4 4s2"), AtomicWeight(51.9961)> Chromium = 24
        <Symbol("Mn"), GroundStateConfiguration("[Ar] 3d5 4s2"), AtomicWeight(54.938045)> Manganese = 25
        <Symbol("Fe"), GroundStateConfiguration("[Ar] 3d6 4s2"), AtomicWeight(55.845)> Iron = 26
        <Symbol("Co"), GroundStateConfiguration("[Ar] 3d7 4s2"), AtomicWeight(58.933195)> Cobalt = 27
        <Symbol("Ni"), GroundStateConfiguration("[Ar] 3d8 4s2"), AtomicWeight(58.6934)> Nickel = 28
        <Symbol("Cu"), GroundStateConfiguration("[Ar] 3d10 4s1"), AtomicWeight(63.546)> Copper = 29
        <Symbol("Zn"), GroundStateConfiguration("[Ar] 3d10 4s2"), AtomicWeight(65.38)> Zinc = 30
        <Symbol("Ga"), GroundStateConfiguration("[Ar] 3d10 4s2 4p1"), AtomicWeight(69.723)> Gallium = 31
        <Symbol("Ge"), GroundStateConfiguration("[Ar] 3d10 4s2 4p2"), AtomicWeight(72.64)> Germanium = 32
        <Symbol("As"), GroundStateConfiguration("[Ar] 3d10 4s2 4p3"), AtomicWeight(74.92160)> Arsenic = 33
        <Symbol("Se"), GroundStateConfiguration("[Ar] 3d10 4s2 4p4"), AtomicWeight(78.96)> Selenium = 34
        <Symbol("Br"), GroundStateConfiguration("[Ar] 3d10 4s2 4p5"), AtomicWeight(79.904)> Bromine = 35
        <Symbol("Kr"), GroundStateConfiguration("[Ar] 3d10 4s2 4p6"), AtomicWeight(83.798)> Krypton = 36
        <Symbol("Rb"), GroundStateConfiguration("[Kr] 5s1"), AtomicWeight(85.4678)> Rubidium = 37
        <Symbol("Sr"), GroundStateConfiguration("[Kr] 5s2"), AtomicWeight(87.62)> Strontium = 38
        <Symbol("Y"), GroundStateConfiguration("[Kr] 4d1 5s2"), AtomicWeight(88.90585)> Yttrium = 39
        <Symbol("Zr"), GroundStateConfiguration("[Kr] 4d2 5s2"), AtomicWeight(91.224)> Zirconium = 40
        <Symbol("Nb"), GroundStateConfiguration("[Kr] 4d4 5s1"), AtomicWeight(92.90638)> Niobium = 41
        <Symbol("Mo"), GroundStateConfiguration("[Kr] 4d5 5s1"), AtomicWeight(95.94)> Molybdenum = 42
        <Symbol("Tc"), GroundStateConfiguration("[Kr] 4d6 5s1"), AtomicWeight(98.0)> Technetium = 43
        <Symbol("Ru"), GroundStateConfiguration("[Kr] 4d7 5s1"), AtomicWeight(101.07)> Ruthenium = 44
        <Symbol("Rh"), GroundStateConfiguration("[Kr] 4d8 5s1"), AtomicWeight(102.90550)> Rhodium = 45
        <Symbol("Pd"), GroundStateConfiguration("[Kr] 4d10"), AtomicWeight(106.42)> Palladium = 46
        <Symbol("Ag"), GroundStateConfiguration("[Kr] 4d10 5s1"), AtomicWeight(107.8682)> Silver = 47
        <Symbol("Cd"), GroundStateConfiguration("[Kr] 4d10 5s2"), AtomicWeight(112.411)> Cadmium = 48
        <Symbol("In"), GroundStateConfiguration("[Kr] 4d10 5s2 5p1"), AtomicWeight(114.818)> Indium = 49
        <Symbol("Sn"), GroundStateConfiguration("[Kr] 4d10 5s2 5p2"), AtomicWeight(118.710)> Tin = 50
        <Symbol("Sb"), GroundStateConfiguration("[Kr] 4d10 5s2 5p3"), AtomicWeight(121.760)> Antimony = 51
        <Symbol("Te"), GroundStateConfiguration("[Kr] 4d10 5s2 5p4"), AtomicWeight(127.6)> Tellurium = 52
        <Symbol("I"), GroundStateConfiguration("[Kr] 4d10 5s2 5p5"), AtomicWeight(126.90447)> Iodine = 53
        <Symbol("Xe"), GroundStateConfiguration("[Kr] 4d10 5s2 5p6"), AtomicWeight(131.293)> Xenon = 54
        <Symbol("Cs"), GroundStateConfiguration("[Xe] 6s1"), AtomicWeight(132.9054519)> Cesium = 55
        <Symbol("Ba"), GroundStateConfiguration("[Xe] 6s2"), AtomicWeight(137.33)> Barium = 56
        <Symbol("La"), GroundStateConfiguration("[Xe] 5d1 6s2"), AtomicWeight(138.90547)> Lanthanum = 57
        <Symbol("Ce"), GroundStateConfiguration("[Xe] 4f1 5d1 6s2"), AtomicWeight(140.116)> Cerium = 58
        <Symbol("Pr"), GroundStateConfiguration("[Xe] 4f3 6s2"), AtomicWeight(140.90765)> Praseodymium = 59
        <Symbol("Nd"), GroundStateConfiguration("[Xe] 4f4 6s2"), AtomicWeight(144.242)> Neodymium = 60
        <Symbol("Pm"), GroundStateConfiguration("[Xe] 4f5 6s2"), AtomicWeight(145.0)> Promethium = 61
        <Symbol("Sm"), GroundStateConfiguration("[Xe] 4f6 6s2"), AtomicWeight(150.36)> Samarium = 62
        <Symbol("Eu"), GroundStateConfiguration("[Xe] 4f7 6s2"), AtomicWeight(151.964)> Europium = 63
        <Symbol("Gd"), GroundStateConfiguration("[Xe] 4f7 5d1 6s2"), AtomicWeight(157.25)> Gadolinium = 64
        <Symbol("Tb"), GroundStateConfiguration("[Xe] 4f9 6s2"), AtomicWeight(158.92535)> Terbium = 65
        <Symbol("Dy"), GroundStateConfiguration("[Xe] 4f10 6s2"), AtomicWeight(162.5)> Dysprosium = 66
        <Symbol("Ho"), GroundStateConfiguration("[Xe] 4f11 6s2"), AtomicWeight(164.93032)> Holmium = 67
        <Symbol("Er"), GroundStateConfiguration("[Xe] 4f12 6s2"), AtomicWeight(167.259)> Erbium = 68
        <Symbol("Tm"), GroundStateConfiguration("[Xe] 4f13 6s2"), AtomicWeight(168.93421)> Thulium = 69
        <Symbol("Yb"), GroundStateConfiguration("[Xe] 4f14 6s2"), AtomicWeight(173.04)> Ytterbium = 70
        <Symbol("Lu"), GroundStateConfiguration("[Xe] 4f14 5d1 6s2"), AtomicWeight(174.967)> Lutetium = 71
        <Symbol("Hf"), GroundStateConfiguration("[Xe] 4f14 5d2 6s2"), AtomicWeight(178.49)> Hafnium = 72
        <Symbol("Ta"), GroundStateConfiguration("[Xe] 4f14 5d3 6s2"), AtomicWeight(180.94788)> Tantalum = 73
        <Symbol("W"), GroundStateConfiguration("[Xe] 4f14 5d4 6s2"), AtomicWeight(183.84)> Tungsten = 74
        <Symbol("Re"), GroundStateConfiguration("[Xe] 4f14 5d5 6s2"), AtomicWeight(186.207)> Rhenium = 75
        <Symbol("Os"), GroundStateConfiguration("[Xe] 4f14 5d6 6s2"), AtomicWeight(190.23)> Osmium = 76
        <Symbol("Ir"), GroundStateConfiguration("[Xe] 4f14 5d7 6s2"), AtomicWeight(192.217)> Iridium = 77
        <Symbol("Pt"), GroundStateConfiguration("[Xe] 4f14 5d9 6s1"), AtomicWeight(195.084)> Platinum = 78
        <Symbol("Au"), GroundStateConfiguration("[Xe] 4f14 5d10 6s1"), AtomicWeight(186.966569)> Gold = 79
        <Symbol("Hg"), GroundStateConfiguration("[Xe] 4f14 5d10 6s2"), AtomicWeight(200.59)> Mercury = 80
        <Symbol("Tl"), GroundStateConfiguration("[Xe] 4f14 5d10 6s2 6p1"), AtomicWeight(204.3883)> Thallium = 81
        <Symbol("Pb"), GroundStateConfiguration("[Xe] 4f14 5d10 6s2 6p2"), AtomicWeight(207.2)> Lead = 82
        <Symbol("Bi"), GroundStateConfiguration("[Xe] 4f14 5d10 6s2 6p3"), AtomicWeight(208.98040)> Bismuth = 83
        <Symbol("Po"), GroundStateConfiguration("[Xe] 4f14 5d10 6s2 6p4"), AtomicWeight(209)> Polonium = 84
        <Symbol("At"), GroundStateConfiguration("[Xe] 4f14 5d10 6s2 6p5"), AtomicWeight(210)> Astatine = 85
        <Symbol("Rn"), GroundStateConfiguration("[Xe] 4f14 5d10 6s2 6p6"), AtomicWeight(222)> Radon = 86
        <Symbol("Fr"), GroundStateConfiguration("[Rn] 7s1"), AtomicWeight(223)> Francium = 87
        <Symbol("Ra"), GroundStateConfiguration("[Rn] 7s2"), AtomicWeight(226)> Radium = 88
        <Symbol("Ac"), GroundStateConfiguration("[Rn] 6d1 7s2"), AtomicWeight(227)> Actinium = 89
        <Symbol("Th"), GroundStateConfiguration("[Rn] 6d2 7s2"), AtomicWeight(232.0381)> Thorium = 90
        <Symbol("Pa"), GroundStateConfiguration("[Rn] 5f2 6d1 7s2"), AtomicWeight(231.03588)> Protactinium = 91
        <Symbol("U"), GroundStateConfiguration("[Rn] 5f3 6d1 7s2"), AtomicWeight(238.02891)> Uranium = 92
        <Symbol("Np"), GroundStateConfiguration("[Rn] 5f4 6d1 7s2"), AtomicWeight(237)> Neptunium = 93
        <Symbol("Pu"), GroundStateConfiguration("[Rn] 5f6 7s2"), AtomicWeight(244)> Plutonium = 94
        <Symbol("Am"), GroundStateConfiguration("[Rn] 5f7 7s2"), AtomicWeight(243)> Americium = 95
        <Symbol("Cm"), GroundStateConfiguration("[Rn] 5f7 6d1 7s2"), AtomicWeight(247)> Curium = 96
        <Symbol("Bk"), GroundStateConfiguration("[Rn] 5f9 7s2"), AtomicWeight(247)> Berkelium = 97
        <Symbol("Cf"), GroundStateConfiguration("[Rn] 5f10 7s2"), AtomicWeight(251)> Californium = 98
        <Symbol("Es"), GroundStateConfiguration("[Rn] 5f11 7s2"), AtomicWeight(252)> Einsteinium = 99
        <Symbol("Fm"), GroundStateConfiguration("[Rn] 5f12 7s2"), AtomicWeight(257)> Fermium = 100
        <Symbol("Md"), GroundStateConfiguration("[Rn] 5f13 7s2"), AtomicWeight(258)> Mendelevium = 101
        <Symbol("No"), GroundStateConfiguration("[Rn] 5f14 7s2"), AtomicWeight(259)> Nobelium = 102
        <Symbol("Lr"), GroundStateConfiguration("[Rn] 5f14 7s2 7p1"), AtomicWeight(262)> Lawrencium = 103
        <Symbol("Rf"), GroundStateConfiguration("[Rn] 5f14 6d2 7s2"), AtomicWeight(267)> Rutherfordium = 104
        <Symbol("Db"), GroundStateConfiguration("[Rn] 5f14 6d3 7s2"), AtomicWeight(268)> Dubnium = 105
        <Symbol("Sg"), GroundStateConfiguration("[Rn] 5f14 6d4 7s2"), AtomicWeight(271)> Seaborgium = 106
        <Symbol("Bh"), GroundStateConfiguration("[Rn] 5f14 6d5 7s2"), AtomicWeight(270)> Borhium = 107
        <Symbol("Hs"), GroundStateConfiguration("[Rn] 5f14 6d6 7s2"), AtomicWeight(269)> Hassium = 108
        <Symbol("Mt"), GroundStateConfiguration("[Rn] 5f14 6d7 7s2"), AtomicWeight(278)> Meitnerium = 109
        <Symbol("Ds"), GroundStateConfiguration("[Rn] 5f14 6d9 7s1"), AtomicWeight(281)> Darmstadtium = 110
        <Symbol("Rg"), GroundStateConfiguration("[Rn] 5f14 6d10 7s1"), AtomicWeight(281)> Roentgenium = 111
        <Symbol("Cn"), GroundStateConfiguration("[Rn] 5f14 6d10 7s2"), AtomicWeight(285)> Copernicium = 112

    End Enum
    
    
<AttributeUsage(AttributeTargets.Field)> _
Public Class SymbolAttribute
    Inherits System.Attribute

    Private _value As String

    Sub New(ByVal value As String)
        _value = value
    End Sub

    Public Property Value() As String
        Get
            Return _value
        End Get
        Set(ByVal value As String)
            _value = value
        End Set
    End Property

End Class



''' <summary>
''' A custom attribute for use with the Element enumeration extension methods.  Encapsulates
''' the ground state electronic configuration of the element.
''' </summary>
<AttributeUsage(AttributeTargets.Field)> _
Public Class GroundStateConfigurationAttribute
    Inherits System.Attribute

    Private _value As ElectronicConfiguration

    Sub New(ByVal value As String)
        _value = New ElectronicConfiguration(value)
    End Sub

    Public Property Value() As ElectronicConfiguration
        Get
            Return _value
        End Get
        Set(ByVal value As ElectronicConfiguration)
            _value = value
        End Set
    End Property

End Class

''' <summary>
''' A custom attribute for use with the Element enumeration extension methods.  Encapsulates
''' the atomic weight of the element, given in grams / mol.
''' </summary>
<AttributeUsage(AttributeTargets.Field)> _
Public Class AtomicWeightAttribute
    Inherits System.Attribute

    Private _value As Double

    Sub New(ByVal value As Double)
        _value = value
    End Sub

    Public Readonly Property InGramsPerMole() As Double
        Get
            Return _value
        End Get
    End Property
    
    Public ReadOnly Property InKilograms As Double
    	Get
            Return 0.001 * _value / Constants.AvogadrosNumber
    	End Get
    End Property
    
    Public ReadOnly Property InElectronMassUnits As Double
    	Get
            Return Me.InKilograms / Constants.ElectronMass
    	End Get
    End Property

End Class

''' <summary>
''' Contains definitions of extension methods for the Element enumeration.
''' </summary>
Public Module ElementExtensions

        ''' <summary>
        ''' Returns the symbol of the given element enumeration value as a string.
        ''' </summary>
        ''' <param name="u"></param>
        ''' <returns></returns>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function Symbol(ByVal u As Element) As String
            Dim attrs As SymbolAttribute() = DirectCast(u.GetType.GetField(u.ToString()).GetCustomAttributes(GetType(SymbolAttribute), False), SymbolAttribute())
            If attrs.Length > 0 Then
                Return attrs(0).Value
            Else
                Return ""
            End If
        End Function
        
        ''' <summary>
        ''' Returns the ground state configuration of the element as an ElectronicConfiguration instance.
        ''' </summary>
        ''' <param name="u"></param>
        ''' <returns></returns>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function Configuration(ByVal u As Element) As ElectronicConfiguration
            Dim attrs As GroundStateConfigurationAttribute() = DirectCast(u.GetType.GetField(u.ToString()).GetCustomAttributes(GetType(GroundStateConfigurationAttribute), False), GroundStateConfigurationAttribute())
            If attrs.Length > 0 Then
                Return attrs(0).Value
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Returns the atomic weight of the element in electron mass units.
        ''' </summary>
        ''' <param name="u"></param>
        ''' <returns></returns>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function ElectronMasses(ByVal u As Element) As Double
            Dim attrs As AtomicWeightAttribute() = DirectCast(u.GetType.GetField(u.ToString()).GetCustomAttributes(GetType(AtomicWeightAttribute), False), AtomicWeightAttribute())
            If attrs.Length > 0 Then
                Return attrs(0).InElectronMassUnits
            Else
                Return double.nan
            End If
        End Function

        ''' <summary>
        ''' Returns the atomic weight of the element in g/mol.
        ''' </summary>
        ''' <param name="u"></param>
        ''' <returns></returns>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function AtomicWeight(ByVal u As Element) As Double
            Dim attrs As AtomicWeightAttribute() = DirectCast(u.GetType.GetField(u.ToString()).GetCustomAttributes(GetType(AtomicWeightAttribute), False), AtomicWeightAttribute())
            If attrs.Length > 0 Then
                Return attrs(0).InGramsPerMole
            Else
                Return double.nan
            End If
        End Function

    End Module


