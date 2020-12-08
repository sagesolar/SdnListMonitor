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
    public class sdnListSdnEntryPlaceOfBirthItem : IEquatable<sdnListSdnEntryPlaceOfBirthItem>
    {
        private int uidField;

        private string placeOfBirthField;

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
        public string placeOfBirth
        {
            get
            {
                return this.placeOfBirthField;
            }
            set
            {
                this.placeOfBirthField = value;
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

        public bool Equals(sdnListSdnEntryPlaceOfBirthItem other)
        {
            return other != null &&
                   uid == other.uid &&
                   placeOfBirth == other.placeOfBirth &&
                   mainEntry == other.mainEntry;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(uid, placeOfBirth, mainEntry);
        }
    }
}
