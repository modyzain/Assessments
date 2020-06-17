using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.DTOs;
using App.Domain.Resources;
using App.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace App.UI.Controllers
{
    public class GpDetailsController : Controller
    {
        private readonly IExecuteAPIService _executeAPIService;

        public GpDetailsController(IExecuteAPIService executeAPIService)
        {
            _executeAPIService = executeAPIService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GPDetailsIndex()
        {
            return View();
        }

        [HttpGet]
        [Route("GetGPDetails")]
        public async Task<IActionResult> GPDetails(int? id)
        {
            var actionUrl = $"GpDetails/GpDetailsList";
            var param = new NextOfKinQuery() { FkPatientId = id };
            var result = await _executeAPIService.ExecuteAPI(actionUrl, Method.GET, DataFormat.Json, param.ToJSON());
            var model = JsonConvert.DeserializeObject<List<GpDetailsResponse>>(result.Content);

            return Json(new { data = model.ToList() });
        }

        [HttpGet]
        [Route("CreateGPDetails")]
        [AllowAnonymous]
        public IActionResult CreateGPDetails(int? id)
        {
            ViewBag.ModalTitle = AppSettings.NewGpDetails;
            var model = new GpDetailsCommand() { FkPatientId = id };
            return PartialView("_CreateGPDetails", model);
        }

        [HttpGet]
        [Route("EditGPDetails")]
        [AllowAnonymous]
        public async Task<IActionResult> EditGPDetails(int id)
        {
            var url = $"GpDetails/GpDetailsById";
            var param = new GpDetailsByIdQuery
            {
                PkGpDetailsId = id
            };
            var result = await _executeAPIService.ExecuteAPI(url, Method.GET, DataFormat.Json, param.ToJSON());
            var model = JsonConvert.DeserializeObject<GpDetailsCommand>(result.Content);

            ViewBag.ModalTitle = string.Format(AppSettings.UpdateGpDetails, model.GpSurName);
            return PartialView("_EditGPDetails", model);
        }

        [HttpGet]
        [Route("RemoveGpDetails")]
        [AllowAnonymous]
        public async Task<IActionResult> RemoveGPDetails(int id)
        {
            var url = $"GpDetails/GpDetailsById";
            var param = new GpDetailsByIdQuery
            {
                PkGpDetailsId = id
            };
            var result = await _executeAPIService.ExecuteAPI(url, Method.GET, DataFormat.Json, param.ToJSON());
            var model = JsonConvert.DeserializeObject<GpDetailsCommand>(result.Content);

            ViewBag.ModalTitle = string.Format(AppSettings.RemoveGpDetails, model.GpSurName);
            ViewBag.ConfirmRemove = string.Format(AppSettings.RemoveConfirmMessage, model.GpSurName, string.Empty);
            return PartialView("_RemoveGPDetails", model);
        }

        [Route("ManageGPDetails")]
        [HttpPost]
        public async Task<IActionResult> ManageGPDetails(GpDetailsCommand model)
        {
            var url = $"GpDetails/Manage";

            var result = await _executeAPIService.ExecuteAPI(url, Method.POST, DataFormat.Json, model.ToJSON());
            var response = JsonConvert.DeserializeObject<int>(result.Content);

            if (response > 0)
                return new JsonResult(new { ok = true });

            else return new JsonResult(new { ok = false });
        }
    }
}