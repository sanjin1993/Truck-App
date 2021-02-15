using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck_WebAPI.Models;
using Truck_WebAPI.Models.Dtos;

namespace Truck_WebAPI.Mapper
{
    public class TruckMappings : Profile
    {
        public TruckMappings()
        {
            CreateMap<TimeSheet, TimeSheetDto>().ReverseMap();
            CreateMap<AuditFile, AuditFileDto>().ReverseMap();
            CreateMap<AuditFile, AuditFileUpdateDto>().ReverseMap();
            CreateMap<AuditFile, AuditFileCreateDto>().ReverseMap();

        }
    }
}
