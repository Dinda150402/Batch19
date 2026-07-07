using AutoMapper;
using FluentValidation;
using CRUDEFCore.Common;
using CRUDEFCore.DTOs;
using CRUDEFCore.Models;
using CRUDEFCore.Repositories;

namespace CRUDEFCore.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<EquipmentCreateDto> _createValidator;
        private readonly IValidator<EquipmentUpdateDto> _updateValidator;

        public EquipmentService(
            IEquipmentRepository equipmentRepo,
            IEmployeeRepository employeeRepo,
            IMapper mapper,
            IValidator<EquipmentCreateDto> createValidator,
            IValidator<EquipmentUpdateDto> updateValidator)
        {
            _equipmentRepo = equipmentRepo;
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<ServiceResult<List<EquipmentReadDto>>> GetAllEquipmentsAsync()
        {
            var equipments = await _equipmentRepo.GetAllWithDetailsAsync();
            return ServiceResult<List<EquipmentReadDto>>.Ok(_mapper.Map<List<EquipmentReadDto>>(equipments));
        }

        public async Task<ServiceResult<EquipmentReadDto>> GetEquipmentByIdAsync(int id)
        {
            var equipment = await _equipmentRepo.GetByIdWithDetailsAsync(id);
            if (equipment == null)
                return ServiceResult<EquipmentReadDto>.Fail($"Equipment dengan ID {id} tidak ditemukan.");

            return ServiceResult<EquipmentReadDto>.Ok(_mapper.Map<EquipmentReadDto>(equipment));
        }

        public async Task<ServiceResult<List<EquipmentReadDto>>> GetEquipmentsByDepartmentAsync(int departmentId)
        {
            var equipments = await _equipmentRepo.GetByRequiredDepartmentIdAsync(departmentId);
            return ServiceResult<List<EquipmentReadDto>>.Ok(_mapper.Map<List<EquipmentReadDto>>(equipments));
        }

        public async Task<ServiceResult<List<EquipmentReadDto>>> SearchEquipmentByNameAsync(string keyword)
        {
            var equipments = await _equipmentRepo.SearchByNameAsync(keyword);
            return ServiceResult<List<EquipmentReadDto>>.Ok(_mapper.Map<List<EquipmentReadDto>>(equipments));
        }

        public async Task<ServiceResult<EquipmentReadDto>> CreateEquipmentAsync(EquipmentCreateDto dto)
        {
            var validation = await _createValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return ServiceResult<EquipmentReadDto>.Fail(
                    "Validasi gagal.", validation.Errors.Select(e => e.ErrorMessage).ToList());

            var equipment = _mapper.Map<Equipment>(dto);
            equipment.LastCalibrationDate = DateTime.Now;

            await _equipmentRepo.AddAsync(equipment);
            await _equipmentRepo.SaveChangesAsync();

            var created = await _equipmentRepo.GetByIdWithDetailsAsync(equipment.Id);
            return ServiceResult<EquipmentReadDto>.Ok(_mapper.Map<EquipmentReadDto>(created), "Equipment berhasil dibuat.");
        }

        public async Task<ServiceResult> UpdateEquipmentAsync(int id, EquipmentUpdateDto dto)
        {
            var validation = await _updateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return ServiceResult.Fail("Validasi gagal.", validation.Errors.Select(e => e.ErrorMessage).ToList());

            var equipment = await _equipmentRepo.GetByIdAsync(id);
            if (equipment == null)
                return ServiceResult.Fail($"Equipment dengan ID {id} tidak ditemukan.");

            equipment.Name = dto.Name;
            _equipmentRepo.Update(equipment);
            await _equipmentRepo.SaveChangesAsync();
            return ServiceResult.Ok("Equipment berhasil diupdate.");
        }

        public async Task<ServiceResult> DeleteEquipmentAsync(int id)
        {
            var equipment = await _equipmentRepo.GetByIdAsync(id);
            if (equipment == null)
                return ServiceResult.Fail($"Equipment dengan ID {id} tidak ditemukan.");

            _equipmentRepo.Remove(equipment);
            await _equipmentRepo.SaveChangesAsync();
            return ServiceResult.Ok("Equipment berhasil dihapus.");
        }

        public async Task<ServiceResult> AssignEquipmentToEmployeeAsync(int equipmentId, int employeeId)
        {
            var equipment = await _equipmentRepo.GetByIdWithDetailsAsync(equipmentId);
            var employee = await _employeeRepo.GetByIdWithDetailsAsync(employeeId);

            if (equipment == null || employee == null)
                return ServiceResult.Fail("ID tidak ditemukan.");

            if (equipment.RequiredDepartmentId != null &&
                equipment.RequiredDepartmentId != employee.DepartmentId)
            {
                return ServiceResult.Fail(
                    $"{equipment.Name} hanya boleh dipakai department '{equipment.RequiredDepartment?.Name}', sedangkan {employee.Name} dari department '{employee.Department.Name}'.");
            }

            if (equipment.Employees.Any(e => e.Id == employeeId))
                return ServiceResult.Fail($"{employee.Name} sudah di-assign ke {equipment.Name} sebelumnya.");

            equipment.Employees.Add(employee);
            await _equipmentRepo.SaveChangesAsync();
            return ServiceResult.Ok($"{employee.Name} berhasil di-assign ke {equipment.Name}");
        }
    }
}
