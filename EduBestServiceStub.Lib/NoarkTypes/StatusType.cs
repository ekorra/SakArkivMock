namespace EduBestServiceStub.Lib.NoarkTypes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.arkivverket.no/Noark4-1-WS-WD/types")]
    public partial class StatusType {
    
        private MessageType[] messageField;
    
        private StatusTypeType typeField;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("message", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public MessageType[] message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public StatusTypeType type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
    }
}