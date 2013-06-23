
Imports DESD.Math

Public Class PhaseShiftLookup
	
	Private Const DELTAE As Double = 0.1
	Private mEmin as Double = 1.0
	Private mEmax As Double = 10.0
	Private mLmax As Integer = 10
	Private mEmesh as Double()
	Private mRealPhases() As PhaseShift
    Private mRealSplines() As CubicSpline
	
	Sub New(muffintin As IMuffinTin, Lmax As Integer, Emin as Double, Emax As Double)
		mEmax = Emax
		mLMax = Lmax
		ReDim mEmesh(Cint(Emax / DELTAE) + 1)
		For i As Integer = 0 To mEmesh.Length-1
			mEmesh(i) = Emin + CDbl(i) * DELTAE
		Next
		mRealPhases = CreatePhases(muffintin, Lmax, mEmesh)
		mRealSplines = CreateSplines(mRealPhases, mEmesh)
		

		
	End Sub
	
	Private Function CreatePhases(mt As IMuffinTin, Lmax As Integer, Emesh As Double()) As PhaseShift()
		Dim retval(Emesh.Length-1) as PhaseShift
		'// Solve the phase shifts for each energy value in the table:
		For i As Integer = 0 To Emesh.Length-1
			retval(i) = new PhaseShift(Emesh(i),Lmax,mt)
		Next
		Return Retval
	End Function
	
	'// One spline for each L value:
    Private Function CreateSplines(phases As PhaseShift(), emesh As Double()) As CubicSpline()
        Dim retval(phases(0).LMax) As CubicSpline
        Dim ps(emesh.Length - 1) As Double
        Dim cs As CubicSpline
        Dim temp As Double
        Dim lasttemp As Double
        Dim adder As Double = 0.0
        For L As Integer = 0 To phases(0).LMax
            '// Copy over the phase shift values, cleaning up (adding factors of pi as needed) as we go
            adder = 0.0
            For i As Integer = 0 To emesh.Length - 1
                temp = phases(i).RealValue(L)
                If i > 0 Then
                    If (System.Math.Sign(temp) = -System.Math.Sign(lasttemp)) Then
                        '// We've changed sign.  Either we are near zero and crossing, or we're wrapping around.
                        '// If the difference in values is big, we must be wrapping around:
                        If System.Math.Abs(lasttemp) > System.Math.PI / 3.0 Then
                            '// last point was large, likely we are wrapping around:
                            '// Determine the direction - rising or falling:
                            If lasttemp < 0 Then
                                '// falling - we should add -pi to future values
                                adder += -System.Math.PI
                            Else
                                '// rising, we should add +pi to future values
                                adder += System.Math.PI
                            End If
                        End If
                    End If
                End If
                ps(i) = temp + adder
                '// Remember last value
                lasttemp = temp
            Next
            '// Create the cubic spline:
            cs = New CubicSpline(emesh, ps, (ps(1) - ps(0)) / DELTAE, (ps(emesh.Length - 1) - ps(emesh.Length - 2)) / DELTAE)
            retval(L) = cs
        Next
        Return retval
    End Function
	
	Public Function GetRealPhaseShift(E As Double, L As Integer) As Double
		return mRealSplines(L).Y(E)
	End Function
	
	Public Function GetComplexPhaseShift(E As Double, L As Integer, Voptical As Double, T As Double) As Complex
		
	End Function
	
	Public Function GetTMatrix(E As Double) As Complex()
		
	End Function
	
	Public Function GetTMatrix(E As Double, Voptical As Double, T As Double) As Complex()
		
	End Function
	
End Class
