namespace EduBestServiceStub.Lib.NoarkTypes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.arkivverket.no/Noark4-1-WS-WD/types")]
    public partial class JournpostReturnRequestsType {
    
        private bool returnAvmotField;
    
        private bool returnAvmotFieldSpecified;
    
        private bool returnTilleggsinfoField;
    
        private bool returnTilleggsinfoFieldSpecified;
    
        private bool returnDokumentField;
    
        private bool returnDokumentFieldSpecified;
    
        private bool returnFilField;
    
        private bool returnFilFieldSpecified;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool returnAvmot {
            get {
                return this.returnAvmotField;
            }
            set {
                this.returnAvmotField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool returnAvmotSpecified {
            get {
                return this.returnAvmotFieldSpecified;
            }
            set {
                this.returnAvmotFieldSpecified = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool returnTilleggsinfo {
            get {
                return this.returnTilleggsinfoField;
            }
            set {
                this.returnTilleggsinfoField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool returnTilleggsinfoSpecified {
            get {
                return this.returnTilleggsinfoFieldSpecified;
            }
            set {
                this.returnTilleggsinfoFieldSpecified = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool returnDokument {
            get {
                return this.returnDokumentField;
            }
            set {
                this.returnDokumentField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool returnDokumentSpecified {
            get {
                return this.returnDokumentFieldSpecified;
            }
            set {
                this.returnDokumentFieldSpecified = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool returnFil {
            get {
                return this.returnFilField;
            }
            set {
                this.returnFilField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool returnFilSpecified {
            get {
                return this.returnFilFieldSpecified;
            }
            set {
                this.returnFilFieldSpecified = value;
            }
        }
    }
}