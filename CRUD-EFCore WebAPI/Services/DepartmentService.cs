using AutoMapper;
using FluentValidation;
using CRUDEFCore.Common;
using CRUDEFCore.DTOs;
using CRUDEFCore.Models;
using CRUDEFCore.Repositories;

namespace CRUDEFCore.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<DepartmentCreateDto> _createValidator;

        public DepartmentService(IDepartmentRepository repo, IMapper mapper, IValidator<DepartmentCreateDto> createValidator)
        {
            _repo = repo;
            _mapper = mapper;
            _createValidator = createValidator;
        }

        public async Task<ServiceResult<List<DepartmentReadDto>>> GetAllDepartmentsAsync()
        {
            var departments = await _repo.GetAllWithEmployeesAsync();
            var dtos = _mapper.Map<List<DepartmentReadDto>>(departments);
            return ServiceResult<List<DepartmentReadDto>>.Ok(dtos);
        }

        public async Task<ServiceResult<DepartmentReadDto>> GetDepartmentByIdAsync(int id)
        {
            var department = await _repo.GetByIdWithEmployeesAsync(id);
            if (department == null)
                return ServiceResult<DepartmentReadDto>.Fail($"Department dengan ID {id} tidak ditemukan.");

            return ServiceResult<DepartmentReadDto>.Ok(_mapper.Map<DepartmentReadDto>(department));
        }

        public async Task<ServiceResult<DepartmentReadDto>> CreateDepartmentAsync(DepartmentCreateDto dto)
        {
            var validation = await _createValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return ServiceResult<DepartmentReadDto>.Fail(
                    "Validasi gagal.", validation.Errors.Select(e => e.ErrorMessage).ToList());

            if (await _repo.ExistsByNameAsync(dto.Name))
                return ServiceResult<DepartmentReadDto>.Fail($"Department '{dto.Name}' sudah ada.");

            var department = _mapper.Map<Department>(dto);
            await _repo.AddAsync(department);
            await _repo.SaveChangesAsync();

            return ServiceResult<DepartmentReadDto>.Ok(_mapper.Map<DepartmentReadDto>(department), "Department berhasil dibuat.");
        }
    }
}
