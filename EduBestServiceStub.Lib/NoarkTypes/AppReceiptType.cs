namespace EduBestServiceStub.Lib.NoarkTypes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.arkivverket.no/Noark/Exchange/types")]
    public partial class AppReceiptType {
    
        private StatusMessageType[] messageField;
    
        private AppReceiptTypeType typeField;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("message", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public StatusMessageType[] message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public AppReceiptTypeType type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
    }
}