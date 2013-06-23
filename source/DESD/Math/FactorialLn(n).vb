Imports System.Math

Namespace Math



    Partial Public Class Functions


        ''' <summary>
        ''' Returns the natural log of n factorial (n!).
        ''' </summary>
        ''' <param name="n">Integer</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 
        ''' Author:  Matt Sieger
        ''' Revision History:
        ''' 10/5/2006 - imported code from the old CancunXL Math project.
        ''' 
        ''' Algorithm ------------------------
        ''' Mathematical identity Ln(n!) = Ln(Gamma(n + 1)) 
        ''' 
        ''' Implementation -------------------
        ''' This routine is a straightforward implementation of the mathematical identity.
        ''' 
        ''' Testing and Accuracy -------------
        ''' 
        ''' References -----------------------
        ''' http://en.wikipedia.org/wiki/Factorial
        ''' 
        ''' 
        ''' </remarks>
        Public Shared Function FactorialLn(ByVal n As Integer) As Double

            If n < 0 Then Throw New ArgumentOutOfRangeException("n", "Parameter must be greater than zero.")

            If n <= 1 Then
                '// 1! = 0! = 1.0.  Natural log of 1.0 = 0.0.
                Return 0.0R
            Else
                Return GammaLn(n + 1.0R)
            End If

        End Function
    End Class
End Namespace