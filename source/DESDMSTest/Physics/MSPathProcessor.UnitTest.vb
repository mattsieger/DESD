Imports DESD
Imports DESD.Math

<TestClass> _
Public Class MSPathProcessor_UnitTest

    '	<TestMethod()> _
    '	Public Sub FMatrix01
    '		Dim k as New Complex(1.8842, 0.0)  
    '		
    '        Dim delta() As Double = {-0.4004146, _
    '                                  0.6112068, _
    '                                  1.329133, _
    '                                  0.3146167, _
    '                                  0.04819505, _
    '                                  0.005902354, _
    '                                  0.0005629561, _
    '                                  0.00004174712, _
    '                                  0.000002449426, _
    '                                  0.0000001161656}
    '        Dim Lmax as Integer = 9                          
    '        Dim tmatrix(Lmax) As Complex
    '        For i As Integer = 0 To Lmax
    '            tmatrix(i) = System.Math.Sin(delta(i)) * Complex.CExp(Complex.i * delta(i))
    '        Next
    '                                        
    '        Dim R1 As New Vector(0.0, 0.0, 7.5589)
    '        Dim R2 As New Vector(0.0, 0.0, 0.0)
    '        Dim R3 As New Vector(-2.5653, 2.5653, -2.5653)
    '        
    ''        Dim lm As Integer(,) = MSPathPRocessor.LM(tmatrix.Length-1)
    '        Dim ra As Integer(,) = MSPathPRocessor.MuNu(2)
    ''        
    ''        Console.WriteLine("lm.Length = " & lm.Length.ToString)
    ''        For i As Integer = 0 To lm.Length\2 - 1
    ''        	Console.WriteLine("lm " & i.ToString & " = (" & lm(i,0).ToString & ", " & lm(i,1).ToString & ")")
    ''        Next
    '        
    ''        For i As Integer = 0 To ra.length\2 - 1
    ''        	Console.WriteLine("ra " & i.ToString & " = (" & ra(i,0).ToString & ", " & ra(i,1).ToString & ")")
    ''        Next
    '                                 
    '        Dim Fmatrix As ComplexMatrix = MSPathProcessor2.F(R1, R2, R3, k, tmatrix, 2)
    '
    '        Console.WriteLine("Rows of F = " & Fmatrix.rows.ToString)
    '        Console.WriteLine("Columns of F = " & Fmatrix.Columns.ToString)
    '        
    '        Console.WriteLine
    '        For lamda As Integer = 0 To ra.Length\2 - 1
    '        	For lamdaprime As Integer = 0 To ra.Length\2 - 1
    '        		Console.WriteLine("(" & ra(lamda,0).ToString & "," & ra(lamda,1).ToString & ")(" & ra(lamdaprime,0).ToString & "," & ra(lamdaprime,1).ToString & ") = " & Fmatrix(lamda,lamdaprime).tostring)
    '        	Next
    '        Next
    '	End Sub


    ''' <summary>
    ''' 100 calculations of F matrix at RAOrder = 2 (6x6), Lmax = 9
    ''' 36 seconds at work.
    ''' </summary>
    <TestMethod()> _
    Public Sub FmatrixSpeedTest01()
        Dim Lmax As Integer = 4
        Dim raorder As Integer = 2
        Dim N As Integer = 100

        Dim k As New Complex(1.8842, 0.0)

        Dim delta() As Double = {-0.4004146, _
                                  0.6112068, _
                                  1.329133, _
                                  0.3146167, _
                                  0.04819505, _
                                  0.005902354, _
                                  0.0005629561, _
                                  0.00004174712, _
                                  0.000002449426, _
                                  0.0000001161656}


        Dim tmatrix(Lmax) As Complex
        For i As Integer = 0 To Lmax
            tmatrix(i) = System.Math.Sin(delta(i)) * Complex.CExp(Complex.i * delta(i))
        Next

        Dim R1 As New Vector(0.0, 0.0, 7.5589)
        Dim R2 As New Vector(0.0, 0.0, 0.0)
        Dim R3 As New Vector(-2.5653, 2.5653, -2.5653)

        Dim Fmatrix As ComplexMatrix

        Console.WriteLine("Start Time = " & Now.ToString)

        For i As Integer = 1 To N
            Fmatrix = MSPathProcessor.F(R1, R2, R3, k, tmatrix, raorder)
        Next

        Console.WriteLine("End Time = " & Now.ToString)

    End Sub

    <TestMethod()> _
    Public Sub DirectTerm01()
        Dim R As New Vector
        Dim k As New Complex(1.8842, 0.0)
        'Dim khat As Vector = Vector.NewFromSpherical(1.0,-System.Math.PI,0.0)
        Dim khat As Vector = Vector.NewFromSpherical(1.0, 1.0, 0.5)
        Dim zv0 As Double = 0.0
        Dim lmax As Integer = 10

        Dim result As ComplexMatrix = MSPathProcessor.DirectTerm(R, k, khat, zv0, lmax)

        Dim lm As Integer(,) = MSPathProcessor.LM(lmax)
        Dim mag As Double
        For il As Integer = 0 To lm.Length \ 2 - 1
            mag = result(il, 0).Magnitude
            Console.WriteLine("(" & lm(il, 0).ToString & "," & lm(il, 1).ToString & ") = " & result(il, 0).tostring)
            'Console.WriteLine("(" & lm(il,0).ToString & "," & lm(il,1).ToString & ") = " & mag.tostring)
        Next


    End Sub

    '	<TestMethod()> _
    '	Public Sub F01
    '		
    '		Dim k as New Complex(1.8842, 0.1)  
    '		
    '        Dim delta() As Double = {-0.4004146, _
    '                                  0.6112068, _
    '                                  1.329133, _
    '                                  0.3146167, _
    '                                  0.04819505, _
    '                                  0.005902354, _
    '                                  0.0005629561, _
    '                                  0.00004174712, _
    '                                  0.000002449426, _
    '                                  0.0000001161656}
    '                                  
    '        Dim Lmax as Integer = 9                          
    '        Dim tmatrix(Lmax) As Complex
    '        For i As Integer = 0 To Lmax
    '            tmatrix(i) = System.Math.Sin(delta(i)) * Complex.CExp(Complex.i * delta(i))
    '        Next
    '                                        
    '        Dim R1 As New Vector(0.0, 0.0, 7.5589)
    '        Dim R2 As New Vector(0.0, 0.0, 0.0)
    '        Dim R3 As New Vector(-2.5653, 2.5653, -2.5653)
    '
    '		Dim Fold As ComplexMatrix = MSPathProcessor.F(R1,R2,R3,k,tmatrix,2)
    '		Dim Fnew As ComplexMatrix = MSPathPRocessor2.F(R1,R2,R3,k,tmatrix,2)
    '		
    '		Dim ra As Integer(,) = MSPathPRocessor.MuNu(2)
    '		Dim del as Double = 1.0E-14
    '		For lamda As Integer = 0 To ra.Length\2 - 1
    '        	For lamdaprime As Integer = 0 To ra.Length\2 - 1
    '        		'Console.WriteLine("(" & ra(lamda,0).ToString & "," & ra(lamda,1).ToString & ")(" & ra(lamdaprime,0).ToString & "," & ra(lamdaprime,1).ToString & ") : " & Fold(lamda,lamdaprime).tostring & "     " & Fnew(lamda,lamdaprime).tostring)
    '        		Assert.AreEqual(Fold(lamda,lamdaprime).Real,Fnew(lamda,lamdaprime).Real,del)
    '        		Assert.AreEqual(Fold(lamda,lamdaprime).Imag,Fnew(lamda,lamdaprime).Imag,del)
    '        	Next
    '        Next
    '
    '
    '	End Sub


    '	<TestMethod()> _
    '	Public Sub F02
    '		
    '		Dim k as New Complex(2.1842, 0.1)  
    '		
    '        Dim delta() As Double = {-0.4004146, _
    '                                  0.6112068, _
    '                                  1.329133, _
    '                                  0.3146167, _
    '                                  0.04819505, _
    '                                  0.005902354, _
    '                                  0.0005629561, _
    '                                  0.00004174712, _
    '                                  0.000002449426, _
    '                                  0.0000001161656}
    '                                  
    '        Dim tmatrix(delta.Length-1) As Complex
    '        For i As Integer = 0 To delta.Length-1
    '            tmatrix(i) = System.Math.Sin(delta(i)) * Complex.CExp(Complex.i * delta(i))
    '        Next
    '                                        
    '        Dim R1 As New Vector(2.1234, 0.0, 7.5589)
    '        Dim R2 As New Vector(0.0, 0.435, 0.0)
    '        Dim R3 As New Vector(2.1653, 1.5653, -3.5653)
    '
    '		Dim Fold As ComplexMatrix = MSPathProcessor.F(R1,R2,R3,k,tmatrix,2)
    '		Dim Fnew As ComplexMatrix = MSPathPRocessor.F_new(R1,R2,R3,k,tmatrix,2)
    '		
    '		Dim ra As Integer(,) = MSPathPRocessor.MuNu(2)
    '		Dim del as Double = 1.0E-13
    '		For lamda As Integer = 0 To ra.Length\2 - 1
    '        	For lamdaprime As Integer = 0 To ra.Length\2 - 1
    '        		Console.WriteLine("(" & ra(lamda,0).ToString & "," & ra(lamda,1).ToString & ")(" & ra(lamdaprime,0).ToString & "," & ra(lamdaprime,1).ToString & ") : " & Fold(lamda,lamdaprime).tostring & "     " & Fnew(lamda,lamdaprime).tostring)
    '        		Assert.AreEqual(Fold(lamda,lamdaprime).Real,Fnew(lamda,lamdaprime).Real,del)
    '        		Assert.AreEqual(Fold(lamda,lamdaprime).Imag,Fnew(lamda,lamdaprime).Imag,del)
    '        	Next
    '        Next
    '
    '
    '	End Sub
    '

    '	<TestMethod()> _
    '	Public Sub F03
    '		
    '		'// 12.15.2010 - After running this with a lot of output from the f_new routine in MSMathProcessor, I find that
    '		'// the Fnew routine is correct, after hand calculation of Flamda,lamdaprime.  
    '		
    '		Dim k as New Complex(3.1842, 0.1)  
    '		
    ''        Dim delta() As Double = {-0.4004146, _
    ''                                  0.6112068, _
    ''                                  1.329133, _
    ''                                  0.3146167, _
    ''                                  0.04819505, _
    ''                                  0.005902354, _
    ''                                  0.0005629561, _
    ''                                  0.00004174712, _
    ''                                  0.000002449426, _
    ''                                  0.0000001161656}
    '         Dim delta() As Double = {-0.4004146, _
    '                                  0.6112068}
    '                                 
    '        Dim tmatrix(delta.Length-1) As Complex
    '        For i As Integer = 0 To delta.Length-1
    '            tmatrix(i) = System.Math.Sin(delta(i)) * Complex.CExp(Complex.i * delta(i))
    '        Next
    '                        
    '        tmatrix(0) = New Complex(0.0, 0.0)
    '        tmatrix(1) = New Complex(1.0, 0.0)
    '        
    '        Dim R1 As New Vector(0.0, 0.0, 0.0)
    '        Dim R2 As New Vector(2.0, 0.0, 0.0)
    '        Dim R3 As New Vector(0.0, 2.0, 0.0)
    '
    '		Dim Fold As ComplexMatrix = MSPathProcessor.F(R1,R2,R3,k,tmatrix,2)
    '		Dim Fnew As ComplexMatrix = MSPathPRocessor.F_new(R1,R2,R3,k,tmatrix,2)
    '		
    '		Dim rho As New Complex(6.3684,0.2)
    '		Dim rhoprime As New Complex(9.00627765061682,0.282842712474619)
    '		Dim beta As Double = 2.35619449019235
    '		
    '		Dim expectval as Complex = Complex.i * 1.5 * System.math.Sin(beta) * (1.0 - 1.0/(complex.i * rhoprime)) / (complex.i * rho)
    '		
    '		Console.Writeline("Expectval = " & expectval.ToString)
    '		
    '		Dim ra As Integer(,) = MSPathPRocessor.MuNu(2)
    '		Dim del as Double = 1.0E-13
    '		For lamda As Integer = 0 To ra.Length\2 - 1
    '        	For lamdaprime As Integer = 0 To ra.Length\2 - 1
    '        		Console.WriteLine("(" & ra(lamda,0).ToString & "," & ra(lamda,1).ToString & ")(" & ra(lamdaprime,0).ToString & "," & ra(lamdaprime,1).ToString & ") : " & Fold(lamda,lamdaprime).tostring & "     " & Fnew(lamda,lamdaprime).tostring)
    '        		Assert.AreEqual(Fold(lamda,lamdaprime).Real,Fnew(lamda,lamdaprime).Real,del)
    '        		Assert.AreEqual(Fold(lamda,lamdaprime).Imag,Fnew(lamda,lamdaprime).Imag,del)
    '        	Next
    '        Next
    '
    '	End Sub

    <TestMethod()> _
    Public Sub F01()

        Dim R1 As New Vector(0, 4.43, -3.14)
        Dim R2 As New Vector(0, 6.65036465165633, -7.05377691382425)
        Dim R3 As New Vector(0, 4.43, -3.14)
        Dim k As New Complex(3.09930915108513, 0.0998656548569126)

        Dim T(5) As Complex

        T(0) = New Complex(-0.389944683675476, 0.218780411711599)
        T(1) = New Complex(0.306727842237608, 0.114239480200205)
        T(2) = New Complex(0.415988817690267, 0.229321347021421)
        T(3) = New Complex(0.0518587080298501, 0.00551084020398789)
        T(4) = New Complex(0.00509679577304822, -0.000049917231309844)
        T(5) = New Complex(0.00031490020679943, -0.0000329955364490677)

        Dim testval As ComplexMatrix = MSPathProcessor.F(R1, R2, R3, k, T, 2)

        Dim _RA As Integer(,) = MSPathProcessor.MuNu(2)
        Dim _NRA As Integer = 6
        Dim mu, nu, muprime, nuprime As Integer
        Dim F As Complex
        For lamda As Integer = 0 To _NRA - 1
            mu = _RA(lamda, 0)
            nu = _RA(lamda, 1)
            For lamdaprime As Integer = 0 To _NRA - 1
                muprime = _RA(lamdaprime, 0)
                nuprime = _RA(lamdaprime, 1)
                Console.WriteLine("(mu, nu)(muprime, nuprime) = (" & mu.ToString & "," & nu.ToString & ")(" & muprime.ToString & "," & nuprime.ToString & ")")
                F = testval(lamda, lamdaprime)
                Console.WriteLine("F(lamda,lamdaprime) = " & F.tostring)
                Assert.IsFalse(Double.IsNaN(F.Real))
                Assert.IsFalse(Double.IsNaN(F.Imag))
            Next
        Next

    End Sub

    <TestMethod()> _
    Public Sub F02()

        Dim R1 As New Vector(5.75938473276443, -3.32518232582816, 0)
        Dim R2 As New Vector(5.75938473276443, -5.54197054304694, -0.783752990424917)
        Dim R3 As New Vector(5.75938473276443, -14.409123411922, -3.91876495212459)
        Dim k As New Complex(3.09930915108513, 0.0998656548569126)

        Dim T(5) As Complex

        T(0) = New Complex(-0.389944683675476, 0.218780411711599)
        T(1) = New Complex(0.306727842237608, 0.114239480200205)
        T(2) = New Complex(0.415988817690267, 0.229321347021421)
        T(3) = New Complex(0.0518587080298501, 0.00551084020398789)
        T(4) = New Complex(0.00509679577304822, -0.000049917231309844)
        T(5) = New Complex(0.00031490020679943, -0.0000329955364490677)

        Dim testval As ComplexMatrix = MSPathProcessor.F(R1, R2, R3, k, T, 2)

        Dim _RA As Integer(,) = MSPathProcessor.MuNu(2)
        Dim _NRA As Integer = 6
        Dim mu, nu, muprime, nuprime As Integer
        Dim F As Complex
        For lamda As Integer = 0 To _NRA - 1
            mu = _RA(lamda, 0)
            nu = _RA(lamda, 1)
            For lamdaprime As Integer = 0 To _NRA - 1
                muprime = _RA(lamdaprime, 0)
                nuprime = _RA(lamdaprime, 1)
                Console.WriteLine("(mu, nu)(muprime, nuprime) = (" & mu.ToString & "," & nu.ToString & ")(" & muprime.ToString & "," & nuprime.ToString & ")")
                F = testval(lamda, lamdaprime)
                Console.WriteLine("F(lamda,lamdaprime) = " & F.tostring)
                Assert.IsFalse(Double.IsNaN(F.Real))
                Assert.IsFalse(Double.IsNaN(F.Imag))
            Next
        Next

    End Sub


    <TestMethod()> _
    Public Sub QBP01()
        '// Tests the proposition that Q = BP
        Dim R1 As New Vector(0, 4.43, -3.14)
        Dim R2 As New Vector(0, 6.65036465165633, -7.05377691382425)
        Dim k As New Complex(3.09930915108513, 0.0998656548569126)
        Dim khat As New Vector(1.0, 1.0, -1.0)
        Dim zv0 As Double = 1.0
        Dim raorder As Integer = 2
        Dim lmax As Integer = 5

        Dim T(5) As Complex

        T(0) = New Complex(-0.389944683675476, 0.218780411711599)
        T(1) = New Complex(0.306727842237608, 0.114239480200205)
        T(2) = New Complex(0.415988817690267, 0.229321347021421)
        T(3) = New Complex(0.0518587080298501, 0.00551084020398789)
        T(4) = New Complex(0.00509679577304822, -0.000049917231309844)
        T(5) = New Complex(0.00031490020679943, -0.0000329955364490677)


        Dim Q As ComplexMatrix = MSPathProcessor.Q(R1, R2, k, khat, T, zv0, raorder)

        Dim B As ComplexMatrix = MSPathProcessor.B(R1, R2, k, T, raorder)

        Dim P As ComplexMatrix = MSPathProcessor.P(R2, k, khat, zv0, lmax)

        Dim Q2 As ComplexMatrix = B * P

        Assert.AreEqual(Q.Rows, Q2.Rows)
        Assert.AreEqual(Q.Columns, Q2.Columns)
        Assert.AreEqual(Q.Rows, 6)
        Assert.AreEqual(Q.Columns, 1)

        Dim expectval As Complex
        Dim testval As Complex
        Dim delta As Double = 0.0000000001
        For i As Integer = 0 To Q.Rows - 1
            expectval = Q(i, 0)
            testval = Q2(i, 0)
            Assert.AreEqual(expectval.real, testval.real, delta)
        Next

    End Sub

End Class
