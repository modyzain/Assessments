using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.API.Controllers.NextOfKin
{
    [Route("api/[controller]")]
    [ApiController]
    public class NextOfKinController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NextOfKinController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Manage")]
        public async Task<IActionResult> ManagePatientNextOfKin([FromBody]NextOfKinCommand command)
        {
            if (command == null)
                throw new Exception("No Data to Save");

            var result = await _mediator.Send(command);

            if (result == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("NextOfKinList")]
        public async Task<IActionResult> PatientNextOfKinList([FromQuery]string param)
        {
            var query = JsonConvert.DeserializeObject<NextOfKinQuery>(param);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("NextOfKinById")]
        public async Task<IActionResult> PatientNextOfKinById([FromQuery]string param)
        {
            var query = JsonConvert.DeserializeObject<NextOfKinByIdQuery>(param);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}