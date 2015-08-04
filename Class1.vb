Public Class TimeConvert
    ''' <summary>
    ''' Convert time from 24 Format to 12 Format.
    ''' This function can throw a InvalidTimeException because the time may be invalid, but will move on if it is given a is a valid time.
    ''' </summary>
    ''' <param name="toConvert">This is the time in 24 Format that will be converted. This must include a : between the hours and the minutes, or else the conversion will fail.</param>
    ''' <param name="forceConvert">Set this to true if you want to force the conversion without throwing InvalidTimeExceptions.</param>
    ''' <remarks></remarks>
    Public Function convertTime(toConvert As String, Optional forceConvert As Boolean = False)
        'Minutes from 24 parameter
        Dim tfMin As Integer = toConvert.Substring(3, 2)
        'Make sure this time is valid.
        If tfMin > 60 Then
            If forceConvert = False Then
                Throw New InvalidTimeException
                Exit Function
            End If
        End If
        'Hours from 24 parameter
        Dim tfHr As Integer = Split(toConvert, ":")(0)
        'Make sure this time is valid.
        If tfHr > 24 Then
            If forceConvert = False Then
                Throw New InvalidTimeException
                Exit Function
            End If
        End If
        'Is Am or Pm
        Dim AmPm As String = "??"
        'No Longer Needed Debug line: MsgBox(String.Concat("Debug: tfHr = ", tfHr.ToString, " tfMin = ", tfMin.ToString()))
        'Hours in 12 format
        Dim tHr As Integer = "00"
        'Minutes in 24 format but who needs it.
        Dim tMin As Integer = tfMin
        'The 12 time that needs to be calculated
        Dim nTime As String = "00:00"
        'Now we need to do some math to find out if the 12-Time should be AM or PM.
        If tfHr >= 12 Then
            AmPm = "PM"
        Else
            AmPm = "AM"
        End If
        'More simple math: Convert the hours if required.
        If tfHr > 12 Then
            'The tfHr is over 12 which means it needs to be converted.
            'Begin the convert process...
            tHr = tfHr - 12
            'Add the : and minutes to the new time to apply to the 12 text box, and glitch fix.
            If tHr >= 10 Then
                If tMin >= 10 Then
                    nTime = String.Concat(tHr.ToString, ":", tMin.ToString)
                Else
                    nTime = String.Concat(tHr.ToString, ":", "0", tMin.ToString)
                End If
            Else
                If tMin >= 10 Then
                    nTime = String.Concat("0", tHr.ToString, ":", tMin.ToString)
                Else
                    nTime = String.Concat("0", tHr.ToString, ":", "0", tMin.ToString)
                End If
            End If
            'Return the new time.
            Return String.Concat(nTime, " ", AmPm)
        Else
            'Nothing needs to be done, because 12 is half of 24.
            'Apply the variables.
            tHr = tfHr
            'Add the : and minutes to the new time to apply to the 12 text box, and glitch fix.
            If tHr >= 10 Then
                If tMin >= 10 Then
                    nTime = String.Concat(tHr.ToString, ":", tMin.ToString)
                Else
                    nTime = String.Concat(tHr.ToString, ":", "0", tMin.ToString)
                End If
            Else
                If tMin >= 10 Then
                    nTime = String.Concat("0", tHr.ToString, ":", tMin.ToString)
                Else
                    nTime = String.Concat("0", tHr.ToString, ":", "0", tMin.ToString)
                End If
            End If
            'Return the new time.
            Return String.Concat(nTime, " ", AmPm)
        End If
    End Function
    ''' <summary>
    ''' Convert the time from the 12 time format to the 24 time format.
    ''' </summary>
    ''' <param name="twelveformat">This is the 12 time format to be converted from.</param>
    ''' <returns>The 24 time format string.</returns>
    ''' <remarks></remarks>
    Function convertTimeFromTwelve(twelveformat As TwelveFormatTime)
        If twelveformat.AmOrPm = TwelveFormatTime.AMPMTime.AM Or twelveformat.AmOrPm = TwelveFormatTime.AMPMTime.ERR Then
            Return String.Concat(twelveformat.Hour.ToString, ":", twelveformat.Minute.ToString, " AM")
        ElseIf twelveformat.AmOrPm = TwelveFormatTime.AMPMTime.PM Then
            Dim nHr As Integer = "24"
            nHr = twelveformat.Hour + 12
            Return String.Concat(nHr, ":", twelveformat.Minute.ToString)
        Else
            Return "Error"
        End If
    End Function
    ''' <summary>
    ''' This is used in convertTime() to tell it which type of time to convert from and which to convert to.
    ''' </summary>
    ''' <remarks></remarks>
    Enum InputFormat
        twelve = 1
        twenty_four = 2
    End Enum
End Class
Public Class InvalidTimeException
    Inherits Exception
    Private Msg As String = String.Empty
    Public Sub New()
        Msg = "The time is invalid."
    End Sub
    Public Overrides ReadOnly Property Message As String
        Get
            Return Msg
        End Get
    End Property
End Class
Public Class TwelveFormatTime
    Private hr As Integer = "00"
    Private min As Integer = "00"
    Private ampm As AMPMTime = AMPMTime.AM
    Public Overridable ReadOnly Property Hour As Integer
        Get
            Return hr
        End Get
    End Property
            Public Overridable ReadOnly Property Minute As Integer
        Get
            Return min
        End Get
    End Property
            Public Overridable ReadOnly Property AmOrPm As AMPMTime
        Get
            Return ampm
        End Get
    End Property
    ''' <summary>
    ''' Creates a new TwelveFormatTime for use with ConvertTime.
    ''' </summary>
    ''' <param name="HourTime">The integer of hours to be used (Must be double digits, 3 would turn into 03).</param>
    ''' <param name="MinTime">The integer of minutes to be used (Must also be double digits).</param>
    ''' <param name="IsAmOrPm">This is the enum for if the time is in the AM or the PM.</param>
    ''' <remarks></remarks>
    Public Sub New(HourTime As Integer, MinTime As Integer, IsAmOrPm As AMPMTime)
        If HourTime > 12 Then
            Throw New InvalidTimeException
            Exit Sub
        End If
        hr = HourTime
        If MinTime > 60 Then
            Throw New InvalidTimeException
            Exit Sub
        End If
        min = MinTime
        'This doesn't need any checks for being invalid, Enums can be any of the options or null.
        ampm = IsAmOrPm
    End Sub
    Enum AMPMTime
        ERR = 0
        AM = 1
        PM = 2
    End Enum
End Class
