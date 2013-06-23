Imports Sieger.Math


'<Serializable()> _
'Public Structure QuantityOfEnergy
'    Implements IFormattable
'    Implements IConvertible
'    Implements IComparable
'    Implements IComparable(Of QuantityOfEnergy)
'
'    Private _value As Double
'    Private _units As UnitExpression
'
'#Region "Constructors"
'
'    Public Sub New(ByVal val As Double, ByVal units As UnitExpression)
'        _value = val
'        _units = units
'    End Sub
'
'    Public Sub New(ByVal val As Double, ByVal units As Unit)
'        _value = val
'        _units = New UnitExpression(units)
'    End Sub
'
'    Public Sub New(ByVal val As Double, ByVal units As String)
'        _value = val
'        _units = New UnitExpression(units)
'    End Sub
'
'#End Region
'
'
'    Public Property Value() As Double
'        Get
'            Return _value
'        End Get
'        Set(ByVal value As Double)
'            _value = value
'        End Set
'    End Property
'
'
'    Public Property Units() As UnitExpression
'        Get
'            Return _units
'        End Get
'        Set(ByVal value As UnitExpression)
'            _units = value
'        End Set
'    End Property
'
'
'    Public Overrides Function ToString() As String
'        Return _value.ToString & " " & _units.ToString
'    End Function
'
'
'    Public Overloads Function ToString(ByVal format As String) As String
'        Return Me.ToString(format, Nothing)
'    End Function
'
'
'    Public Shared Function Parse(ByVal s As String) As Quantity
'        '// s should be formatted value (space) units
'
'        '// Trim off any leading or trailing spaces:
'        Dim delimiter() As Char = {Chr(32)}
'
'        Dim s2 As String = s.Trim
'
'        Dim parts() As String = s2.Split(delimiter)
'
'        If parts.Length = 0 Then Throw New FormatException("String is not a properly formatted Quantity.")
'
'        '// First part is value
'        Dim val As Double = CDbl(parts(0))
'        Dim units As String = ""
'
'        If parts.Length >= 2 Then units = parts(1)
'
'        Return New Quantity(val, units)
'
'    End Function
'
'
'    Public Function Reciprocal() As Quantity
'        Return New Quantity(1.0 / _value, _units.Reciprocal)
'    End Function
'
'
'    Public ReadOnly Property Dimensions() As String
'        Get
'            Return _units.Dimensions
'        End Get
'    End Property
'
'
'    Public Shared Function ConvertUnits(ByVal q As Quantity, ByVal desiredunits As UnitExpression) As Quantity
'        If q.Dimensions <> desiredunits.Dimensions Then Throw New IncompatibleDimensionsException
'
'        '// Handle the trivial case (already in the right units):
'        If q.Units = desiredunits Then Return q
'
'        '// Strategy - convert first to SI units, then to the desired units:
'
'        Dim q1 As Quantity = q
'
'        '// Is q1 already in SI units?  if not, convert:
'        If Not (q1.Units.IsSI) Then
'            q1.Value *= q1.Units.GetSIConversionFactor
'            q1.Units = New UnitExpression(q1.Dimensions)
'        End If
'
'        '// If the desired units are SI, then return what we've got:
'        If desiredunits.IsSI Then Return q1
'
'        Return New Quantity(q1.Value / desiredunits.GetSIConversionFactor, desiredunits)
'
'    End Function
'
'
'    Public Shared Function ConvertUnits(ByVal q As Quantity, ByVal desiredunits As String) As Quantity
'        Return ConvertUnits(q, New UnitExpression(desiredunits))
'    End Function
'
'
'#Region "Operators"
'
'
'    Public Shared Operator +(ByVal x As Quantity, ByVal y As Quantity) As Quantity
'        '// Only quantities of like units can be added, otherwise throw an "incompatible units" exception
'
'        If x.Units <> y.Units Then Throw New IncompatibleUnitsException
'
'        Return New Quantity(x.Value + y.Value, x.Units + y.Units)
'
'    End Operator
'
'
'    Public Shared Operator -(ByVal x As Quantity, ByVal y As Quantity) As Quantity
'        '// Only quantities of like units can be subtracted, otherwise throw an "incompatible units" exception
'
'        If x.Units <> y.Units Then Throw New IncompatibleUnitsException
'
'        Return New Quantity(x.Value - y.Value, x.Units + y.Units)
'
'    End Operator
'
'
'    Public Shared Operator *(ByVal x As Quantity, ByVal y As Quantity) As Quantity
'        Return New Quantity(x.Value * y.Value, x.Units * y.Units)
'    End Operator
'
'
'    Public Shared Operator *(ByVal x As Quantity, ByVal y As IConvertible) As Quantity
'        Return New Quantity(x.Value * CDbl(y), x.Units)
'    End Operator
'
'
'    Public Shared Operator *(ByVal x As IConvertible, ByVal y As Quantity) As Quantity
'        Return New Quantity(y.Value * CDbl(x), y.Units)
'    End Operator
'
'
'    Public Shared Operator /(ByVal x As Quantity, ByVal y As Quantity) As Quantity
'        Return New Quantity(x.Value / y.Value, x.Units / y.Units)
'    End Operator
'
'
'    Public Shared Operator /(ByVal x As Quantity, ByVal y As IConvertible) As Quantity
'        Return New Quantity(x.Value / CDbl(y), x.Units)
'    End Operator
'
'
'    Public Shared Operator /(ByVal x As IConvertible, ByVal y As Quantity) As Quantity
'        Return New Quantity(CDbl(x) / y.Value, y.Units.Reciprocal)
'    End Operator
'
'    Public Shared Operator =(ByVal x As Quantity, ByVal y As Quantity) As Boolean
'        Return (x.ToString = y.ToString)
'    End Operator
'
'    Public Shared Operator <>(ByVal x As Quantity, ByVal y As Quantity) As Boolean
'        Return (x.ToString <> y.ToString)
'    End Operator
'
'    Public Shared Operator >(ByVal x As Quantity, ByVal y As Quantity) As Boolean
'        If x.Dimensions <> y.Dimensions Then Throw New IncompatibleDimensionsException
'        Return x.Value > y.Value
'    End Operator
'
'    Public Shared Operator <(ByVal x As Quantity, ByVal y As Quantity) As Boolean
'        If x.Dimensions <> y.Dimensions Then Throw New IncompatibleDimensionsException
'        Return x.Value < y.Value
'    End Operator
'
'    Public Shared Operator >=(ByVal x As Quantity, ByVal y As Quantity) As Boolean
'        If x.Dimensions <> y.Dimensions Then Throw New IncompatibleDimensionsException
'        Return x.Value >= y.Value
'    End Operator
'
'    Public Shared Operator <=(ByVal x As Quantity, ByVal y As Quantity) As Boolean
'        If x.Dimensions <> y.Dimensions Then Throw New IncompatibleDimensionsException
'        Return x.Value <= y.Value
'    End Operator
'
'#End Region
'
'
'#Region "IFormattable Implementation"
'
'    Public Overloads Function ToString(ByVal format As String, ByVal formatProvider As System.IFormatProvider) As String Implements System.IFormattable.ToString
'        Select Case format
'            Case "[]", "[/]"
'                Return _value.ToString & " [" & _units.ToString & "]"
'            Case "[^]"
'                Return _value.ToString & " [" & _units.ToString2("^", formatProvider) & "]"
'            Case "^"
'                Return _value.ToString & " " & _units.ToString2("^", formatProvider)
'            Case Else
'                Return ToString()
'        End Select
'    End Function
'
'#End Region
'
'
'#Region "IConvertible Implementation"
'
'    Public Function GetTypeCode() As System.TypeCode Implements System.IConvertible.GetTypeCode
'        Return TypeCode.Object
'    End Function
'
'    Public Function ToBoolean(ByVal provider As System.IFormatProvider) As Boolean Implements System.IConvertible.ToBoolean
'        Return System.Convert.ToBoolean(_value)
'    End Function
'
'    Public Function ToByte(ByVal provider As System.IFormatProvider) As Byte Implements System.IConvertible.ToByte
'        Return System.Convert.ToByte(_value)
'    End Function
'
'    Public Function ToChar(ByVal provider As System.IFormatProvider) As Char Implements System.IConvertible.ToChar
'        Return System.Convert.ToChar(_value)
'    End Function
'
'    Public Function ToDateTime(ByVal provider As System.IFormatProvider) As Date Implements System.IConvertible.ToDateTime
'        Return System.Convert.ToDateTime(_value)
'    End Function
'
'    Public Function ToDecimal(ByVal provider As System.IFormatProvider) As Decimal Implements System.IConvertible.ToDecimal
'        Return System.Convert.ToDecimal(_value)
'    End Function
'
'    Public Function ToDouble(ByVal provider As System.IFormatProvider) As Double Implements System.IConvertible.ToDouble
'        Return _value
'    End Function
'
'    Public Function ToInt16(ByVal provider As System.IFormatProvider) As Short Implements System.IConvertible.ToInt16
'        Return System.Convert.ToInt16(_value)
'    End Function
'
'    Public Function ToInt32(ByVal provider As System.IFormatProvider) As Integer Implements System.IConvertible.ToInt32
'        Return System.Convert.ToInt32(_value)
'    End Function
'
'    Public Function ToInt64(ByVal provider As System.IFormatProvider) As Long Implements System.IConvertible.ToInt64
'        Return System.Convert.ToInt64(_value)
'    End Function
'
'    Public Function ToSByte(ByVal provider As System.IFormatProvider) As SByte Implements System.IConvertible.ToSByte
'        Return System.Convert.ToSByte(_value)
'    End Function
'
'    Public Function ToSingle(ByVal provider As System.IFormatProvider) As Single Implements System.IConvertible.ToSingle
'        Return System.Convert.ToSingle(_value)
'    End Function
'
'    Public Overloads Function ToString(ByVal provider As System.IFormatProvider) As String Implements System.IConvertible.ToString
'        Return ToString("", provider)
'    End Function
'
'    Public Function ToType(ByVal conversionType As System.Type, ByVal provider As System.IFormatProvider) As Object Implements System.IConvertible.ToType
'
'    End Function
'
'    Public Function ToUInt16(ByVal provider As System.IFormatProvider) As UShort Implements System.IConvertible.ToUInt16
'        Return System.Convert.ToUInt16(_value)
'    End Function
'
'    Public Function ToUInt32(ByVal provider As System.IFormatProvider) As UInteger Implements System.IConvertible.ToUInt32
'        Return System.Convert.ToUInt32(_value)
'    End Function
'
'    Public Function ToUInt64(ByVal provider As System.IFormatProvider) As ULong Implements System.IConvertible.ToUInt64
'        Return System.Convert.ToUInt64(_value)
'    End Function
'
'#End Region
'
'
'#Region "IComparable Implementation"
'
'    Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
'
'
'
'    End Function
'
'    Public Function CompareTo1(ByVal other As Quantity) As Integer Implements System.IComparable(Of Quantity).CompareTo
'        If Me.Dimensions <> other.Dimensions Then Throw New IncompatibleDimensionsException
'
'    End Function
'
'#End Region
'
'
'End Structure
'
'
'
'
'Public Class IncompatibleUnitsException
'    Inherits System.Exception
'
'End Class
'
'
'Public Class IncompatibleDimensionsException
'    Inherits System.Exception
'
'End Class