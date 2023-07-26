using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SmartdataPatient.Models
{
    //get data of patient model
    public class PatientDataModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public long PhoneNumber { get; set; }
        public DateTime DOB {  get; set; }
        public string? UserName { get; set; }
        public string? CountryName { get;set; }
        public string? FirstName { get; set;}
        public string? LastName { get; set; }
        public int? countryID { get; set; }
    }

    //message printing model
    public class ResponseMessageModel
    {
        public List<PatientDataModel>? AllPatientData { get; set; }
        public PatientDataModel? PatientData { get; set; }
        public List<CountryModel>? Country { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
    }

    //post model

    public class PostModel
    {
        public int id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get;set; }
        [Required]
        [EmailAddress(ErrorMessage ="Email Invalid")]
        public string? Email { get; set; }
        [Required]
        [Range(1111111111,9999999999,ErrorMessage ="Less than 10 Chars in phone number")]
        public long PhoneNumber { get; set; }
        [Required]
        public int Country { get; set; }
        public string? UserName { get; set; }
    }

    //delete model

    public class DeleteModel
    {
        public int Id { get; set; } 
        public string? userName { get; set; }
    }

    public class CountryModel
    {
        public int CountryId { get; set; }
        public string? CountryName { get; set; }

    }

}
