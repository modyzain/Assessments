using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.DTOs
{
    public class GpDetailsCommand : IRequest<int>
    {
        public byte Check { get; set; }
        public int? PkGpDetailsId { get; set; }
        public int? FkPatientId { get; set; }
        public string GpCode { get; set; }
        public string GpSurName { get; set; }
        public string GpInitials { get; set; }
        public string GpPhone { get; set; }
    }

    public class GpDetailsQuery : IRequest<List<GpDetailsResponse>>
    {
        public int? FkPatientId { get; set; }
    }

    public class GpDetailsByIdQuery : IRequest<GpDetailsResponse>
    {
        public int PkGpDetailsId { get; set; }
    }

    public class GpDetailsResponse
    {
        public string ForeName { get; set; }
        public string SurName { get; set; }
        public int? PkGpDetailsId { get; set; }
        public int? FkPatientId { get; set; }
        public string GpCode { get; set; }
        public string GpSurname { get; set; }
        public string GpInitials { get; set; }
        public string GpPhone { get; set; }
    }
}
