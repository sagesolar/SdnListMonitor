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
    public class sdnListSdnEntryDateOfBirthItem : IEquatable<sdnListSdnEntryDateOfBirthItem>
    {
        private int uidField;

        private string dateOfBirthField;

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
        public string dateOfBirth
        {
            get
            {
                return this.dateOfBirthField;
            }
            set
            {
                this.dateOfBirthField = value;
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

        public bool Equals(sdnListSdnEntryDateOfBirthItem other)
        {
            return other != null &&
                   uid == other.uid &&
                   dateOfBirth == other.dateOfBirth &&
                   mainEntry == other.mainEntry;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(uid, dateOfBirth, mainEntry);
        }
    }
}
