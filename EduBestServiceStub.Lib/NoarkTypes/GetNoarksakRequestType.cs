namespace EduBestServiceStub.Lib.NoarkTypes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.arkivverket.no/Noark4-1-WS-WD/types")]
    public partial class GetNoarksakRequestType {
    
        private object[] itemsField;
    
        private NoarksakReturnRequestsType returnNoarksakRequestsField;
    
        private JournpostReturnRequestsType returnJournpostRequestsField;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("saEksternNokkel", typeof(EksternNokkelType), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("saId", typeof(string), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("saksnummer", typeof(SaksnummerType), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("searchCriteria", typeof(SearchCriteriaType), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public NoarksakReturnRequestsType returnNoarksakRequests {
            get {
                return this.returnNoarksakRequestsField;
            }
            set {
                this.returnNoarksakRequestsField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public JournpostReturnRequestsType returnJournpostRequests {
            get {
                return this.returnJournpostRequestsField;
            }
            set {
                this.returnJournpostRequestsField = value;
            }
        }
    }
}