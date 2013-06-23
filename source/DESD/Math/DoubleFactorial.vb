Namespace Math


    Partial Public Class Functions

        ''' <summary>
        ''' Returns n!!, the double factorial of n, for any odd value of n.
        ''' Attempting to pass an even value of n will throw an exception.
        ''' </summary>
        ''' <param name="n"></param>
        ''' <returns></returns>
        Public Shared Function DoubleFactorial(n As Integer) As Double

            If (n < 1) Then Throw New ArgumentException("n must be greater than zero.")
            If (n Mod 2) <> 1 Then Throw New ArgumentException("n must be an odd integer.")

            Dim retval As Double = 1.0

            For i As Integer = n To 1 Step -2
                retval *= CDbl(i)
            Next

            Return retval

        End Function

    End Class
End Namespace