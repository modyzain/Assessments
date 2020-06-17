using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.API.Controllers.GpDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class GpDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GpDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Manage")]
        public async Task<IActionResult> ManagePatientGpDetails([FromBody]GpDetailsCommand command)
        {
            if (command == null)
                throw new Exception("No Data to Save");

            var result = await _mediator.Send(command);

            if (result == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("GpDetailsList")]
        public async Task<IActionResult> PatientGpDetailsList([FromQuery]string param)
        {
            var query = JsonConvert.DeserializeObject<GpDetailsQuery>(param);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("GpDetailsById")]
        public async Task<IActionResult> PatientGpDetailsById([FromQuery]string param)
        {
            var query = JsonConvert.DeserializeObject<GpDetailsByIdQuery>(param);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}