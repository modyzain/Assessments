using System;
using System.Collections.Generic;

namespace App.API.Database
{
    public partial class Patients
    {
        public Patients()
        {
            NextOfKin = new HashSet<NextOfKin>();
        }

        public int PkPatientId { get; set; }
        public string Gender { get; set; }
        public int? PasNumber { get; set; }
        public string ForeName { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string HomeTelephoneNumber { get; set; }

        public virtual ICollection<NextOfKin> NextOfKin { get; set; }
    }
}
