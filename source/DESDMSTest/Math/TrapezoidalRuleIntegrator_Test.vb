Imports DESD.Math

Namespace Integration_Tests
	
    <TestClass> _
Public Class TrapezoidalRuleIntegrator_UnitTest

        <TestMethod> _
        Public Sub Integrate01()
            Dim x() As Double = {1.0, 2.0, 3.0}
            Dim y() As Double = {1.0, 2.0, 3.0}

            Dim integral As Double = Integration.TrapezoidalRuleIntegrator.Integrate(x, y, 0, 2)

            Assert.AreEqual(4.0, integral)

        End Sub

        <TestMethod> _
        Public Sub Integrate02()
            Dim x() As Double = {1.0, 2.0, 3.0}
            Dim y() As Double = {1.0, 2.0, 3.0}

            Dim integral As Double = Integration.TrapezoidalRuleIntegrator.Integrate(x, y, 0, 1)

            Assert.AreEqual(1.5, integral)

        End Sub

        <TestMethod> _
        Public Sub Integrate03()
            Dim x() As Double = {1.0, 2.0, 3.0}
            Dim y() As Double = {1.0, 2.0, 3.0}

            Dim integral As Double = Integration.TrapezoidalRuleIntegrator.Integrate(x, y, 1, 2)

            Assert.AreEqual(2.5, integral)

        End Sub

        <TestMethod> _
        Public Sub Integrate04()
            Dim x() As Double = {1.0, 2.0, 3.0}
            Dim y() As Double = {1.0, 2.0, 3.0}

            Dim integral As Double = Integration.TrapezoidalRuleIntegrator.Integrate(x, y, 0, 0)

            Assert.AreEqual(0.0, integral)

        End Sub

        <TestMethod(), ExpectedException(GetType(ArgumentException))> _
        Public Sub Exception01()
            Dim x() As Double = {1.0, 2.0, 3.0}
            Dim y() As Double = {1.0, 2.0, 3.0}

            Dim integral As Double = Integration.TrapezoidalRuleIntegrator.Integrate(x, y, 2, 0)

        End Sub

        <TestMethod(), ExpectedException(GetType(ArgumentException))> _
        Public Sub Exception02()
            Dim x() As Double = {1.0, 2.0}
            Dim y() As Double = {1.0, 2.0, 3.0}

            Dim integral As Double = Integration.TrapezoidalRuleIntegrator.Integrate(x, y, 0, 2)

        End Sub

        <TestMethod(), ExpectedException(GetType(ArgumentException))> _
        Public Sub Exception03()
            Dim x() As Double = {1.0, 2.0, 3.0}
            Dim y() As Double = {1.0, 2.0}

            Dim integral As Double = Integration.TrapezoidalRuleIntegrator.Integrate(x, y, 0, 2)

        End Sub

        <TestMethod(), ExpectedException(GetType(ArgumentException))> _
        Public Sub Exception04()
            Dim x() As Double = {1.0, 2.0, 3.0}
            Dim y() As Double = {1.0, 2.0, 3.0}

            Dim integral As Double = Integration.TrapezoidalRuleIntegrator.Integrate(x, y, -1, 2)

        End Sub

        <TestMethod(), ExpectedException(GetType(ArgumentException))> _
        Public Sub Exception05()
            Dim x() As Double = {1.0, 2.0, 3.0}
            Dim y() As Double = {1.0, 2.0, 3.0}

            Dim integral As Double = Integration.TrapezoidalRuleIntegrator.Integrate(x, y, 0, 3)

        End Sub

    End Class

End Namespace