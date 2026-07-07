using FluentValidation;
using CRUDEFCore.DTOs;

namespace CRUDEFCore.Validators
{
    public class MaintenanceLogCreateDtoValidator : AbstractValidator<MaintenanceLogCreateDto>
    {
        public MaintenanceLogCreateDtoValidator()
        {
            RuleFor(x => x.EquipmentId).GreaterThan(0);
            RuleFor(x => x.PerformedBy).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Result).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Notes).MaximumLength(500);
            RuleFor(x => x.MaintenanceDate).LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Tanggal maintenance tidak boleh di masa depan.");
        }
    }
}
