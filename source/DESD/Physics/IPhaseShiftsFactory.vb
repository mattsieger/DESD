'
' Created by SharpDevelop.
' User: Matt
' Date: 2/13/2012
' Time: 9:40 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Interface IPhaseShiftsFactory
	
    ReadOnly Property V0() As Double
	
    ReadOnly Property Voptical(energy As Double) As DESD.Math.Complex
	
    ReadOnly Property PhaseShifts(speciesID As Integer) As IPhaseShiftProvider2
	
	''' <summary>
	''' Returns the (complex) magnitude of the k vector inside of the solid, accounting for the energy reference (the inner potential
	''' and for optical potential effects).   
	''' </summary>
	''' <param name="energy">Energy of incident electron, in electron volts relative to the vacuum level.</param>
	''' <returns>k in inverse Angstroms</returns>
    Function k(energy As Double) As DESD.Math.Complex
	
End Interface
