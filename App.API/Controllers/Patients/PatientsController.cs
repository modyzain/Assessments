using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.API.Controllers.Patients
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Manage")]
        public async Task<IActionResult> ManagePatientBasic([FromBody]PatientCommand command)
        {
            if (command == null)
                throw new Exception("No Data to Save");

            var result = await _mediator.Send(command);

            if (result == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("Patients")]
        public async Task<IActionResult> PatientList()
        {
            var query = new PatientQuery();
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("PatientById")]
        public async Task<IActionResult> PatientById([FromQuery]string param)
        {
            var query = JsonConvert.DeserializeObject<PatientByIdQuery>(param);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("PatientInfoById")]
        public async Task<IActionResult> PatientInfoById([FromQuery]string param)
        {
            var query = JsonConvert.DeserializeObject<PatientInfoQuery>(param);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}