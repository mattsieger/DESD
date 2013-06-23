
Namespace Math.Integration

    Public Class TrapezoidalRuleIntegrator

        ''' <summary>
        ''' Integrates the function y(x) over the range [startIndex, endIndex] using the trapezoidal rule.
        ''' </summary>
        ''' <param name="x">An array of x values.  Must be monotonically increasing.</param>
        ''' <param name="y"></param>
        ''' <param name="startIndex"></param>
        ''' <param name="endIndex"></param>
        ''' <returns></returns>
        Public Overloads Shared Function Integrate(x As Double(), y As Double(), startIndex As Integer, endIndex As Integer) As Double

            '// Check input parameters for validity:
            If (x Is Nothing) Then Throw New ArgumentException("Invalid input array: x() = Nothing.")
            If (x.Length < 2) Then Throw New ArgumentException("Invalid input array: x() must be length 2 or greater.")
            If (y Is Nothing) Then Throw New ArgumentException("Invalid input array: y() = Nothing.")
            If (y.Length <> x.Length) Then Throw New ArgumentException("Invalid input array: y() must be same length as x().")
            If (startIndex < 0) Then Throw New ArgumentException("Must be greater than or equal to zero.", "startIndex")
            If (endIndex > x.Length - 1) Then Throw New ArgumentException("Must be less than or equal to x.length.", "endIndex")
            If (startIndex > endIndex) Then Throw New ArgumentException("startIndex must be less than or equal to endIndex.")

            If (startIndex = endIndex) Then Return 0.0

            Dim integral As Double = 0.0

            For i As Integer = startIndex To endIndex - 1
                integral += (x(i + 1) - x(i)) * (y(i) + y(i + 1)) / 2.0
            Next

            Return integral

        End Function

        Public Shared Function IntegrateArray(x As Double(), y As Double()) As Double()

            '// Check input parameters for validity:
            If (x Is Nothing) Then Throw New ArgumentException("Invalid input array: x() = Nothing.")
            If (x.Length < 2) Then Throw New ArgumentException("Invalid input array: x() must be length 2 or greater.")
            If (y Is Nothing) Then Throw New ArgumentException("Invalid input array: y() = Nothing.")
            If (y.Length <> x.Length) Then Throw New ArgumentException("Invalid input array: y() must be same length as x().")

            Dim retval(x.Length - 1) As Double
            retval(0) = 0.0

            For i As Integer = 1 To x.Length - 1
                retval(i) = retval(i - 1) + (x(i) - x(i - 1)) * (y(i) + y(i - 1)) / 2.0
            Next

            Return retval

        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        ''' <param name="startX"></param>
        ''' <param name="endX"></param>
        ''' <returns></returns>
        Public Overloads Shared Function Integrate(x As Double(), y As Double(), startX As Double, endX As Double) As Double

            '// Check input parameters for validity:
            If (x Is Nothing) Then Throw New ArgumentException("Invalid input array: x() = Nothing.")
            If (x.Length < 2) Then Throw New ArgumentException("Invalid input array: x() must be length 2 or greater.")
            If (y Is Nothing) Then Throw New ArgumentException("Invalid input array: y() = Nothing.")
            If (y.Length <> x.Length) Then Throw New ArgumentException("Invalid input array: y() must be same length as x().")

            If (startX = endX) Then Return 0.0

            '// Get the indices bracketing the startX and endX values:
            Dim starti = System.Array.BinarySearch(x, startX) Xor -1
            Dim endi = System.Array.BinarySearch(x, endX) Xor -1

            Dim integral As Double = 0.0

            '// Add the starting partial trapezoid:
            Dim x1 As Double = x(starti)
            Dim x2 As Double = x(starti + 1)
            Dim y1 As Double = y(starti)
            Dim y2 As Double = y(starti + 1)
            integral += (x2 - startX) * 0.5 * (y2 + (y1 * (x2 - startX) + y2 * (startX - x1)) / (x2 - x1))

            '// Add the ending partial trapezoid:
            If endi > starti Then
                x1 = x(endi)
                x2 = x(endi + 1)
                y1 = y(endi)
                y2 = y(endi + 1)
                integral += (endX - x1) * 0.5 * (y1 + (y1 * (x2 - endX) + y2 * (endX - x1)) / (x2 - x1))
            End If

            For i As Integer = starti + 1 To endi - 1
                integral += (x(i + 1) - x(i)) * (y(i) + y(i + 1)) / 2.0
            Next

            Return integral

        End Function


    End Class

End Namespace