﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
'
Namespace Marking2
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4161.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="MarkingCodeSoap", [Namespace]:="http://az_zulhisham.org/")>  _
    Partial Public Class MarkingCode
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private AboutMeOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetMarkingCodeOperationCompleted As System.Threading.SendOrPostCallback
        
        Private SetSequenceMarkedOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetMarkingSequenceFCOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.az_LMservices.My.MySettings.Default.az_LMservices_Marking2_MarkingCode
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event AboutMeCompleted As AboutMeCompletedEventHandler
        
        '''<remarks/>
        Public Event GetMarkingCodeCompleted As GetMarkingCodeCompletedEventHandler
        
        '''<remarks/>
        Public Event SetSequenceMarkedCompleted As SetSequenceMarkedCompletedEventHandler
        
        '''<remarks/>
        Public Event GetMarkingSequenceFCCompleted As GetMarkingSequenceFCCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://az_zulhisham.org/AboutMe", RequestNamespace:="http://az_zulhisham.org/", ResponseNamespace:="http://az_zulhisham.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function AboutMe() As String
            Dim results() As Object = Me.Invoke("AboutMe", New Object(-1) {})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub AboutMeAsync()
            Me.AboutMeAsync(Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub AboutMeAsync(ByVal userState As Object)
            If (Me.AboutMeOperationCompleted Is Nothing) Then
                Me.AboutMeOperationCompleted = AddressOf Me.OnAboutMeOperationCompleted
            End If
            Me.InvokeAsync("AboutMe", New Object(-1) {}, Me.AboutMeOperationCompleted, userState)
        End Sub
        
        Private Sub OnAboutMeOperationCompleted(ByVal arg As Object)
            If (Not (Me.AboutMeCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent AboutMeCompleted(Me, New AboutMeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://az_zulhisham.org/GetMarkingCode", RequestNamespace:="http://az_zulhisham.org/", ResponseNamespace:="http://az_zulhisham.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetMarkingCode(ByVal LotNo As String, ByVal SpecFile As String, ByVal MarkingDate As String) As String
            Dim results() As Object = Me.Invoke("GetMarkingCode", New Object() {LotNo, SpecFile, MarkingDate})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetMarkingCodeAsync(ByVal LotNo As String, ByVal SpecFile As String, ByVal MarkingDate As String)
            Me.GetMarkingCodeAsync(LotNo, SpecFile, MarkingDate, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetMarkingCodeAsync(ByVal LotNo As String, ByVal SpecFile As String, ByVal MarkingDate As String, ByVal userState As Object)
            If (Me.GetMarkingCodeOperationCompleted Is Nothing) Then
                Me.GetMarkingCodeOperationCompleted = AddressOf Me.OnGetMarkingCodeOperationCompleted
            End If
            Me.InvokeAsync("GetMarkingCode", New Object() {LotNo, SpecFile, MarkingDate}, Me.GetMarkingCodeOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetMarkingCodeOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetMarkingCodeCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetMarkingCodeCompleted(Me, New GetMarkingCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://az_zulhisham.org/SetSequenceMarked", RequestNamespace:="http://az_zulhisham.org/", ResponseNamespace:="http://az_zulhisham.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function SetSequenceMarked(ByVal LotNo As String, ByVal SpecFile As String, ByVal MarkingCode1 As String) As String
            Dim results() As Object = Me.Invoke("SetSequenceMarked", New Object() {LotNo, SpecFile, MarkingCode1})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub SetSequenceMarkedAsync(ByVal LotNo As String, ByVal SpecFile As String, ByVal MarkingCode1 As String)
            Me.SetSequenceMarkedAsync(LotNo, SpecFile, MarkingCode1, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub SetSequenceMarkedAsync(ByVal LotNo As String, ByVal SpecFile As String, ByVal MarkingCode1 As String, ByVal userState As Object)
            If (Me.SetSequenceMarkedOperationCompleted Is Nothing) Then
                Me.SetSequenceMarkedOperationCompleted = AddressOf Me.OnSetSequenceMarkedOperationCompleted
            End If
            Me.InvokeAsync("SetSequenceMarked", New Object() {LotNo, SpecFile, MarkingCode1}, Me.SetSequenceMarkedOperationCompleted, userState)
        End Sub
        
        Private Sub OnSetSequenceMarkedOperationCompleted(ByVal arg As Object)
            If (Not (Me.SetSequenceMarkedCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent SetSequenceMarkedCompleted(Me, New SetSequenceMarkedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://az_zulhisham.org/GetMarkingSequenceFC", RequestNamespace:="http://az_zulhisham.org/", ResponseNamespace:="http://az_zulhisham.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetMarkingSequenceFC(ByVal LotNo As String, ByVal SpecFile As String, ByVal MarkingDate As String) As String
            Dim results() As Object = Me.Invoke("GetMarkingSequenceFC", New Object() {LotNo, SpecFile, MarkingDate})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetMarkingSequenceFCAsync(ByVal LotNo As String, ByVal SpecFile As String, ByVal MarkingDate As String)
            Me.GetMarkingSequenceFCAsync(LotNo, SpecFile, MarkingDate, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetMarkingSequenceFCAsync(ByVal LotNo As String, ByVal SpecFile As String, ByVal MarkingDate As String, ByVal userState As Object)
            If (Me.GetMarkingSequenceFCOperationCompleted Is Nothing) Then
                Me.GetMarkingSequenceFCOperationCompleted = AddressOf Me.OnGetMarkingSequenceFCOperationCompleted
            End If
            Me.InvokeAsync("GetMarkingSequenceFC", New Object() {LotNo, SpecFile, MarkingDate}, Me.GetMarkingSequenceFCOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetMarkingSequenceFCOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetMarkingSequenceFCCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetMarkingSequenceFCCompleted(Me, New GetMarkingSequenceFCCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4161.0")>  _
    Public Delegate Sub AboutMeCompletedEventHandler(ByVal sender As Object, ByVal e As AboutMeCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4161.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class AboutMeCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4161.0")>  _
    Public Delegate Sub GetMarkingCodeCompletedEventHandler(ByVal sender As Object, ByVal e As GetMarkingCodeCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4161.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetMarkingCodeCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4161.0")>  _
    Public Delegate Sub SetSequenceMarkedCompletedEventHandler(ByVal sender As Object, ByVal e As SetSequenceMarkedCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4161.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class SetSequenceMarkedCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4161.0")>  _
    Public Delegate Sub GetMarkingSequenceFCCompletedEventHandler(ByVal sender As Object, ByVal e As GetMarkingSequenceFCCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4161.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetMarkingSequenceFCCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
End Namespace
