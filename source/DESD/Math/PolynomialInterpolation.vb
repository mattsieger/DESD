'
' Created by SharpDevelop.
' User: Matt
' Date: 2/5/2012
' Time: 4:43 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Namespace Math


    Partial Public Class Functions


        ''' <summary>
        ''' Given the arrays xa() and ya() of length n, defining the set of n data points, 
        ''' and the given value x within the range of xa(0) and xa(n-1), returns a two-
        ''' element array y() such that y(0) = the interpolated value of y at x, using
        ''' a polynomial of order n-1, and y(1) = dy, the estimated error in y.
        ''' </summary>
        ''' <param name="xa"></param>
        ''' <param name="ya"></param>
        ''' <param name="x"></param>
        ''' <returns>a double array y() of length 2 such that y(0) = the interpolated value of y at x, using
        ''' a polynomial of order n-1, and y(1) = dy, the estimated error in y.</returns>
        Public Shared Function PolynomialInterpolation(ByRef xa() As Double, ByRef ya() As Double, ByVal x As Double) As Double()

            Dim n As Integer = xa.Length
            Dim NMAX As Integer = 10 ' Largest anticipated value of n.
            Dim dy As Double
            Dim y As Double

            '// Error condition:  xa.length <> ya.length
            '// error condition:  x not in range of xa
            '// Error condition:  xa.length > NMAX

            Dim ns As Integer = 1

            Dim den As Double
            Dim dif As Double = System.Math.Abs(x - xa(0))
            Dim dift As Double
            Dim ho As Double
            Dim hp As Double
            Dim w As Double
            Dim c(NMAX) As Double
            Dim d(NMAX) As Double

            ' Here we find the index ns of the closest table entry
            For i As Integer = 1 To n
                dift = System.Math.Abs(x - xa(i - 1))
                If dift < dif Then
                    ns = i
                    dif = dift
                End If
                c(i) = ya(i - 1) ' and initialize the tableau of c's and d's
                d(i) = ya(i - 1)
            Next

            y = ya(ns - 1) 'This is the initial approximation to y.

            ns = ns - 1

            For m As Integer = 1 To n - 1 ' For each column of the tableau,

                For i As Integer = 1 To n - m ' we loop over the current c's and d's and update them.

                    ho = xa(i - 1) - x
                    hp = xa(i + m - 1) - x
                    w = c(i + 1) - d(i)
                    den = ho - hp

                    ' This error can occur only if two input xa's are (to within roundoff) identical.
                    If den = 0.0 Then Throw New System.Exception("Two input xa's are identical")

                    den = w / den
                    d(i) = hp * den 'Here the c's and d's are updated:
                    c(i) = ho * den

                Next

                If (2 * ns < n - m) Then
                    'After each column in the tableau is completed, we decide
                    'which correction, c or d, we want to add to our accu-
                    'mulating value of y, i.e., which path to take through
                    'the tableau|forking up or down. We do this in such a
                    'way as to take the most \straight line" route through the
                    'tableau to its apex, updating ns accordingly to keep track
                    'of where we are. This route keeps the partial approxima-
                    'tions centered (insofar as possible) on the target x. The
                    'last dy added is thus the error indication.
                    dy = c(ns + 1)
                Else
                    dy = d(ns)
                    ns = ns - 1
                End If

                y = y + dy

            Next

            Dim retval As Double() = {y, dy}

            Return retval


        End Function

    End Class
End Namespace