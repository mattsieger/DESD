

	
	''' <summary>
	''' This class encapsulates methods used to integrate a radial mesh (defined as a monotonically increasing
	''' mesh as mesh index increases) both in the outward (index = 0 to an arbitrary high index limit) and in
	''' the inward (index = maximum to an arbitrary low index limit) directions.
	'''
	''' The class is designed to support the Herman Skillman method of solving the radial Schrodinger equation,
	''' but it can be successfully used for any generic problem requiring solution of the differential equation
	''' y'' = v(r) * y(r) provided the mesh implements IRadialMesh and is monotonically increasing with no
	''' duplicate values.
	''' </summary>
	Public Class NumerovRadialIntegrator
		
		''' <summary>
		''' Returns the solution to the differential equation y'' = v(r) * y(r) tabulated on mesh.
		''' Integration proceeds from index = 0 to index = iend (outward).
		''' This method is thread-safe.
		''' </summary>
		''' <param name="mesh">An object implementing IRadialMesh that provides the mesh of r values.</param>
		''' <param name="v">An array containing the value of the potential function v(r) tabluated as the mesh points.</param>
		''' <param name="iend">The index of the mesh point at which integration should be stopped.</param>
		''' <param name="y0">The initial value of the solution y(0).</param>
		''' <param name="yprime0">The derivative of the solution y'(0)</param>
		''' <returns>A Double array containing the solution y(r) tabulated at the mesh points [0,iend].</returns>
        Public Shared Function IntegrateOutward(ByVal mesh As IRadialMesh, ByVal v As Double(), ByVal iend As Integer, ByVal y0 As Double, ByVal yprime0 As Double) As Double()

            '// First, check inputs for validity:
            If mesh Is Nothing Then Throw New ArgumentNullException()
            If mesh.Count < 3 Then Throw New ArgumentException("Mesh must have 3 or more points to be valid for integration.")
            If v.Length < mesh.Count Then Throw New ArgumentException("Array v() must contain at least as many elements as the Mesh.")
            If (iend < 0) Or (iend > mesh.Count - 1) Then Throw New ArgumentException("iEnd must be in the valid range of the Mesh indices.")
            If iend < 3 Then Throw New ArgumentException("iEnd must be greater than or equal to 3.")
            If Double.IsNaN(y0) Then Throw New ArgumentException("Cannot be NaN.", "y0")
            If Double.IsInfinity(y0) Then Throw New ArgumentException("Cannot be Infinity.", "y0")
            If Double.IsNaN(yprime0) Then Throw New ArgumentException("yPrime0 cannot be NaN.")
            If Double.IsInfinity(yprime0) Then Throw New ArgumentException("yPrime0 cannot be Infinity.")


            '// Set up some variables for the calculation

            '// Create an array for the solution y()
            Dim y(mesh.Count - 1) As Double

            '// Create variables for a and b
            '// a = Mesh(i+1) - Mesh(i)
            '// b = Mesh(i) - Mesh(i-1)
            Dim a As Double = mesh.DR(0, 1)
            Dim b As Double

            '// Record the first point of the solution, easy because it was passed in:
            y(0) = y0

            '// Compute the second point of the solution from the Numerov formula:
            '// Potential divide-by-zero exception if a^2 * v(1) = 6.0
            '// In this event, throw an exception
            If (a * a * v(1) = 6.0) Then Throw New ArgumentException("Divide by zero in computation of y1.  Use a different mesh spacing or potential value.")
            
            If y0 = 0.0 Then
            	y(1) = 6.0 * a * yprime0 / (6.0 - a * a * v(1))
            Else
            	y(1) = ((6.0 + 2.0 * a * a * v(0)) * y0 + 6.0 * a * yprime0) / (6.0 - a * a * v(1))
            End If
            
            
            '// Now fill out the rest of the solution:
            Dim tol As Double = 0.0000000001
            'Dim tol As Double = 0.000000000001
            Dim BigNumber As Double = 1.0E+200
            
            For i As Integer = 1 To (iend - 1)
                a = mesh.DR(i, i + 1)
                b = mesh.DR(i - 1, i)
                If (System.Math.Abs(a - b) < tol) Then
                    '// Mesh is locally uniform
                    '// Return the Numerov result for the uniform mesh:
                    y(i + 1) = StepForwardUniform(a, y(i), y(i - 1), v(i - 1), v(i), v(i + 1))
                Else
                    '// Mesh is locally nonuniform
                    '// Return the Generalized Numerov result for the nonuniform mesh:
                    y(i + 1) = StepForwardNonuniform(a, b, y(i), y(i - 1), v(i - 1), v(i), v(i + 1))
                End If
                
                '// Check for runaway
                If system.Math.Abs(y(i+1)) > BigNumber then throw new ArgumentOutOfRangeException()
            Next

            Return y

        End Function
		
		''' <summary>
		''' Returns a single step forward (yplus) in the outward integration using Numerov's method for a uniform mesh.
		''' The arguments are defined on the 3 point mesh range ( - , 0 , + ) as (y-, y0, (y+ = returned value))
		''' with the potential points (v-, v0, v+).
		'''
		''' This method is thread-safe.
		''' </summary>
		''' <param name="h">The (uniform) interpoint spacing.</param>
		''' <param name="y0">The value of y(r) tabulated at the midpoint of the 3 point step range.</param>
		''' <param name="yminus">The value of y(r) tabulated at the lowest point of the 3 point step range.</param>
		''' <param name="vminus">The value of v(r) tabulated at the lowest point of the 3 point step range.</param>
		''' <param name="v0">The value of v(r) tabulated at the midpoint of the 3 point step range.</param>
		''' <param name="vplus">The value of v(r) tabulated at the highest point of the 3 point step range.</param>
		''' <returns></returns>
		Private Shared Function StepForwardUniform(h As Double, y0 As Double, yminus As Double, vminus As Double, v0 As Double, vplus As Double) As Double
			If (h * h * vplus = 12.0) Then Throw New DivideByZeroException("Divide by zero in computation of y+.  Use a different mesh spacing or potential value.")
			Return (y0 * (24.0 + 10.0 * h * h * v0) - yminus * (12.0 - h * h * vminus) ) / (12.0 - h * h * vplus)
		End Function
		
		''' <summary>
		''' Returns a single step forward (yplus) in the outward integration using Numerov's method for a non-uniform mesh.
		''' The arguments are defined on the 3 point mesh range ( - , 0 , + ) as (y-, y0, (y+ = returned value))
		''' with the potential points (v-, v0, v+).
		'''
		''' This method is thread-safe.
		''' </summary>
		''' <param name="a">The absolute value of the distance between the midpoint and the last point in the 3 point mesh range, Mesh(i+1) - Mesh(i).</param>
		''' <param name="b">The absolute value of the distance between the midpoint and the first point in the 3 point mesh range, Mesh(i) - Mesh(i-1).</param>
		''' <param name="y0">The value of y(r) tabulated at the midpoint of the 3 point step range.</param>
		''' <param name="yminus">The value of y(r) tabulated at the lowest point of the 3 point step range.</param>
		''' <param name="vminus">The value of v(r) tabulated at the lowest point of the 3 point step range.</param>
		''' <param name="v0">The value of v(r) tabulated at the midpoint of the 3 point step range.</param>
		''' <param name="vplus">The value of v(r) tabulated at the highest point of the 3 point step range.</param>
		''' <returns></returns>
        Private Shared Function StepForwardNonuniform(ByVal a As Double, ByVal b As Double, ByVal y0 As Double, ByVal yminus As Double, ByVal vminus As Double, ByVal v0 As Double, ByVal vplus As Double) As Double
 			
 			If (b = 0.0) Then Throw New DivideByZeroException("Divide by zero in computation of y+, invalid value (b = 0).")
           
            Dim a2 As Double = a * a
            Dim b2 As Double = b * b
            Dim ab As Double = a * b

			Dim temp0 As Double = (a2 + ab - b2) * vplus
			
			If (temp0 = 12.0) Then Throw New DivideByZeroException("Divide by zero in computation of y+.  Use a different mesh spacing or potential value.")

            Dim temp1 As Double = y0 * (a + b) * (12.0 + v0 * (a2 + 3.0 * ab + b2))
            Dim temp2 As Double
            If yminus = 0.0 Then
                temp2 = 0.0
            Else
            	temp2 = yminus * a * (12.0 + vminus * (a2 - ab - b2))
            End If
            Return (temp1 - temp2) / (b * (12.0 - temp0))

        End Function


		''' <summary>
		''' Returns the solution to the differential equation y'' = v(r) * y(r) tabulated on mesh.
		''' Integration proceeds from index = imax to index = iend (inward).
		''' This method is thread-safe.
		''' </summary>
		''' <param name="mesh">An object implementing IRadialMesh that provides the mesh of r values.</param>
		''' <param name="v">An array containing the value of the potential function v(r) tabluated as the mesh points.</param>
		''' <param name="iend">The index of the mesh point at which integration should be stopped.</param>
		''' <param name="ystart">The value of the solution y(imax).</param>
		''' <param name="ystart2">The value of the solution y(imax-1)</param>
		''' <returns>A Double array containing the solution y(r) tabulated at the mesh points [0,iend].</returns>
        Public Shared Function IntegrateInward(ByVal mesh As IRadialMesh, ByVal v As Double(), ByVal iend As Integer, ByVal ystart As Double, ByVal ystart2 As Double) As Double()

            '// First, check inputs for validity:
            If mesh Is Nothing Then Throw New ArgumentNullException()
            If mesh.Count < 3 Then Throw New ArgumentException("Mesh must have 3 or more points to be valid for integration.")
            If v.Length < mesh.Count Then Throw New ArgumentException("Array v() must contain at least as many elements as the Mesh.")
            If (iend < 0) Or (iend > mesh.Count - 1) Then Throw New ArgumentException("iEnd must be in the valid range of the Mesh indices.")
            If System.Math.Abs(iend) > mesh.Count - 3 Then Throw New ArgumentException("iEnd must be at least 3 indices away from iMax.")
            If Double.IsNaN(ystart) Then Throw New ArgumentException("yStart cannot be NaN.")
            If Double.IsInfinity(ystart) Then Throw New ArgumentException("yStart cannot be Infinity.")
            If Double.IsNaN(ystart2) Then Throw New ArgumentException("ystart2 cannot be NaN.")
            If Double.IsInfinity(ystart2) Then Throw New ArgumentException("ystart2 cannot be Infinity.")


            '// Set up some variables for the calculation

            '// Create an array for the solution y()
            Dim IMax As Integer = mesh.Count-1
            
            Dim y(IMax) As Double

            '// Create variables for a and b
            '// a = Mesh(i+1) - Mesh(i)
            '// b = Mesh(i) - Mesh(i-1)
            Dim a As Double
            Dim b As Double

            '// Record the first 2 points of the solution, easy because they were passed in:
            y(imax) = ystart
            y(imax-1) = ystart2

            '// Now fill out the rest of the solution:
            Dim tol As Double = 0.0000000001
            Dim temp as double
            For i As Integer = imax-1 To iend+1 step -1
                a = mesh.DR(i, i + 1)
                b = mesh.DR(i - 1, i)
                If (System.Math.Abs(a - b) < tol) Then
	                '// Mesh is locally uniform
	                '// Return the Numerov result for the uniform mesh:
	                y(i-1) = StepBackwardUniform(a, y(i), y(i + 1), v(i - 1), v(i), v(i + 1))
                Else
                	'// Mesh is locally non-uniform
	                y(i-1) = StepBackwardNonuniform(a, b, y(i), y(i + 1), v(i - 1), v(i), v(i + 1))
            	End If
				'// Check and see if we need to renormalize:
				If y(i-1) > 1.0E+100 Then
					'// We're in danger of diverging - renormalize to this value = 1.0
					temp = y(i-1)
					For j As Integer = i-1 To imax
						y(j) = y(j) / temp
					Next
				End If
            Next

            Return y

        End Function
	
	
	    ''' <summary>
		''' Returns a single step backward (yminus) in the inward integration using Numerov's method for a uniform mesh.
		''' The arguments are defined on the 3 point mesh range ( - , 0 , + ) as ((y- = returned value), y0, y+)
		''' with the potential points (v-, v0, v+).
		'''
		''' This method is thread-safe.
	    ''' </summary>
		''' <param name="h">The (uniform) interpoint spacing.</param>
		''' <param name="y0">The value of y(r) tabulated at the midpoint of the 3 point step range.</param>
	    ''' <param name="yplus">The value of y(r) tabulated at the highest point of the 3 point step range.</param>
		''' <param name="vminus">The value of v(r) tabulated at the lowest point of the 3 point step range.</param>
		''' <param name="v0">The value of v(r) tabulated at the midpoint of the 3 point step range.</param>
		''' <param name="vplus">The value of v(r) tabulated at the highest point of the 3 point step range.</param>
	    ''' <returns></returns>
		Private Shared Function StepBackwardUniform(h As Double, y0 As Double, yplus As Double, vminus As Double, v0 As Double, vplus As Double) As Double
        If (h * h * vminus = 12.0) Then

            Throw New DivideByZeroException("Divide by zero in computation of y-.  Use a different mesh spacing or potential value.")
        End If
        Return (y0 * (24.0 + 10.0 * h * h * v0) - yplus * (12.0 - h * h * vplus)) / (12.0 - h * h * vminus)
		End Function
		
		
    ''' <summary>
		''' Returns a single step backward (yminus) in the inward integration using Numerov's method for a non-uniform mesh.
		''' The arguments are defined on the 3 point mesh range ( - , 0 , + ) as ((y- = returned value), y0, y+)
		''' with the potential points (v-, v0, v+).
		'''
		''' This method is thread-safe.
		''' </summary>
		''' <param name="a">The absolute value of the distance between the midpoint and the last point in the 3 point mesh range, Mesh(i+1) - Mesh(i).</param>
		''' <param name="b">The absolute value of the distance between the midpoint and the first point in the 3 point mesh range, Mesh(i) - Mesh(i-1).</param>
		''' <param name="y0">The value of y(r) tabulated at the midpoint of the 3 point step range.</param>
		''' <param name="yplus">The value of y(r) tabulated at the highest point of the 3 point step range.</param>
		''' <param name="vminus">The value of v(r) tabulated at the lowest point of the 3 point step range.</param>
		''' <param name="v0">The value of v(r) tabulated at the midpoint of the 3 point step range.</param>
		''' <param name="vplus">The value of v(r) tabulated at the highest point of the 3 point step range.</param>
    ''' <returns></returns>
    Private Shared Function StepBackwardNonuniform(ByVal a As Double, ByVal b As Double, ByVal y0 As Double, ByVal yplus As Double, ByVal vminus As Double, ByVal v0 As Double, ByVal vplus As Double) As Double

        If (a = 0.0) Then Throw New DivideByZeroException("Divide by zero in computation of y-, invalid value (a = 0).")

        Dim a2 As Double = a * a
        Dim b2 As Double = b * b
        Dim ab As Double = a * b

        Dim temp0 As Double = vminus * (a2 - ab - b2)

        If (temp0 = -12.0) Then
            'Dim blah As Integer = 1
            Throw New DivideByZeroException("Divide by zero in computation of y-.  Use a different mesh spacing or potential value.")
        End If

        Dim temp1 As Double = y0 * (a + b) * (12.0 + v0 * (a2 + 3.0 * ab + b2))
        Dim temp2 As Double = yplus * b * (12.0 - vplus * (a2 + ab - b2))
        Return (temp1 - temp2) / (a * (12.0 + temp0))

    End Function

End Class

