using BGMaterial.Application.Dtos;
using FluentValidation;

namespace BGMaterial.Application.Validators.MaterialValidators
{
    public class CreateMaterialValidator : AbstractValidator<CreateMaterialDto>
    {
        public CreateMaterialValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen Materyal Adını  Boş Geçmeyin");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Lütfen Materyal Adı  En az 3 karekter olmalıdır");
            RuleFor(x => x.Code).NotEmpty().WithMessage("Lütfen Materyal Kodunu Boş Geçmeyin");
            RuleFor(x => x.Code).MinimumLength(3).WithMessage("Lütfen Materyal Kodu En az 3 karekter olmalıdır");
            RuleFor(x => x.StockMrk).Must(x => BeAValidNumber(x.ToString())).WithMessage("{PropertyName} bir sayı olmalıdır");
            RuleFor(x => x.StockMrk).InclusiveBetween(0, int.MaxValue).WithMessage("{PropertyName} negatif sayı olamaz");
            RuleFor(x => x.StockLzm).Must(x => BeAValidNumber(x.ToString())).WithMessage("{PropertyName} bir sayı olmalıdır");
            RuleFor(x => x.StockLzm).InclusiveBetween(0, int.MaxValue).WithMessage("{PropertyName} negatif sayı olamaz");
            RuleFor(x => x.StockAnk).Must(x => BeAValidNumber(x.ToString())).WithMessage("{PropertyName} bir sayı olmalıdır");
            RuleFor(x => x.StockAnk).InclusiveBetween(0, int.MaxValue).WithMessage("{PropertyName} negatif sayı olamaz");
            RuleFor(x => x.StockAdn).Must(x => BeAValidNumber(x.ToString())).WithMessage("{PropertyName} bir sayı olmalıdır");
            RuleFor(x => x.StockAdn).InclusiveBetween(0, int.MaxValue).WithMessage("{PropertyName} negatif sayı olamaz");
            RuleFor(x => x.StockErz).Must(x => BeAValidNumber(x.ToString())).WithMessage("{PropertyName} bir sayı olmalıdır");
            RuleFor(x => x.StockErz).InclusiveBetween(0, int.MaxValue).WithMessage("{PropertyName} negatif sayı olamaz");
        }
        private bool BeAValidNumber(string arg)
        {
            return int.TryParse(arg, out _);

        }
    }


}
