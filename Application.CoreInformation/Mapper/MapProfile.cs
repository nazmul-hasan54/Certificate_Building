using Application.Domain.Models;
using Application.SharedDTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoreInformation.Mapper
{
    public class MapProfile: Profile
    {
        public MapProfile() 
        {
            CreateMap<Department, DepartmentDTO>()
                .ForMember(o=> o.DepartmentId, q=> q.MapFrom(s=> s.DepartmentId))
                .ForMember(o=> o.DepartmentName, q=> q.MapFrom(s=> s.DepartmentName))
                .ReverseMap();
            CreateMap<Employee, EmployeeDTO>()
                .ReverseMap();
            CreateMap<Certificate, CertificateDTO>();
        }
    }
}
