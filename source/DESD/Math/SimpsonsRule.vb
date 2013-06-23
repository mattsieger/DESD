
Namespace Math.Integration

    Public Class SimpsonsRule

        Public Shared Function NonuniformArray(x As Double(), y As Double()) As Double()

            Dim retval(x.Length - 1) As Double

            retval(0) = 0.0
            '// First point is found by trapezoidal rule
            retval(1) = (x(1) - x(0)) * (y(1) + y(0)) / 2.0

            '// The rest of the points are found using the recursion relation:
            Dim a As Double
            Dim b As Double
            Dim a2 As Double
            Dim b2 As Double
            Dim a3 As Double
            Dim b3 As Double
            Dim temp1 As Double
            Dim temp2 As Double
            Dim temp3 As Double
            For i As Integer = 2 To x.Length - 1
                a = x(i - 1) - x(i - 2)
                b = x(i) - x(i - 1)
                a2 = a * a
                b2 = b * b
                a3 = a2 * a
                b3 = b2 * b

                temp1 = (retval(i - 1) * (a - b) + retval(i - 2) * b) / a
                temp2 = temp1 + y(i - 2) * b * (2.0 * a3 + 5.0 * a2 * b + 2.0 * a * b2 - b3) / (6.0 * a * (a + b) ^ 2)
                temp3 = temp2 + y(i - 1) * (a2 + 6.0 * a * b + b2) / (6.0 * a)
                temp1 = temp3 + y(i) * (2.0 * b3 + 5.0 * a * b2 + 2.0 * a2 * b - a3) / (6.0 * (a + b) ^ 2)

                retval(i) = temp1

            Next

            Return retval

        End Function

        ''' <summary>
        ''' Performs Simpson's Rule integration on the uniformly spaced mesh.
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        ''' <returns></returns>
        Public Shared Function Integrate(x() As Double, y() As Double) As Double
            Dim deltaX As Double = x(1) - x(0)
            Dim imax As Integer = x.Length - 1
            If imax Mod 2 <> 0 Then
                imax = imax - 1
            End If

            Dim sum As Double = y(0) + y(imax)
            For j As Integer = 1 To imax \ 2 - 1
                sum += 2.0 * y(2 * j)
            Next
            For j As Integer = 1 To imax \ 2
                sum += 4.0 * y(2 * j - 1)
            Next

            If imax <> x.Length - 1 Then
                '// add in the last point via trapezoidal rule:
                sum += 0.5 * (y(imax) + y(imax + 1)) * deltaX
            End If
            Return deltaX * sum / 3.0

        End Function

    End Class

End Namespace
