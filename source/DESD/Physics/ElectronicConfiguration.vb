


    ''' <summary>
    ''' Represents the electronic configuration of an atom.
    ''' </summary>
    ''' <remarks>
    ''' Configuration can be represented as a text string of delimited orbital
    ''' for example, the ground state of carbon can be given as "1s2 2s2 2p2"
    ''' </remarks>
    Public Class ElectronicConfiguration

        Private mConfiguration As New Dictionary(Of String, Double)
        Private mTotalOccupancy As Double = 0.0
        Private mNMax As Integer = 0
        Private mLMax as Integer = 0

#Region "Constructors"

        Sub New(ByVal configuration As String)

            Parse(configuration)

        End Sub

        Sub New(ByVal z As Element)
            Dim configuration As String = "[" & z.Symbol() & "]"
            Parse(configuration)
        End Sub


#End Region

		''' <summary>
		''' Parses the input configuration expressed as a string into its constiuent
		''' orbitals and occupancies.
		''' </summary>
		''' <param name="configuration">The electronic configuration string.</param>
        Private Sub Parse(ByVal configuration As String)
            '// Parse from left to right, split on space delimiter

            '// Check the trivial case (empty string)
            If configuration = "" Or (configuration Is Nothing) Then
                '// Assume an empty configuration:
                Exit Sub
            End If
            '// Need a regular expression check here to ensure valid format,
            '// and throw an exception if invalid.

            '// First, trim off any leading or trailing spaces - convert to all uppercase while we're at it:
            Dim input As String = configuration.Trim()

            '// Determine if the configuration has one or more element references in it somewhere, ex: "[He]".
            '// If it does, replace the reference with the full configuration:
            '// NOTE - generalized to include reference to any element:
            '// NOTE - note that while the GetElementConfiguration method is not supposed to return
            '//        any bracketed references, I'm adding a DO WHILE loop here to handle the special
            '//        case where it *could* recurse.

            Dim istart As Integer
            Dim iend As Integer
            Dim instring As String

            Dim openBracket As Char = CChar("[")
            Dim closeBracket As Char = CChar("]")
            Dim alphaString As String = "abcdefghijklmnopqrstuvwxyz"
            Dim alphaChars As Char() = alphaString.ToCharArray
            Dim space As Char = CChar(" ")
            Dim temp As String

            Do While input.Contains(openBracket)
                '// Get the inner string to replace, delimited by "[]" brackets:
                istart = input.IndexOf(openBracket)
                iend = input.IndexOf(closeBracket)
                instring = input.Substring(istart, (iend - istart) + 1)
                temp = GetElementConfiguration(instring)
                input = input.Replace(instring, temp)

            Loop

            '// Convert the whole string to lower case:
            input = input.ToLower()

            '// Clear the internal collection:
            mConfiguration.Clear()
            mTotalOccupancy = 0.0
            mNMax = 0
			mLMax = 0
			
            Dim segments As String() = input.Split(space)
            Dim segment As String
            Dim angmom As String
            Dim occupation As Double
            Dim quantumn As Integer
            Dim orbital As String
            Dim iAngMom As Integer
            Dim iLast As Integer
            Dim temp2 As Double


            For i As Integer = 0 To segments.Length - 1
                segment = segments(i).Trim()

                '// orbital is defined by the primary quantum number and
                '// the angular momentum letter - everything out to and
                '// including the first non-numeric character:

                '// First, get the index of the first non-numeric character:
                '// This should be the angular momentum, and should be only 1 character.
                '// Actually, this is where a regular expression would really be good:
                iAngMom = segment.IndexOfAny(alphaChars)

                '// Get the highest index.
                iLast = segment.Length - 1

                '// Clip out the important quantities:

                '// First, the quantum number N:
                Try
                    quantumn = CInt(segment.Substring(0, iAngMom))
                Catch ex As Exception
                    Throw New InvalidExpressionException("Invalid primary quantum number format in: " & segment)
                End Try

                '// Next, the angular momentum:
                Try
                    angmom = segment.Substring(iAngMom, 1)
                Catch ex As Exception
                    Throw New InvalidExpressionException("Invalid angular momentum format in: " & segment)
                End Try

                '// Get the whole orbital as a string:
                Try
                    orbital = segment.Substring(0, iAngMom + 1)
                Catch ex As Exception
                    Throw New InvalidExpressionException("Invalid angular momentum format in: " & segment)
                End Try

                '// Now get the occupation
                '// This is tricky - occupation might not be there.  Need to account for the case
                '// where the occupation is omitted (and assumed to be 1.0)
                If iLast = iAngMom Then
                    '// Occupation is omitted.
                    occupation = 1.0
                Else
                    occupation = CDbl(segment.Substring(iAngMom + 1))
                End If


                mTotalOccupancy += occupation

                If quantumn > mNMax Then mNMax = quantumn
                If GetIntegralValue(angmom) > mLMax then mLMax = GetIntegralValue(angmom)

                If mConfiguration.ContainsKey(orbital) Then
                    temp2 = mConfiguration(orbital)
                    mConfiguration(orbital) = temp2 + occupation
                Else
                    mConfiguration.Add(orbital, occupation)
                End If
            Next



        End Sub


        Private Function GetElementConfiguration(ByVal symbol As String) As String

            '// First, strip off any leading or trailing delimiter characters:
            Static trimChars As Char() = {CChar("["), CChar("]")}

            Dim text As String = symbol.Trim(trimChars).ToLower()

            Select Case text

                Case "h", "hydrogen"
                    Return "1s1"
                Case "he", "helium"
                    Return "1s2"
                Case "li", "lithium"
                    Return GetElementConfiguration("he") & " 2s1"
                Case "be", "beryllium"
                    Return GetElementConfiguration("he") & " 2s2"
                Case "b", "boron"
                    Return GetElementConfiguration("he") & " 2s2 2p1"
                Case "c", "carbon"
                    Return GetElementConfiguration("he") & " 2s2 2p2"
                Case "n", "nitrogen"
                    Return GetElementConfiguration("he") & " 2s2 2p3"
                Case "o", "oxygen"
                    Return GetElementConfiguration("he") & " 2s2 2p4"
                Case "f", "flourine"
                    Return GetElementConfiguration("he") & " 2s2 2p5"
                Case "ne", "neon"
                    Return GetElementConfiguration("he") & " 2s2 2p6"
                Case "na", "sodium"
                    Return GetElementConfiguration("ne") & " 3s1"
                Case "mg", "magnesium"
                    Return GetElementConfiguration("ne") & " 3s2"
                Case "al", "aluminum"
                    Return GetElementConfiguration("ne") & " 3s2 3p1"
                Case "si", "silicon"
                    Return GetElementConfiguration("ne") & " 3s2 3p2"
                Case "p", "phosphorus"
                    Return GetElementConfiguration("ne") & " 3s2 3p3"
                Case "s", "sulfur"
                    Return GetElementConfiguration("ne") & " 3s2 3p4"
                Case "cl", "chlorine"
                    Return GetElementConfiguration("ne") & " 3s2 3p5"
                Case "ar", "argon"
                    Return GetElementConfiguration("ne") & " 3s2 3p6"
                Case "k", "potassium"
                    Return GetElementConfiguration("ar") & " 4s1"
                Case "ca", "calcium"
                    Return GetElementConfiguration("ar") & " 4s2"
                Case "sc", "scanadium"
                    Return GetElementConfiguration("ar") & " 3d1 4s2"
                Case "ti", "titanium"
                    Return GetElementConfiguration("ar") & " 3d2 4s2"
                Case "v", "vanadium"
                    Return GetElementConfiguration("ar") & " 3d3 4s2"
                Case "cr", "chromium"
                    Return GetElementConfiguration("ar") & " 3d4 4s2"
                Case "mn", "magnesium"
                    Return GetElementConfiguration("ar") & " 3d5 4s2"
                Case "fe", "iron"
                    Return GetElementConfiguration("ar") & " 3d6 4s2"
                Case "co", "cobalt"
                    Return GetElementConfiguration("ar") & " 3d7 4s2"
                Case "ni", "nickel"
                    Return GetElementConfiguration("ar") & " 3d8 4s2"
                Case "cu", "copper"
                    Return GetElementConfiguration("ar") & " 3d10 4s1"
                Case "zn", "zinc"
                    Return GetElementConfiguration("ar") & " 3d10 4s2"
                Case "ga", "gallium"
                    Return GetElementConfiguration("ar") & " 3d10 4s2 4p1"
                Case "ge", "germanium"
                    Return GetElementConfiguration("ar") & " 3d10 4s2 4p2"
                Case "as", "arsenic"
                    Return GetElementConfiguration("ar") & " 3d10 4s2 4p3"
                Case "se", "selenium"
                    Return GetElementConfiguration("ar") & " 3d10 4s2 4p4"
                Case "br", "bromine"
                    Return GetElementConfiguration("ar") & " 3d10 4s2 4p5"
                Case "kr", "krypton"
                    Return GetElementConfiguration("ar") & " 3d10 4s2 4p6"
                Case "rb", "rubidium"
                    Return GetElementConfiguration("kr") & " 5s1"
                Case "sr", "strontium"
                    Return GetElementConfiguration("kr") & " 5s2"
                Case "y", "yttrium"
                    Return GetElementConfiguration("kr") & " 4d1 5s2"
                Case "zr", "zirconium"
                    Return GetElementConfiguration("kr") & " 4d2 5s2"
                Case "nb", "niobium"
                    Return GetElementConfiguration("kr") & " 4d4 5s1"
                Case "mo", "molybdenum"
                    Return GetElementConfiguration("kr") & " 4d5 5s1"
                Case "tc", "technetium"
                    Return GetElementConfiguration("kr") & " 4d6 5s1"
                Case "ru", "ruthenium"
                    Return GetElementConfiguration("kr") & " 4d7 5s1"
                Case "rh", "rhodium"
                    Return GetElementConfiguration("kr") & " 4d8 5s1"
                Case "pd", "palladium"
                    Return GetElementConfiguration("kr") & " 4d10"
                Case "ag", "silver"
                    Return GetElementConfiguration("kr") & " 4d10 5s1"
                Case "cd", "cadmium"
                    Return GetElementConfiguration("kr") & " 4d10 5s2"
                Case "in", "indium"
                    Return GetElementConfiguration("kr") & " 4d10 5s2 5p1"
                Case "sn", "tin"
                    Return GetElementConfiguration("kr") & " 4d10 5s2 5p2"
                Case "sb", "antimony"
                    Return GetElementConfiguration("kr") & " 4d10 5s2 5p3"
                Case "te", "tellurium"
                    Return GetElementConfiguration("kr") & " 4d10 5s2 5p4"
                Case "i", "iodine"
                    Return GetElementConfiguration("kr") & " 4d10 5s2 5p5"
                Case "xe", "xenon"
                    Return GetElementConfiguration("kr") & " 4d10 5s2 5p6"
                Case "cs", "cesium"
                    Return GetElementConfiguration("xe") & " 6s1"
                Case "ba", "barium"
                    Return GetElementConfiguration("xe") & " 6s2"
                Case "la", "lanthanum"
                    Return GetElementConfiguration("xe") & " 5d1 6s2"
                Case "ce", "cerium"
                    Return GetElementConfiguration("xe") & " 4f1 5d1 6s2"
                Case "pr", "praseodymium"
                    Return GetElementConfiguration("xe") & " 4f3 6s2"
                Case "nd", "neodymium"
                    Return GetElementConfiguration("xe") & " 4f4 6s2"
                Case "pm", "promethium"
                    Return GetElementConfiguration("xe") & " 4f5 6s2"
                Case "sm", "samarium"
                    Return GetElementConfiguration("xe") & " 4f6 6s2"
                Case "eu", "europium"
                    Return GetElementConfiguration("xe") & " 4f7 6s2"
                Case "gd", "gadolinium"
                    Return GetElementConfiguration("xe") & " 4f7 5d1 6s2"
                Case "tb", "terbium"
                    Return GetElementConfiguration("xe") & " 4f9 6s2"
                Case "dy", "dysprosium"
                    Return GetElementConfiguration("xe") & " 4f10 6s2"
                Case "ho", "holmium"
                    Return GetElementConfiguration("xe") & " 4f11 6s2"
                Case "er", "erbium"
                    Return GetElementConfiguration("xe") & " 4f12 6s2"
                Case "tm", "thulium"
                    Return GetElementConfiguration("xe") & " 4f13 6s2"
                Case "yb", "ytterbium"
                    Return GetElementConfiguration("xe") & " 4f14 6s2"
                Case "lu", "lutetium"
                    Return GetElementConfiguration("xe") & " 4f14 5d1 6s2"
                Case "hf", "hafnium"
                    Return GetElementConfiguration("xe") & " 4f14 5d2 6s2"
                Case "ta", "tantalum"
                    Return GetElementConfiguration("xe") & " 4f14 5d3 6s2"
                Case "w", "tungsten"
                    Return GetElementConfiguration("xe") & " 4f14 5d4 6s2"
                Case "re", "rhenium"
                    Return GetElementConfiguration("xe") & " 4f14 5d5 6s2"
                Case "os", "osmium"
                    Return GetElementConfiguration("xe") & " 4f14 5d6 6s2"
                Case "ir", "iridium"
                    Return GetElementConfiguration("xe") & " 4f14 5d7 6s2"
                Case "pt", "platinum"
                    Return GetElementConfiguration("xe") & " 4f14 5d9 6s1"
                Case "au", "gold"
                    Return GetElementConfiguration("xe") & " 4f14 5d10 6s1"
                Case "hg", "mercury"
                    Return GetElementConfiguration("xe") & " 4f14 5d10 6s2"
                Case "tl", "thallium"
                    Return GetElementConfiguration("xe") & " 4f14 5d10 6s2 6p1"
                Case "pb", "lead"
                    Return GetElementConfiguration("xe") & " 4f14 5d10 6s2 6p2"
                Case "bi", "bismuth"
                    Return GetElementConfiguration("xe") & " 4f14 5d10 6s2 6p3"
                Case "po", "polonium"
                    Return GetElementConfiguration("xe") & " 4f14 5d10 6s2 6p4"
                Case "at", "astatine"
                    Return GetElementConfiguration("xe") & " 4f14 5d10 6s2 6p5"
                Case "rn", "radon"
                    Return GetElementConfiguration("xe") & " 4f14 5d10 6s2 6p6"
                Case "fr", "francium"
                    Return GetElementConfiguration("rn") & " 7s1"
                Case "ra", "radium"
                    Return GetElementConfiguration("rn") & " 7s2"
                Case "ac", "actinium"
                    Return GetElementConfiguration("rn") & " 6d1 7s2"
                Case "th", "thorium"
                    Return GetElementConfiguration("rn") & " 6d2 7s2"
                Case "pa", "protactinium"
                    Return GetElementConfiguration("rn") & " 5f2 6d1 7s2"
                Case "u", "uranium"
                    Return GetElementConfiguration("rn") & " 5f3 6d1 7s2"
                Case "np", "neptunium"
                    Return GetElementConfiguration("rn") & " 5f4 6d1 7s2"
                Case "pu", "plutonium"
                    Return GetElementConfiguration("rn") & " 5f6 7s2"
                Case "am", "americium"
                    Return GetElementConfiguration("rn") & " 5f7 7s2"
                Case "cm", "curium"
                    Return GetElementConfiguration("rn") & " 5f7 6d1 7s2"
                Case "bk", "berkelium"
                    Return GetElementConfiguration("rn") & " 5f9 7s2"
                Case "cf", "californium"
                    Return GetElementConfiguration("rn") & " 5f10 7s2"
                Case "es", "einsteinium"
                    Return GetElementConfiguration("rn") & " 5f11 7s2"
                Case "fm", "fermium"
                    Return GetElementConfiguration("rn") & " 5f12 7s2"
                Case "md", "mendelevium"
                    Return GetElementConfiguration("rn") & " 5f13 7s2"
                Case "no", "nobelium"
                    Return GetElementConfiguration("rn") & " 5f14 7s2"
                Case "lr", "lawrencium"
                    Return GetElementConfiguration("rn") & " 5f14 7s2 7p1"
                Case "rf", "rutherfordium"
                    Return GetElementConfiguration("rn") & " 5f14 6d2 7s2"
                Case "db", "dubnium"
                    Return GetElementConfiguration("rn") & " 5f14 6d3 7s2"
                Case "sg", "seaborgium"
                    Return GetElementConfiguration("rn") & " 5f14 6d4 7s2"
                Case "bh", "borhium"
                    Return GetElementConfiguration("rn") & " 5f14 6d5 7s2"
                Case "hs", "hassium"
                    Return GetElementConfiguration("rn") & " 5f14 6d6 7s2"
                Case "mt", "meitnerium"
                    Return GetElementConfiguration("rn") & " 5f14 6d7 7s2"
                Case "ds", "darmstadtium"
                    Return GetElementConfiguration("rn") & " 5f14 6d9 7s1"
                Case "rg", "roentgenium"
                    Return GetElementConfiguration("rn") & " 5f14 6d10 7s1"
                Case "cn", "copernicium"
                    Return GetElementConfiguration("rn") & " 5f14 6d10 7s2"
                Case Else
                    Throw New FormatException()
            End Select


        End Function

        ''' <summary>
        ''' Returns the number of electrons in the orbital defined by primary quantum number n
        ''' and angular momentum quantum number l.
        ''' </summary>
        ''' <param name="n">The primary quantum number of the orbital.</param>
        ''' <param name="l">The angular momentum quantum number of the orbital.</param>
        ''' <returns></returns>
        Public Overloads Function Occupancy(ByVal n As Integer, ByVal l As AngularMomentum) As Double

            Dim orbital As String = n.ToString & l.ToString

            If mConfiguration.ContainsKey(orbital) Then
                Return mConfiguration(orbital)
            Else
                Return 0.0
            End If

        End Function

		
       Public Overloads Function Occupancy(ByVal n As Integer, ByVal l As integer) As Double

            Return Occupancy(n,ctype(l,AngularMomentum))

        End Function
        
        ''' <summary>
        ''' Returns the total number of electrons in the configuration.
        ''' </summary>
        ''' <returns></returns>
        Public Overloads Function Occupancy() As Double
            Return mTotalOccupancy
        End Function

''' <summary>
''' Returns the total number of orbitals in the configuration.
''' </summary>
''' <returns></returns>
        Public Function OrbitalCount() As Integer
            Return mConfiguration.Count
        End Function

		''' <summary>
		''' Returns the largest value of the primary quantum number N in the configuration.
		''' </summary>
		''' <returns></returns>
        Public Function NMax() As Integer
			Return mNMax
        End Function
        
        Public Function LMax() As Integer
        	Return mLMax
        End Function

        ''' <summary>
        ''' Returns the configuration as a (fully qualified) string.
        ''' </summary>
        ''' <returns></returns>
        Overrides Function ToString() As String

            Dim occ As Double
            Dim configString As String = ""

            For n As Integer = 1 To mNMax

                For l As Integer = 0 To n - 1

                    occ = Me.Occupancy(n, CType(l, AngularMomentum))

                    If occ > 0 Then

                        configString &= n.ToString & CType(l, AngularMomentum).ToString & occ.ToString & " "

                    End If

                Next

            Next

            Return configString.Trim()

        End Function


		Public Function GetQuantumNumbers As Integer(,)
			
			Dim nOrbitals as integer = Me.OrbitalCount
			
			Dim Retval(NOrbitals-1,1) As Integer
        	
        	Dim N As Integer
        	Dim L As Integer
        	
        	Dim i as Integer = 0
        	For Each orb As String In mConfiguration.Keys
        		N = CInt(orb.Substring(0,orb.Length-1))
        		Select Case orb.Substring(orb.Length-1,1).ToLower
        			Case "s"
        				L = 0
        			Case "p"
        				L = 1
        			Case "d"
        				L = 2
        			Case "f"
        				L = 3
        			Case "g"
        				L = 4
        			Case "h"
        				L = 5
        			Case "i"
        				L = 6
        		End Select
        		Retval(i,0) = N
        		Retval(i,1) = L
        		i += 1
        	Next
        	
        	Return Retval
        	
		End Function
		
		Private Function GetIntegralValue(angularmomentumletter As String) As Integer
			Dim L as integer
			    Select Case angularmomentumletter.ToLower
        			Case "s"
        				L = 0
        			Case "p"
        				L = 1
        			Case "d"
        				L = 2
        			Case "f"
        				L = 3
        			Case "g"
        				L = 4
        			Case "h"
        				L = 5
        			Case "i"
        				L = 6
        			Case Else
        				throw new ArgumentException("Invalid angular momentum letter")
        		End Select
        		return L

		End Function
		
		''' <summary>
		''' Returns a 2D array with the occupancy of each (n,l) orbital:
		''' </summary>
		''' <returns></returns>
		Public Function GetOccupancyArray As Double(,)
			Dim retval(mNMax,mLMax) As Double
			For n = 0 To mNMax
				For l = 0 To mLmax
					retval(n,l) = Occupancy(n,l)
				Next
			Next
			return retval
		End Function
		
		Public Shared Operator =(ByVal c1 As ElectronicConfiguration, ByVal c2 As ElectronicConfiguration) As Boolean
        	Return (c1.ToString = c2.ToString)
    	End Operator

    	Public Shared Operator <>(ByVal c1 As ElectronicConfiguration, ByVal c2 As ElectronicConfiguration) As Boolean
        	Return Not(c1 = c2)
    	End Operator

		
    End Class


