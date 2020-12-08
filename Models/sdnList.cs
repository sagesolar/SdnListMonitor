namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/sdnList.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/sdnList.xsd", IsNullable = false)]
    public class sdnList
    {
        private sdnListPublshInformation publshInformationField;

        private List<sdnListSdnEntry> sdnEntryField;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }

        /// <remarks/>
        public sdnListPublshInformation publshInformation
        {
            get
            {
                return this.publshInformationField;
            }
            set
            {
                this.publshInformationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("sdnEntry")]
        public List<sdnListSdnEntry> sdnEntry
        {
            get
            {
                return this.sdnEntryField;
            }
            set
            {
                this.sdnEntryField = value;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is sdnList list &&
                EqualityComparer<sdnListPublshInformation>.Default.Equals(publshInformationField, list.publshInformationField) &&
                EqualityComparer<List<sdnListSdnEntry>>.Default.Equals(sdnEntryField, list.sdnEntryField) &&
                EqualityComparer<sdnListPublshInformation>.Default.Equals(publshInformation, list.publshInformation) &&
                EqualityComparer<List<sdnListSdnEntry>>.Default.Equals(sdnEntry, list.sdnEntry);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(publshInformationField, sdnEntryField, publshInformation, sdnEntry);
        }
    }
}
