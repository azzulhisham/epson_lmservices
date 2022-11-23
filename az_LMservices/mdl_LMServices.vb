Module mdl_LMServices

    'Public Const sqlServer As String = "172.16.59.254\SQLEXPRESS"
    'Public Const sqlName As String = "Marking"
    'Public Const sqluid As String = "VB-SQL"
    'Public Const sqlpwd As String = "Anyn0m0us"

    Public Const sqlServer As String = "DESKTOP-TLVFD7V\SQLEXPRESS"
    Public Const sqlName As String = "Marking"
    Public Const sqluid As String = "sa"
    Public Const sqlpwd As String = "Az@HoePinc0615"


    Public Const odbcServer As String = "local"
    Public Const odbcName As String = "Marking.mdb"
    Public Const odbcuid As String = ""
    Public Const odbcpwd As String = ""

    Public Const IMI_Path As String = "D:\MachineNet\MacDB\Marking\PX\IMI"
    Public Const NetPath As String = "D:\MachineNet\MacDB\NetTermData"
    Public Const TestingServerPath As String = "\\172.16.59.2\epmmn\Control\CP\PX Line\MI\LaserMarking"

    Public Const SQL As Integer = 1


    Public Structure SpecItem
        Public sFreq As String
        Public sPlant As String
        Public sProdCode As String
        Public sVersion As String
        Public sWkCdFormat As String
        Public sParameter As String
        Public sFormat As String
    End Structure

    Public Structure Rec
        Public Lot_No As String
        Public IMI_No As String
        Public FreqVal As String
        Public Opt As String
        Public RecDate As String
        Public Profile As String
        Public CtrlNo As String
        Public MacNo As String
        Public MData1 As String
        Public MData2 As String
        Public MData3 As String
        Public MData4 As String
        Public MData5 As String
        Public MData6 As String
    End Structure

    Public Structure ParameterProfile
        Public Spec As String
        Public StartNo As String
        Public UseDot As String
        Public UseBlock As String
    End Structure

    Public IMIDataItem As SpecItem

End Module
