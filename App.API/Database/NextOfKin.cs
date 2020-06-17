using System;
using System.Collections.Generic;

namespace App.API.Database
{
    public partial class NextOfKin
    {
        public int PkNokId { get; set; }
        public int FkPatientId { get; set; }
        public byte FkNokRelationshipId { get; set; }
        public string NokName { get; set; }
        public string NokAddressLine1 { get; set; }
        public string NokAddressLine2 { get; set; }
        public string NokAddressLine3 { get; set; }
        public string NokAddressLine4 { get; set; }
        public string NokPostcode { get; set; }

        public virtual NokRelationship FkNokRelationship { get; set; }
        public virtual Patients FkPatient { get; set; }
    }
}
