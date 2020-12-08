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
    public class sdnListSdnEntryID : IEquatable<sdnListSdnEntryID>
    {
        private int uidField;

        private string idTypeField;

        private string idNumberField;

        private string idCountryField;

        private string issueDateField;

        private string expirationDateField;

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
        public string idType
        {
            get
            {
                return this.idTypeField;
            }
            set
            {
                this.idTypeField = value;
            }
        }

        /// <remarks/>
        public string idNumber
        {
            get
            {
                return this.idNumberField;
            }
            set
            {
                this.idNumberField = value;
            }
        }

        /// <remarks/>
        public string idCountry
        {
            get
            {
                return this.idCountryField;
            }
            set
            {
                this.idCountryField = value;
            }
        }

        /// <remarks/>
        public string issueDate
        {
            get
            {
                return this.issueDateField;
            }
            set
            {
                this.issueDateField = value;
            }
        }

        /// <remarks/>
        public string expirationDate
        {
            get
            {
                return this.expirationDateField;
            }
            set
            {
                this.expirationDateField = value;
            }
        }

        public bool Equals(sdnListSdnEntryID other)
        {
            return other != null &&
                   uid == other.uid &&
                   idType == other.idType &&
                   idNumber == other.idNumber &&
                   idCountry == other.idCountry &&
                   issueDate == other.issueDate &&
                   expirationDate == other.expirationDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(uid, idType, idNumber, idCountry, issueDate, expirationDate);
        }
    }
}
