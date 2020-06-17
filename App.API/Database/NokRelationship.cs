using System;
using System.Collections.Generic;

namespace App.API.Database
{
    public partial class NokRelationship
    {
        public NokRelationship()
        {
            NextOfKin = new HashSet<NextOfKin>();
        }

        public byte PkNokRelationshipId { get; set; }
        public string NokRelationshipCode { get; set; }

        public virtual ICollection<NextOfKin> NextOfKin { get; set; }
    }
}
