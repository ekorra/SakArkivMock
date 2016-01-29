namespace EduBestServiceStub.Lib.NoarkTypes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.arkivverket.no/Noark/Exchange/types")]
    public partial class GetCanReceiveMessageResponseType {
    
        private bool resultField;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool result {
            get {
                return this.resultField;
            }
            set {
                this.resultField = value;
            }
        }
    }
}