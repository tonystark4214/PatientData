using System;
using System.Collections.Generic;

namespace SmartdataPatient.Models
{
    public partial class SmartdataPatientDatum
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public long PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public int CountryId { get; set; }
        public string UserName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual SmartdataCountryDatum Country { get; set; } = null!;
    }
}
