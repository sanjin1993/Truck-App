using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck_Web.Models;
using Truck_Web.Services.IRepository;
using Truck_Web.ViewModel;

namespace Truck_Web.Controllers
{
    [Authorize]

    public class AuditFilesController : Controller
    {
        private readonly ITimeSheetRepository _timeSheetRepository;
        private readonly IAuditFileRepository _auditFileRepository;

        public AuditFilesController(ITimeSheetRepository timeSheetRepository, IAuditFileRepository auditFileRepository)
        {
            _timeSheetRepository = timeSheetRepository;
            _auditFileRepository = auditFileRepository;
        }

        public IActionResult Index()
        {
            return View(new AuditFile() { });
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<TimeSheet> timeSheetList = await _timeSheetRepository.GetAllAsync(SD.TimeSheetAPIPath, HttpContext.Session.GetString("JWToken"));

            AuditFileViewModel objVM = new AuditFileViewModel()
            {
                TimeSheetList = timeSheetList.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = i.Type,
                    Value = i.Id.ToString()
                }),
                AuditFile = new AuditFile()
            };

            if (id == null)
            {
                return View(objVM);
            }

            objVM.AuditFile = await _auditFileRepository.GetAsync(SD.AuditFilePath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            if (objVM.AuditFile == null)
            {
                return NotFound();
            }
            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AuditFileViewModel obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.AuditFile.Id == 0)
                {
                    await _auditFileRepository.CreateAsync(SD.AuditFilePath, obj.AuditFile, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _auditFileRepository.UpdateAsync(SD.AuditFilePath + obj.AuditFile.Id, obj.AuditFile, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }

        public async Task<IActionResult> GetAllAuditFile()
        {
            return Json(new { data = await _auditFileRepository.GetAllAsync(SD.AuditFilePath, HttpContext.Session.GetString("JWToken")) });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _auditFileRepository.DeleteAsync(SD.AuditFilePath, id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {
                return Json(new { success = true, message = "Delete successful" });
            }
            return Json(new { success = false, message = "Delete not successful" });
        }

    }
}
