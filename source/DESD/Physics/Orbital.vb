

    Public Structure Orbital

        Private mP As Double()
        Private mN As Integer
        Private mL As Integer
        Private mEnergy As Double
        Private mOccupancy As Double
        Private mSigma as Double()
		
#Region "Constructors"

        Sub New(ByVal n As Integer, ByVal l As Integer, ByVal occupancy As Double, ByVal energy As Double, ByVal wavefunction As Double())
            mN = n
            mL = l
            mEnergy = energy
            mOccupancy = occupancy
            
            '// Store a copy of the input array, to avoid any problems
            '// with modifying the source array.
            ReDim mP(wavefunction.Length-1)
            system.Array.Copy(wavefunction,mP,wavefunction.Length)
            
            '// Compute sigma:
			ComputeSigma()
            
        End Sub
        
        Private Sub ComputeSigma()
        	ReDim mSigma(mP.Length-1)
            For i As Integer = 0 To mP.Length-1
            	mSigma(i) = -mOccupancy * mP(i) * mP(i)	
            Next
        End Sub

#End Region

        Public ReadOnly Property PArray() As Double()
            Get
                Return mP
            End Get
        End Property

        Public Readonly Property Occupancy() As Double
            Get
                Return mOccupancy
            End Get
'            Set (value As Double)
'            	mOccupancy = value
'            	ComputeSigma()
'            End Set
        End Property

        Public ReadOnly Property N() As Integer
            Get
                Return mN
            End Get
        End Property

        Public ReadOnly Property L() As Integer
            Get
                Return mL
            End Get
        End Property

        Public ReadOnly Property Energy() As Double
            Get
                Return mEnergy
            End Get
        End Property

		Public Function P(index As Integer) As Double
			return mP(index)
		End Function
		
		Public Function Sigma(index As Integer) As Double
			return mSigma(index)
		End Function
		
		Public Function SigmaArray As Double()
			return mSigma
		End Function

    End Structure


