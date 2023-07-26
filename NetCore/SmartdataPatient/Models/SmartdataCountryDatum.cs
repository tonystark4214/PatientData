using System;
using System.Collections.Generic;

namespace SmartdataPatient.Models
{
    public partial class SmartdataCountryDatum
    {
        public SmartdataCountryDatum()
        {
            SmartdataPatientData = new HashSet<SmartdataPatientDatum>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; } = null!;

        public virtual ICollection<SmartdataPatientDatum> SmartdataPatientData { get; set; }
    }
}
