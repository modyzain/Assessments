using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Domain.DTOs
{
    public class PatientCommand : IRequest<int>
    {
        public byte Check { get; set; }
        public int? PkPatientId { get; set; }
        public string Gender { get; set; }
        public int? PasNumber { get; set; }
        public string ForeName { get; set; }
        public string SurName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? DateOfBirth { get; set; }
        public string HomeTelephoneNumber { get; set; }
    }

    public class PatientQuery : IRequest<List<PatientResponse>> { }

    public class PatientByIdQuery : IRequest<PatientResponse>
    {
        public int PkPatientId { get; set; }
    }

    public class PatientResponse
    {
        public int PkPatientId { get; set; }
        public string Gender { get; set; }
        public int PasNumber { get; set; }
        public string ForeName { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string HomeTelephoneNumber { get; set; }
    }

    public class PatientInfoQuery : IRequest<List<PatientInfoResponse>>
    {
        public int PkPatientId { get; set; }
    }


    public class PatientInfoResponse
    {
        public int PkPatientId { get; set; }
        public string Gender { get; set; }
        public int PasNumber { get; set; }
        public string ForeName { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string HomeTelephoneNumber { get; set; }
        public int? PkNokId { get; set; }
        public byte? FkNokRelationshipId { get; set; }
        public string NokRelationshipCode { get; set; }
        public string NokName { get; set; }
        public string NokAddressLine1 { get; set; }
        public string NokAddressLine2 { get; set; }
        public string NokAddressLine3 { get; set; }
        public string NokAddressLine4 { get; set; }
        public string NokPostcode { get; set; }
        public int? PkGpDetailsId { get; set; }
        public string GpCode { get; set; }
        public string GpSurname { get; set; }
        public string GpInitials { get; set; }
        public string GpPhone { get; set; }

    }
}
