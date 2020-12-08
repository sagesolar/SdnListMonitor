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
    public class sdnListSdnEntryAddress : IEquatable<sdnListSdnEntryAddress>
    {
        private int uidField;

        private string address1Field;

        private string address2Field;

        private string address3Field;

        private string cityField;

        private string stateOrProvinceField;

        private string postalCodeField;

        private string countryField;

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
        public string address1
        {
            get
            {
                return this.address1Field;
            }
            set
            {
                this.address1Field = value;
            }
        }

        /// <remarks/>
        public string address2
        {
            get
            {
                return this.address2Field;
            }
            set
            {
                this.address2Field = value;
            }
        }

        /// <remarks/>
        public string address3
        {
            get
            {
                return this.address3Field;
            }
            set
            {
                this.address3Field = value;
            }
        }

        /// <remarks/>
        public string city
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public string stateOrProvince
        {
            get
            {
                return this.stateOrProvinceField;
            }
            set
            {
                this.stateOrProvinceField = value;
            }
        }

        /// <remarks/>
        public string postalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }

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

        public bool Equals(sdnListSdnEntryAddress other)
        {
            return other != null &&
                   uid == other.uid &&
                   address1 == other.address1 &&
                   address2 == other.address2 &&
                   address3 == other.address3 &&
                   city == other.city &&
                   stateOrProvince == other.stateOrProvince &&
                   postalCode == other.postalCode &&
                   country == other.country;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(uid, address1, address2, address3, city, stateOrProvince, postalCode, country);
        }
    }
}
