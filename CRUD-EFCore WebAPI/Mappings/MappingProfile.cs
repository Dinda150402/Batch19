using AutoMapper;
using CRUDEFCore.Models;
using CRUDEFCore.DTOs;

namespace CRUDEFCore.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Department
            CreateMap<Department, DepartmentReadDto>()
                .ForMember(dest => dest.EmployeeCount, opt => opt.MapFrom(src => src.Employees.Count));
            CreateMap<DepartmentCreateDto, Department>();

            // Employee
            CreateMap<Employee, EmployeeReadDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.AssignedEquipmentNames,
                    opt => opt.MapFrom(src => src.EquipmentList.Select(e => e.Name).ToList()));
            CreateMap<EmployeeCreateDto, Employee>();

            // Equipment
            CreateMap<Equipment, EquipmentReadDto>()
                .ForMember(dest => dest.RequiredDepartmentName,
                    opt => opt.MapFrom(src => src.RequiredDepartment != null ? src.RequiredDepartment.Name : null))
                .ForMember(dest => dest.AssignedEmployeeNames,
                    opt => opt.MapFrom(src => src.Employees.Select(e => e.Name).ToList()))
                .ForMember(dest => dest.MaintenanceLogCount,
                    opt => opt.MapFrom(src => src.MaintenanceLogs.Count));
            CreateMap<EquipmentCreateDto, Equipment>();

            // MaintenanceLog
            CreateMap<MaintenanceLog, MaintenanceLogReadDto>()
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src => src.Equipment.Name));
            CreateMap<MaintenanceLogCreateDto, MaintenanceLog>();
        }
    }
}
