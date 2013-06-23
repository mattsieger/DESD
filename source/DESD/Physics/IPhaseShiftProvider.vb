Imports DESD.Math

'
' Created by SharpDevelop.
' User: Matt
' Date: 8/22/2011
' Time: 8:47 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Interface IPhaseShiftProvider
	
	Function GetPhaseShift(E As Double, L As Integer, Voptical As Double, Temperature As Double, debyeTemperature As Double) As Complex	
	
	Function GetPhaseShifts(E As Double, Voptical As Double, Temperature As Double, debyeTemperature As Double) As Complex()
		
	Function GetTMatrix(E As Double, Voptical As Double, Temperature As Double, debyeTemperature As Double) As Complex()

End Interface
