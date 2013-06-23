

Imports DESD

Namespace AtomicPhysics_Tests
	
    <TestClass()> _
    Public Class ElementEnumeration

        <TestMethod> _
        Public Sub SymbolExtensionMethod()
            Dim u As Element = Element.Hydrogen

            Assert.IsTrue(u.Symbol = "H")

        End Sub

        <TestMethod> _
        Public Sub ConfigurationExtensionMethod()

            Dim u As Element = Element.Hydrogen

            Dim c As ElectronicConfiguration = u.Configuration

            Assert.IsTrue(TypeOf (c) Is ElectronicConfiguration)

        End Sub

        <TestMethod> _
        Public Sub ElectronMassesExtensionMethod()

            Dim u As Element = Element.Hydrogen

            Dim w As Double = u.ElectronMasses

            Assert.IsTrue(True)

        End Sub

        <TestMethod> _
        Public Sub AtomicWeightExtensionMethod()

            Dim u As Element = Element.Hydrogen

            Dim w As Double = u.AtomicWeight

            Assert.AreEqual(1.00794, w)

        End Sub

    End Class

End Namespace
