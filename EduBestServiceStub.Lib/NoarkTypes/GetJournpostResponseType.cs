namespace EduBestServiceStub.Lib.NoarkTypes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.arkivverket.no/Noark4-1-WS-WD/types")]
    public partial class GetJournpostResponseType {
    
        private StatusType statusField;
    
        private JournpostType[] journpostField;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public StatusType status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("journpost", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public JournpostType[] journpost {
            get {
                return this.journpostField;
            }
            set {
                this.journpostField = value;
            }
        }
    }
}