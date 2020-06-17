using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.DTOs;
using App.Domain.Resources;
using App.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NToastNotify;
using RestSharp;

namespace App.UI.Controllers
{
    public class PatientController : Controller
    {
        private readonly IExecuteAPIService _executeAPIService;

        public PatientController(IExecuteAPIService executeAPIService)
        {
            _executeAPIService = executeAPIService;
        }

        [HttpGet]
        [Route("DetailsPatient")]
        public async Task<IActionResult> DetailsPatient(int id)
        {
            var url = $"Patients/PatientById";
            var param = new PatientByIdQuery
            {
                PkPatientId = id
            };
            var result = await _executeAPIService.ExecuteAPI(url, Method.GET, DataFormat.Json, param.ToJSON());
            var model = JsonConvert.DeserializeObject<PatientCommand>(result.Content);

            ViewBag.ModalTitle = string.Format(AppSettings.PatientInfoDetails, model.ForeName, model.SurName);
            return PartialView("_DetailsPatient", model);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetPatienBasicInfo")]
        public async Task<IActionResult> PatienBasicInfo()
        {
            var actionUrl = $"Patients/Patients";

            var result = await _executeAPIService.ExecuteAPI(actionUrl, Method.GET, DataFormat.Json, null);
            var model = JsonConvert.DeserializeObject<List<PatientResponse>>(result.Content);

            return Json(new { data = model.ToList() });
        }

        [HttpGet]
        [Route("CreatePatient")]
        [AllowAnonymous]
        public IActionResult CreatePatient()
        {
            ViewBag.ModalTitle = AppSettings.NewPatient;
            return PartialView("_CreatePatient");
        }

        [HttpGet]
        [Route("EditPatient")]
        [AllowAnonymous]
        public async Task<IActionResult> EditPatient(int id)
        {
            var url = $"Patients/PatientById";
            var param = new PatientByIdQuery
            {
                PkPatientId = id
            };
            var result = await _executeAPIService.ExecuteAPI(url, Method.GET, DataFormat.Json, param.ToJSON());
            var model = JsonConvert.DeserializeObject<PatientCommand>(result.Content);

            ViewBag.ModalTitle = string.Format(AppSettings.UpdatePatient, model.ForeName, model.SurName);
            return PartialView("_EditPatient", model);
        }

        [HttpGet]
        [Route("RemovePatient")]
        [AllowAnonymous]
        public async Task<IActionResult> RemovePatient(int id)
        {
            var url = $"Patients/PatientById";
            var param = new PatientByIdQuery
            {
                PkPatientId = id
            };
            var result = await _executeAPIService.ExecuteAPI(url, Method.GET, DataFormat.Json, param.ToJSON());
            var model = JsonConvert.DeserializeObject<PatientCommand>(result.Content);

            ViewBag.ModalTitle = string.Format(AppSettings.RemovePatient, model.ForeName, model.SurName);
            ViewBag.ConfirmRemove= string.Format(AppSettings.RemoveConfirmMessage, model.ForeName, model.SurName);
            return PartialView("_RemovePatient", model);
        }

        [Route("ManagePatientBasicInfo")]
        [HttpPost]
        public async Task<IActionResult> ManagePatientBasicInfo(PatientCommand model)
        {
            var url = $"Patients/Manage";

            var result = await _executeAPIService.ExecuteAPI(url, Method.POST, DataFormat.Json, model.ToJSON());
            var response = JsonConvert.DeserializeObject<int>(result.Content);

            if (response > 0)
                return new JsonResult(new { ok = true });
            else return new JsonResult(new { ok = false });
        }              
    }
}