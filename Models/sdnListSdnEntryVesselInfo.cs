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
    public class sdnListSdnEntryVesselInfo : IEquatable<sdnListSdnEntryVesselInfo>
    {
        private string callSignField;

        private string vesselTypeField;

        private string vesselFlagField;

        private string vesselOwnerField;

        private int tonnageField;

        private bool tonnageFieldSpecified;

        private int grossRegisteredTonnageField;

        private bool grossRegisteredTonnageFieldSpecified;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }

        /// <remarks/>
        public string callSign
        {
            get
            {
                return this.callSignField;
            }
            set
            {
                this.callSignField = value;
            }
        }

        /// <remarks/>
        public string vesselType
        {
            get
            {
                return this.vesselTypeField;
            }
            set
            {
                this.vesselTypeField = value;
            }
        }

        /// <remarks/>
        public string vesselFlag
        {
            get
            {
                return this.vesselFlagField;
            }
            set
            {
                this.vesselFlagField = value;
            }
        }

        /// <remarks/>
        public string vesselOwner
        {
            get
            {
                return this.vesselOwnerField;
            }
            set
            {
                this.vesselOwnerField = value;
            }
        }

        /// <remarks/>
        public int tonnage
        {
            get
            {
                return this.tonnageField;
            }
            set
            {
                this.tonnageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool tonnageSpecified
        {
            get
            {
                return this.tonnageFieldSpecified;
            }
            set
            {
                this.tonnageFieldSpecified = value;
            }
        }

        /// <remarks/>
        public int grossRegisteredTonnage
        {
            get
            {
                return this.grossRegisteredTonnageField;
            }
            set
            {
                this.grossRegisteredTonnageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool grossRegisteredTonnageSpecified
        {
            get
            {
                return this.grossRegisteredTonnageFieldSpecified;
            }
            set
            {
                this.grossRegisteredTonnageFieldSpecified = value;
            }
        }

        public bool Equals(sdnListSdnEntryVesselInfo other)
        {
            return other != null &&
                   callSign == other.callSign &&
                   vesselType == other.vesselType &&
                   vesselFlag == other.vesselFlag &&
                   vesselOwner == other.vesselOwner &&
                   tonnage == other.tonnage &&
                   tonnageSpecified == other.tonnageSpecified &&
                   grossRegisteredTonnage == other.grossRegisteredTonnage &&
                   grossRegisteredTonnageSpecified == other.grossRegisteredTonnageSpecified;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(callSign);
            hash.Add(vesselType);
            hash.Add(vesselFlag);
            hash.Add(vesselOwner);
            hash.Add(tonnage);
            hash.Add(tonnageSpecified);
            hash.Add(grossRegisteredTonnage);
            hash.Add(grossRegisteredTonnageSpecified);
            return hash.ToHashCode();
        }
    }
}
