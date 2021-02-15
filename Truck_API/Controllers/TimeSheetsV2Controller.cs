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
    [ApiVersion("2.0")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TimeSheets2Controller : ControllerBase
    {
        private ITimeSheetRepository _timeSheetRepository;
        private readonly IMapper _mapper;

        public TimeSheets2Controller(ITimeSheetRepository timeSheetRepository, IMapper mapper)
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
        [ProducesResponseType(400)]
        public IActionResult GetTimeSheets()
        {
            var obj = _timeSheetRepository.GetTimeSheets().FirstOrDefault();

            return Ok(_mapper.Map<TimeSheetDto>(obj));
        }

    

    }
}
