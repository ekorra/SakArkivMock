namespace EduBestServiceStub.Lib.NoarkTypes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.arkivverket.no/Noark/Exchange/types")]
    public partial class PutMessageResponseType {
    
        private AppReceiptType resultField;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AppReceiptType result {
            get {
                return this.resultField;
            }
            set {
                this.resultField = value;
            }
        }
    }
}