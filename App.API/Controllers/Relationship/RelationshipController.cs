using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Relationship
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RelationshipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("PopulateRelationship")]
        public async Task<IActionResult> PopulateRelationship()
        {
            var query = new NokRelationshipQuery();
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}