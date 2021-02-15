using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck_WebAPI.Models;
using Truck_WebAPI.Models.Dtos;
using Truck_WebAPI.Services.IRepository;

namespace Truck_WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TimeSheetsController : ControllerBase
    {
        private ITimeSheetRepository _timeSheetRepository;
        private readonly IMapper _mapper;

        public TimeSheetsController(ITimeSheetRepository timeSheetRepository, IMapper mapper)
        {
            _timeSheetRepository = timeSheetRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of time sheets
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TimeSheetDto>))]
        public IActionResult GetTimeSheets()
        {
            var objList = _timeSheetRepository.GetTimeSheets();

            var objDto = new List<TimeSheetDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TimeSheetDto>(obj));
            }

            return Ok(objDto);
        }

        /// <summary>
        /// Get individual time sheet
        /// </summary>
        /// <param name="timeSheetId"></param>
        /// <returns></returns>

        [HttpGet("{timeSheetId:int}", Name = "GetTimeSheet")]
        [ProducesResponseType(200, Type = typeof(TimeSheetDto))]
        [ProducesResponseType(404)]
        [Authorize]
        [ProducesDefaultResponseType]
        public IActionResult GetTimeSheet(int timeSheetId)
        {
            var obj = _timeSheetRepository.GetTimeSheet(timeSheetId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<TimeSheetDto>(obj);
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TimeSheetDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTimeSheet([FromBody] TimeSheetDto timeSheetDto)
        {
            if (timeSheetDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_timeSheetRepository.TimeSheetExists(timeSheetDto.Id))
            {
                ModelState.AddModelError("", "Time Sheet Exists!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timeSheetObj = _mapper.Map<TimeSheet>(timeSheetDto);

            if (!_timeSheetRepository.CreateTimeSheet(timeSheetObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {timeSheetDto.Id}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetTimeSheet", new { timeSheetId = timeSheetDto.Id }, timeSheetObj);

        }

        [HttpPatch("{timeSheetId:int}", Name = "UpdateTimeSheet")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTimeSheet(int timeSheetId, [FromBody] TimeSheetDto timeSheetDto)
        {
            if (timeSheetDto == null || timeSheetId != timeSheetDto.Id)
            {
                return BadRequest(ModelState);
            }

            var timeSheetObj = _mapper.Map<TimeSheet>(timeSheetDto);

            if (!_timeSheetRepository.UpdateTimeSheet(timeSheetObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {timeSheetObj.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{timeSheetId:int}", Name = "DeleteTimeSheet")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteTimeSheet(int timeSheetId)
        {
            if (!_timeSheetRepository.TimeSheetExists(timeSheetId))
            {
                return NotFound();
            }

            var timeSheetObj = _timeSheetRepository.GetTimeSheet(timeSheetId);

            if (!_timeSheetRepository.DeleteTimeSheet(timeSheetObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {timeSheetObj.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
