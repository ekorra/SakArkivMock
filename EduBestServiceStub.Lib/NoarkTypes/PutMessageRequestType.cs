namespace EduBestServiceStub.Lib.NoarkTypes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.arkivverket.no/Noark/Exchange/types")]
    public partial class PutMessageRequestType {
    
        private EnvelopeType envelopeField;
        private string payload;
        
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "payload")]
        public string Payload
        {
            get { return payload; }
            set { payload = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public EnvelopeType envelope {
            get {
                return this.envelopeField;
            }
            set {
                this.envelopeField = value;
            }
        }
    }
}