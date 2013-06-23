Namespace Math


    Public Class CubicSpline
        Private XA() As Double
        Private YA() As Double
        Private Y2A() As Double
        Private iMax As Integer  '// maxium index of array.

        Public Sub New(ByVal x As Double(), ByVal y As Double(), ByVal dydx0 As Double, ByVal dydxN As Double)
            '// This constructor sets up the cubic spline for further calls
            '// Make copies of the arrays here
            ReDim XA(x.Length - 1)
            ReDim YA(y.Length - 1)
            System.Array.Copy(x, XA, x.Length)
            System.Array.Copy(y, YA, y.Length)
            '        XA = x
            '        YA = y
            iMax = XA.Length - 1
            Y2A = Spline(XA, YA, dydx0, dydxN)
        End Sub

        '// Create an overloaded constructor that takes no derivatives but computes them
        '// from the input arrays.

        '// Also create a shared function that does the whole spline for an array of X values
        '// (can create a self-instance in the shared method??)

        Public Function Y(ByVal X As Double) As Double

            '// Find the values of KLO and KHI that bracket the value X
            '// Using a binary search algorithm.
            Dim KLO As Integer = 1
            Dim KHI As Integer = iMax
            Dim K As Integer
            Do While (KHI - KLO > 1)
                K = (KHI + KLO) \ 2
                If (XA(K) > X) Then
                    KHI = K
                Else
                    KLO = K
                End If
            Loop

            '// KHI and KLO now bracket the input value of X
            Dim H As Double = XA(KHI) - XA(KLO)
            If (H = 0.0) Then Throw New Exception("Cannot have multiple same values of x")

            '// Cubic spline polynomial is now evaluated
            Dim A As Double = (XA(KHI) - X) / H
            Dim B As Double = (X - XA(KLO)) / H
            Y = A * YA(KLO) + B * YA(KHI) + ((A ^ 3 - A) * Y2A(KLO) + (B ^ 3 - B) * Y2A(KHI)) * (H ^ 2) / 6.0
            Return Y

        End Function


        Public Function Spline(ByVal x As Double(), ByVal y As Double(), ByVal YP1 As Double, ByVal YPN As Double) As Double()

            Dim U(iMax) As Double
            Dim Y2(iMax) As Double

            If YP1 > 1.0E+30 Then    '// The lower boundary condition is set either to be "natural"
                Y2(0) = 0.0
                U(0) = 0.0
            Else            '// or else have a specified first derivative
                Y2(0) = -0.5
                U(0) = (3.0 / (x(1) - x(0))) * ((y(1) - y(0)) / (x(1) - x(0)) - YP1)
            End If

            Dim SIG As Double
            Dim P As Double
            For i As Integer = 1 To iMax - 1
                '// This is the decomposition loop of the tridiagonal algorithm
                '// Y2 and U are used for temporary storage of the decomposed factors.
                SIG = (x(i) - x(i - 1)) / (x(i + 1) - x(i - 1))
                P = SIG * Y2(i - 1) + 2.0
                Y2(i) = (SIG - 1.0) / P
                U(i) = (6.0 * ((y(i + 1) - y(i)) / (x(i + 1) - x(i)) - (y(i) - y(i - 1)) _
                 / (x(i) - x(i - 1))) / (x(i + 1) - x(i - 1)) - SIG * U(i - 1)) / P
            Next

            Dim QN As Double
            Dim UN As Double
            If (YPN > 1.0E+30) Then  '// The upper boundary condition is set either to be "natural"
                QN = 0.0
                UN = 0.0
            Else            '// Or else to have a specified first derivative.
                QN = 0.5
                UN = (3.0 / (x(iMax) - x(iMax - 1))) * (YPN - (y(iMax) - y(iMax - 1)) / (x(iMax) - x(iMax - 1)))
            End If

            Y2(iMax) = (UN - QN * U(iMax - 1)) / (QN * Y2(iMax - 1) + 1.0)

            '// This is the backsubstitution loop of the tridiagonal algorithm
            For K As Integer = iMax - 1 To 0 Step -1
                Y2(K) = Y2(K) * Y2(K + 1) + U(K)
            Next

            Return Y2

        End Function

    End Class
End Namespace