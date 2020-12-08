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
    public class sdnListPublshInformation
    {
        private string publish_DateField;

        private int record_CountField;

        private bool record_CountFieldSpecified;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }

        /// <remarks/>
        public string Publish_Date
        {
            get
            {
                return this.publish_DateField;
            }
            set
            {
                this.publish_DateField = value;
            }
        }

        /// <remarks/>
        public int Record_Count
        {
            get
            {
                return this.record_CountField;
            }
            set
            {
                this.record_CountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Record_CountSpecified
        {
            get
            {
                return this.record_CountFieldSpecified;
            }
            set
            {
                this.record_CountFieldSpecified = value;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is sdnListPublshInformation information &&
                   publish_DateField == information.publish_DateField &&
                   record_CountField == information.record_CountField &&
                   record_CountFieldSpecified == information.record_CountFieldSpecified &&
                   Publish_Date == information.Publish_Date &&
                   Record_Count == information.Record_Count &&
                   Record_CountSpecified == information.Record_CountSpecified;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(publish_DateField, record_CountField, record_CountFieldSpecified, Publish_Date, Record_Count, Record_CountSpecified);
        }
    }
}
