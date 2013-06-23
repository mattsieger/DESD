
Imports System.Math
Imports System.ComponentModel
Imports DESD.Math


Public Class TestUtilities
    <TestMethod> _
    Public Shared Sub AssertEqualWithinTolerance(ByVal testvalue As Double, ByVal expectedvalue As Double, ByVal tolerance As Double)
        Assert.IsTrue((expectedvalue + tolerance) >= testvalue)
        Assert.IsTrue((expectedvalue - tolerance) <= testvalue)
    End Sub

    Public Shared Function AreEqual(ByVal testval As Complex, ByVal expectval As Complex, ByVal maxUlps As Long, Optional ByVal zeroTolerance As Double = 0.0) As Boolean

        Return AreEqual(testval.Real, expectval.Real, maxUlps, zerotolerance) And AreEqual(testval.Imag, expectval.Imag, maxUlps, zeroTolerance)

    End Function


    ' Usable AlmostEqual 
    Public Shared Function AreEqual(ByVal testval As Double, ByVal expectval As Double, ByVal maxUlps As Long, Optional ByVal zeroTolerance As Double = 0.0) As Boolean

        '// Try the stupid solution first:
        If testval = expectval Then Return True

        '// If expectval = 0.0, it's possible that testval is coming out very small, but still a float.
        '// In this case, you'll never match to within a reasonable number of Ulps.
        '// So instead multiply maxUlps * 1E-15 and compare.
        If expectval = 0.0 Then
            Dim eval As Double = 0.000000000000001 * maxUlps
            If eval >= testval Then
                Return True
            Else
                Return False
            End If
        End If

        '// I often see the problem that when comparing results against test functions that are supposed to evaluate to zero
        '// but evaluate to a float instead, that the resulting small numbers do not pass.  This flag is supposed to capture
        '// this condition
        If zeroTolerance > 0.0 Then
            If (expectval <= zeroTolerance) And (testval <= zeroTolerance) Then
                Return True
            End If

        End If

        '// Convert the double values directly to 64-bit integers:
        Dim aInt As Long = BitConverter.DoubleToInt64Bits(testval)
        Dim bInt As Long = BitConverter.DoubleToInt64Bits(expectval)

        ' Make sure maxUlps is non-negative and small enough that the     
        ' default NAN won't compare as equal to anything.     
        ' assert(maxUlps > 0 And maxUlps < 4 * 1024 * 1024)

        ' Make aInt lexicographically ordered as a twos-complement int     
        If aInt < 0 Then
            aInt = &H80000000L - aInt
        End If


        ' Make bInt lexicographically ordered as a twos-complement int     
        If bInt < 0 Then
            bInt = &H80000000L - bInt
        End If

        Dim intDiff As Long = Abs(aInt - bInt)

        If intDiff <= maxUlps Then
            Return True
        End If

        Return False


    End Function
    
    Public Shared Function AreEqualToSigFigs(d1 As Double, d2 As Double, significantfigures as integer) As Boolean

		'// If signs are wrong, then we're totally off:
		If system.Math.Sign(d1) <> system.Math.Sign(d2) Then Return False
		
		'// Try doing as a string compare operation - slow, but I don't know how else to do it!
		'// This should return formatted like #.####E+000
		'// The remove statement strips out the decimal point.
		Dim d1str As String = system.Math.Abs(d1).ToString("E15").Remove(1,1)
		Dim d2str As String = system.Math.Abs(d2).ToString("E15").Remove(1,1)
		
		'// Check to see if the exponents are different:
		Dim exp1 As String = d1str.Substring(d1str.Length-5) 
		Dim exp2 As String = d2str.Substring(d2str.Length-5)
		If exp1 <> exp2 Then Return False
		
		'// The number of significant figures is the number of characters before a difference occurs:
		Dim d1char As Char() = d1str.ToCharArray
		Dim d2char As Char() = d2str.ToCharArray
		Dim count as Integer = 0
		For i As Integer = 0 To d1char.Length-6
			If d1char(i) <> d2char(i) Then Exit For
			count += 1
		Next
		
		Return (count >= significantfigures)
		
	End Function


    ''' <summary>
    ''' Returns n random double values between 0 and 1.
    ''' </summary>
    ''' <param name="n"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRandomDoubles(ByVal n As Integer) As Double()
        Randomize()
        Dim rand As New Random

        Dim retval(n - 1) As Double

        For i As Integer = 0 To n - 1
            retval(i) = rand.NextDouble
        Next

        Return retval

    End Function


    Public Shared Function GetRandomDoubles(ByVal n As Integer, ByVal minval As Double, ByVal maxval As Double) As Double()
        Randomize()
        Dim rnd As New Random

        Dim retval(n - 1) As Double

        For i As Integer = 0 To n - 1
            retval(i) = minval + rnd.NextDouble * (maxval - minval)
        Next

        Return retval

    End Function

End Class




Public Module StockTests

    Public Const err26 As Double = 1.0E-26
    Public Const err25 As Double = 1.0E-25
    Public Const err24 As Double = 1.0E-24
    Public Const err23 As Double = 1.0E-23
    Public Const err22 As Double = 1.0E-22
    Public Const err21 As Double = 1.0E-21
    Public Const err20 As Double = 1.0E-20
    Public Const err19 As Double = 1.0E-19
    Public Const err18 As Double = 1.0E-18
    Public Const err17 As Double = 1.0E-17
    Public Const err16 As Double = 0.0000000000000001R
    Public Const err15 As Double = 0.000000000000001R
    Public Const err14 As Double = 0.00000000000001R
    Public Const err13 As Double = 0.0000000000001R
    Public Const err12 As Double = 0.000000000001R
    Public Const err11 As Double = 0.00000000001R
    Public Const err10 As Double = 0.0000000001R
    Public Const err9 As Double = 0.000000001R
    Public Const err8 As Double = 0.00000001R
    Public Const err7 As Double = 0.0000001R
    Public Const err6 As Double = 0.000001R
    Public Const err5 As Double = 0.00001R
    Public Const err4 As Double = 0.0001R
    Public Const err3 As Double = 0.001R
    Public Const err2 As Double = 0.01R
    Public Const err1 As Double = 0.1R


    ''' <summary>
    ''' Standard inequality operators for use in stock test functions that check ranges.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Inequality
        EqualTo
        LessThan
        LessThanOrEqualTo
        GreaterThan
        GreaterThanOrEqualTo
        NotEqualTo
    End Enum




    ''' <summary>
    ''' Tests a property of type double that has no range validation exceptions.
    ''' </summary>
    ''' <param name="component">A reference to the object that owns the property to be tested.</param>
    ''' <param name="propertyname">The name of the property to be tested.</param>
    ''' <remarks></remarks>
    Public Sub TestPropertyDouble(ByVal component As Object, ByVal propertyname As String)

        Dim pdc As PropertyDescriptorCollection = TypeDescriptor.GetProperties(component)
        Dim pd As PropertyDescriptor = Nothing
        For Each pd2 As PropertyDescriptor In pdc
            If pd2.Name = propertyname Then
                pd = pd2
            End If
        Next

        '// make the assertion that there is a match of the proper type:
        Assert.IsNotNull(pd)
        Assert.IsTrue(pd.PropertyType Is GetType(Double))

        '// Test double precision:
        pd.SetValue(component, 1.2345678901234567R)
        Assert.IsTrue(CDbl(pd.GetValue(component)) = 1.2345678901234567R)

        '// Test extreme assignments:
        pd.SetValue(component, Double.MaxValue)
        Assert.IsTrue(CDbl(pd.GetValue(component)) = Double.MaxValue)

        pd.SetValue(component, Double.MinValue)
        Assert.IsTrue(CDbl(pd.GetValue(component)) = Double.MinValue)

        pd.SetValue(component, Double.NegativeInfinity)
        Assert.IsTrue(Double.IsNegativeInfinity(CDbl(pd.GetValue(component))))

        pd.SetValue(component, Double.PositiveInfinity)
        Assert.IsTrue(Double.IsPositiveInfinity(CDbl(pd.GetValue(component))))

        pd.SetValue(component, Double.Epsilon)
        Assert.IsTrue(CDbl(pd.GetValue(component)) = Double.Epsilon)

        '// Test NaN assignment:
        pd.SetValue(component, Double.NaN)
        Assert.IsTrue(Double.IsNaN(CDbl(pd.GetValue(component))))


    End Sub


    ''' <summary>
    ''' Tests a property of type double that has a one-sided bound validation check that
    ''' throws an ArgumentOutOfRange exception on invalid input.
    ''' </summary>
    ''' <param name="component">A reference to the object that owns the property to be tested.</param>
    ''' <param name="propertyname">The name of the property to be tested.</param>
    ''' <param name="boundingvalue">The value of the validation bound.</param>
    ''' <param name="comparator">An inequality used to compare the bounding value to the property value.</param>
    ''' <remarks></remarks>
    Public Sub TestPropertyDouble(ByVal component As Object, _
                                  ByVal propertyname As String, _
                                  ByVal boundingvalue As Double, _
                                  ByVal comparator As Inequality)

        Dim pdc As PropertyDescriptorCollection = TypeDescriptor.GetProperties(component)
        Dim pd As PropertyDescriptor = Nothing
        For Each pd2 As PropertyDescriptor In pdc
            If pd2.Name = propertyname Then
                pd = pd2
            End If
        Next

        '// make the assertion that there is a match of the proper type:
        Assert.IsNotNull(pd)
        Assert.IsTrue(pd.PropertyType Is GetType(Double))

        Dim delta As Double = Abs(boundingvalue / 10000000000.0R)

        Dim passvalue1 As Double
        Dim passvalue2 As Double
        Dim failvalue1 As Double
        Dim failvalue2 As Double
        Select Case comparator
            Case inequality.EqualTo
                If boundingvalue = 0.0R Then
                    passvalue1 = boundingvalue
                    passvalue2 = boundingvalue
                    failvalue1 = Double.Epsilon
                    failvalue2 = -Double.Epsilon
                Else
                    passvalue1 = boundingvalue
                    passvalue2 = boundingvalue
                    failvalue1 = boundingvalue + delta
                    failvalue2 = boundingvalue - delta
                End If

            Case Inequality.GreaterThan
                If boundingvalue = 0.0R Then
                    passvalue1 = Double.MaxValue
                    passvalue2 = Double.Epsilon
                    failvalue1 = boundingvalue
                    failvalue2 = Double.MinValue
                Else
                    passvalue1 = Double.MaxValue
                    passvalue2 = boundingvalue + delta
                    failvalue1 = boundingvalue
                    failvalue2 = Double.MinValue
                End If

            Case Inequality.GreaterThanOrEqualTo
                If boundingvalue = 0.0R Then
                    passvalue1 = Double.MaxValue
                    passvalue2 = boundingvalue
                    failvalue1 = -Double.Epsilon
                    failvalue2 = Double.MinValue
                Else
                    passvalue1 = Double.MaxValue
                    passvalue2 = boundingvalue
                    failvalue1 = boundingvalue - delta
                    failvalue2 = Double.MinValue
                End If

            Case Inequality.LessThan
                If boundingvalue = 0.0R Then
                    passvalue1 = Double.MinValue
                    passvalue2 = -Double.Epsilon
                    failvalue1 = boundingvalue
                    failvalue2 = Double.MaxValue
                Else
                    passvalue1 = Double.MinValue
                    passvalue2 = boundingvalue - delta
                    failvalue1 = boundingvalue
                    failvalue2 = Double.MaxValue
                End If

            Case Inequality.LessThanOrEqualTo
                If boundingvalue = 0.0R Then
                    passvalue1 = Double.MinValue
                    passvalue2 = boundingvalue
                    failvalue1 = Double.Epsilon
                    failvalue2 = Double.MaxValue
                Else
                    passvalue1 = Double.MinValue
                    passvalue2 = boundingvalue
                    failvalue1 = boundingvalue + delta
                    failvalue2 = Double.MaxValue
                End If

            Case Inequality.NotEqualTo
                If boundingvalue = 0.0R Then
                    passvalue1 = Double.Epsilon
                    passvalue2 = -Double.Epsilon
                    failvalue1 = boundingvalue
                    failvalue2 = boundingvalue
                Else
                    passvalue1 = boundingvalue - delta
                    passvalue2 = boundingvalue + delta
                    failvalue1 = boundingvalue
                    failvalue2 = boundingvalue
                End If

        End Select

        '// Test the passing values:
        Dim testflag As Boolean = True
        Try
            pd.SetValue(component, passvalue1)
        Catch ex As Exception
            testflag = False
        End Try
        Assert.IsTrue(testflag)

        Try
            pd.SetValue(component, passvalue2)
        Catch ex As Exception
            testflag = False
        End Try
        Assert.IsTrue(testflag)

        '// Now test the failing values:
        testflag = False
        Try
            pd.SetValue(component, failvalue1)
        Catch aoorex As ArgumentOutOfRangeException
            testflag = True
        Catch ex As Exception

        End Try
        Assert.IsTrue(testflag)

        testflag = False
        Try
            pd.SetValue(component, failvalue2)
        Catch aoorex As ArgumentOutOfRangeException
            testflag = True
        Catch ex As Exception

        End Try
        Assert.IsTrue(testflag)

        '// Test persistence - what is assigned comes back OK:
        pd.SetValue(component, passvalue1)
        Assert.IsTrue(CDbl(pd.GetValue(component)) = passvalue1)

        pd.SetValue(component, passvalue2)
        Assert.IsTrue(CDbl(pd.GetValue(component)) = passvalue2)


    End Sub


    ''' <summary>
    ''' Tests a property of type double that has a two-sided bounds validation check that
    ''' throws an ArgumentOutOfRange exception on invalid input. String inequality wrapper.
    ''' </summary>
    ''' <param name="component">A reference to the object that owns the property to be tested.</param>
    ''' <param name="propertyname">The name of the property to be tested.</param>
    ''' <param name="lowerboundingvalue">The value of the lower validation bound.</param>
    ''' <param name="lowercomparator">An inequality used to compare the lower bounding value to the property value.</param>
    ''' <param name="upperboundingvalue">The value of the upper validation bound.</param>
    ''' <param name="uppercomparator">An inequality used to compare the upper bounding value to the property value.</param>
    ''' <remarks></remarks>
    Public Sub TestPropertyDouble2(ByVal component As Object, _
                                  ByVal propertyname As String, _
                                  ByVal lowerboundingvalue As Double, _
                                  ByVal lowercomparator As String, _
                                  ByVal upperboundingvalue As Double, _
                                  ByVal uppercomparator As String)

        Dim lowerinequality, upperinequality As Inequality
        Select Case lowercomparator
            Case "<"
                lowerinequality = Inequality.LessThan
            Case "<=", "=<"
                lowerinequality = Inequality.LessThanOrEqualTo
            Case ">"
                lowerinequality = Inequality.GreaterThan
            Case ">=", "=>"
                lowerinequality = Inequality.GreaterThanOrEqualTo
            Case "=="
                lowerinequality = Inequality.EqualTo
            Case "!="
                lowerinequality = Inequality.NotEqualTo
        End Select
        Select Case uppercomparator
            Case "<"
                upperinequality = Inequality.LessThan
            Case "<=", "=<"
                upperinequality = Inequality.LessThanOrEqualTo
            Case ">"
                upperinequality = Inequality.GreaterThan
            Case ">=", "=>"
                upperinequality = Inequality.GreaterThanOrEqualTo
            Case "=="
                upperinequality = Inequality.EqualTo
            Case "!="
                upperinequality = Inequality.NotEqualTo
        End Select

        TestPropertyDouble(Component, propertyname, lowerboundingvalue, lowerinequality, upperboundingvalue, upperinequality)

    End Sub


    ''' <summary>
    ''' Tests a property of type double that has a two-sided bounds validation check that
    ''' throws an ArgumentOutOfRange exception on invalid input.
    ''' </summary>
    ''' <param name="component">A reference to the object that owns the property to be tested.</param>
    ''' <param name="propertyname">The name of the property to be tested.</param>
    ''' <param name="lowerboundingvalue">The value of the lower validation bound.</param>
    ''' <param name="lowercomparator">An inequality used to compare the lower bounding value to the property value.</param>
    ''' <param name="upperboundingvalue">The value of the upper validation bound.</param>
    ''' <param name="uppercomparator">An inequality used to compare the upper bounding value to the property value.</param>
    ''' <remarks></remarks>
    Public Sub TestPropertyDouble(ByVal component As Object, _
                                  ByVal propertyname As String, _
                                  ByVal lowerboundingvalue As Double, _
                                  ByVal lowercomparator As Inequality, _
                                  ByVal upperboundingvalue As Double, _
                                  ByVal uppercomparator As Inequality)

        Dim pdc As PropertyDescriptorCollection = TypeDescriptor.GetProperties(component)
        Dim pd As PropertyDescriptor = Nothing
        For Each pd2 As PropertyDescriptor In pdc
            If pd2.Name = propertyname Then
                pd = pd2
            End If
        Next

        '// make the assertion that there is a match of the proper type:
        Assert.IsNotNull(pd)
        Assert.IsTrue(pd.PropertyType Is GetType(Double))

        Dim lowerdelta As Double = Abs(lowerboundingvalue / 10000000000.0R)
        Dim upperdelta As Double = Abs(upperboundingvalue / 10000000000.0R)

        Dim passvalue1 As Double
        Dim passvalue2 As Double
        Dim failvalue1 As Double
        Dim failvalue2 As Double
        Select Case lowercomparator

            Case Inequality.GreaterThan
                If lowerboundingvalue = 0.0R Then
                    passvalue1 = Double.Epsilon
                ElseIf Double.IsNegativeInfinity(lowerboundingvalue) Then
                    passvalue1 = Double.MinValue
                ElseIf Double.IsPositiveInfinity(lowerboundingvalue) Then
                    Throw New Exception("Invalid bounding value, there's nothing greater than +inf")
                Else
                    passvalue1 = lowerboundingvalue + lowerdelta
                End If
                passvalue2 = passvalue1
                failvalue1 = lowerboundingvalue

            Case Inequality.GreaterThanOrEqualTo
                If lowerboundingvalue = 0.0R Then
                    passvalue1 = Double.Epsilon
                    failvalue1 = -Double.Epsilon
                ElseIf Double.IsNegativeInfinity(lowerboundingvalue) Then
                    passvalue1 = Double.MinValue
                    failvalue1 = Double.NaN
                ElseIf Double.IsPositiveInfinity(lowerboundingvalue) Then
                    Throw New Exception("Invalid bounding value, there's nothing greater than +inf")
                Else
                    passvalue1 = lowerboundingvalue + lowerdelta
                    failvalue1 = lowerboundingvalue - lowerdelta
                End If
                passvalue2 = lowerboundingvalue

            Case Inequality.LessThan
                If lowerboundingvalue = 0.0R Then
                    passvalue1 = -Double.Epsilon
                ElseIf Double.IsPositiveInfinity(lowerboundingvalue) Then
                    passvalue1 = Double.MaxValue
                ElseIf Double.IsNegativeInfinity(lowerboundingvalue) Then
                    Throw New Exception("Invalid bounding value, there's nothing less than -inf")
                Else
                    passvalue1 = lowerboundingvalue - lowerdelta
                End If
                passvalue2 = passvalue1
                failvalue1 = lowerboundingvalue

            Case Inequality.LessThanOrEqualTo
                If lowerboundingvalue = 0.0R Then
                    passvalue1 = -Double.Epsilon
                    failvalue1 = Double.Epsilon
                ElseIf Double.IsPositiveInfinity(lowerboundingvalue) Then
                    passvalue1 = Double.MaxValue
                    failvalue1 = Double.NaN
                ElseIf Double.IsNegativeInfinity(lowerboundingvalue) Then
                    Throw New Exception("Invalid bounding value, there's nothing less than -inf")
                Else
                    passvalue1 = lowerboundingvalue - lowerdelta
                    failvalue1 = lowerboundingvalue + lowerdelta
                End If
                passvalue2 = lowerboundingvalue

            Case Inequality.NotEqualTo
                If lowerboundingvalue = 0.0R Then
                    passvalue1 = Double.Epsilon
                    passvalue2 = -Double.Epsilon
                    failvalue1 = lowerboundingvalue
                    failvalue2 = lowerboundingvalue
                ElseIf Double.IsPositiveInfinity(lowerboundingvalue) Or Double.IsNegativeInfinity(lowerboundingvalue) Then
                    passvalue1 = Double.MaxValue
                    passvalue2 = Double.MinValue
                    failvalue1 = lowerboundingvalue
                    failvalue2 = Double.NaN
                Else
                    passvalue1 = lowerboundingvalue - lowerdelta
                    passvalue2 = lowerboundingvalue + lowerdelta
                    failvalue1 = lowerboundingvalue
                    failvalue2 = lowerboundingvalue
                End If

        End Select

        Dim testflag As Boolean = True

        '// Test the passing values:
        Try
            Try
                pd.SetValue(component, passvalue1)
            Catch ex As Exception
                testflag = False
            End Try
            Assert.IsTrue(testflag)

            Try
                pd.SetValue(component, passvalue2)
            Catch ex As Exception
                testflag = False
            End Try
            Assert.IsTrue(testflag)

            '// Now test the failing values:
            testflag = False
            Try
                pd.SetValue(component, failvalue1)
            Catch aoorex As ArgumentOutOfRangeException
                testflag = True
            Catch ex As Exception

            End Try
            Assert.IsTrue(testflag)

            '// Test persistence - what is assigned comes back OK:
            pd.SetValue(component, passvalue1)
            Assert.IsTrue(CDbl(pd.GetValue(component)) = passvalue1)

            pd.SetValue(component, passvalue2)
            Assert.IsTrue(CDbl(pd.GetValue(component)) = passvalue2)
        Catch ex As AssertFailedException
            MsgBox(ex.ToString, , "AssertionException")
            Throw ex
        End Try


        '// Now repeat the sequency for the upper bound:
        Select Case uppercomparator

            Case Inequality.GreaterThan
                If upperboundingvalue = 0.0R Then
                    passvalue1 = Double.Epsilon
                ElseIf Double.IsNegativeInfinity(upperboundingvalue) Then
                    passvalue1 = Double.MinValue
                ElseIf Double.IsPositiveInfinity(upperboundingvalue) Then
                    Throw New Exception("Invalid bounding value, there's nothing greater than +inf")
                Else
                    passvalue1 = upperboundingvalue + upperdelta
                End If
                passvalue2 = passvalue1
                failvalue1 = upperboundingvalue

            Case Inequality.GreaterThanOrEqualTo
                If upperboundingvalue = 0.0R Then
                    passvalue1 = Double.Epsilon
                    failvalue1 = -Double.Epsilon
                ElseIf Double.IsNegativeInfinity(upperboundingvalue) Then
                    passvalue1 = Double.MinValue
                    failvalue1 = Double.NaN
                ElseIf Double.IsPositiveInfinity(upperboundingvalue) Then
                    Throw New Exception("Invalid bounding value, there's nothing greater than +inf")
                Else
                    passvalue1 = upperboundingvalue + upperdelta
                    failvalue1 = upperboundingvalue - upperdelta
                End If
                passvalue2 = upperboundingvalue

            Case Inequality.LessThan
                If upperboundingvalue = 0.0R Then
                    passvalue1 = -Double.Epsilon
                ElseIf Double.IsPositiveInfinity(upperboundingvalue) Then
                    passvalue1 = Double.MaxValue
                ElseIf Double.IsNegativeInfinity(upperboundingvalue) Then
                    Throw New Exception("Invalid bounding value, there's nothing less than -inf")
                Else
                    passvalue1 = upperboundingvalue - upperdelta
                End If
                passvalue2 = passvalue1
                failvalue1 = upperboundingvalue

            Case Inequality.LessThanOrEqualTo
                If upperboundingvalue = 0.0R Then
                    passvalue1 = -Double.Epsilon
                    failvalue1 = Double.Epsilon
                ElseIf Double.IsPositiveInfinity(upperboundingvalue) Then
                    passvalue1 = Double.MaxValue
                    failvalue1 = Double.NaN
                ElseIf Double.IsNegativeInfinity(upperboundingvalue) Then
                    Throw New Exception("Invalid bounding value, there's nothing less than -inf")
                Else
                    passvalue1 = upperboundingvalue - upperdelta
                    failvalue1 = upperboundingvalue + upperdelta
                End If
                passvalue2 = upperboundingvalue

            Case Inequality.NotEqualTo
                If upperboundingvalue = 0.0R Then
                    passvalue1 = Double.Epsilon
                    passvalue2 = -Double.Epsilon
                    failvalue1 = upperboundingvalue
                    failvalue2 = upperboundingvalue
                ElseIf Double.IsPositiveInfinity(upperboundingvalue) Or Double.IsNegativeInfinity(upperboundingvalue) Then
                    passvalue1 = Double.MaxValue
                    passvalue2 = Double.MinValue
                    failvalue1 = upperboundingvalue
                    failvalue2 = Double.NaN
                Else
                    passvalue1 = upperboundingvalue - upperdelta
                    passvalue2 = upperboundingvalue + upperdelta
                    failvalue1 = upperboundingvalue
                    failvalue2 = upperboundingvalue
                End If

        End Select

        '// Test the passing values:
        Try
            testflag = True
            Try
                pd.SetValue(component, passvalue1)
            Catch ex As Exception
                testflag = False
            End Try
            Assert.IsTrue(testflag)

            Try
                pd.SetValue(component, passvalue2)
            Catch ex As Exception
                testflag = False
            End Try
            Assert.IsTrue(testflag)

            '// Now test the failing values:
            testflag = False
            Try
                pd.SetValue(component, failvalue1)
            Catch aoorex As ArgumentOutOfRangeException
                testflag = True
            Catch ex As Exception

            End Try
            Assert.IsTrue(testflag)
        Catch ex As AssertFailedException
            MsgBox(ex.ToString, , "AssertionException")
            Throw ex
        End Try



    End Sub




    ''' <summary>
    ''' Tests a property of type single that has no range validation exceptions.
    ''' </summary>
    ''' <param name="component">A reference to the object that owns the property to be tested.</param>
    ''' <param name="propertyname">The name of the property to be tested.</param>
    ''' <remarks></remarks>
    Public Sub TestPropertySingle(ByVal component As Object, ByVal propertyname As String)

        Dim pdc As PropertyDescriptorCollection = TypeDescriptor.GetProperties(component)
        Dim pd As PropertyDescriptor = Nothing
        For Each pd2 As PropertyDescriptor In pdc
            If pd2.Name = propertyname Then
                pd = pd2
            End If
        Next

        '// make the assertion that there is a match of the proper type:
        Assert.IsNotNull(pd)
        Assert.IsTrue(pd.PropertyType Is GetType(Single))

        '// Test single precision:
        pd.SetValue(component, 1.23456788F)
        Assert.IsTrue(CSng(pd.GetValue(component)) = 1.23456788F)

        '// Test extreme assignments:
        pd.SetValue(component, Single.MaxValue)
        Assert.IsTrue(CSng(pd.GetValue(component)) = Single.MaxValue)

        pd.SetValue(component, Single.MinValue)
        Assert.IsTrue(CSng(pd.GetValue(component)) = Single.MinValue)

        pd.SetValue(component, Single.NegativeInfinity)
        Assert.IsTrue(Single.IsNegativeInfinity(CSng(pd.GetValue(component))))

        pd.SetValue(component, Single.PositiveInfinity)
        Assert.IsTrue(Single.IsPositiveInfinity(CSng(pd.GetValue(component))))

        pd.SetValue(component, Single.Epsilon)
        Assert.IsTrue(CSng(pd.GetValue(component)) = Single.Epsilon)

        '// Test NaN assignment:
        pd.SetValue(component, Single.NaN)
        Assert.IsTrue(Single.IsNaN(CSng(pd.GetValue(component))))

    End Sub


    ''' <summary>
    ''' Tests a property of type double that has a one-sided bound validation check that
    ''' throws an ArgumentOutOfRange exception on invalid input.
    ''' </summary>
    ''' <param name="component">A reference to the object that owns the property to be tested.</param>
    ''' <param name="propertyname">The name of the property to be tested.</param>
    ''' <param name="boundingvalue">The value of the validation bound.</param>
    ''' <param name="comparator">An inequality used to compare the bounding value to the property value.</param>
    ''' <remarks></remarks>
    Public Sub TestPropertySingle(ByVal component As Object, _
                                  ByVal propertyname As String, _
                                  ByVal boundingvalue As Single, _
                                  ByVal comparator As Inequality)

        Dim pdc As PropertyDescriptorCollection = TypeDescriptor.GetProperties(component)
        Dim pd As PropertyDescriptor = Nothing
        For Each pd2 As PropertyDescriptor In pdc
            If pd2.Name = propertyname Then
                pd = pd2
            End If
        Next

        '// make the assertion that there is a match of the proper type:
        Assert.IsNotNull(pd)
        Assert.IsTrue(pd.PropertyType Is GetType(Single))

        Dim delta As Single = CSng(Abs(boundingvalue / 100000.0F))

        Dim passvalue1 As Single
        Dim passvalue2 As Single
        Dim failvalue1 As Single
        Dim failvalue2 As Single
        Select Case comparator
            Case Inequality.EqualTo
                If boundingvalue = 0.0R Then
                    passvalue1 = boundingvalue
                    passvalue2 = boundingvalue
                    failvalue1 = Single.Epsilon
                    failvalue2 = -Single.Epsilon
                Else
                    passvalue1 = boundingvalue
                    passvalue2 = boundingvalue
                    failvalue1 = boundingvalue + delta
                    failvalue2 = boundingvalue - delta
                End If

            Case Inequality.GreaterThan
                If boundingvalue = 0.0R Then
                    passvalue1 = Single.MaxValue
                    passvalue2 = Single.Epsilon
                    failvalue1 = boundingvalue
                    failvalue2 = Single.MinValue
                Else
                    passvalue1 = Single.MaxValue
                    passvalue2 = boundingvalue + delta
                    failvalue1 = boundingvalue
                    failvalue2 = Single.MinValue
                End If

            Case Inequality.GreaterThanOrEqualTo
                If boundingvalue = 0.0R Then
                    passvalue1 = Single.MaxValue
                    passvalue2 = boundingvalue
                    failvalue1 = -Single.Epsilon
                    failvalue2 = Single.MinValue
                Else
                    passvalue1 = Single.MaxValue
                    passvalue2 = boundingvalue
                    failvalue1 = boundingvalue - delta
                    failvalue2 = Single.MinValue
                End If

            Case Inequality.LessThan
                If boundingvalue = 0.0R Then
                    passvalue1 = Single.MinValue
                    passvalue2 = -Single.Epsilon
                    failvalue1 = boundingvalue
                    failvalue2 = Single.MaxValue
                Else
                    passvalue1 = Single.MinValue
                    passvalue2 = boundingvalue - delta
                    failvalue1 = boundingvalue
                    failvalue2 = Single.MaxValue
                End If

            Case Inequality.LessThanOrEqualTo
                If boundingvalue = 0.0R Then
                    passvalue1 = Single.MinValue
                    passvalue2 = boundingvalue
                    failvalue1 = Single.Epsilon
                    failvalue2 = Single.MaxValue
                Else
                    passvalue1 = Single.MinValue
                    passvalue2 = boundingvalue
                    failvalue1 = boundingvalue + delta
                    failvalue2 = Single.MaxValue
                End If

            Case Inequality.NotEqualTo
                If boundingvalue = 0.0R Then
                    passvalue1 = Single.Epsilon
                    passvalue2 = -Single.Epsilon
                    failvalue1 = boundingvalue
                    failvalue2 = boundingvalue
                Else
                    passvalue1 = boundingvalue - delta
                    passvalue2 = boundingvalue + delta
                    failvalue1 = boundingvalue
                    failvalue2 = boundingvalue
                End If

        End Select

        '// Test the passing values:
        Dim testflag As Boolean = True
        Try
            pd.SetValue(component, passvalue1)
        Catch ex As Exception
            testflag = False
        End Try
        Assert.IsTrue(testflag)

        Try
            pd.SetValue(component, passvalue2)
        Catch ex As Exception
            testflag = False
        End Try
        Assert.IsTrue(testflag)

        '// Now test the failing values:
        testflag = False
        Try
            pd.SetValue(component, failvalue1)
        Catch aoorex As ArgumentOutOfRangeException
            testflag = True
        Catch ex As Exception

        End Try
        Assert.IsTrue(testflag)

        testflag = False
        Try
            pd.SetValue(component, failvalue2)
        Catch aoorex As ArgumentOutOfRangeException
            testflag = True
        Catch ex As Exception

        End Try
        Assert.IsTrue(testflag)

        '// Test persistence - what is assigned comes back OK:
        pd.SetValue(component, passvalue1)
        Assert.IsTrue(CSng(pd.GetValue(component)) = passvalue1)

        pd.SetValue(component, passvalue2)
        Assert.IsTrue(CSng(pd.GetValue(component)) = passvalue2)


    End Sub


    ''' <summary>
    ''' Tests a property of type double that has a two-sided bounds validation check that
    ''' throws an ArgumentOutOfRange exception on invalid input.
    ''' </summary>
    ''' <param name="component">A reference to the object that owns the property to be tested.</param>
    ''' <param name="propertyname">The name of the property to be tested.</param>
    ''' <param name="lowerboundingvalue">The value of the lower validation bound.</param>
    ''' <param name="lowercomparator">An inequality used to compare the lower bounding value to the property value.</param>
    ''' <param name="upperboundingvalue">The value of the upper validation bound.</param>
    ''' <param name="uppercomparator">An inequality used to compare the upper bounding value to the property value.</param>
    ''' <remarks></remarks>
    Public Sub TestPropertySingle(ByVal component As Object, _
                                  ByVal propertyname As String, _
                                  ByVal lowerboundingvalue As Single, _
                                  ByVal lowercomparator As Inequality, _
                                  ByVal upperboundingvalue As Single, _
                                  ByVal uppercomparator As Inequality)

        Dim pdc As PropertyDescriptorCollection = TypeDescriptor.GetProperties(component)
        Dim pd As PropertyDescriptor = Nothing
        For Each pd2 As PropertyDescriptor In pdc
            If pd2.Name = propertyname Then
                pd = pd2
            End If
        Next

        '// make the assertion that there is a match of the proper type:
        Assert.IsNotNull(pd)
        Assert.IsTrue(pd.PropertyType Is GetType(Single))

        Dim lowerdelta As Single = CSng(Abs(lowerboundingvalue / 100000.0F))
        Dim upperdelta As Single = CSng(Abs(upperboundingvalue / 100000.0F))

        Dim passvalue1 As Single
        Dim passvalue2 As Single
        Dim failvalue1 As Single
        Dim failvalue2 As Single
        Select Case lowercomparator

            Case Inequality.GreaterThan
                If lowerboundingvalue = 0.0R Then
                    passvalue1 = Single.Epsilon
                Else
                    passvalue1 = lowerboundingvalue + lowerdelta
                End If
                passvalue2 = passvalue1
                failvalue1 = lowerboundingvalue

            Case Inequality.GreaterThanOrEqualTo
                If lowerboundingvalue = 0.0R Then
                    passvalue1 = Single.Epsilon
                    failvalue1 = -Single.Epsilon
                Else
                    passvalue1 = lowerboundingvalue + lowerdelta
                    failvalue1 = lowerboundingvalue - lowerdelta
                End If
                passvalue2 = lowerboundingvalue

            Case Inequality.LessThan
                If lowerboundingvalue = 0.0R Then
                    passvalue1 = -Single.Epsilon
                Else
                    passvalue1 = lowerboundingvalue - lowerdelta
                End If
                passvalue2 = passvalue1
                failvalue1 = lowerboundingvalue

            Case Inequality.LessThanOrEqualTo
                If lowerboundingvalue = 0.0R Then
                    passvalue1 = -Single.Epsilon
                    failvalue1 = Single.Epsilon
                Else
                    passvalue1 = lowerboundingvalue - lowerdelta
                    failvalue1 = lowerboundingvalue + lowerdelta
                End If
                passvalue2 = lowerboundingvalue

            Case Inequality.NotEqualTo
                If lowerboundingvalue = 0.0R Then
                    passvalue1 = Single.Epsilon
                    passvalue2 = -Single.Epsilon
                    failvalue1 = lowerboundingvalue
                    failvalue2 = lowerboundingvalue
                Else
                    passvalue1 = lowerboundingvalue - lowerdelta
                    passvalue2 = lowerboundingvalue + lowerdelta
                    failvalue1 = lowerboundingvalue
                    failvalue2 = lowerboundingvalue
                End If

        End Select

        '// Test the passing values:
        Dim testflag As Boolean = True
        Try
            pd.SetValue(component, passvalue1)
        Catch ex As Exception
            testflag = False
        End Try
        Assert.IsTrue(testflag)

        Try
            pd.SetValue(component, passvalue2)
        Catch ex As Exception
            testflag = False
        End Try
        Assert.IsTrue(testflag)

        '// Now test the failing values:
        testflag = False
        Try
            pd.SetValue(component, failvalue1)
        Catch aoorex As ArgumentOutOfRangeException
            testflag = True
        Catch ex As Exception

        End Try
        Assert.IsTrue(testflag)

        '// Test persistence - what is assigned comes back OK:
        pd.SetValue(component, passvalue1)
        Assert.IsTrue(CSng(pd.GetValue(component)) = passvalue1)

        pd.SetValue(component, passvalue2)
        Assert.IsTrue(CSng(pd.GetValue(component)) = passvalue2)

        '// Now repeat the sequency for the upper bound:
        Select Case uppercomparator

            Case Inequality.GreaterThan
                If upperboundingvalue = 0.0R Then
                    passvalue1 = Single.Epsilon
                Else
                    passvalue1 = upperboundingvalue + upperdelta
                End If
                passvalue2 = passvalue1
                failvalue1 = upperboundingvalue

            Case Inequality.GreaterThanOrEqualTo
                If upperboundingvalue = 0.0R Then
                    passvalue1 = Single.Epsilon
                    failvalue1 = -Single.Epsilon
                Else
                    passvalue1 = upperboundingvalue + upperdelta
                    failvalue1 = upperboundingvalue - upperdelta
                End If
                passvalue2 = upperboundingvalue

            Case Inequality.LessThan
                If upperboundingvalue = 0.0R Then
                    passvalue1 = -Single.Epsilon
                Else
                    passvalue1 = upperboundingvalue - upperdelta
                End If
                passvalue2 = passvalue1
                failvalue1 = upperboundingvalue

            Case Inequality.LessThanOrEqualTo
                If upperboundingvalue = 0.0R Then
                    passvalue1 = -Single.Epsilon
                    failvalue1 = Single.Epsilon
                Else
                    passvalue1 = upperboundingvalue - upperdelta
                    failvalue1 = upperboundingvalue + upperdelta
                End If
                passvalue2 = upperboundingvalue

            Case Inequality.NotEqualTo
                If upperboundingvalue = 0.0R Then
                    passvalue1 = Single.Epsilon
                    passvalue2 = -Single.Epsilon
                    failvalue1 = upperboundingvalue
                    failvalue2 = upperboundingvalue
                Else
                    passvalue1 = upperboundingvalue - upperdelta
                    passvalue2 = upperboundingvalue + upperdelta
                    failvalue1 = upperboundingvalue
                    failvalue2 = upperboundingvalue
                End If

        End Select

        '// Test the passing values:
        testflag = True
        Try
            pd.SetValue(component, passvalue1)
        Catch ex As Exception
            testflag = False
        End Try
        Assert.IsTrue(testflag)

        Try
            pd.SetValue(component, passvalue2)
        Catch ex As Exception
            testflag = False
        End Try
        Assert.IsTrue(testflag)

        '// Now test the failing values:
        testflag = False
        Try
            pd.SetValue(component, failvalue1)
        Catch aoorex As ArgumentOutOfRangeException
            testflag = True
        Catch ex As Exception

        End Try
        Assert.IsTrue(testflag)


    End Sub




    Public Function FractionalError(ByVal testvalue As Double, ByVal expectedvalue As Double) As Double

        Return (testvalue - expectedvalue) / expectedvalue

    End Function

    Public Function FractionalError(ByVal testvalue As Single, ByVal expectedvalue As Single) As Double

        Return CDbl((testvalue - expectedvalue) / expectedvalue)

    End Function


    ''' <summary>
    ''' Tests the agreement of abs((testvalue - expectedvalue)/expectedvalue) to within the stated precision.
    ''' </summary>
    ''' <param name="testvalue"></param>
    ''' <param name="expectedvalue"></param>
    ''' <param name="precision">If the values must be exactly equal, set precision = 0.0.</param>
    ''' <remarks></remarks>
    Public Function TestFractionalPrecision(ByVal testvalue As Double, ByVal expectedvalue As Double, ByVal precision As Double) As Boolean

        If precision < 0.0R Then precision = Abs(precision)

        If precision = 0.0R Then Return (testvalue = expectedvalue)

        '// If the expected value is zero, then the fractional error will, by definition, be infinity
        '// for any testvalue not equal to zero.
        If expectedvalue = 0.0R Then
            If testvalue = 0.0R Then Return True
            Return False
        End If

        Dim frac As Double = Abs((testvalue - expectedvalue) / expectedvalue)
        Return (frac <= precision)

    End Function


    ''' <summary>
    ''' Tests the agreement of abs(testvalue - expectedvalue) to within the stated precision.
    ''' </summary>
    ''' <param name="testvalue"></param>
    ''' <param name="expectedvalue"></param>
    ''' <param name="precision"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TestAbsolutePrecision(ByVal testvalue As Double, ByVal expectedvalue As Double, ByVal precision As Double) As Boolean

        If precision < 0.0R Then precision = Abs(precision)

        If precision = 0.0R Then Return (testvalue = expectedvalue)

        Dim delta As Double = testvalue - expectedvalue

        Return (Abs(delta) <= precision)

    End Function


End Module
