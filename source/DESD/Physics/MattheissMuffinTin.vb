
Imports DESD.Math


Public Class MattheissMuffinTin
	Implements IMuffinTin
	
	Private _Atom As Atom
	Private _Potential As Double()
	Private _Radius As Double
	Private _Imt As Integer
	Private _InnerPotential As Double
	Private _Configuration as ElectronicConfiguration
	
#Region "Constructors"

	''' <summary>
	''' Note that rmax is in Angstroms.
	''' </summary>
	''' <param name="atomcluster"></param>
	''' <param name="atomid"></param>
	''' <param name="rmax"></param>
	Sub New(atomcluster As Cluster, atomid As Integer, rmax As double)

		Solve(atomcluster, atomid, rmax)
		
	End Sub
		
#End Region
	
	
#Region "IMuffinTin Implementation"
	
	ReadOnly Property AtomicNumber() As Integer Implements IMuffinTin.AtomicNumber
		Get
			Return _Atom.AtomicNumber
		End Get
	End Property
	
	ReadOnly Property Element() As Element Implements IMuffinTin.Element
		Get
			Return _Atom.Element
		End Get
	End Property
	
	ReadOnly Property Mesh As IRadialMesh Implements IMuffinTin.Mesh
		Get
			Return _Atom.Mesh
		End Get
	End Property
	
	ReadOnly Property Potential As Double() Implements IMuffinTin.Potential
		Get
			Dim retval(_Potential.Length-1) as Double
			system.Array.Copy(_Potential,retval,_Potential.Length)
			Return retval
		End Get
	End Property
	
	ReadOnly Property Radius As Double Implements IMuffinTin.Radius
		Get
			Return _Radius
		End Get
	End Property
	
	ReadOnly Property InnerPotential As Double Implements IMuffinTin.InnerPotential
		Get
			Return _InnerPotential
		End Get
	End Property
	
	ReadOnly Property Imt As Integer Implements IMuffinTin.Imt
		Get
			Return _Imt
		End Get
	End Property
	
	ReadOnly Property Configuration As ElectronicConfiguration Implements IMuffinTin.Configuration
		Get
			return _Configuration
		End Get
	End Property

#End Region
	

#Region "Private Methods"
	
	''' <summary>
	''' Solve for the muffin-tin potential.
	''' </summary>
	Private Sub Solve(atomcluster as Cluster, atomid as Integer, rmax as double)
		
		'// Note that the NearestNeighbors collection should NOT contain the central atom ID:
		Dim NearestNeighbors As SortedList(Of Integer, Double) = atomcluster.GetNearestNeighbors(atomid, rmax)
		
		
		
		'// Check to make sure nearestneighbors has at least 1 element in it:
		
		'// Find the muffin-tin radius.  This will be 0.5 * the smallest radius in the nearestneighbors list.
		Dim minRadius as Double = double.MaxValue
		For Each key As Integer In NearestNeighbors.Keys
			if NearestNeighbors(key) < minRadius then minRadius = NearestNeighbors(key)
		Next
		
		'// Note that _Radius is in Bohr radii = 0.52917729859 * R [Angstroms],
		'// so we convert here:
		_Radius = 0.5 * minradius / 0.52917729859
		
		'// Get a reference to the central atom:
		'_Atom = atomcluster.GetAtom(atomid)
		
		'// Get the radial mesh values:
		Dim r As Double() = _Atom.Mesh.GetArray()

        '// Find the index of the first point in the radial mesh
        '// bigger than Rmt:
        _Imt = System.Array.BinarySearch(r, _Radius) Xor -1

		'// Get the contribution to the total charge density from the central atom:
		Dim Rho As Double() = _Atom.Rho
		Dim Vnoex As Double() = _Atom.PotentialSansExchange
		
		'// Now we're ready to loop over all nearest-neighbor atoms and add their contributions to the
		'// potential:
		Dim NeighborAtom As Atom
		Dim Rneighbor As Double
		Dim NeighborRho As Double()
		Dim NeighborR As Double()
		Dim NeighborVnoex As Double()
		Dim RtimesNeighborRho As Double()
		Dim RtimesNeighborVnoex As Double()
'		Dim starti As Integer
'		Dim endi as integer
		For Each neighborid As Integer In NearestNeighbors.Keys
			
			'// Get a reference to the neighbor (free) Atom
			'NeighborAtom = atomcluster.GetAtom(neighborid)
			
			'// Get the distance to this atom, converting from Angstroms to Bohr Radii:
			Rneighbor = NearestNeighbors(neighborid) / 0.52917720859
			
			NeighborR = NeighborAtom.Mesh.GetArray
			NeighborRho = NeighborAtom.Rho
			NeighborVnoex = Neighboratom.PotentialSansExchange
			
			ReDim RtimesNeighborRho(NeighborR.Length-1)
			Redim RtimesNeighborVnoex(NeighborR.Length-1)
			RtimesNeighborRho(0) = 0.0
			RtimesNeighborVnoex(0) = 0.0
			For j As Integer = 1 To NeighborR.Length-1
				RtimesNeighborRho(j) = NeighborR(j) * NeighborRho(j)
				RtimesNeighborVnoex(j) = NeighborR(j) * NeighborVnoex(j)
			Next
			
			'// Add the contribution of this atom to the total
			For i As Integer = 1 To _Imt
'				endi = System.Array.BinarySearch(neighborr, system.Math.Abs(rneighbor + r(i))) Xor -1
'				starti = System.Array.BinarySearch(neighborr, system.Math.Abs(rneighbor - r(i))) Xor -1
'				Rho(i) += (0.5/(RNeighbor*r(i)))*Sieger.Math.Integration.TrapezoidalRuleIntegrator.Integrate(neighborr,rtimesneighborrho,starti,endi)
'				Vnoex(i) += (0.5/(RNeighbor*r(i)))*Sieger.Math.Integration.TrapezoidalRuleIntegrator.Integrate(neighborr,rtimesneighborVnoex,starti,endi)
                Rho(i) += (0.5 / (Rneighbor * r(i))) * Integration.TrapezoidalRuleIntegrator.Integrate(NeighborR, RtimesNeighborRho, Rneighbor - r(i), Rneighbor + r(i))
                Vnoex(i) += (0.5 / (Rneighbor * r(i))) * Integration.TrapezoidalRuleIntegrator.Integrate(NeighborR, RtimesNeighborVnoex, Rneighbor - r(i), Rneighbor + r(i))
			Next

		Next
		
		'// Now add exchange to the total potential - note the usage of statistical exchange:
		Dim ThreeOver8Pi As Double = 3.0 / (8.0 * System.Math.PI)
        Dim OneThird As Double = 1.0 / 3.0
		Dim V(r.Length-1) As Double
		Dim alpha as Double = HermanSkillmanPotential.StatisticalAlpha(_Atom.AtomicNumber)
		V(0) = double.NegativeInfinity
		For i As Integer = 1 To _Imt
          	V(i) = Vnoex(i) - 6.0 * alpha * System.Math.Pow(System.Math.Abs(ThreeOver8Pi * System.Math.Abs(Rho(i))), OneThird)
		Next

		'// Now compute the inner potential and renormalize the potential:
		_InnerPotential = V(_Imt)
		For i As Integer = 1 To r.Length-1
			If i <= _Imt Then
				V(i) -= _InnerPotential
			Else
				V(i) = 0.0	
			End If
		Next
		
		_Potential = V
		
		
	End Sub
	
	
#End Region
	
	
End Class
