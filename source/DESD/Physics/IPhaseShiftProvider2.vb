Imports DESD.Math


'
' Created by SharpDevelop.
' User: Matt
' Date: 2/6/2012
' Time: 8:48 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Interface IPhaseShiftProvider2
	
	''' <summary>
	''' Returns the complex phase shift for the given energy E and angular momentum L.
	''' </summary>
	''' <param name="E">Electron energy, in Rydbergs</param>
	''' <param name="L"></param>
	''' <returns></returns>
	Function GetPhaseShift(E As Double, L As Integer) As Complex
	
	''' <summary>
	''' Returns the complex optical potential for the given energy E.
	''' </summary>
	''' <param name="E">Electron energy, in Rydbergs</param>
	''' <returns></returns>
	Function GetVoptical(E As Double) As Complex
	
	''' <summary>
	''' Returns the complex T matrix for energy E.
	''' </summary>
	''' <param name="E">Electron energy, in Rydbergs</param>
	''' <returns></returns>
	Function GetTMatrix(E As Double) As Complex()

End Interface
