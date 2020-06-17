using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.DTOs
{
    public class NokRelationshipQuery : IRequest<List<NokRelationshipResponse>> { }

    public class NokRelationshipResponse
    {
        public int PkNokRelationshipId { get; set; }
        public string NokRelationshipCode { get; set; }
    }
}
