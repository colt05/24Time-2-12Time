Public Class TimeConvert
    ''' <summary>
    ''' Convert time from 24 Format to 12 Format.
    ''' This function can throw a _24212C-DLL.InvalidTimeException because the time may be invalid, but will move on if it is given a is a valid time.
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
        Return "Error"
    End Function
End Class
Public Class InvalidTimeException
    Inherits Exception
    Private MessageDim As String = String.Empty
    Public Sub New()
        MessageDim = "The time is invalid."
    End Sub
    Public Overrides ReadOnly Property Message As String
        Get
            Return MessageDim
        End Get
    End Property
End Class
