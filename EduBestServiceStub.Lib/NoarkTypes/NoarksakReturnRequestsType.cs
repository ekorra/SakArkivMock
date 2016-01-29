namespace EduBestServiceStub.Lib.NoarkTypes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.arkivverket.no/Noark4-1-WS-WD/types")]
    public partial class NoarksakReturnRequestsType {
    
        private bool returnJpostsField;
    
        private bool returnJpostsFieldSpecified;
    
        private bool returnSakspartField;
    
        private bool returnSakspartFieldSpecified;
    
        private bool returnKlasseringField;
    
        private bool returnKlasseringFieldSpecified;
    
        private bool returnTilleggsinfoField;
    
        private bool returnTilleggsinfoFieldSpecified;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool returnJposts {
            get {
                return this.returnJpostsField;
            }
            set {
                this.returnJpostsField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool returnJpostsSpecified {
            get {
                return this.returnJpostsFieldSpecified;
            }
            set {
                this.returnJpostsFieldSpecified = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool returnSakspart {
            get {
                return this.returnSakspartField;
            }
            set {
                this.returnSakspartField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool returnSakspartSpecified {
            get {
                return this.returnSakspartFieldSpecified;
            }
            set {
                this.returnSakspartFieldSpecified = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool returnKlassering {
            get {
                return this.returnKlasseringField;
            }
            set {
                this.returnKlasseringField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool returnKlasseringSpecified {
            get {
                return this.returnKlasseringFieldSpecified;
            }
            set {
                this.returnKlasseringFieldSpecified = value;
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
    }
}