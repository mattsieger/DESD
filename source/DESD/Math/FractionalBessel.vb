Imports System.Math
Namespace Math


    Partial Public Class Functions

        ''' <summary>
        ''' Returns the Bessel functions of fractional order J_nu(x), Y_nu(x), and their derivatives stored in the return
        ''' array:
        ''' ReturnValue(0) = J_nu(x)
        ''' ReturnValue(1) = Y_nu(x)
        ''' ReturnValue(2) = d(J_nu(x))/dx
        ''' ReturnValue(3) = d(Y_nu(x))/dx
        ''' Algorithm adapted from Numerical Recipes for Fortran.
        ''' </summary>
        ''' <param name="nu"></param>
        ''' <param name="x"></param>
        Public Shared Function FractionalBessel(nu As Double, x As Double) As Double()

            If x < 0.0 Then Throw New ArgumentException("x must be greater than or equal to zero.")

            Dim returnval(3) As Double

            If (x = 0.0) Then
                '// J is one, Y is singular
                returnval(0) = 1.0
                returnval(1) = Double.NegativeInfinity
                returnval(2) = 0.0
                returnval(3) = Double.PositiveInfinity
                Return returnval
            End If

            '// Apply reflection formula for negative nu values.
            If nu < 0.0 Then
                Dim posnu As Double() = FractionalBessel(Abs(nu), x)
                Dim nupi As Double = Abs(nu) * PI
                returnval(0) = Cos(nupi) * posnu(0) - Sin(nupi) * posnu(1)
                returnval(1) = Sin(nupi) * posnu(0) + Cos(nupi) * posnu(1)
                returnval(2) = Cos(nupi) * posnu(2) - Sin(nupi) * posnu(3)
                returnval(3) = Sin(nupi) * posnu(2) + Cos(nupi) * posnu(3)
                Return returnval
            End If

            Dim rj As Double
            Dim rjp As Double
            Dim ry As Double
            Dim ryp As Double

            Dim EPS As Double = 0.0000000000000001
            'Dim FPMIN As Double = 1.0E-30   '// A number close to the machine's smallest floating point number.
            Dim FPMIN As Double = 1.0E-300   '// A number close to the machine's smallest floating point number.
            Dim MAXIT As Integer = 10000
            Dim XMIN As Double = 2.0


            '//C USES beschb
            '// Returns the Bessel functions rj = Jν, ry = Yν and their derivatives rjp = J_
            '// ν, ryp = Y_ν,
            '// for positive x and for xnu = ν ≥ 0. The relative accuracy is within one or two significant
            '// digits of EPS, except near a zero of one of the functions, where EPS controls its absolute
            '// accuracy. FPMIN is a number close to the machine’s smallest floating-point number. All
            '// internal arithmetic is in double precision. To convert the entire routine to double precision,
            '// change the REAL declaration above and decrease EPS to 10−16. Also convert the subroutine
            '// beschb.

            Dim i As Integer
            Dim isign As Integer
            Dim l As Integer
            Dim nl As Integer

            Dim a, b, br, bi, c, cr, ci, d, del, del1, den, di, dlr, dli As Double
            Dim dr, e, f, fact, fact2, fact3, ff, gam, gam1, gam2, gammi, gampl, h As Double
            Dim p, pimu, pimu2, q, r, rjl, rjl1, rjmu, rjp1, rjpl, rjtemp, ry1 As Double
            Dim rymu, rymup, rytemp, sum, sum1, temp, w, x2, xi, xi2, xmu, xmu2 As Double



            If (x < XMIN) Then  '// nl is the number of downward recurrences of the J’s and
                '// upward recurrences of Y ’s. xmu lies between −1/2 and
                '// 1/2 for x < XMIN, while it is chosen so that x is greater
                '// than the turning point for x ≥ XMIN.
                nl = Convert.ToInt32(nu + 0.5)
            Else
                nl = System.Math.Max(0, Convert.ToInt32(nu - x + 1.5))
            End If

            xmu = nu - CDbl(nl)
            xmu2 = xmu * xmu
            xi = 1.0 / x
            xi2 = 2.0 * xi
            w = xi2 / PI '// The Wronskian.

            isign = 1       '// Evaluate CF1 by modified Lentz’s method (§5.2). isign keeps
            '// track of sign changes 
            h = nu * xi    '// in the denominator.
            If (h < FPMIN) Then h = FPMIN
            b = xi2 * nu
            d = 0.0
            c = h

            For i = 1 To MAXIT
                b = b + xi2
                d = b - d
                If (Abs(d) < FPMIN) Then d = FPMIN
                c = b - 1.0 / c
                If (Abs(c) < FPMIN) Then c = FPMIN
                d = 1.0 / d
                del = c * d
                h = del * h
                If (d < 0.0) Then isign = -isign
                If (Abs(del - 1.0) < EPS) Then Exit For
            Next i
            If i = MAXIT Then Throw New Exception("x too large in bessjy; try asymptotic expansion’")


            rjl = isign * FPMIN     '// Initialize Jν and Jν for downward recurrence.
            rjpl = h * rjl
            rjl1 = rjl            '// Store values for later rescaling.
            rjp1 = rjpl
            fact = nu * xi
            For l = nl To 1 Step -1
                rjtemp = fact * rjl + rjpl
                fact = fact - xi
                rjpl = fact * rjtemp - rjl
                rjl = rjtemp
            Next l
            If (rjl = 0.0) Then rjl = EPS
            f = rjpl / rjl          '// Now have unnormalized Jμ and Jμ.

            If (x < XMIN) Then    '// Use series.
                x2 = 0.5 * x
                pimu = PI * xmu
                If (Abs(pimu) < EPS) Then
                    fact = 1.0
                Else
                    fact = pimu / Sin(pimu)
                End If
                d = -Log(x2)  '// Is this base e or base 10?  VB assumes base e.  FORTRAN reference says base e too.
                e = xmu * d
                If (Abs(e) < EPS) Then
                    fact2 = 1.0
                Else
                    fact2 = Sinh(e) / e
                End If
                Call Beschb(xmu, gam1, gam2, gampl, gammi)      '// Chebyshev evaluation of Γ1 and Γ2.
                ff = 2.0 / PI * fact * (gam1 * Cosh(e) + gam2 * fact2 * d)  '// f0.
                e = Exp(e)
                p = e / (gampl * PI)          '// p0.
                q = 1.0 / (e * PI * gammi)      '// q0.
                pimu2 = 0.5 * pimu
                If (Abs(pimu2) < EPS) Then
                    fact3 = 1.0
                Else
                    fact3 = Sin(pimu2) / pimu2
                End If
                r = PI * pimu2 * fact3 * fact3
                c = 1.0
                d = -x2 * x2
                sum = ff + r * q
                sum1 = p

                For i = 1 To MAXIT
                    ff = (CDbl(i) * ff + p + q) / (CDbl(i) * CDbl(i) - xmu2)
                    c = c * d / CDbl(i)
                    p = p / (CDbl(i) - xmu)
                    q = q / (CDbl(i) + xmu)
                    del = c * (ff + r * q)
                    sum = sum + del
                    del1 = c * p - CDbl(i) * del
                    sum1 = sum1 + del1
                    If (Abs(del) < (1.0 + Abs(sum)) * EPS) Then Exit For
                Next i
                If i = MAXIT Then Throw New Exception("bessy series failed to converge.")

                rymu = -sum
                ry1 = -sum1 * xi2
                rymup = xmu * xi * rymu - ry1
                rjmu = w / (rymup - f * rymu)       '// Equation (6.7.13).
            Else                            '// Evaluate CF2 by modified Lentz’s method (§5.2).

                a = 0.25 - xmu2
                p = -0.5 * xi
                q = 1.0
                br = 2.0 * x
                bi = 2.0
                fact = a * xi / (p * p + q * q)
                cr = br + q * fact
                ci = bi + p * fact
                den = br * br + bi * bi
                dr = br / den
                di = -bi / den
                dlr = cr * dr - ci * di
                dli = cr * di + ci * dr
                temp = p * dlr - q * dli
                q = p * dli + q * dlr
                p = temp
                For i = 2 To MAXIT
                    a = a + 2.0 * CDbl(i - 1)
                    bi = bi + 2.0
                    dr = a * dr + br
                    di = a * di + bi
                    If (Abs(dr) + Abs(di) < FPMIN) Then dr = FPMIN
                    fact = a / (cr * cr + ci * ci)
                    cr = br + cr * fact
                    ci = bi - ci * fact
                    If (Abs(cr) + Abs(ci) < FPMIN) Then cr = FPMIN
                    den = dr * dr + di * di
                    dr = dr / den
                    di = -di / den
                    dlr = cr * dr - ci * di
                    dli = cr * di + ci * dr
                    temp = p * dlr - q * dli
                    q = p * dli + q * dlr
                    p = temp
                    If (Abs(dlr - 1.0) + Abs(dli) < EPS) Then Exit For
                Next i
                If i = MAXIT Then Throw New Exception("cf2 failed in bessjy")
                gam = (p - f) / q     '// Equations (6.7.6) – (6.7.10).
                rjmu = Sqrt(w / ((p - f) * gam + q))
                '// rjmu=sign(rjmu,rjl)  -- bleah, funky FORTRAN statement, equivalent to the following:
                If rjl >= 0.0 Then
                    rjmu = Abs(rjmu)
                Else
                    rjmu = -Abs(rjmu)
                End If

                rymu = rjmu * gam
                rymup = rymu * (p + q / gam)
                ry1 = xmu * xi * rymu - rymup

            End If

            fact = rjmu / rjl
            rj = rjl1 * fact    '// Scale original Jν and Jν.
            rjp = rjp1 * fact
            For i = 1 To nl   '// Upward recurrence of Yν.
                rytemp = (xmu + i) * xi2 * ry1 - rymu
                rymu = ry1
                ry1 = rytemp
            Next i
            ry = rymu
            ryp = nu * xi * rymu - ry1

            '// Now construct the return array:
            Dim retval() As Double = {rj, ry, rjp, ryp}

            Return retval

        End Function


        Private Shared Sub Beschb(ByVal x As Double, ByRef gam1 As Double, ByRef gam2 As Double, ByRef gampl As Double, ByRef gammi As Double)

            '// C USES chebev
            '// Evaluates Γ1 and Γ2 by Chebyshev expansion for |x| ≤ 1/2. Also returns 1/Γ(1 + x) and
            '// 1/Γ(1 − x). If converting to double precision, set NUSE1 = 7, NUSE2 = 8.
            Dim xx As Double
            Static c1() As Double = {-1.1420226803711679, 0.0065165112670737, 0.0003087090173086, -0.0000034706269649, 0.0000000069437664, 0.0000000000367795, -0.0000000000001356}
            Static c2() As Double = {1.843740587300905, -0.0768528408447867, 0.0012719271366546, -0.0000049717367042, -0.0000000331261198, 0.0000000002423096, -0.0000000000001702, -0.00000000000000149}

            xx = 8.0 * x * x - 1.0    '// Multiply x by 2 to make range be −1 to 1, and then
            '// apply transformation for evaluating even Chebyshev
            '// series.
            gam1 = chebev(-1.0, 1.0, c1, xx)
            gam2 = chebev(-1.0, 1.0, c2, xx)
            gampl = gam2 - x * gam1
            gammi = gam2 + x * gam1

        End Sub


        Private Shared Function chebev(a As Double, b As Double, c As Double(), x As Double) As Double

            Dim m As Integer = c.Length - 1

            '// Chebyshev evaluation: All arguments are input. c(1:m) is an array of Chebyshev coefficients,
            '// the first m elements of c output from chebft (which must have been called with
            '// the same a and b). The Chebyshev polynomial	
            '// mk
            '// =1 ckTk−1(y) − c1/2 is evaluated at a
            '// point y = [x − (b + a)/2]/[(b − a)/2], and the result is returned as the function value.
            Dim j As Integer
            Dim d, dd, sv, y, y2 As Double

            If ((x - a) * (x - b) > 0.0) Then Throw New Exception("x not in range in chebev")

            d = 0.0
            dd = 0.0
            y = (2.0 * x - a - b) / (b - a)     '// Change of variable.
            y2 = 2.0 * y
            For j = m To 1 Step -1           '// Clenshaw’s recurrence.
                sv = d
                d = y2 * d - dd + c(j)
                dd = sv
            Next j

            Return y * d - dd + 0.5 * c(0)  '// Last step is different.

        End Function


    End Class
End Namespace