using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.DTOs;
using App.Domain.Resources;
using App.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RestSharp;

namespace App.UI.Controllers
{
    public class NextOfKinController : Controller
    {
        private readonly IExecuteAPIService _executeAPIService;

        public NextOfKinController(IExecuteAPIService executeAPIService)
        {
            _executeAPIService = executeAPIService;
        }

        public async Task PopulatekRelationship(byte? id)
        {
            var url = $"Relationship/PopulateRelationship";

            var result = await _executeAPIService.ExecuteAPI(url, Method.GET, DataFormat.Json, null);
            var model = JsonConvert.DeserializeObject<List<NokRelationshipResponse>>(result.Content);

            ViewBag.RelationshipId = model != null ? new SelectList(model.ToList(), "PkNokRelationshipId", "NokRelationshipCode", id ?? 0) : null;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NextOfKinIndex()
        {
            return View();
        }

        [HttpGet]
        [Route("GetNextOfKin")]
        public async Task<IActionResult> NextOfKin(int? id)
        {
            var actionUrl = $"NextOfKin/NextOfKinList";
            var param = new NextOfKinQuery() { FkPatientId = id };
            var result = await _executeAPIService.ExecuteAPI(actionUrl, Method.GET, DataFormat.Json, param.ToJSON());
            var model = JsonConvert.DeserializeObject<List<NextOfKinResponse>>(result.Content);

            return Json(new { data = model.ToList() });
        }

        [HttpGet]
        [Route("CreateNextOfKin")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateNextOfKin(int? id)
        {
            ViewBag.ModalTitle = AppSettings.NewNextOfKin;
            await PopulatekRelationship(null);
            var model = new NextOfKinCommand() { FkPatientId = id };
            return PartialView("_CreateNextOfKin", model);
        }

        [HttpGet]
        [Route("EditNextOfKin")]
        [AllowAnonymous]
        public async Task<IActionResult> EditNextOfKin(int id)
        {
            var url = $"NextOfKin/NextOfKinById";
            var param = new NextOfKinByIdQuery
            {
                PkNokId = id
            };
            var result = await _executeAPIService.ExecuteAPI(url, Method.GET, DataFormat.Json, param.ToJSON());
            var model = JsonConvert.DeserializeObject<NextOfKinCommand>(result.Content);

            ViewBag.ModalTitle = string.Format(AppSettings.UpdateNextOfKin, model.NokName);
            await PopulatekRelationship(model.FkNokRelationshipId);
            return PartialView("_EditNextOfKin", model);
        }

        [HttpGet]
        [Route("RemoveNextOfKin")]
        [AllowAnonymous]
        public async Task<IActionResult> RemoveNextOfKin(int id)
        {
            var url = $"NextOfKin/NextOfKinById";
            var param = new NextOfKinByIdQuery
            {
                PkNokId = id
            };
            var result = await _executeAPIService.ExecuteAPI(url, Method.GET, DataFormat.Json, param.ToJSON());
            var model = JsonConvert.DeserializeObject<NextOfKinCommand>(result.Content);

            ViewBag.ModalTitle = string.Format(AppSettings.RemoveNextOfKin, model.NokName);
            ViewBag.ConfirmRemove = string.Format(AppSettings.RemoveConfirmMessage, model.NokName, string.Empty);
            return PartialView("_RemoveNextOfKin", model);
        }

        [Route("ManageNextOfKin")]
        [HttpPost]
        public async Task<IActionResult> ManageNextOfKin(NextOfKinCommand model)
        {
            var url = $"NextOfKin/Manage";

            var result = await _executeAPIService.ExecuteAPI(url, Method.POST, DataFormat.Json, model.ToJSON());
            var response = JsonConvert.DeserializeObject<int>(result.Content);

            if (response > 0)
                return new JsonResult(new { ok = true });

            else return new JsonResult(new { ok = false });
        }
    }
}