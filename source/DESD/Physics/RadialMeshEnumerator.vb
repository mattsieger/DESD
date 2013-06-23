


Public Class RadialMeshEnumerator


    Implements IEnumerator(Of Double)

    Private mMesh As IRadialMesh
    Private mCurrentIndex As Integer = -1

    Sub New(ByVal mesh As IRadialMesh)
        mMesh = mesh
    End Sub

    Function MoveNext() As Boolean Implements IEnumerator.MoveNext
        '•	Increments the CurrentIndex pointer.
        '•	When called at end of list, throws an InvalidOperationException.
        '•	Returns FALSE if pointer is at the end of the list, TRUE otherwise.
        If mCurrentIndex = mMesh.Count Then Return False

        mCurrentIndex += 1

        Return Not (mCurrentIndex = mMesh.Count)

    End Function

'    Function MoveNext1() As Boolean Implements IEnumerator(Of Double).MoveNext
'        '•	Increments the CurrentIndex pointer.
'        '•	When called at end of list, throws an InvalidOperationException.
'        '•	Returns FALSE if pointer is at the end of the list, TRUE otherwise.
'        If mCurrentIndex = mMesh.Count - 1 Then
'            Throw New InvalidOperationException
'        Else
'            mCurrentIndex += 1
'        End If
'
'        Return Not (mCurrentIndex = mMesh.Count - 1)
'
'    End Function

    Function MovePrevious() As Boolean
        '•	Decrements the CurrentIndex pointer.
        '•	When called at the beginning of the list, throws an InvalidOperationException.
        '•	Returns FALSE if pointer is at the beginning of the list, TRUE otherwise.
        If mCurrentIndex = 0 Then
            Throw New InvalidOperationException
        Else
            mCurrentIndex -= 1
        End If

        Return Not (mCurrentIndex = 0)

    End Function


    Sub SetToBeginning() Implements IEnumerator.Reset
        '•	Sets the CurrentIndex pointer at -1 (enumerators are positioned before the first element until MoveNext is called)
        mCurrentIndex = -1
    End Sub

'    Sub SetToBeginning1() Implements IEnumerator(Of Double).Reset
'        '•	Sets the CurrentIndex pointer at -1 (enumerators are positioned before the first element until MoveNext is called)
'        mCurrentIndex = -1
'    End Sub

    Sub SetToEnd()
        '•	Sets the CurrentIndex pointer at Count (positioned after the last element until MovePrevious is called)
        mCurrentIndex = mMesh.Count
    End Sub

    Function DRPlus() As Double
        '•	Returns X(currentIndex + 1) – X(currentIndex)
        '•	If currentIndex > Count-1, then throw an InvalidOperationException.
        '•	If called when currentIndex = -1, then throw an InvalidOperationException.
        If (mCurrentIndex > mMesh.Count - 1) Or (mCurrentIndex = -1) Then
            Throw New InvalidOperationException
        Else
            Return (mMesh.R(mCurrentIndex + 1) - mMesh.R(mCurrentIndex))
        End If

    End Function

    Function DRMinus() As Double
        '•	Returns X(currentIndex) – X(currentIndex – 1)
        '•	If currentIndex < 1, then throw an InvalidOperationException.
        '•	If called when currentIndex = Count, then throw an InvalidOperationException.
        If (mCurrentIndex < 1) Or (mCurrentIndex = mMesh.Count) Then
            Throw New InvalidOperationException
        Else
            Return (mMesh.R(mCurrentIndex) - mMesh.R(mCurrentIndex - 1))
        End If

    End Function

    Function DRPlus2() As Double
        '•	Returns X(currentIndex + 2) – X(currentIndex + 1)
        '•	If currentIndex > Count-2, then throw an InvalidOperationException.
        '•	If called when currentIndex = -1, then throw an InvalidOperationException.
        If (mCurrentIndex > mMesh.Count - 2) Or (mCurrentIndex = -1) Then
            Throw New InvalidOperationException
        Else
            Return (mMesh.R(mCurrentIndex + 2) - mMesh.R(mCurrentIndex + 1))
        End If

    End Function

    Function DRMinus2() As Double
        '•	Returns X(currentIndex – 1) – X(currentIndex – 2)
        '•	If currentIndex < 2, then throw an InvalidOperationException.
        '•	If called when currentIndex = Count, then throw an InvalidOperationException.
        If (mCurrentIndex < 2) Or (mCurrentIndex = mMesh.Count) Then
            Throw New InvalidOperationException
        Else
            Return (mMesh.R(mCurrentIndex - 1) - mMesh.R(mCurrentIndex - 2))
        End If

    End Function

    Function CurrentIndex() As Integer
        Return mCurrentIndex
    End Function


    Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
        '•	Return the value of the current point X(currentIndex)
        Get
        	if mCurrentIndex = -1 then return nothing
            Return mMesh.R(mCurrentIndex)
        End Get
    End Property

    Public ReadOnly Property Current1() As Double Implements System.Collections.Generic.IEnumerator(Of Double).Current
        Get
        	if mCurrentIndex = -1 then return nothing
            Return mMesh.R(mCurrentIndex)
        End Get
    End Property

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
