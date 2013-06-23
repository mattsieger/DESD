
Imports system.math

Namespace Math


    Partial Public Class Functions


        ''' <summary>
        ''' The lower-case spherical harmonic rotation matrix r.
        ''' </summary>
        ''' <param name="j"></param>
        ''' <param name="m"></param>
        ''' <param name="mprime"></param>
        ''' <param name="beta"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function LRotm_old(ByVal j As Integer, ByVal m As Integer, ByVal mprime As Integer, ByVal beta As Double) As Double

            '// Do some range checking on inputs:
            '// m and mprime must both be less than or equal to j
            If (Abs(m) > j) Or (Abs(mprime) > j) Then
                'Throw New ArgumentOutOfRangeException("m, mprime", "m and mprime must be less than or equal to j.")
                Return 0.0
            End If

            '// j must be positive or zero
            If (j < 0) Then Throw New ArgumentOutOfRangeException("j", "j must be greater than or equal to zero.")


            Dim eta As Double = Sin(0.5 * beta)
            Dim xi As Double = Cos(0.5 * beta)

            Dim num As Double = Exp(0.5 * (FactorialLn(j + m) + FactorialLn(j - m) + FactorialLn(j + mprime) + FactorialLn(j - mprime)))

            '// find range of summation
            Dim tmin As Integer = Max(0, m - mprime)
            Dim tmax As Integer = Min(j + m, j - mprime)

            Dim sum As Double = 0.0
            Dim temp1 As Double
            Dim temp2 As Double
            Dim temp3 As Double
            Dim temp4 As Double

            For t As Integer = tmin To tmax

                temp1 = Pow(-1.0, CDbl(t))
                temp2 = Exp(-1.0 * (FactorialLn(j + m - t) + _
                                    FactorialLn(j - mprime - t) + _
                                    FactorialLn(t) + _
                                    FactorialLn(t - m + mprime)))
                temp3 = Pow(xi, 2.0 * j + m - mprime - 2.0 * t)
                temp4 = Pow(eta, 2.0 * t - m + mprime)
                sum += temp1 * temp2 * temp3 * temp4

            Next

            Return num * sum

        End Function


        Public Shared Function LRotm(ByVal j As Integer, ByVal m As Integer, ByVal mprime As Integer, ByVal beta As Double) As Double

            '// Do some range checking on inputs:
            '// m and mprime must both be less than or equal to j
            If (Abs(m) > j) Or (Abs(mprime) > j) Then
                'Throw New ArgumentOutOfRangeException("m, mprime", "m and mprime must be less than or equal to j.")
                Return 0.0
            End If

            '// j must be positive or zero
            If (j < 0) Then Throw New ArgumentOutOfRangeException("j", "j must be greater than or equal to zero.")

            If (j = 0) Then Return 1.0

            If (j = 1) Then
                If m = -1 Then
                    If mprime = -1 Then
                        Return 0.5 * (1.0 + Cos(beta))
                    ElseIf mprime = 0 Then
                        Return (1.0 / Sqrt(2.0)) * Sin(beta)
                    ElseIf mprime = 1 Then
                        Return 0.5 * (1.0 - Cos(beta))
                    End If
                ElseIf m = 0 Then
                    If mprime = -1 Then
                        Return -(1.0 / Sqrt(2.0)) * Sin(beta)
                    ElseIf mprime = 0 Then
                        Return Cos(beta)
                    ElseIf mprime = 1 Then
                        Return (1.0 / Sqrt(2.0)) * Sin(beta)
                    End If
                ElseIf m = 1 Then
                    If mprime = -1 Then
                        Return 0.5 * (1.0 - Cos(beta))
                    ElseIf mprime = 0 Then
                        Return -(1.0 / Sqrt(2.0)) * Sin(beta)
                    ElseIf mprime = 1 Then
                        Return 0.5 * (1.0 + Cos(beta))
                    End If
                End If
            End If

            Dim eta As Double = Sin(0.5 * beta)
            Dim xi As Double = Cos(0.5 * beta)

            Dim num As Double = Exp(0.5 * (FactorialLn(j + m) + FactorialLn(j - m) + FactorialLn(j + mprime) + FactorialLn(j - mprime)))

            '// find range of summation
            Dim tmin As Integer = Max(0, m - mprime)
            Dim tmax As Integer = Min(j + m, j - mprime)

            Dim sum As Double = 0.0
            Dim temp1 As Double
            Dim temp2 As Double
            Dim temp3 As Double
            Dim temp4 As Double

            For t As Integer = tmin To tmax

                temp1 = Pow(-1.0, CDbl(t))
                temp2 = Exp(-1.0 * (FactorialLn(j + m - t) + _
                                    FactorialLn(j - mprime - t) + _
                                    FactorialLn(t) + _
                                    FactorialLn(t - m + mprime)))
                temp3 = Pow(xi, 2.0 * j + m - mprime - 2.0 * t)
                temp4 = Pow(eta, 2.0 * t - m + mprime)
                sum += temp1 * temp2 * temp3 * temp4

            Next

            Return num * sum

        End Function



        ''' <summary>
        ''' The lower-case spherical harmonic rotation matrix r, for non-integral values of j, m, and mprime.
        ''' </summary>
        ''' <param name="j"></param>
        ''' <param name="m"></param>
        ''' <param name="mprime"></param>
        ''' <param name="beta"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function LRotm2(ByVal j As Double, ByVal m As Double, ByVal mprime As Double, ByVal beta As Double) As Double

            '// One of the key assumptions is that j+m, j+mprime, and m+mprime are all integral:
            Dim jpm As Integer = CInt(j + m)
            Dim jmm As Integer = CInt(j - m)
            Dim jpmp As Integer = CInt(j + mprime)
            Dim jmmp As Integer = CInt(j - mprime)

            '// check for various zero conditions
            If (jpm < 0) OrElse (jmm < 0) OrElse (jpmp < 0) OrElse (jmmp < 0) Then Return 0.0

            Dim eta As Double = System.Math.Sin(0.5 * beta)
            Dim xi As Double = System.Math.Cos(0.5 * beta)

            Dim jpmf As Double = FactorialLn(jpm)
            Dim jmmf As Double = FactorialLn(jmm)
            Dim jpmpf As Double = FactorialLn(jpmp)
            Dim jmmpf As Double = FactorialLn(jmmp)

            Dim num As Double = System.Math.Exp(0.5 * (jpmf + jmmf + jpmpf + jmmpf))

            '// find range of summation
            Dim tmin As Integer '= Max(0, m - mprime)
            Dim tmax As Integer '= Min(Min(jpm, jmmp), (m + mprime))

            'If tmax < 0 Then tmax = 0
            'If tmin < 0 Then tmin = 0


            '// find range of summation
            If ((m - mprime) > 0) Then
                tmin = CInt(m - mprime)
            Else
                tmin = 0
            End If

            If (jpm > jmmp) Then
                tmax = jmmp
            Else
                tmax = jpm
            End If


            Dim sum As Double = 0.0
            Dim temp1 As Double
            Dim temp2 As Double
            Dim temp3 As Double
            Dim temp4 As Double

            For t As Integer = tmin To tmax

                temp1 = System.Math.Pow(-1.0, CDbl(t))
                temp2 = System.Math.Exp(-1.0 * (FactorialLn(CInt(jpm) - t) + _
                                                FactorialLn(CInt(jmmp) - t) + _
                                                FactorialLn(t) + _
                                                FactorialLn(t - CInt(m - mprime))))
                temp3 = System.Math.Pow(xi, 2.0 * j + m - mprime - 2.0 * CDbl(t))
                temp4 = System.Math.Pow(eta, 2.0 * CDbl(t) - m + mprime)
                sum += temp1 * temp2 * temp3 * temp4

            Next

            Return num * sum

        End Function


    End Class
End Namespace