namespace Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/sdnList.xsd")]
    public class sdnListSdnEntryCitizenship : IEquatable<sdnListSdnEntryCitizenship>
    {
        private int uidField;

        private string countryField;

        private bool mainEntryField;

        /// <remarks/>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int uid
        {
            get
            {
                return this.uidField;
            }
            set
            {
                this.uidField = value;
            }
        }


        //public string id { get; set; }

        /// <remarks/>
        public string country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public bool mainEntry
        {
            get
            {
                return this.mainEntryField;
            }
            set
            {
                this.mainEntryField = value;
            }
        }

        public bool Equals(sdnListSdnEntryCitizenship other)
        {
            return other != null &&
                   uid == other.uid &&
                   country == other.country &&
                   mainEntry == other.mainEntry;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(uid, country, mainEntry);
        }
    }
}
