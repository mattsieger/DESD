
Imports DESD

'
' Created by SharpDevelop.
' User: Matt
' Date: 9/29/2011
' Time: 12:03 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
<TestClass> _
Public Class FEFFCalculation_UnitTest


    <TestMethod> _
    Public Sub WriteInpFile01()
        Dim c As New Cluster("C:\temp\sicl_version3a.cst")

        Dim fileDir As String = "C:\Users\Matt\Documents\FEFF\Feff6ldist"

        FEFFCalculation.WriteInpFile(fileDir, c, 300, 645)

    End Sub

    <TestMethod> _
    Public Sub RunProcess01()

        Dim filename As String = "C:\Users\Matt\Documents\FEFF\feff6ldist\feff6l.exe"

        FEFFCalculation.RunProcess(filename)

    End Sub

End Class
