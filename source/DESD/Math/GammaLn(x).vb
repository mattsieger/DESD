Imports System.Math

Namespace Math



    Partial Public Class Functions


        ''' <summary>
        ''' Returns the natural log of the Gamma function ln(Gamma(x)) for x >= 0.
        ''' </summary>
        ''' <param name="x">Must be greater than or equal to zero.</param>
        ''' <returns>Double</returns>
        ''' <remarks>
        ''' Author:  Matt Sieger
        ''' Revision History:
        ''' 10/5/2006 - imported code from the old CancunXL Math project.
        ''' 
        ''' Algorithm ------------------------
        '''    William Cody, Kenneth Hillstrom,
        '''    Chebyshev Approximations for the Natural Logarithm of the Gamma Function,
        '''    Mathematics of Computation,
        '''    Volume 21, 1967, pages 198-203.
        ''' 
        ''' Implementation -------------------
        ''' Adapted from the FORTRAN routine GAMMA_LOG in the polpak library:
        ''' http://www.scs.fsu.edu/~burkardt/f_src/polpak/polpak.f90
        ''' 
        ''' Testing and Accuracy -------------
        ''' Unit testing shows this routine is good to machine precision.
        ''' 
        ''' References -----------------------
        ''' http://en.wikipedia.org/wiki/Gamma_function 
        ''' 
        ''' </remarks>
        Public Shared Function GammaLn(ByVal x As Double) As Double


            Dim c() As Double = {-0.001910444077728, _
                                  0.00084171387781295, _
                                 -0.00059523799130430121, _
                                  0.0007936507935003503, _
                                 -0.0027777777777776816, _
                                  0.083333333333333329, _
                                  0.0057083835261}

            Dim d1 As Double = -0.57721566490153287
            Dim d2 As Double = 0.42278433509846713
            Dim d4 As Double = 1.791759469228055
            Dim frtbig As Double = 1420000000.0
            Dim p1() As Double = {4.9452353592967269, _
                                     201.8112620856775, _
                                     2290.8383738313464, _
                                     11319.672059033808, _
                                     28557.246356716354, _
                                     38484.962284437934, _
                                     26377.487876241954, _
                                     7225.8139797002877}
            Dim p2() As Double = {4.974607845568932, _
                                     542.4138599891071, _
                                     15506.938649783649, _
                                     184793.29044456323, _
                                     1088204.7694688288, _
                                     3338152.96798703, _
                                     5106661.6789273527, _
                                     3074109.0548505397}
            Dim p4() As Double = {14745.0216605994, _
                                     2426813.3694867045, _
                                     121475557.40450932, _
                                     2663432449.6309772, _
                                     29403789566.345539, _
                                     170266573776.53989, _
                                     492612579337.7431, _
                                     560625185622.39514}

            Dim corr As Double
            Dim eps As Double = Double.Epsilon
            Dim i As Integer
            Dim pnt68 As Double = 0.6796875
            Dim q1() As Double = {67.482125503037778, _
                                     1113.3323938571993, _
                                     7738.7570569353984, _
                                     27639.870744033407, _
                                     54993.102062261576, _
                                     61611.221800660023, _
                                     36351.2759150194, _
                                     8785.5363024310136}

            Dim q2() As Double = {183.03283993705926, _
                                     7765.0493214450062, _
                                     133190.38279660742, _
                                     1136705.8213219696, _
                                     5267964.1174379466, _
                                     13467014.543111017, _
                                     17827365.303532742, _
                                     9533095.5918443538}
            Dim q4() As Double = {2690.5301758708993, _
                                     639388.56543000927, _
                                     41355999.302413881, _
                                     1120872109.616148, _
                                     14886137286.788137, _
                                     101680358627.24382, _
                                     341747634550.73773, _
                                     446315818741.97131}
            Dim res As Double
            Dim sqrtpi_ As Double = 0.91893853320467278
            Dim xbig As Double = 4.08E+36
            Dim xden As Double
            Dim xm1 As Double
            Dim xm2 As Double
            Dim xm4 As Double
            Dim xnum As Double
            Dim xsq As Double

            '!
            '!  Return immediately if the argument is out of range.
            '!
            If (x < 0.0R Or xbig < x) Then Throw New ArgumentOutOfRangeException("x", "Parameter must be greater than or equal to zero and less than or equal to 4.08E+36")
            If x = 0.0R Then Return Double.PositiveInfinity


            If (x <= eps) Then

                res = -Log(x)

            ElseIf (x <= 1.5) Then

                If (x < pnt68) Then
                    corr = -Log(x)
                    xm1 = x
                Else
                    corr = 0.0
                    xm1 = (x - 0.5) - 0.5
                End If

                If (x <= 0.5 Or pnt68 <= x) Then

                    xden = 1.0
                    xnum = 0.0

                    For i = 0 To 7
                        xnum = xnum * xm1 + p1(i)
                        xden = xden * xm1 + q1(i)
                    Next i

                    res = corr + (xm1 * (d1 + xm1 * (xnum / xden)))

                Else

                    xm2 = (x - 0.5) - 0.5
                    xden = 1.0
                    xnum = 0.0
                    For i = 0 To 7
                        xnum = xnum * xm2 + p2(i)
                        xden = xden * xm2 + q2(i)
                    Next i

                    res = corr + xm2 * (d2 + xm2 * (xnum / xden))

                End If

            ElseIf (x <= 4.0) Then

                xm2 = x - 2.0
                xden = 1.0
                xnum = 0.0
                For i = 0 To 7
                    xnum = xnum * xm2 + p2(i)
                    xden = xden * xm2 + q2(i)
                Next i

                res = xm2 * (d2 + xm2 * (xnum / xden))

            ElseIf (x <= 12.0) Then

                xm4 = x - 4.0
                xden = -1.0
                xnum = 0.0
                For i = 0 To 7
                    xnum = xnum * xm4 + p4(i)
                    xden = xden * xm4 + q4(i)
                Next i

                res = d4 + xm4 * (xnum / xden)

            Else

                res = 0.0

                If (x <= frtbig) Then

                    res = c(6)
                    xsq = x * x

                    For i = 0 To 5
                        res = res / xsq + c(i)
                    Next i

                End If

                res = res / x
                corr = Log(x)
                res = res + sqrtpi_ - 0.5 * corr
                res = res + x * (corr - 1.0)

            End If

            Return res

        End Function
    End Class
End Namespace