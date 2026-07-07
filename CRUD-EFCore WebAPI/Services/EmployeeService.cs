using AutoMapper;
using FluentValidation;
using CRUDEFCore.Common;
using CRUDEFCore.DTOs;
using CRUDEFCore.Models;
using CRUDEFCore.Repositories;

namespace CRUDEFCore.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<EmployeeCreateDto> _createValidator;

        public EmployeeService(
            IEmployeeRepository employeeRepo,
            IDepartmentRepository departmentRepo,
            IMapper mapper,
            IValidator<EmployeeCreateDto> createValidator)
        {
            _employeeRepo = employeeRepo;
            _departmentRepo = departmentRepo;
            _mapper = mapper;
            _createValidator = createValidator;
        }

        public async Task<ServiceResult<List<EmployeeReadDto>>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepo.GetAllWithDetailsAsync();
            return ServiceResult<List<EmployeeReadDto>>.Ok(_mapper.Map<List<EmployeeReadDto>>(employees));
        }

        public async Task<ServiceResult<EmployeeReadDto>> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepo.GetByIdWithDetailsAsync(id);
            if (employee == null)
                return ServiceResult<EmployeeReadDto>.Fail($"Employee dengan ID {id} tidak ditemukan.");

            return ServiceResult<EmployeeReadDto>.Ok(_mapper.Map<EmployeeReadDto>(employee));
        }

        public async Task<ServiceResult<List<EmployeeReadDto>>> GetEmployeesByDepartmentAsync(int departmentId)
        {
            var employees = await _employeeRepo.GetByDepartmentIdAsync(departmentId);
            return ServiceResult<List<EmployeeReadDto>>.Ok(_mapper.Map<List<EmployeeReadDto>>(employees));
        }

        public async Task<ServiceResult<EmployeeReadDto>> CreateEmployeeAsync(EmployeeCreateDto dto)
        {
            var validation = await _createValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return ServiceResult<EmployeeReadDto>.Fail(
                    "Validasi gagal.", validation.Errors.Select(e => e.ErrorMessage).ToList());

            var department = await _departmentRepo.GetByIdAsync(dto.DepartmentId);
            if (department == null)
                return ServiceResult<EmployeeReadDto>.Fail($"Department dengan ID {dto.DepartmentId} tidak ditemukan.");

            var employee = _mapper.Map<Employee>(dto);
            await _employeeRepo.AddAsync(employee);
            await _employeeRepo.SaveChangesAsync();

            var created = await _employeeRepo.GetByIdWithDetailsAsync(employee.Id);
            return ServiceResult<EmployeeReadDto>.Ok(_mapper.Map<EmployeeReadDto>(created), "Employee berhasil dibuat.");
        }
    }
}
