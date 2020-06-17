using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.DTOs
{
    public class NextOfKinCommand : IRequest<int>
    {
        public byte Check { get; set; }
        public int? PkNokId { get; set; }
        public int? FkPatientId { get; set; }
        public byte? FkNokRelationshipId { get; set; }
        public string NokName { get; set; }
        public string NokAddressLine1 { get; set; }
        public string NokAddressLine2 { get; set; }
        public string NokAddressLine3 { get; set; }
        public string NokAddressLine4 { get; set; }
        public string NokPostcode { get; set; }
    }

    public class NextOfKinQuery : IRequest<List<NextOfKinResponse>>
    {
        public int? FkPatientId { get; set; }
    }

    public class NextOfKinByIdQuery : IRequest<NextOfKinResponse>
    {
        public int PkNokId { get; set; }
    }

    public class NextOfKinResponse
    {
        public string ForeName { get; set; }
        public string SurName { get; set; }
        public string NokRelationshipCode { get; set; }
        public int? PkNokId { get; set; }
        public int? FkPatientId { get; set; }
        public byte? FkNokRelationshipId { get; set; }
        public string NokName { get; set; }
        public string NokAddressLine1 { get; set; }
        public string NokAddressLine2 { get; set; }
        public string NokAddressLine3 { get; set; }
        public string NokAddressLine4 { get; set; }
        public string NokPostcode { get; set; }
    }
}
