using Aspose.Pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Truck_Web.Models;
using Truck_Web.Services.IRepository;

namespace Truck_Web.Controllers
{
    [Authorize]
    public class TimeSheetsController : Controller
    {
        private readonly ITimeSheetRepository _timeSheetRepository;

        public TimeSheetsController(ITimeSheetRepository timeSheetRepository)
        {
            _timeSheetRepository = timeSheetRepository;
        }

        public IActionResult Index()
        {
            return View(new TimeSheet() {  });
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            var obj = new TimeSheet();

            if (id == null)
            {
                return View(obj);
            }

            obj = await _timeSheetRepository.GetAsync(SD.TimeSheetAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TimeSheet obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    await _timeSheetRepository.CreateAsync(SD.TimeSheetAPIPath, obj, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _timeSheetRepository.UpdateAsync(SD.TimeSheetAPIPath + obj.Id, obj, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }

        public async Task<IActionResult> GetAllTimeSheet()
        {
            return Json(new { data = await _timeSheetRepository.GetAllAsync(SD.TimeSheetAPIPath, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpDelete]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _timeSheetRepository.DeleteAsync(SD.TimeSheetAPIPath, id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {
                return Json(new { success = true, message = "Delete successful" });
            }
            return Json(new { success = false, message = "Delete not successful" });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ExportTimeSheets()
        {
            var document = new Document
            {
                PageInfo = new PageInfo { Margin = new MarginInfo(28, 28, 28, 40) }
            };
            var pdfPage = document.Pages.Add();
            Table table = new Table
            {
                ColumnWidths = "25% 25% 25% 25%",
                DefaultCellPadding = new MarginInfo(10, 5, 10, 5),
                Border = new BorderInfo(BorderSide.All, .5f, Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, .5f, Color.Black),
            };

            DataTable dt = _timeSheetRepository.GetRecord();
            table.ImportDataTable(dt, true, 0, 0);
            document.Pages[1].Paragraphs.Add(table);

            using (var streamout = new MemoryStream())
            {
                document.Save(streamout);
                return new FileContentResult(streamout.ToArray(), "application/pdf")
                {
                    FileDownloadName = "TimeSheet.pdf"
                };
            }

        }

    }
}
