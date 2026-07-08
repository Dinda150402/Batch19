using FluentValidation;
using CRUDEFCore.DTOs;

namespace CRUDEFCore.Validators
{
    public class EquipmentCreateDtoValidator : AbstractValidator<EquipmentCreateDto>
    {
        public EquipmentCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.SerialNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.RequiredDepartmentId)
                .GreaterThan(0)
                .When(x => x.RequiredDepartmentId.HasValue)
                .WithMessage("RequiredDepartmentId harus valid (lebih besar dari 0) atau dikosongkan");
        }
    }

    public class EquipmentUpdateDtoValidator : AbstractValidator<EquipmentUpdateDto>
    {
        public EquipmentUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}
