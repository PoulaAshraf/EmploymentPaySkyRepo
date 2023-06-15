using AutoMapper;
using EmploymentApi.DTOs;
using EmploymentApi.Models;
using System;

namespace EmploymentApi.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Vacancy, VecancyDTO>().ReverseMap();
        }
    }
}
