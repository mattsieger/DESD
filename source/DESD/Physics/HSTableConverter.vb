
	
	''' <summary>
	''' This class encodes the algorithm for expanding an array tabulated on a 110-point
	''' mesh (as is used in Herman and Skillman's book) onto the full 441 point mesh.
	''' </summary>
	Public Class HSTableConverter
		
	''    '    'C       110pt. Normalized Potential, from point 1 to point 437 with
    ''    '    'C       every 4th point only.  It will be extrapolated to a full 441pt.
    ''    '    'C       U(r) = -rV(r) / 2Z:  U(0) = 1.00, U(infin) = 1 / Z

        Public Shared Function ExpandU(ByVal input As Double(), ByVal Z As Element) As Double()

            '// Input should be of length 110 - if not, throw an exception.
            If input.Length <> 110 Then Throw New ArgumentException("Input array must have 110 elements.")

            '// Return value will be a 441 point array.
            Dim RU(444) As Double


            ''C   Take the input data and create the trial atomic potential.  If the
            ''C   trial mesh is chosen to go beyond 441 points or the Latter tail 
            ''C   correction is enacted, define the LIMIT point where the potential
            ''C   is first set to its theoretical value.
            ''C
            ''C d TWOZ, ZOZ = dummy variables used for computational savings
            ''C i LIMIT = mesh point where limiting value of the potential, whichever
            ''C           is chosen, is first used to fill in data in place of input
            ''C i ICUT = mesh point where modified Latter tail correction to the H-F-S
            ''C          potential takes over; i.e. r0 -> r0V(r0) = -2(Z-N + 1)/r0
            ''C ----------------------------------------------------------------------


            ''C     First, create rV(r) from the given input
            '// Input is tabulated as r * V(r) / (2 * Z)

            Dim TWOZ As Double = CDbl(Z + Z)

            '// Copy over the input values:
            For I = 0 To 436 Step 4
                RU(I) = -input(I \ 4) * TWOZ
            Next

            RU(440) = RU(436)
            RU(444) = RU(436)

            '// Fill in the blank points:
            Dim M As Integer = 9

            For I As Integer = 0 To 436 Step 4
                M = M - 1
                If (M >= 0) Then
                    'C           i mod 36 not = 0  :: so, still within DELTAX block
                    RU(I + 1) = (21.0 * RU(I) + 14.0 * RU(I + 4) - 3.0 * RU(I + 8)) / 32.0
                    RU(I + 2) = (3.0 * RU(I) + 6.0 * RU(I + 4) - RU(I + 8)) / 8.0
                    RU(I + 3) = (5.0 * RU(I) + 30.0 * RU(I + 4) - 3.0 * RU(I + 8)) / 32.0
                Else
                    'C(I Mod 36 = 0)  :: so, set up the 38th, 39th, 40th values
                    'C           This is the end of the values for this DELTAX.
                    RU(I + 1) = (22.0 * RU(I) + 11.0 * RU(I + 4) - RU(I + 8)) / 32.0
                    RU(I + 2) = (10.0 * RU(I) + 15.0 * RU(I + 4) - RU(I + 8)) / 24.0
                    RU(I + 3) = (6.0 * RU(I) + 27.0 * RU(I + 4) - RU(I + 8)) / 32.0
                    M = 9
                End If
            Next

            '// Copy the result into the return array, dropping the last 4 points,
            '// which were there only for purposes of computing the intermediate values
            '// for the last block.
            Dim Retval(440) As Double

            System.Array.Copy(RU, Retval, 441)

            Return Retval

        End Function


		Public Shared Function ExpandP(input As Double()) As Double()
			
			'// Input should be of length 110 - if not, throw an exception.
			If input.Length <> 110 Then Throw New ArgumentException("Input array must have 110 elements.")
			
			'// Return value will be a 441 point array.
			Dim P(444) As Double
			
		
	        ''C   Take the input data and create the trial atomic potential.  If the
	        ''C   trial mesh is chosen to go beyond 441 points or the Latter tail 
	        ''C   correction is enacted, define the LIMIT point where the potential
	        ''C   is first set to its theoretical value.
	        ''C
	        ''C d TWOZ, ZOZ = dummy variables used for computational savings
	        ''C i LIMIT = mesh point where limiting value of the potential, whichever
	        ''C           is chosen, is first used to fill in data in place of input
	        ''C i ICUT = mesh point where modified Latter tail correction to the H-F-S
	        ''C          potential takes over; i.e. r0 -> r0V(r0) = -2(Z-N + 1)/r0
	        ''C ----------------------------------------------------------------------
	        
	        
        
	        '// Copy over the input values:
	        For I = 0 To 436 Step 4
	            P(I) = input(I\4) 
	        Next
	        
	        P(440) = P(436)
	        P(444) = P(436)
	        
	        '// Fill in the blank points:
	        Dim M as Integer = 9
	            
	        For I as Integer = 0 To 436 Step 4
	            M = M - 1
	            If (M >= 0) Then
	                'C           i mod 36 not = 0  :: so, still within DELTAX block
	                P(I + 1) = (21.0 * P(I) + 14.0 * P(I + 4) - 3.0 * P(I + 8)) / 32.0
	                P(I + 2) = (3.0 * P(I) + 6.0 * P(I + 4) - P(I + 8)) / 8.0
	                P(I + 3) = (5.0 * P(I) + 30.0 * P(I + 4) - 3.0 * P(I + 8)) / 32.0
	            Else
	                'C(I Mod 36 = 0)  :: so, set up the 38th, 39th, 40th values
	                'C           This is the end of the values for this DELTAX.
	                P(I + 1) = (22.0 * P(I) + 11.0 * P(I + 4) - P(I + 8)) / 32.0
	                P(I + 2) = (10.0 * P(I) + 15.0 * P(I + 4) - P(I + 8)) / 24.0
	                P(I + 3) = (6.0 * P(I) + 27.0 * P(I + 4) - P(I + 8)) / 32.0
	                M = 9
	            End If
	        Next
	    
	    	'// Copy the result into the return array, dropping the last 4 points,
	    	'// which were there only for purposes of computing the intermediate values
	    	'// for the last block.
	    	Dim Retval(440) As Double
	    	
	    	System.Array.Copy(P,Retval,441)
	    	
	    	Return Retval
	    	
			
		End Function

		Public Shared Function CompressU(mesh As IRadialMesh, Z As Element, V As Double()) As Double()
			Dim Retval(mesh.Count\4) As Double
			Retval(0) = 1.0
			For i As Integer = 4 To mesh.Count-1 step 4
				Retval(i\4) = mesh.R(i) * V(i) / Cdbl(2 * Z)
			Next
			return Retval
		End Function
		
	End Class

