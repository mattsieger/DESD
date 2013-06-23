


'<TestFixture> _
'Public Class MattheissMuffinTin_UnitTest
'	
'	<Test> _
'	Public Sub Constructor1
'		
'		Dim clust As New Cluster()
'		
'		'// Note that per cluster requirements, coordinates are in Angstrom units
'		clust.AddMember(element.Silicon,0.0,0.0,0.0)
'		clust.AddMember(element.Silicon,-1.92,1.11,-0.784)
'		clust.AddMember(element.Silicon,+1.92,1.11,-0.784)
'		clust.AddMember(element.Silicon,0.0,-2.22,-0.784)
'		clust.AddMember(element.Silicon,0.0,0.0,2.351)
'		
'		Dim mt As New MattheissMuffinTin(clust,0,10.0)
'		
'		console.WriteLine("Inner Potential = " &  mt.InnerPotential.ToString)
'		console.WriteLine("Muffin-tin Radius = " & mt.Radius.ToString)
'		
'		Dim pendrymt As New NeutralMuffinTin(element.Silicon,mt.radius)
'		
'		console.WriteLine("Pendry muffin-tin Inner Potential = " & pendrymt.InnerPotential.ToString)
'		
'	End Sub
'	
'		<Test> _
'	Public Sub Constructor2
'		
'		Dim clust As New Cluster()
'		
'		'// Note that per cluster requirements, coordinates are in Angstrom units
'		clust.AddMember(element.Silicon,0.0,0.0,0.0)
'		clust.AddMember(element.Silicon,-1.92,1.11,-0.784)
'		clust.AddMember(element.Silicon,+1.92,1.11,-0.784)
'		clust.AddMember(element.Silicon,0.0,-2.22,-0.784)
'		clust.AddMember(element.Silicon,0.0,0.0,2.351)
'		
'		clust.AddMember(element.Silicon,1.919794911,	-1.108394109,	3.135011962)
'		clust.AddMember(element.Silicon,-1.919794911,	-1.108394109,	3.135011962)
'		clust.AddMember(element.Silicon,0,	2.216788217,	3.135011962)
'		clust.AddMember(element.Silicon,3.839589822,	0,	0)
'		clust.AddMember(element.Silicon,-3.839589822,	0,	0)
'		clust.AddMember(element.Silicon,-1.919794911,	3.325182326,	0)
'		clust.AddMember(element.Silicon,-1.919794911,	-3.325182326,	0)
'		clust.AddMember(element.Silicon,1.919794911,	3.325182326,	0)
'		clust.AddMember(element.Silicon,1.919794911,	-3.325182326,	0)
'		clust.AddMember(element.Silicon,-1.919794911,	1.108394109	,-3.135011962)
'		clust.AddMember(element.Silicon,0,	-2.216788217,	-3.135011962)
'		clust.AddMember(element.Silicon,1.919794911,	1.108394109,	-3.135011962)
'
'		
'		Dim mt As New MattheissMuffinTin(clust,0,10.0)
'		
'		console.WriteLine("Inner Potential = " &  mt.InnerPotential.ToString)
'		console.WriteLine("Muffin-tin Radius = " & mt.Radius.ToString)
'		
'		Dim pendrymt As New NeutralMuffinTin(element.Silicon,mt.radius)
'		
'		console.WriteLine("Pendry muffin-tin Inner Potential = " & pendrymt.InnerPotential.ToString)
'		
'		console.WriteLine()
'		
'		'// Calculate phase shifts:
'		Dim ps As PhaseShift
'		
'		    console.WriteLine("Phase shifts:")
'           	
'            console.WriteLine("Energy = 0.01 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(0.01,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'           	console.WriteLine()
'          	
'           	console.WriteLine("Energy = 0.5 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(0.5,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'           	console.WriteLine()
'           	console.WriteLine("Energy = 1.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(1.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'           	console.WriteLine()
'           	console.WriteLine("Energy = 2.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(2.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'           	console.WriteLine()
'           	console.WriteLine("Energy = 3.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(3.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'           	console.WriteLine()
'           	console.WriteLine("Energy = 4.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(4.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'           	console.WriteLine()
'           	console.WriteLine("Energy = 5.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(5.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'			Console.WriteLine()
'           	console.WriteLine("Energy = 6.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(6.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'			Console.WriteLine()
'           	console.WriteLine("Energy = 7.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(7.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'			Console.WriteLine()
'
'			Console.WriteLine("Muffin-tin potential")
'            Dim ipot as Double() =  mt.Potential
'            For i As Integer = 0 To mt.Mesh.Count-1
'            	console.WriteLine(mt.Mesh.R(i).ToString & ", " & (-ipot(i)).tostring)
'            Next
'
'	End Sub
'
'	<Test> _
'	Public Sub CompareMuffinTins
'		
'		Dim clust As New Cluster()
'
'		clust.AddMember(element.Silicon,0.0,0.0,0.0)
'		clust.AddMember(element.Silicon,-1.92,1.11,-0.784)
'		clust.AddMember(element.Silicon,+1.92,1.11,-0.784)
'		clust.AddMember(element.Silicon,0.0,-2.22,-0.784)
'		clust.AddMember(element.Silicon,0.0,0.0,2.351)
'		
'		Dim mt As New MattheissMuffinTin(clust,0,10.0)
'		
'		console.WriteLine("Inner Potential = " &  mt.InnerPotential.ToString)
'		console.WriteLine("Muffin-tin Radius = " & mt.Radius.ToString)
'		
'		Dim pendrymt As New NeutralMuffinTin(element.Silicon,mt.radius)
'		
'		console.WriteLine("Pendry muffin-tin Inner Potential = " & pendrymt.InnerPotential.ToString)
'
'		Console.WriteLine("Muffin-tin potential")
'        Dim ipot As Double() =  mt.Potential
'        Dim ipot2 as Double() = pendrymt.potential
'        For i As Integer = 0 To mt.Mesh.Count-1
'            console.WriteLine(mt.Mesh.R(i).ToString & ", " & (-ipot(i)).tostring & ", " & (-ipot2(i)).tostring)
'        Next
'
'	End Sub
'
'		<Test> _
'	Public Sub ChlorineSi
'		
'		Dim clust As New Cluster()
'		
'		'// Note that per cluster requirements, coordinates are in Angstrom units
'		clust.AddMember(element.Chlorine,0.0,0.0,0.0)
'		clust.AddMember(element.Silicon,0.0,0.0,-2.03)
'		
'		Dim mt As New MattheissMuffinTin(clust,0,10.0)
'		
'		console.WriteLine("Inner Potential = " &  mt.InnerPotential.ToString)
'		console.WriteLine("Muffin-tin Radius = " & mt.Radius.ToString)
'		
'		Dim pendrymt As New NeutralMuffinTin(element.Chlorine, mt.radius)
'		
'		console.WriteLine("Pendry muffin-tin Inner Potential = " & pendrymt.InnerPotential.ToString)
'		
'		console.WriteLine()
'		
'		'// Calculate phase shifts:
'		Dim ps As PhaseShift
'		
'		    console.WriteLine("Phase shifts:")
'           	
'            console.WriteLine("Energy = 0.01 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(0.01,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'           	console.WriteLine()
'          	
'           	console.WriteLine("Energy = 0.5 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(0.5,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'           	console.WriteLine()
'           	console.WriteLine("Energy = 1.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(1.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'           	console.WriteLine()
'           	console.WriteLine("Energy = 2.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(2.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'           	console.WriteLine()
'           	console.WriteLine("Energy = 3.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(3.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'           	console.WriteLine()
'           	console.WriteLine("Energy = 4.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(4.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'           	console.WriteLine()
'           	console.WriteLine("Energy = 5.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(5.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'			Console.WriteLine()
'           	console.WriteLine("Energy = 6.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(6.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'			Console.WriteLine()
'           	console.WriteLine("Energy = 7.0 Ry")
'           	console.WriteLine("L     delta_L (Rad)")
'           	ps = new PhaseShift(7.0,10,mt)
'           	For l As Integer = 0 To 10
'           		console.WriteLine(l.ToString & "     " & ps.RealValue(l).ToString)
'           	Next
'			Console.WriteLine()
'
'	End Sub
'
'End Class
'

