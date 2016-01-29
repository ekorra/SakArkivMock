namespace EduBestServiceStub.Lib.NoarkTypes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.arkivverket.no/Noark/Exchange/types")]
    public partial class EnvelopeType {
    
        private AddressType senderField;
    
        private AddressType receiverField;
    
        private string conversationIdField;
    
        private string contentNamespaceField;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AddressType sender {
            get {
                return this.senderField;
            }
            set {
                this.senderField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AddressType receiver {
            get {
                return this.receiverField;
            }
            set {
                this.receiverField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string conversationId {
            get {
                return this.conversationIdField;
            }
            set {
                this.conversationIdField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string contentNamespace {
            get {
                return this.contentNamespaceField;
            }
            set {
                this.contentNamespaceField = value;
            }
        }
    }
}