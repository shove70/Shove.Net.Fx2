﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace Shove.NewtonServices {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="http://tempuri.org/")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getSIOperationCompleted;
        
        private System.Threading.SendOrPostCallback getSI2OperationCompleted;
        
        private System.Threading.SendOrPostCallback getClientSIServiceStatusOperationCompleted;
        
        private MySoapHeader mySoapHeaderValueField;
        
        private System.Threading.SendOrPostCallback TestSoapHeaderOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Service() {
            this.Url = global::Shove.Properties.Settings.Default.Shove_NewtonServices_Service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public MySoapHeader MySoapHeaderValue {
            get {
                return this.mySoapHeaderValueField;
            }
            set {
                this.mySoapHeaderValueField = value;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event getSICompletedEventHandler getSICompleted;
        
        /// <remarks/>
        public event getSI2CompletedEventHandler getSI2Completed;
        
        /// <remarks/>
        public event getClientSIServiceStatusCompletedEventHandler getClientSIServiceStatusCompleted;
        
        /// <remarks/>
        public event TestSoapHeaderCompletedEventHandler TestSoapHeaderCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getSI", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int getSI(string Url, string Sign) {
            object[] results = this.Invoke("getSI", new object[] {
                        Url,
                        Sign});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void getSIAsync(string Url, string Sign) {
            this.getSIAsync(Url, Sign, null);
        }
        
        /// <remarks/>
        public void getSIAsync(string Url, string Sign, object userState) {
            if ((this.getSIOperationCompleted == null)) {
                this.getSIOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetSIOperationCompleted);
            }
            this.InvokeAsync("getSI", new object[] {
                        Url,
                        Sign}, this.getSIOperationCompleted, userState);
        }
        
        private void OngetSIOperationCompleted(object arg) {
            if ((this.getSICompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getSICompleted(this, new getSICompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getSI2", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int getSI2(string Content, string Sign) {
            object[] results = this.Invoke("getSI2", new object[] {
                        Content,
                        Sign});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void getSI2Async(string Content, string Sign) {
            this.getSI2Async(Content, Sign, null);
        }
        
        /// <remarks/>
        public void getSI2Async(string Content, string Sign, object userState) {
            if ((this.getSI2OperationCompleted == null)) {
                this.getSI2OperationCompleted = new System.Threading.SendOrPostCallback(this.OngetSI2OperationCompleted);
            }
            this.InvokeAsync("getSI2", new object[] {
                        Content,
                        Sign}, this.getSI2OperationCompleted, userState);
        }
        
        private void OngetSI2OperationCompleted(object arg) {
            if ((this.getSI2Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getSI2Completed(this, new getSI2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getClientSIServiceStatus", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int getClientSIServiceStatus() {
            object[] results = this.Invoke("getClientSIServiceStatus", new object[0]);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void getClientSIServiceStatusAsync() {
            this.getClientSIServiceStatusAsync(null);
        }
        
        /// <remarks/>
        public void getClientSIServiceStatusAsync(object userState) {
            if ((this.getClientSIServiceStatusOperationCompleted == null)) {
                this.getClientSIServiceStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetClientSIServiceStatusOperationCompleted);
            }
            this.InvokeAsync("getClientSIServiceStatus", new object[0], this.getClientSIServiceStatusOperationCompleted, userState);
        }
        
        private void OngetClientSIServiceStatusOperationCompleted(object arg) {
            if ((this.getClientSIServiceStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getClientSIServiceStatusCompleted(this, new getClientSIServiceStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("MySoapHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/TestSoapHeader", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string TestSoapHeader() {
            object[] results = this.Invoke("TestSoapHeader", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void TestSoapHeaderAsync() {
            this.TestSoapHeaderAsync(null);
        }
        
        /// <remarks/>
        public void TestSoapHeaderAsync(object userState) {
            if ((this.TestSoapHeaderOperationCompleted == null)) {
                this.TestSoapHeaderOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTestSoapHeaderOperationCompleted);
            }
            this.InvokeAsync("TestSoapHeader", new object[0], this.TestSoapHeaderOperationCompleted, userState);
        }
        
        private void OnTestSoapHeaderOperationCompleted(object arg) {
            if ((this.TestSoapHeaderCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TestSoapHeaderCompleted(this, new TestSoapHeaderCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://tempuri.org/", IsNullable=false)]
    public partial class MySoapHeader : System.Web.Services.Protocols.SoapHeader {
        
        private string userNameField;
        
        private string passwordField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        public string UserName {
            get {
                return this.userNameField;
            }
            set {
                this.userNameField = value;
            }
        }
        
        /// <remarks/>
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void getSICompletedEventHandler(object sender, getSICompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getSICompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getSICompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void getSI2CompletedEventHandler(object sender, getSI2CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getSI2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getSI2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void getClientSIServiceStatusCompletedEventHandler(object sender, getClientSIServiceStatusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getClientSIServiceStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getClientSIServiceStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void TestSoapHeaderCompletedEventHandler(object sender, TestSoapHeaderCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TestSoapHeaderCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TestSoapHeaderCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591