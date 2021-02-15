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
    public class AuditFilesController : ControllerBase
    {
        private IAuditFileRepository _auditFileRepository;
        private readonly IMapper _mapper;

        public AuditFilesController(IAuditFileRepository auditFileRepository, IMapper mapper)
        {
            _auditFileRepository = auditFileRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of audit files.
        /// </summary>
        /// <returns></returns>

        [HttpGet(Name = "GetAuditFiles")]
        [ProducesResponseType(200, Type = typeof(List<AuditFileDto>))]
        public IActionResult GetAuditFiles()
        {
            var objList = _auditFileRepository.GetAuditFiles();

            var objDto = new List<AuditFileDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<AuditFileDto>(obj));
            }

            return Ok(objDto);
        }

        /// <summary>
        /// Get individual audit file
        /// </summary>
        /// <param name="auditFileId"></param>
        /// <returns></returns>

        [HttpGet("{auditFileId:int}", Name = "GetAuditFile")]
        [ProducesResponseType(200, Type = typeof(AuditFileDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize(Roles ="Admin")]
        public IActionResult GetAuditFile(int auditFileId)
        {
            var obj = _auditFileRepository.GetAuditFile(auditFileId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<AuditFileDto>(obj);
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(AuditFileDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTimeSheet([FromBody] AuditFileCreateDto auditFileDto)
        {
            if (auditFileDto == null)
            {
                return BadRequest(ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var auditFileObj = _mapper.Map<AuditFile>(auditFileDto);

            if (!_auditFileRepository.CreateAuditFile(auditFileObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetAuditFiles", auditFileObj);

        }

        [HttpPatch("{auditFileId:int}", Name = "UpdateAuditFile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAuditFile(int auditFileId, [FromBody] AuditFileUpdateDto auditFileDto)
        {
            if (auditFileDto == null || auditFileId != auditFileDto.Id)
            {
                return BadRequest(ModelState);
            }

            var auditFileObj = _mapper.Map<AuditFile>(auditFileDto);

            if (!_auditFileRepository.UpdateAuditFile(auditFileObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {auditFileObj.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{auditFileId:int}", Name = "DeleteAuditFile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteAuditFile(int auditFileId)
        {
            if (!_auditFileRepository.AuditFileExists(auditFileId))
            {
                return NotFound();
            }

            var auditTimeObj = _auditFileRepository.GetAuditFile(auditFileId);

            if (!_auditFileRepository.DeleteAuditFile(auditTimeObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {auditTimeObj.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
