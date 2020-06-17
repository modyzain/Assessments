using System;
using System.Collections.Generic;

namespace App.API.Database
{
    public partial class GpDetails
    {
        public int PkGpDetailsId { get; set; }
        public int FkPatientId { get; set; }
        public string GpCode { get; set; }
        public string GpSurname { get; set; }
        public string GpInitials { get; set; }
        public string GpPhone { get; set; }
    }
}
