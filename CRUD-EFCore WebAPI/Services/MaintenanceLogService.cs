using AutoMapper;
using FluentValidation;
using CRUDEFCore.Common;
using CRUDEFCore.DTOs;
using CRUDEFCore.Models;
using CRUDEFCore.Repositories;

namespace CRUDEFCore.Services
{
    public class MaintenanceLogService : IMaintenanceLogService
    {
        private readonly IMaintenanceLogRepository _logRepo;
        private readonly IEquipmentRepository _equipmentRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<MaintenanceLogCreateDto> _createValidator;

        public MaintenanceLogService(
            IMaintenanceLogRepository logRepo,
            IEquipmentRepository equipmentRepo,
            IMapper mapper,
            IValidator<MaintenanceLogCreateDto> createValidator)
        {
            _logRepo = logRepo;
            _equipmentRepo = equipmentRepo;
            _mapper = mapper;
            _createValidator = createValidator;
        }

        public async Task<ServiceResult<List<MaintenanceLogReadDto>>> GetAllLogsAsync()
        {
            var logs = await _logRepo.GetAllWithEquipmentAsync();
            return ServiceResult<List<MaintenanceLogReadDto>>.Ok(_mapper.Map<List<MaintenanceLogReadDto>>(logs));
        }

        public async Task<ServiceResult<List<MaintenanceLogReadDto>>> GetLogsByEquipmentIdAsync(int equipmentId)
        {
            var logs = await _logRepo.GetByEquipmentIdAsync(equipmentId);
            return ServiceResult<List<MaintenanceLogReadDto>>.Ok(_mapper.Map<List<MaintenanceLogReadDto>>(logs));
        }

        public async Task<ServiceResult<MaintenanceLogReadDto>> CreateLogAsync(MaintenanceLogCreateDto dto)
        {
            var validation = await _createValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return ServiceResult<MaintenanceLogReadDto>.Fail(
                    "Validasi gagal.", validation.Errors.Select(e => e.ErrorMessage).ToList());

            var equipment = await _equipmentRepo.GetByIdAsync(dto.EquipmentId);
            if (equipment == null)
                return ServiceResult<MaintenanceLogReadDto>.Fail($"Equipment dengan ID {dto.EquipmentId} tidak ditemukan.");

            var log = _mapper.Map<MaintenanceLog>(dto);
            await _logRepo.AddAsync(log);

            if (dto.MaintenanceDate > equipment.LastCalibrationDate)
            {
                equipment.LastCalibrationDate = dto.MaintenanceDate;
                _equipmentRepo.Update(equipment);
            }

            await _logRepo.SaveChangesAsync();

            var created = (await _logRepo.GetByEquipmentIdAsync(dto.EquipmentId))
                .FirstOrDefault(l => l.Id == log.Id);

            return ServiceResult<MaintenanceLogReadDto>.Ok(
                _mapper.Map<MaintenanceLogReadDto>(created), "Maintenance log berhasil dicatat.");
        }
    }
}
